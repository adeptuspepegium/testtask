namespace MeetingPlanner.BLL.Exceptions;

public class StartDateAfterEndDateException : Exception
{
    public StartDateAfterEndDateException()
        :base("Дата начала собрания не может быть позже даты окончания.")
    {
        
    }
}