using MeetingPlanner.BLL.Services;
using MeetingPlanner.PLL.Extensions;
using MeetingPlanner.PLL.Helpers;

namespace MeetingPlanner.PLL.Views;

public class PreviewMeetingsMenuView
{
    private readonly IMeetingService _meetingService;
    private readonly PreviewMeetingsView _previewMeetingsView;
    private readonly INotificationService _notificationService;

    public PreviewMeetingsMenuView(IMeetingService meetingService, PreviewMeetingsView previewMeetingsView, INotificationService notificationService)
    {
        _meetingService = meetingService;
        _previewMeetingsView = previewMeetingsView;
        _notificationService = notificationService;
    }
    public void Show()
    {
        _notificationService.Notify();
        Console.WriteLine("Введите дату за который день показать собрания в формате \"01.01.2024\"");
        Console.WriteLine("Для показа собраний за сегодня - оставьте пустое поле ввода и нажмите \"Enter\"");
        Console.WriteLine("0. Выход в главное меню");
        
        var userInput = Console.ReadLine();
        if (userInput == "0")
        {
            return;
        }

        if (string.IsNullOrEmpty(userInput))
        {
            ViewHelper.ClearConsole();
            var meetings = _meetingService.GetByDate(DateTime.Now);
            _previewMeetingsView.Show(meetings);
            return;
        }

        DateTime inputValue;
        if (ViewHelper.IsValidDate(userInput, out inputValue))
        {
            ViewHelper.ClearConsole();
            var meetings = _meetingService.GetByDate(inputValue);
            _previewMeetingsView.Show(meetings);
            return;
        }
        ViewHelper.ClearConsole();
        ViewHelper.AlertMessage("Введено некорректное значение");
        Show();
        return;
        
        
        
        

    }
}