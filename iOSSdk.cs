namespace MAUI_Sample;

#if __IOS__

using FMSDKMAUIiOS;

/// <summary>
/// Fairmatic delegate implementation.
/// </summary>
public class FairmaticDelegateImpl: FairmaticDelegate {
    public override void ProcessAccidentDetected(FairmaticAccidentInfo accidentInfo)
    {
        Console.WriteLine("ProcessAccidentDetected");
    }

    public override void ProcessAnalysisOfDrive(FairmaticAnalyzedDriveInfo analyzedDriveInfo)
    {
        Console.WriteLine("ProcessAnalysisOfDrive");
    }

    public override void ProcessEndOfDrive(FairmaticEstimatedDriveInfo estimatedDriveInfo)
    {
        Console.WriteLine($"ProcessEndOfDrive {estimatedDriveInfo.DriveId}");
    }

    public override void ProcessPotentialAccidentDetected(FairmaticAccidentInfo accidentInfo)
    {
        Console.WriteLine("ProcessPotentialAccidentDetected");
    }

    public override void ProcessResumeOfDrive(FairmaticDriveResumeInfo resumeInfo)
    {
        Console.WriteLine("ProcessResumeOfDrive");
    }

    public override void ProcessStartOfDrive(FairmaticDriveStartInfo startInfo)
    {
        Console.WriteLine($"ProcessStartOfDrive {startInfo.DriveId}");
    }

    public override void SettingsChanged(FairmaticSettings settings)
    {
        Console.WriteLine("SettingsChanged");
    }
}


public class iOSSdk() : ISdkImplementation
{
    FairmaticDelegate fairmaticDelegate = new FairmaticDelegateImpl();

    public void OnSetupClicked()
    {
        Console.WriteLine("Setup clicked");
        
        var sdkKey = "YOUR_SDK_KEY_HERE";

        FairmaticDriverAttributes fairmaticDriverAttributes = new FairmaticDriverAttributes(
            name: "Driver Name",
            email: "email_id@something.com",
            phoneNumber: "1234567890"
        );

        FairmaticConfiguration fairmaticConfiguration = new FairmaticConfiguration(
            driverId: "driver_id",
            sdkKey: sdkKey,
            driverAttributes: fairmaticDriverAttributes
        );

        Fairmatic.SetupWithConfiguration(fairmaticConfiguration, fairmaticDelegate, (success, error) =>
        {
            if (success)
            {
                Console.WriteLine("Fairmatic SDK Setup Success");
            }
            else
            {
                Console.WriteLine($"Fairmatic SDK Setup Failed: {error}");
            }
        });
    }

    public void OnStartPeriod1Clicked()
    {
        Fairmatic.StartDriveWithPeriod1((success, error) =>
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
    }

    public void OnStartPeriod2Clicked()
    {
        Fairmatic.StartDriveWithPeriod2("trackingId-MAUI", (success, error) =>
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
    }

    public void OnStartPeriod3Clicked()
    {
        Fairmatic.StartDriveWithPeriod3("trackingId-MAUI", (success, error) =>
        {
            if (success)
            {
                Console.WriteLine("Start Drive Period 3 Success");
            }
            else
            {
                Console.WriteLine($"Start Drive Period 3 Failed: {error}");
            }
        }); // Add a closing parenthesis here
    }

    public void OnStopPeriodClicked()
    {
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
    }

    public void OnTeardownClicked()
    {
        Fairmatic.TeardownWithCompletionHandler(() =>
        {
            Console.WriteLine("Teardown Success");
        });
    }

    public void OnGetSettingsClicked() {
        FairmaticSettings? fairmaticSettings = Fairmatic.Settings;

        if (fairmaticSettings != null)
        {
            FairmaticSettingsError[] fairmaticSettingsError = fairmaticSettings.Errors;

            Console.WriteLine($"Settings: {fairmaticSettings}");

            // If fairmaticSettingsError has elements, For each element in fairmaticSettingsError, print the error message
            if (fairmaticSettingsError.Length > 0)
            {
                foreach (var sdkError in fairmaticSettingsError)
                {
                    Console.WriteLine($"Error: {sdkError.ErrorType}");
                }
            } else {
                Console.WriteLine("No errors found in settings");
            }
        } else {
            Console.WriteLine("Settings not available, please set up Fairmatic SDK first.");
        }
    }
}
#endif