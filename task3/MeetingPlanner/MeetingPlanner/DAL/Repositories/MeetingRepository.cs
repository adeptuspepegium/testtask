using System.Text;
using MeetingPlanner.DAL.Entities;

namespace MeetingPlanner.DAL.Repositories;

public class MeetingRepository : IMeetingRepository
{
    private readonly MeetingStorage _storage;

    public MeetingRepository(MeetingStorage storage)
    {
        _storage = storage;
    }

    public List<MeetingEntity> GetAll()
    {
        return _storage.Meetings;
    }

    public MeetingEntity GetById(Guid Id)
    {
        return _storage.Meetings.FirstOrDefault(meeting => meeting.Id == Id);
    }

    public bool Add(MeetingEntity meetingEntity)
    {
        try
        {
            _storage.Meetings.Add(meetingEntity);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public bool Remove(MeetingEntity meetingEntity)
    {
        try
        {
            _storage.Meetings.Remove(meetingEntity);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Update(MeetingEntity meetingEntity)
    {
        try
        {
            var meetingToUpdate = GetById(meetingEntity.Id);
            if (meetingToUpdate == null)
            {
                return false;
            }

            meetingToUpdate.Name = meetingEntity.Name;
            meetingToUpdate.StartTime = meetingEntity.StartTime;
            meetingToUpdate.EndTime = meetingEntity.EndTime;
            meetingToUpdate.NotifyBeforeMinutes = meetingEntity.NotifyBeforeMinutes;
            return true;
        }
        catch
        {
            return false;
        }
        
    }
}