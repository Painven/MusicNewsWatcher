﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicNewsWatcher.Core;
using MusicNewsWatcher.TelegramBot;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContextFactory<MusicWatcherDbContext>(options =>
                {
                    string connectionString = context.Configuration["ConnectionStrings:remote_docker"];
                    options.UseNpgsql(connectionString);
                });

                services.AddSingleton<MusicProviderBase, MusifyMusicProvider>();
                services.AddSingleton<MusicProviderBase, BandcampMusicProvider>();
                services.AddSingleton<MusicUpdateManager>();
                services.AddTransient<IMusicNewsMessageFormatter, MusicNewsHtmlMessageFormatter>();
                services.AddSingleton<MusicNewsWatcherTelegramBot>();

                // Запускает Телеграм бота и службу обновления(парсер)
                services.AddHostedService<ServiceBackgroundWorker>();
            })
            .Build();

        //Должен быть последним, что бы обрабатывать завершение от systemctl stop
        await host.RunAsync();
    }
}
