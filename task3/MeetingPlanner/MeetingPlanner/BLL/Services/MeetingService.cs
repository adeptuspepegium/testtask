
using MeetingPlanner.BLL.Exceptions;
using MeetingPlanner.BLL.Validators.Interfaces;
using MeetingPlanner.DAL.Entities;
using MeetingPlanner.DAL.Repositories;

namespace MeetingPlanner.BLL.Services;

public class MeetingService : IMeetingService
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly IMeetingValidator _meetingValidator;

    public MeetingService(IMeetingRepository meetingRepository, IMeetingValidator meetingValidator)
    {
        _meetingRepository = meetingRepository;
        _meetingValidator = meetingValidator;
    }
    
    public void Add(MeetingEntity meeting)
    {
        _meetingValidator.ValidateTime(meeting);
        _meetingRepository.Add(meeting);
    }

    public void Remove(MeetingEntity meeting)
    {
        _meetingRepository.Remove(meeting);
    }

    public List<MeetingEntity> GetAll()
    {
        return _meetingRepository.GetAll().OrderBy(x => x.StartTime).ToList();
    }

    public List<MeetingEntity> GetByDate(DateTime meetingDate)
    {
        return GetAll().Where(x => x.StartTime.Date == meetingDate.Date || x.EndTime.Date == meetingDate.Date).ToList();
    }

    public void Update(MeetingEntity meeting)
    {
        var oldMeeting = _meetingRepository.GetById(meeting.Id);
        if (oldMeeting.StartTime != meeting.StartTime || oldMeeting.EndTime != meeting.EndTime)
        {
            _meetingValidator.ValidateTime(meeting);
        }
        _meetingRepository.Update(meeting);
    }
}