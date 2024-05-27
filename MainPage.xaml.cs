namespace MAUI_Sample;

public partial class MainPage : ContentPage
{

	ISdkImplementation sdkImplementation = default!;

	public MainPage()
	{
		InitializeComponent();

		if (DeviceInfo.Platform == DevicePlatform.Android)
		{
			#if __ANDROID__
			sdkImplementation = new AndroidSdk();
			#endif
		}
		else if (DeviceInfo.Platform == DevicePlatform.iOS)
		{
			#if __IOS__
			sdkImplementation = new iOSSdk();
			#endif
		}
	}

	private void OnSetupClicked(object sender, EventArgs e)
	{
		sdkImplementation.OnSetupClicked();
	}

	private void OnStartPeriod1Clicked(object sender, EventArgs e)
	{
		sdkImplementation.OnStartPeriod1Clicked();
	}

	private void OnStartPeriod2Clicked(object sender, EventArgs e)
	{
		sdkImplementation.OnStartPeriod2Clicked();
	}

	private void OnStartPeriod3Clicked(object sender, EventArgs e)
	{
		sdkImplementation.OnStartPeriod3Clicked();
	}

	private void OnStopPeriodClicked(object sender, EventArgs e)
	{
		sdkImplementation.OnStopPeriodClicked();
	}

	private void OnTeardownClicked(object sender, EventArgs e)
	{
		sdkImplementation.OnTeardownClicked();
	}

	private void OnGetSettingsClicked(object sender, EventArgs e)
	{
		sdkImplementation.OnGetSettingsClicked();
	}
}
