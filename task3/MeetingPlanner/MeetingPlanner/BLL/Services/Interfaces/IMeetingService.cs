using MeetingPlanner.DAL.Entities;

namespace MeetingPlanner.BLL.Services;

public interface IMeetingService
{
    void Add(MeetingEntity meeting);
    void Remove(MeetingEntity meeting);
    List<MeetingEntity> GetAll();
    void Update(MeetingEntity meeting);
    List<MeetingEntity> GetByDate(DateTime meetingDate);
}