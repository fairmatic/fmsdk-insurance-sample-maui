# Binding library for the Fairmatic iOS SDK

## Prerequisites

- The SDK supports iOS 13 or above. Your app should target iOS 13 or above to use the SDK.
- `dotnet` SDK version 8.0.300 or above.
- You should have the latest stable version of Xcode installed.
- [Sign in](https://app.fairmatic.com/settings/advanced) to the Fairmatic dashboard to access your Fairmatic SDK Key.

## Adjusting project settings

### Background Modes

Allow background location updates and background fetch for your app:
On the project screen, click Capabilities → Turn Background Modes on → Select Location updates and Background Fetch

### Permission-related keys in `Info.plist`

If your app does not already have them, please include the following keys in your app's `Info.plist`:

```xml
<key>NSLocationAlwaysAndWhenInUseUsageDescription</key>
<string>We need background location permission to provide you with
driving analytics</string>
<key>NSLocationWhenInUseUsageDescription</key>
<string>We need background location permission to provide you with
driving analytics</string>
<key>NSMotionUsageDescription</key>
<string>We use activity to detect your trips faster and more accurately.
This also reduces the amount of battery we use.</string>
<key>NSBluetoothAlwaysUsageDescription</key>
<string>Bluetooth</string>
<key>NSBluetoothPeripheralUsageDescription</key>
<string>Bluetooth</string>
```

> [!NOTE] 
> Even though we won't actually use Bluetooth features, Apple requires this message whenever Bluetooth code is present in an app. This is just a technical requirement.


### Background task ID

For the Fairmatic SDK to be more accurate in uploading all trip data, it needs to have [background fetch capability](https://developer.apple.com/documentation/uikit/using-background-tasks-to-update-your-app) and a background task id declared in your Info.plist file. You must add the following line in `Info.plist` file:

```xml
<key>BGTaskSchedulerPermittedIdentifiers</key>
<array>
	<string>com.fairmatic.sdk.bgrefreshtask</string>
</array>
```

In the case you already have a background refresh task, as iOS allows only one scheduled background fetch task, you will need to reuse your existing `BGAppRefreshTask` to call the following function:

```swift
Fairmatic.logSDKHealth(.backgroundProcessing) { _ in
    // task.setTaskCompleted(success: success)
}
```

In this case, don’t add the new `BGTaskSchedulerPermittedIdentifiers` to your Info.plist.

### Keychain entitlements for Simulators

> [!IMPORTANT]
> This setup is mandatory for the SDK to work correctly on iOS simulators.

The SDK uses iOS `Keychain` to store data, and the Keychain related APIs do not work out of the box in MAUI projects on Simulators. Hence, to set up the SDK correctly on simulators, please include an `Entitlements.plist` file with the following content in your project under the `Platforms/iOS` directory. Replace the `com.fairmatic.MauiTestApp` with the correct bundle identifier.

```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>keychain-access-groups</key>
    <array>
        <string>$(AppIdentifierPrefix)com.fairmatic.MauiTestApp</string>
    </array>
</dict>
</plist>
```


## Setup the Fairmatic SDK

To put the SDK into a “ready” state, you’ll first need to set up the Fairmatic SDK properly. This allows subsequent Insurance Period APIs to be called and the SDK to start actively capturing information. Replace the `"YOUR_SDK_KEY"` with the SDK key provided by the Fairmatic team.

```csharp
using Com.Fairmatic.Sdk.iOS;

FairmaticDriverAttributes fairmaticDriverAttributes = new FairmaticDriverAttributes(
  firstName: "Sagar",
  lastName: "MAUI",
  email: "sagarmaui@test.com",
  phoneNumber: "1234567890"
);

FairmaticConfiguration fairmaticConfiguration = new FairmaticConfiguration(
  driverId: "sagar+maui@fairmatic.com",
  sdkKey: "YOUR_SDK_KEY",
  driverAttributes: fairmaticDriverAttributes
);

Fairmatic.SetupWithConfiguration(fairmaticConfiguration, (success, error) => {
  if (success) {
    Console.WriteLine("Fairmatic SDK Setup Success");
  } else {
    Console.WriteLine($"Fairmatic SDK Setup Failed: {error}");
  }
});
```
> [!IMPORTANT]
> This code should also be present in the flow when your driver logs in successfully into the app, and it should be called at every app launch with proper configuration. Failing to do so will result in errors in the trip APIs.


## Call the insurance APIs

### Insurance period 1
Start insurance period 1 when the driver starts the day and is waiting for a request. The tracking ID is a key that is used to uniquely identify the insurance trip.

```csharp
Fairmatic.StartDriveWithPeriod1("trackingId1-MAUI", (success, error) =>
{
    if (success)
    {
        Console.WriteLine("Start Drive Period 1 Success");
    }
    else
    {
        Console.WriteLine($"Start Drive Period 1 Failed: {error}");
    }
});
```

### Insurance period 2
Start insurance period 2 when the driver accepts the passenger's or the company's request.

```csharp
Fairmatic.StartDriveWithPeriod2("trackingId2-MAUI", (success, error) =>
{
    if (success)
    {
        Console.WriteLine("Start Drive Period 2 Success");
    }
    else
    {
        Console.WriteLine($"Start Drive Period 2 Failed: {error}");
    }
});
```

## Insurance period 3
Stop the insurance period when the driver ends the work day. Call stop period when the driver is no longer looking for a request.

```csharp
Fairmatic.StartDriveWithPeriod3("trackingId3-MAUI", (success, error) =>
{
    if (success)
    {
        Console.WriteLine("Start Drive Period 3 Success");
    }
    else
    {
        Console.WriteLine($"Start Drive Period 3 Failed: {error}");
    }
});
```

### Stopping the insurance period
Stop the insurance period when the driver ends the work day. Call stop period when the driver is no longer looking for a request.

```csharp
Fairmatic.StopPeriod((success, error) =>
{
    if (success)
    {
        Console.WriteLine("Stop Period Success");
    }
    else
    {
        Console.WriteLine($"Stop Period Failed: {error}");
    }
});
```

## Fairmatic SDK settings

Ensure you check for any errors and take appropriate actions in your app to resolve them, ensuring the Fairmatic SDK operates smoothly. Use the following code snippet to perform this check:

```csharp
Fairmatic.GetSettingsWithCompletionHandler((settings) =>
{
    Console.WriteLine("Get Settings Success");

    FairmaticSettingsError[] fairmaticSettingsError = settings.Errors;
    if (fairmaticSettingsError.Length == 0)
    {
        Console.WriteLine("No errors found in settings.");
        return;
    }
    
    string errorMessage = string.Join(", ", fairmaticSettingsError.Select(e => e.ErrorType));
    Console.WriteLine($"Errors found in settings: {errorMessage}");

    // Act on those errors ...
});
```

## Disable SDK [Optional step]
Call teardown API when the driver is no longer working with the application and logs out. This will completely disable the SDK on the application.

```csharp
Fairmatic.TeardownWithCompletionHandler(() =>
{
    Console.WriteLine("Teardown Success");
});
```