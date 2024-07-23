using System;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;

namespace pmm.mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhotoBtn_OnClicked(object sender, EventArgs e)
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = MediaPicker.Default.CapturePhotoAsync().Result;

                if (photo != null)
                {
                    // save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = photo.OpenReadAsync().Result;
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    sourceStream.CopyToAsync(localFileStream).Wait();
                }
            }
        }
    }
}
