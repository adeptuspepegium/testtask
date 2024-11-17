using MeetingPlanner.BLL.Services;
using MeetingPlanner.BLL.Validators;
using MeetingPlanner.BLL.Validators.Interfaces;
using MeetingPlanner.DAL;
using MeetingPlanner.DAL.Repositories;
using MeetingPlanner.PLL.Extensions;
using MeetingPlanner.PLL.Views;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection services = new ServiceCollection();
services.AddSingleton<MeetingStorage>();
services.AddTransient<INotificationService, NotificationService>();
services.AddTransient<IMeetingRepository, MeetingRepository>();
services.AddTransient<IMeetingService, MeetingService>();
services.AddTransient<IMeetingValidator, MeetingValidator>();
services.AddTransient<IExportService, ExportService>();

services.AddViews();

var serviceProvider = services.BuildServiceProvider();

var mainView = serviceProvider.GetService<MainView>();

while (true)
{
    mainView.Show();
}