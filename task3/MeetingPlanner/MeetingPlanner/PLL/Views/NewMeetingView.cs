using System.Threading.Channels;
using MeetingPlanner.BLL.Exceptions;
using MeetingPlanner.BLL.Services;
using MeetingPlanner.DAL.Entities;
using MeetingPlanner.PLL.Helpers;

namespace MeetingPlanner.PLL.Views;

public class NewMeetingView
{

    private readonly IMeetingService _meetingService;
    private readonly SuccessView _successView;

    public NewMeetingView(IMeetingService meetingService, SuccessView successView)
    {
        _meetingService = meetingService;
        _successView = successView;
    }
    public void Show()
    {
        string userInputName;
        DateTime meetingDateStart;
        DateTime meetingDateEnd;
        int meetingNotifyBefore;
        
        
        string userInputDateStart;
        string userInputDateEnd;
        string userInputNotify;
        
        
        
        Console.WriteLine("Введите название собрания:");
        userInputName =  Console.ReadLine();
        while (string.IsNullOrEmpty(userInputName))
        {
            ViewHelper.ClearConsole();
            ViewHelper.AlertMessage("Название не может быть пустым");
            Console.WriteLine("Введите название собрания:");
            userInputName =  Console.ReadLine();
        }
        
        ViewHelper.ClearConsole();
        Console.WriteLine("Введите дату и время начала собрания в формате \"01.01.2024 12:35:36\": ");
        userInputDateStart = Console.ReadLine();
        while (!ViewHelper.IsValidDateTime(userInputDateStart, out meetingDateStart))
        {
            ViewHelper.ClearConsole();
            ViewHelper.AlertMessage("Введены некорректные данные");
            Console.WriteLine("Введите дату и время начала собрания в формате \"01.01.2024 12:35:36\": ");
            userInputDateStart = Console.ReadLine();
        }
        ViewHelper.ClearConsole();
        Console.WriteLine("Введите дату и время окончания собрания в формате \"01.01.2024 12:35:36\": ");
        userInputDateEnd = Console.ReadLine();
        while (!ViewHelper.IsValidDateTime(userInputDateEnd, out meetingDateEnd))
        {
            ViewHelper.ClearConsole();
            ViewHelper.AlertMessage("Введены некорректные данные");
            Console.WriteLine("Введите дату и время окончания собрания в формате \"01.01.2024 12:35:36\": ");
            userInputDateEnd = Console.ReadLine();
        }
        ViewHelper.ClearConsole();
        Console.WriteLine("За сколько минут напомнить о начале собрания (можно оставить пустым):");
        userInputNotify = Console.ReadLine();
        while (!ViewHelper.IsValidNotifyPeriod(userInputNotify, out meetingNotifyBefore))
        {
            ViewHelper.ClearConsole();
            ViewHelper.AlertMessage("Введены некорректные данные");
            Console.WriteLine("За сколько минут напомнить о начале собрания (можно оставить пустым):");
            userInputNotify = Console.ReadLine();
        }

        ViewHelper.ClearConsole();
        var meeting = new MeetingEntity()
        {
            Name = userInputName,
            StartTime = meetingDateStart,
            EndTime = meetingDateEnd,
            NotifyBeforeMinutes = meetingNotifyBefore
        };

        try
        {
            _meetingService.Add(meeting);
            ViewHelper.ClearConsole();
            _successView.Show("Собрание успешно создано");
        }
        catch (MeetingDateInPastException e)
        {
            ViewHelper.AlertMessage(e.Message);
            Console.WriteLine("Для перехода в главное меню нажмите любую клавишу");
            Console.ReadKey();

        }
        catch (StartDateAfterEndDateException e)
        {
            ViewHelper.AlertMessage(e.Message);
            Console.WriteLine("Для перехода в главное меню нажмите любую клавишу");
            Console.ReadKey();
        }
        catch (Exception e)
        {
            throw e;
        }


    }
}