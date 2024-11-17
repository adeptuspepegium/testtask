using MeetingPlanner.BLL.Services;
using MeetingPlanner.DAL.Entities;
using MeetingPlanner.PLL.Helpers;

namespace MeetingPlanner.PLL.Views;

public class EditMeetingMenuView
{
    private readonly IMeetingService _meetingService;
    private readonly EditMeetingView _editMeetingView;
    private readonly SuccessView _successView;
    private readonly INotificationService _notificationService;

    public EditMeetingMenuView(IMeetingService meetingService,
        EditMeetingView editMeetingView, SuccessView successView, INotificationService notificationService)
    {
        _meetingService = meetingService;
        _editMeetingView = editMeetingView;
        _successView = successView;
        _notificationService = notificationService;
    }

    public void Show(MeetingEntity meeting)
    {
        _notificationService.Notify();
        Console.WriteLine("Выбранное собрание:");
        ViewHelper.PrettyPrint(meeting);

        Console.WriteLine("1. Удалить собрание");
        Console.WriteLine("2. Изменить название");
        Console.WriteLine("3. Изменить время проведения собрания");
        Console.WriteLine("4. Изменить время за сколько напомнить о собрании");
        Console.WriteLine("0. Выход в главное меню");

        var userInput = Console.ReadLine();


        if (string.IsNullOrEmpty(userInput))
        {
            Show(meeting);
        }

        switch (userInput)
        {
            case "0":
            {
                return;
            }
            case "1":
            {
                _meetingService.Remove(meeting);
                ViewHelper.ClearConsole();
                _successView.Show("Собрание успешно удалено");
                break;
            }
            case "2":
            {
                ViewHelper.ClearConsole();
                _editMeetingView.EditName(meeting);
                break;
            }
            case "3":
            {
                ViewHelper.ClearConsole();
                _editMeetingView.EditTime(meeting);
                break;
            }
            case "4":
            {
                ViewHelper.ClearConsole();
                _editMeetingView.EditNotificationPeriod(meeting);
                break;
            }
            default:
            {
                Show(meeting);
                break;
            }
        }
    }
}