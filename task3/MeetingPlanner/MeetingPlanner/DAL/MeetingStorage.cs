using MeetingPlanner.DAL.Entities;

namespace MeetingPlanner.DAL;

public class MeetingStorage
{
    public List<MeetingEntity> Meetings;

    public MeetingStorage()
    {
        Meetings = new List<MeetingEntity>();
    }
}