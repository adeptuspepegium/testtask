using MeetingPlanner.DAL.Entities;

namespace MeetingPlanner.BLL.Services;

public class NotificationService : INotificationService
{
    private readonly IMeetingService _meetingService;

    public NotificationService(IMeetingService meetingService)
    {
        _meetingService = meetingService;

        
    }
    
    public void Notify()
    {
        var upComingMeetings = GetUpcomingMeetings();
        if (upComingMeetings.Count > 0)
        {
            Console.WriteLine(new String('-', 80));
            Console.WriteLine("Напоминание:");
            upComingMeetings.ForEach(item =>
            {
                Console.WriteLine($"Собрание {item.Name} начнется в {item.StartTime}");
            });
            Console.WriteLine(new String('-', 80));
        }
        
    }

    private List<MeetingEntity> GetUpcomingMeetings()
    {
        var meetings = _meetingService.GetAll();
        return meetings.Where(x => DateTime.Now > x.StartTime.AddMinutes(-x.NotifyBeforeMinutes) && DateTime.Now< x.StartTime).ToList();
    }
}