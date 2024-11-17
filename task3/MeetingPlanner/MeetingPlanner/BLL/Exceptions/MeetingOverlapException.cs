using MeetingPlanner.DAL.Entities;

namespace MeetingPlanner.BLL.Exceptions;

public class MeetingOverlapException : Exception
{
    public MeetingEntity OverlappingMeeting { get; }
    public MeetingOverlapException(MeetingEntity meeting)
        :base($"Время проведения собрания пересекаетс с собранием \"{meeting.Name}\" {meeting.StartTime}-{meeting.EndTime}")
    {
        OverlappingMeeting = meeting;
    }
}