using MeetingPlanner.BLL.Services;
using MeetingPlanner.DAL.Entities;
using MeetingPlanner.PLL.Extensions;
using MeetingPlanner.PLL.Helpers;

namespace MeetingPlanner.PLL.Views;

public class PreviewMeetingsView
{

    private readonly EditMeetingMenuView _editMeetingMenuView;
    private readonly ExportMeetingsView _exportMeetingsView;
    private readonly INotificationService _notificationService;

    public PreviewMeetingsView(EditMeetingMenuView editMeetingMenuView, ExportMeetingsView exportMeetingsView, INotificationService notificationService)
    {
        _editMeetingMenuView = editMeetingMenuView;
        _exportMeetingsView = exportMeetingsView;
        _notificationService = notificationService;
    }
    public void Show(List<MeetingEntity> meetings)
    {
        _notificationService.Notify();
        Console.WriteLine("Запланированные собрания:");
        ViewHelper.PrettyPrint(meetings);

        Console.WriteLine("Для выбора собрания введите его номер");
        Console.WriteLine("export - Экспортировать собрания в файл");
        Console.WriteLine("0. Выход в главное меню");
        var userInput = Console.ReadLine();
        if (userInput == "0")
        {
            return;
        }
        if (string.IsNullOrEmpty(userInput))
        {
            ViewHelper.ClearConsole();
            Show(meetings);
            return;
        }

        if (userInput == "export")
        {
            ViewHelper.ClearConsole();
            _exportMeetingsView.Show(meetings);
            return;
        }

        int userInputValue;
        if (!int.TryParse(userInput, out userInputValue))
        {
            ViewHelper.ClearConsole();
            ViewHelper.AlertMessage("Введено некорректное значение");
            Show(meetings);
            return;
        }

        if (userInputValue > meetings.Count || userInputValue < 1)
        {
            ViewHelper.ClearConsole();
            ViewHelper.AlertMessage("Введено некорректное значение");
            Show(meetings);
            return;
        }

        var selectedMeeting = meetings[userInputValue - 1];

        ViewHelper.ClearConsole();
        _editMeetingMenuView.Show(selectedMeeting);
        
        
    }
}