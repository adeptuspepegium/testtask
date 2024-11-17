namespace MeetingPlanner.BLL.Exceptions;

public class MeetingDateInPastException : Exception
{
    public MeetingDateInPastException()
        :base("Собрание не может быть назначено на прошлое время.")
    {
        
    }
}