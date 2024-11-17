using MeetingPlanner.DAL.Entities;

namespace MeetingPlanner.BLL.Services;

public interface IExportService
{
    void ExportReport(string filePath, List<MeetingEntity> meetings);
    bool IsValidPath(string path);

}