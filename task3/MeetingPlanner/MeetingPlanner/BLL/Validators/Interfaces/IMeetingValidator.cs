using MeetingPlanner.DAL.Entities;

namespace MeetingPlanner.BLL.Validators.Interfaces;

public interface IMeetingValidator
{
    void ValidateTime(MeetingEntity meetingEntity);
}