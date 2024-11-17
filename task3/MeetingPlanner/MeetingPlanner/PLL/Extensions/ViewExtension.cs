using ConsoleTables;
using MeetingPlanner.DAL.Entities;
using MeetingPlanner.PLL.Views;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingPlanner.PLL.Extensions;

public static class ViewExtension
{
    public static IServiceCollection AddViews(this IServiceCollection service)
    {
        service.AddTransient<MainView>();
        service.AddTransient<NewMeetingView>();
        service.AddTransient<PreviewMeetingsMenuView>();
        service.AddTransient<PreviewMeetingsView>();
        service.AddTransient<EditMeetingMenuView>();
        service.AddTransient<EditMeetingView>();
        service.AddTransient<ExportMeetingsView>();
        service.AddTransient<SuccessView>();
        return service;
    }
    
}