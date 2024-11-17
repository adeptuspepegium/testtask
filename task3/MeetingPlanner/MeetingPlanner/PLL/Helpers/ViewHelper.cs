using ConsoleTables;
using MeetingPlanner.DAL.Entities;

namespace MeetingPlanner.PLL.Helpers;

public class ViewHelper
{
    public static bool IsValidDateTime(string input, out DateTime dateTime)
    {
        var format = "dd.MM.yyyy HH:mm:ss";
        return DateTime.TryParseExact(input, format, null, 0, out dateTime);
    }
    
    public static bool IsValidDate(string input, out DateTime dateTime)
    {
        var format = "dd.MM.yyyy";
        return DateTime.TryParseExact(input, format, null, 0, out dateTime);
    }

    public static bool IsValidNotifyPeriod(string input, out int value)
    {
        if (string.IsNullOrEmpty(input))
        {
            value = 0;
            return true;
        }

        return int.TryParse(input, out value);
    }

    public static void ClearConsole()
    {
        Console.Clear();
    }

    public static void AlertMessage(string text)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ForegroundColor = originalColor;
    }
    
    public static void SuccessMessage(string text)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
        Console.ForegroundColor = originalColor;
    }
    
    public static void PrettyPrint(List<MeetingEntity> meetingEntities)
    {
        var table = new ConsoleTable("№", "Название", "Дата начала", "Дата окончания", "Напоминание за, мин");

        for (int i = 0; i < meetingEntities.Count; i++)
        {
            table.AddRow(i+1, meetingEntities[i].Name, meetingEntities[i].StartTime, meetingEntities[i].EndTime, meetingEntities[i].NotifyBeforeMinutes);
        }
        
        table.Write(Format.MarkDown);
    }
    
    public static void PrettyPrint(MeetingEntity meetingEntity)
    {
        var table = new ConsoleTable("Название", "Дата начала", "Дата окончания", "Напоминание за, мин");

        
        table.AddRow(meetingEntity.Name, meetingEntity.StartTime, meetingEntity.EndTime, meetingEntity.NotifyBeforeMinutes);

        
        table.Write(Format.MarkDown);
    }
}