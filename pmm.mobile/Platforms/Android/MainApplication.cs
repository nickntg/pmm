using System;
using Android.App;
using Android.Runtime;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace pmm.mobile.Platforms.Android
{
    [Application]
    public class MainApplication(IntPtr handle, JniHandleOwnership ownership) : MauiApplication(handle, ownership)
    {
        public static readonly string ChannelId = "backgroundServiceChannel";

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
