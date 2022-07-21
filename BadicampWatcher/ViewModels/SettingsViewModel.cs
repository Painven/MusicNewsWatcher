﻿using Microsoft.EntityFrameworkCore;
using MusicNewsWatcher.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows.Input;

namespace MusicNewsWatcher.ViewModels;

public class SettingsWindowViewModel : ViewModelBase
{

    private readonly IDbContextFactory<MusicWatcherDbContext> dbFactory;

    int downloadThreadsNumber;
    public int DownloadThreadsNumber
    {
        get => downloadThreadsNumber;
        set
        {
            bool inBounds = value >= 1 && value <= 32;
            int actualValue = inBounds ? value : 1;

            if (Set(ref downloadThreadsNumber, actualValue) && IsLoaded)
            {
                settingsChanges[nameof(DownloadThreadsNumber)] = actualValue.ToString();
                RaisePropertyChanged(nameof(HasChanges));
            }
        }
    }

    string telegramApiToken;
    public string TelegramApiToken
    {
        get => telegramApiToken;
        set
        {
            if (Set(ref telegramApiToken, value) && IsLoaded)
            {
                settingsChanges[nameof(TelegramApiToken)] = value;
                RaisePropertyChanged(nameof(HasChanges));
            }
        }
    }

    public bool HasChanges => settingsChanges.Values.Any(v => v != null);
    public bool IsLoaded { get; private set; }

    Dictionary<string, string?> settingsChanges;

    public ICommand SaveCommand { get; }
    public ICommand LoadedCommand { get; }

    public SettingsWindowViewModel()
    {
        LoadedCommand = new LambdaCommand(Loaded);
        SaveCommand = new LambdaCommand(SaveSettings, e => HasChanges);
        settingsChanges = new Dictionary<string, string?>();
    }

    private void Loaded(object obj)
    {
        var pi = this.GetType().GetProperties();

        using var db = dbFactory.CreateDbContext();

        foreach(var settings in db.Settings)
        {
            var prop = pi.FirstOrDefault(p => p.Name == settings.Name);
            if(prop != null)
            {
                dynamic val = prop.PropertyType == typeof(string) ? settings.Value : int.Parse(settings.Value);
                prop.SetValue(this, val);
                RaisePropertyChanged(prop.Name);
            }
        }
        IsLoaded = true;
    }

    public SettingsWindowViewModel(IDbContextFactory<MusicWatcherDbContext> dbFactory) : this()
    {
        this.dbFactory = dbFactory;
    }

    private void SaveSettings(object obj)
    {
        using var db = dbFactory.CreateDbContext();
        
        foreach (var kvp in settingsChanges.Where(i => i.Value != null))
        {
            var alreadyAddedSetting = db.Settings.FirstOrDefault(i => i.Name.Equals(kvp.Key));
            if (alreadyAddedSetting != null)
            {
                db.Settings.Remove(alreadyAddedSetting);
            }
            db.Settings.Add(new SettingsEntity()
            {
                Name = kvp.Key,
                Value = kvp.Value
            });
            settingsChanges[kvp.Key] = null;
        }
        db.SaveChanges();
        
        RaisePropertyChanged(nameof(HasChanges));
    }
}
