namespace MeetingPlanner.DAL.Entities;

public class MeetingEntity : ICloneable
{
    public Guid Id { get;  }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int NotifyBeforeMinutes { get; set; }


    public MeetingEntity()
    {
        Id = Guid.NewGuid();
    }


    public object Clone()
    {
        return this.MemberwiseClone();
    }
}