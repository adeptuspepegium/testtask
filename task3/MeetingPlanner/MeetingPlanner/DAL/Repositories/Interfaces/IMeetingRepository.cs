using MeetingPlanner.DAL.Entities;

namespace MeetingPlanner.DAL.Repositories;

public interface IMeetingRepository
{
    List<MeetingEntity> GetAll();
    MeetingEntity GetById(Guid Id);
    bool Add(MeetingEntity meetingEntity);
    bool Remove(MeetingEntity meetingEntity);
    bool Update(MeetingEntity meetingEntity);
}