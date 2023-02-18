﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicNewsWatcher.Core;

[Table(name: "settings")]
public class SettingsEntity
{
    [Key]
    [Column(name: "name")]
    public string Name { get; set; }

    [Column(name: "value")]
    public string Value { get; set; } = string.Empty;
}

[Table(name: "sync_host")]
public class SyncHostEntity
{
    [Key]
    [Column(name: "id")]
    public Guid Id { get; set; }

    [Column(name: "name")]
    public string Name { get; set; }

    [Column(name: "root_folder_path")]
    public string RootFolderPath { get; set; } = string.Empty;
}