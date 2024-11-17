using MeetingPlanner.BLL.Services;
using MeetingPlanner.PLL.Helpers;

namespace MeetingPlanner.PLL.Views;

public class SuccessView
{
    private readonly INotificationService _notificationService;

    public SuccessView(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    public void Show(string message)
    {
        _notificationService.Notify();
        ViewHelper.SuccessMessage(message);
        Console.WriteLine("Для продолжения нажмите любую клавишу");
        Console.ReadKey();
    }
}