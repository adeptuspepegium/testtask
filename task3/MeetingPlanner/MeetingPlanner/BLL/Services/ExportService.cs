using MeetingPlanner.DAL.Entities;

namespace MeetingPlanner.BLL.Services;

public class ExportService : IExportService
{
    public void ExportReport(string path, List<MeetingEntity> meetings)
    {
        CreateFolderIfNotExist(path);
        var filePath = GetFullName(path);
        using (var writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Название; Дата начала; Дата окончания;Напоминание за");
            foreach (var meeting in meetings)
            {
                writer.WriteLine($"{meeting.Name};{meeting.StartTime};{meeting.EndTime};{meeting.NotifyBeforeMinutes}");
            }
        };

    }

    public bool IsValidPath(string path)
    {
        return Path.IsPathFullyQualified(path);
    }

    private void CreateFolderIfNotExist(string path)
    {
        var directoryPath = path.EndsWith(".csv") ? Path.GetDirectoryName(path) : path;
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    private string GetFullName(string path)
    {
        return path.EndsWith(".csv") ? path : Path.Combine(path, "report.csv");
    }
}