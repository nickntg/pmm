using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Microsoft.Maui.Devices;
using Console = System.Console;
using Environment = Android.OS.Environment;

namespace pmm.mobile.Platforms.Android;

[Service]
internal class MyBackgroundService : Service
{
    private bool _working;
    private bool _firstRun = true;
    private List<string> _ignoredFiles = [];
    private readonly List<QueuedFile> _queuedFiles = [];
    private readonly JsonSerializerOptions _serializationOptions = new();


    public override IBinder OnBind(Intent intent)
    {
        throw new NotImplementedException();
    }

    public override StartCommandResult OnStartCommand(Intent intent,
        StartCommandFlags flags, int startId)
    {
        _ = new Timer(Timer_Elapsed, null, 0, 10000);

        return StartCommandResult.Sticky;
    }

    private async void Timer_Elapsed(object state)
    {
        if (_working)
        {
            return;
        }

        _working = true;

        var pdir = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures);
        var files = Directory.GetFileSystemEntries(pdir.AbsolutePath, "*.jpg");

        if (_firstRun)
        {
            _firstRun = false;
            _ignoredFiles = files.ToList();
        }
        else
        {
            foreach (var file in files)
            {
                if (_ignoredFiles.Contains(file))
                {
                    continue;
                }

                var queued = new QueuedFile
                {
                    Contents = await File.ReadAllBytesAsync(file)
                };

                _queuedFiles.Add(queued);

                _ignoredFiles.Add(file);

                Console.WriteLine($"Queued {file}");
            }
        }

        var toRemove = new List<QueuedFile>();

        foreach (var queued in _queuedFiles)
        {
            if (queued.RetryAt.CompareTo(DateTimeOffset.UtcNow) > 0)
            {
                continue;
            }
            
            var data = new MobileDataDto
            {
                ImageContent = queued.Contents,
                BatteryLevel = Battery.ChargeLevel,
                RecordedAt = queued.ReadAt,
                DeviceName = DeviceInfo.Current.Name
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(Configuration.ServerUrl);
                    var result = await client.PostAsync(uri,
                        new StringContent(JsonSerializer.Serialize(data, _serializationOptions),
                            Encoding.UTF8, "application/json"));

                    if (!result.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException($"POST failed with code {result.StatusCode}");
                    }
                }

                toRemove.Add(queued);
            }
            catch (Exception ex)
            {
                queued.RetryAt = DateTimeOffset.UtcNow.AddMinutes(1);
                Console.WriteLine(ex.ToString());
            }
        }

        foreach (var remove in toRemove)
        {
            _queuedFiles.Remove(remove);
        }

        _working = false;
    }

    internal class QueuedFile
    {
        public byte[] Contents { get; set; }
        public DateTimeOffset ReadAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset RetryAt { get; set; } = DateTimeOffset.UtcNow.AddSeconds(-1);
    }

    internal class MobileDataDto
    {
        public byte[] ImageContent { get; set; }
        public double BatteryLevel { get; set; }
        public string DeviceName { get; set; }
        public DateTimeOffset RecordedAt { get; set; }
    }
}