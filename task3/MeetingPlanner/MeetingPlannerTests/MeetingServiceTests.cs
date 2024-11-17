using MeetingPlanner.BLL.Exceptions;
using MeetingPlanner.BLL.Services;
using MeetingPlanner.BLL.Validators;
using MeetingPlanner.BLL.Validators.Interfaces;
using MeetingPlanner.DAL.Entities;
using MeetingPlanner.DAL.Repositories;
using Moq;

namespace MeetingPlannerTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class MeetingServiceTests
{
    
    private Mock<IMeetingRepository> _meetingRepositoryMock;
    private IMeetingValidator _meetingValidator;
    private IMeetingService _meetingService;
    
    [SetUp]
    public void SetUp()
    {
        _meetingRepositoryMock = new Mock<IMeetingRepository>();
        _meetingRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<MeetingEntity>());
        _meetingValidator = new MeetingValidator(_meetingRepositoryMock.Object);
        _meetingService = new MeetingService(_meetingRepositoryMock.Object, _meetingValidator);
    }

   
    
    [Test]
    public void Add_StartTimeInThePast_ShouldThrowMeetingDateInPastException()
    {
        var pastMeeting = new MeetingEntity
        {
            Name = "Past Meeting",
            StartTime = DateTime.Now.AddHours(-1),
            EndTime = DateTime.Now.AddHours(1),
            NotifyBeforeMinutes = 15
        };
        
        Assert.Throws<MeetingDateInPastException>(() => _meetingService.Add(pastMeeting));

        _meetingRepositoryMock.Verify(repo => repo.Add(It.IsAny<MeetingEntity>()), Times.Never);
    }
    
    [Test]
    public void Add_ValidMeeting_ShouldCallRepositoryAdd()
    {
        var newMeeting = new MeetingEntity
        {
            Name = "Valid Meeting",
            StartTime = DateTime.Now.AddHours(1),
            EndTime = DateTime.Now.AddHours(2),
            NotifyBeforeMinutes = 15
        };
        
        _meetingService.Add(newMeeting);

        _meetingRepositoryMock.Verify(repo => repo.Add(newMeeting), Times.Once);
    }
    
    [Test]
    public void Add_OverlappingMeeting_ShouldThrowMeetingOverlapException()
    {
        var existingMeeting = new MeetingEntity
        {
            Name = "Existing Meeting",
            StartTime = DateTime.Now.AddHours(1),
            EndTime = DateTime.Now.AddHours(2),
            NotifyBeforeMinutes = 15
        };

        _meetingRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<MeetingEntity> { existingMeeting });

        var overlappingMeeting = new MeetingEntity
        {
            Name = "Overlapping Meeting",
            StartTime = DateTime.Now.AddHours(1).AddMinutes(30),
            EndTime = DateTime.Now.AddHours(2).AddMinutes(30),
            NotifyBeforeMinutes = 10
        };

        var ex = Assert.Throws<MeetingOverlapException>(() => _meetingService.Add(overlappingMeeting));

        Assert.AreEqual(existingMeeting, ex.OverlappingMeeting);

        _meetingRepositoryMock.Verify(repo => repo.Add(It.IsAny<MeetingEntity>()), Times.Never);
    }
    
    [Test]
    public void Add_StartTimeAfterEndTime_ShouldThrowStartDateAfterEndDateException()
    {
        var invalidMeeting = new MeetingEntity
        {
            Name = "Invalid Meeting",
            StartTime = DateTime.Now.AddHours(2),
            EndTime = DateTime.Now.AddHours(1),
            NotifyBeforeMinutes = 15
        };

        Assert.Throws<StartDateAfterEndDateException>(() => _meetingService.Add(invalidMeeting));

        _meetingRepositoryMock.Verify(repo => repo.Add(It.IsAny<MeetingEntity>()), Times.Never);
    }
    
    [Test]
    public void Update_ExistingMeeting_ShouldChangeMeetingName()
    {
        
        var meeting = new MeetingEntity
        {
            Name = "Initial Meeting",
            StartTime = DateTime.Now.AddHours(1),
            EndTime = DateTime.Now.AddHours(2),
            NotifyBeforeMinutes = 15
        };
        
        _meetingRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<MeetingEntity> { meeting });
        _meetingRepositoryMock.Setup(repo => repo.GetById(meeting.Id)).Returns(meeting);
        
        meeting.Name = "Updated Meeting";
        _meetingService.Update(meeting);

        
        _meetingRepositoryMock.Verify(repo => repo.Update(It.Is<MeetingEntity>(m => m.Name == "Updated Meeting")), Times.Once);
    }
}