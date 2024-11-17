using System.Threading.Channels;
using MeetingPlanner.BLL.Services;
using MeetingPlanner.PLL.Helpers;

namespace MeetingPlanner.PLL.Views;

public class MainView
{
    private readonly NewMeetingView _newMeetingView;
    private readonly PreviewMeetingsMenuView _previewMeetingsMenuView;
    private readonly INotificationService _notificationService;

    public MainView(NewMeetingView newMeetingView, PreviewMeetingsMenuView previewMeetingsMenuView, INotificationService notificationService)
    {
        _newMeetingView = newMeetingView;
        _previewMeetingsMenuView = previewMeetingsMenuView;
        _notificationService = notificationService;
    }
    public void Show()
    {
        ViewHelper.ClearConsole();
        _notificationService.Notify();
        Console.WriteLine("1. Посмотреть собрания");
        Console.WriteLine("2. Создать собрание");

        switch (Console.ReadLine())
        {
            case "1":
            {
                ViewHelper.ClearConsole();
                _previewMeetingsMenuView.Show();
                break;
            }
            case "2":
            {
                ViewHelper.ClearConsole();
                _newMeetingView.Show();
                break;
            }
            
        }
    }
}