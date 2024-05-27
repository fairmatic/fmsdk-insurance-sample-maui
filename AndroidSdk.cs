namespace MAUI_Sample;

#if __ANDROID__
using Android.App;
using Android.Content;
using AndroidX.Core.App;
using Com.Fairmatic.Sdk;
using Com.Fairmatic.Sdk.Classes;

public class AndroidSdk() : ISdkImplementation {

    FairmaticOperationCallbackImpl fairmaticOperationCallback = new FairmaticOperationCallbackImpl();

    public void OnSetupClicked()
	{
		setupNotification();

		Console.WriteLine("Fairmatic SDK : " + "Setup called");

		var sdkKey = "YOUR_SDK_KEY_HERE";

		// Add DriverAttributes object
		FairmaticDriverAttributes driverAttributes = new FairmaticDriverAttributes("Driver Name", "email_id@something.com", "phone_number");
		
		// Add FairmaticConfiguration object
		FairmaticConfiguration fairmaticConfiguration = new FairmaticConfiguration(sdkKey, "driver_id", driverAttributes);
		
		// Setup Fairmatic
		Fairmatic.Instance.Setup(Android.App.Application.Context, fairmaticConfiguration, Java.Lang.Class.FromType(typeof(FairmaticBroadcastReceiverImpl)),
			Java.Lang.Class.FromType(typeof(FairmaticNotificationProviderImpl)), fairmaticOperationCallback);
	}

	public void OnStartPeriod1Clicked()
	{
		Console.WriteLine("Fairmatic SDK : " + "Start Drive With Period 1 called");
		Fairmatic.Instance.StartDriveWithPeriod1(Android.App.Application.Context, fairmaticOperationCallback);
	}

	public void OnStartPeriod2Clicked()
	{
		Console.WriteLine("Fairmatic SDK : " + "Start Drive With Period 2 called");
		Fairmatic.Instance.StartDriveWithPeriod2(Android.App.Application.Context, "MAUI", fairmaticOperationCallback);
	}

	public void OnStartPeriod3Clicked()
	{
		Console.WriteLine("Fairmatic SDK : " + "Start Drive With Period 3 called");
		Fairmatic.Instance.StartDriveWithPeriod3(Android.App.Application.Context, "MAUI", fairmaticOperationCallback);
	}

	public void OnStopPeriodClicked()
	{
		Console.WriteLine("Fairmatic SDK : " + "Stop Period called");
		Fairmatic.Instance.StopPeriod(Android.App.Application.Context, fairmaticOperationCallback);
	}

	public void OnTeardownClicked()
	{
		Console.WriteLine("Fairmatic SDK : " + "Teardown called");
		Fairmatic.Instance.Teardown(Android.App.Application.Context, fairmaticOperationCallback);
	}

	public void OnGetSettingsClicked()
	{
		Console.WriteLine("Fairmatic SDK : " + "Get Settings called");
		Fairmatic.Instance.GetFairmaticSettings(Android.App.Application.Context, new FairmaticSettingsCallbackImpl());	

		
	}

	public void setupNotification() {
		NotificationManager manager = (NotificationManager) Android.App.Application.Context.GetSystemService(Context.NotificationService);

		if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O) {
			NotificationChannel chan = new NotificationChannel(Android.App.Application.Context.PackageName,
				"channel_id", NotificationImportance.Max);
			manager.CreateNotificationChannel(chan);
		}

		// Build the notification
        NotificationCompat.Builder builder = new NotificationCompat.Builder(Android.App.Application.Context, "channel_id")
            .SetSmallIcon(MAUI_Sample.Resource.Drawable.dotnet_bot)
            .SetContentTitle("SDK setup")
            .SetContentText("Fairmatic SDK is setup");

        // Create a notification manager to send the notification
        NotificationManagerCompat notificationManager = NotificationManagerCompat.From(Android.App.Application.Context);
        notificationManager.Notify(123, builder.Build());
	}
}
#endif
