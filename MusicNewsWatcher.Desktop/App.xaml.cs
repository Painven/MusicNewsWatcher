﻿global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using MusicNewsWatcher.Core;
global using MusicNewsWatcher.Desktop.Models;
global using MusicNewsWatcher.Desktop.Services;
global using MusicNewsWatcher.Desktop.ViewModels;
global using MusicNewsWatcher.Desktop.Views;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using ToastNotifications;
global using ToastNotifications.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MusicNewsWatcher.Services;
using System.Threading;
using System.Windows;

namespace MusicNewsWatcher.Desktop;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IHost HostContainer { get; private set; }
    public static Mutex mutex;

    public App()
    {
        mutex = new Mutex(false, "MusicNewsWatcherWpfApp");

        if (!mutex.WaitOne())
        {
            Application.Current.Shutdown();
        }

        HostContainer = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(options =>
            {
                //options.AddUserSecrets(this.GetType().Assembly);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContextFactory<MusicWatcherDbContext>(options =>
                {
                    string connectionString = context.Configuration.GetConnectionString("default");
                    options.UseNpgsql(connectionString);
                });

                services.AddToasts();
                //services.AddNotifyIcon();
                services.AddTelegramBot(context);

                services.AddTransient<ISyncLibraryTracker, SyncLibraryTracker>();
                services.AddTransient<IDialogWindowService, DialogWindowService>();

                services.AddTransient<AddNewArtistDialogViewModel>();
                services.AddTransient<AddNewArtistDialog>();

                services.AddMusicProviders();
                services.AddSingleton<MusicDownloadManager>();
                services.AddSingleton<MusicUpdateManager>();

                services.AddSingleton<SettingsWindowViewModel>();
                services.AddTransient<SettingsWindow>();

                services.AddTransient<SyncLibraryWindow>();
                services.AddTransient<SyncLibraryWindowViewModel>();

                services.AddSingleton<MainWindowViewModel>();
            })
            .Build();

    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        //Окно занимает 85% экрана
        double sizeRatio = 0.85;

        var mainWindow = new MainWindow();
        //mainWindow.Closing += (o, e) => { e.Cancel = true; mainWindow.Hide(); };
        mainWindow.DataContext = HostContainer.Services.GetRequiredService<MainWindowViewModel>();
        mainWindow.Width = SystemParameters.PrimaryScreenWidth * sizeRatio;
        mainWindow.Height = SystemParameters.PrimaryScreenHeight * sizeRatio;
        mainWindow.ShowDialog();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        HostContainer.StopAsync();
        mutex?.ReleaseMutex();

        base.OnExit(e);
    }
}



