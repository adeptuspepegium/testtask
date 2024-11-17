using MeetingPlanner.BLL.Services;
using MeetingPlanner.DAL.Entities;
using MeetingPlanner.PLL.Helpers;

namespace MeetingPlanner.PLL.Views;

public class ExportMeetingsView
{
    private readonly IExportService _exportService;
    private readonly SuccessView _successView;


    public ExportMeetingsView(IExportService exportService, SuccessView successView)
    {
        _exportService = exportService;
        _successView = successView;
    }
    public void Show(List<MeetingEntity> meetings)
    {
        Console.WriteLine("Введите путь для экспорта csv файла (полный путь или папку):");
        var inputValue = Console.ReadLine();

        if (!_exportService.IsValidPath(inputValue))
        {
            ViewHelper.ClearConsole();
            ViewHelper.AlertMessage("Введен некорректный путь");
            Show(meetings);
            return;

        }
        _exportService.ExportReport(inputValue, meetings);
        ViewHelper.ClearConsole();
        _successView.Show("Собрания успешно экспортированы");
    }
}