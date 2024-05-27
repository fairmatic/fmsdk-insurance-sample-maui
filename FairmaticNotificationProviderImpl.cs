#if __ANDROID__

using Android.Content;
using Com.Fairmatic.Sdk.Classes;
using Java.Interop;
using Android.App;
using AndroidX.Core.App;

public class FairmaticNotificationProviderImpl : Java.Lang.Object, IFairmaticNotificationProvider
{
    public FairmaticNotificationContainer GetInDriveNotificationContainer(Context context)
    {
        NotificationCompat.Builder builder = new NotificationCompat.Builder(context, Android.App.Application.Context.PackageName)
            .SetSmallIcon(MAUI_Sample.Resource.Drawable.dotnet_bot)
            .SetContentTitle("In Drive Notification")
            .SetContentText("You are now in drive mode.");

        Notification notification = builder.Build();
        return new FairmaticNotificationContainer(123, notification);
    }

    public FairmaticNotificationContainer GetMaybeInDriveNotificationContainer(Context context)
    {
        NotificationCompat.Builder builder = new NotificationCompat.Builder(context, Android.App.Application.Context.PackageName)
            .SetSmallIcon(MAUI_Sample.Resource.Drawable.dotnet_bot)
            .SetContentTitle("Maybe In Drive Notification")
            .SetContentText("You maybe in drive.");

        Notification notification = builder.Build();
        return new FairmaticNotificationContainer(123, notification);
    }

    public FairmaticNotificationContainer GetWaitingForDriveNotificationContainer(Context context)
    {
        NotificationCompat.Builder builder = new NotificationCompat.Builder(context, Android.App.Application.Context.PackageName)
            .SetSmallIcon(MAUI_Sample.Resource.Drawable.dotnet_bot)
            .SetContentTitle("Waiting for a Drive Notification")
            .SetContentText("You are waiting for a drive.");

        Notification notification = builder.Build();
        return new FairmaticNotificationContainer(123, notification);
    }
}

#endif