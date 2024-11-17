using MeetingPlanner.BLL.Exceptions;
using MeetingPlanner.BLL.Validators.Interfaces;
using MeetingPlanner.DAL.Entities;
using MeetingPlanner.DAL.Repositories;

namespace MeetingPlanner.BLL.Validators;

public class MeetingValidator : IMeetingValidator
{
    private readonly IMeetingRepository _meetingRepository;

    public MeetingValidator(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }
    
    private void CheckOverlapping(MeetingEntity meetingEntity)
    {
        var meetings = _meetingRepository.GetAll().Where(x => x.Id != meetingEntity.Id).ToList();
        foreach (var existingMeeting in meetings)
        {
            if (meetingEntity.StartTime < existingMeeting.EndTime &&
                meetingEntity.EndTime > existingMeeting.StartTime)
            {
                throw new MeetingOverlapException(existingMeeting);
            }
        }
    }

    private void CheckMeetingDateInPast(MeetingEntity meetingEntity)
    {
        if (meetingEntity.StartTime < DateTime.Now)
        {
            throw new MeetingDateInPastException();
        }
    }

    private void CheckStartDateAfterEndDate(MeetingEntity meetingEntity)
    {
        if (meetingEntity.StartTime >= meetingEntity.EndTime)
        {
            throw new StartDateAfterEndDateException();
        }
    }

    public void ValidateTime(MeetingEntity meetingEntity)
    {
        CheckStartDateAfterEndDate(meetingEntity);
        CheckMeetingDateInPast(meetingEntity);
        CheckOverlapping(meetingEntity);
    }
}