using MeetingPlanner.BLL.Exceptions;
using MeetingPlanner.BLL.Services;
using MeetingPlanner.DAL.Entities;
using MeetingPlanner.PLL.Helpers;

namespace MeetingPlanner.PLL.Views;

public class EditMeetingView
{
    private readonly IMeetingService _meetingService;
    private readonly SuccessView _successView;

    public EditMeetingView(IMeetingService meetingService, SuccessView successView)
    {
        _meetingService = meetingService;
        _successView = successView;
    }
    public void EditName(MeetingEntity meetingContext)
    {
        Console.WriteLine("Введите название собрания:");
        var userInputName =  Console.ReadLine();
        while (string.IsNullOrEmpty(userInputName))
        {
            ViewHelper.ClearConsole();
            ViewHelper.AlertMessage("Название не может быть пустым");
            Console.WriteLine("Введите название собрания:");
            userInputName =  Console.ReadLine();
        }

        var meeting = (MeetingEntity)meetingContext.Clone();
        meeting.Name = userInputName;
        _meetingService.Update(meeting);
        ViewHelper.ClearConsole();
        _successView.Show("Собрание успешно изменено");
    }

    public void EditTime(MeetingEntity meetingContext)
    {
        Console.WriteLine("Введите дату и время начала собрания в формате \"01.01.2024 12:35:36\": ");
        var userInputDateStart = Console.ReadLine();
        DateTime meetingDateStart;
        while (!ViewHelper.IsValidDateTime(userInputDateStart, out meetingDateStart))
        {
            ViewHelper.ClearConsole();
            ViewHelper.AlertMessage("Введены некорректные данные");
            Console.WriteLine("Введите дату и время начала собрания в формате \"01.01.2024 12:35:36\": ");
            userInputDateStart = Console.ReadLine();
        }
        
        ViewHelper.ClearConsole();
        Console.WriteLine("Введите дату и время окончания собрания в формате \"01.01.2024 12:35:36\": ");
        var userInputDateEnd = Console.ReadLine();
        DateTime meetingDateEnd;
        while (!ViewHelper.IsValidDateTime(userInputDateEnd, out meetingDateEnd))
        {
            ViewHelper.ClearConsole();
            ViewHelper.AlertMessage("Введены некорректные данные");
            Console.WriteLine("Введите дату и время окончания собрания в формате \"01.01.2024 12:35:36\": ");
            userInputDateEnd = Console.ReadLine();
        }


        var meeting = (MeetingEntity)meetingContext.Clone();
        meeting.StartTime = meetingDateStart;
        meeting.EndTime = meetingDateEnd;


        try
        {
            _meetingService.Update(meeting);
            ViewHelper.ClearConsole();
            _successView.Show("Собрание успешно изменено");
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
    
    public void EditNotificationPeriod(MeetingEntity meetingContext)
    {
        Console.WriteLine("За сколько минут напомнить о начале собрания (можно оставить пустым):");
        var userInputNotify = Console.ReadLine();
        int meetingNotifyBefore;
        while (!ViewHelper.IsValidNotifyPeriod(userInputNotify, out meetingNotifyBefore))
        {
            ViewHelper.ClearConsole();
            ViewHelper.AlertMessage("Введены некорректные данные");
            Console.WriteLine("За сколько минут напомнить о начале собрания (можно оставить пустым):");
            userInputNotify = Console.ReadLine();
        }

        var meeting = (MeetingEntity)meetingContext.Clone();
        meeting.NotifyBeforeMinutes = meetingNotifyBefore;
        _meetingService.Update(meeting);
        ViewHelper.ClearConsole();
        _successView.Show("Собрание успешно изменено");
    }
    
}