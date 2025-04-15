namespace MAUI_Sample;

#if __IOS__

using Com.Fairmatic.Sdk.iOS;

public class iOSSdk() : ISdkImplementation
{
    public void OnSetupClicked()
    {
        Console.WriteLine("Setup clicked");
        
        var sdkKey = "YOUR_SDK_KEY_HERE";

        FairmaticDriverAttributes fairmaticDriverAttributes = new FairmaticDriverAttributes(
            firstName: "FirstName",
            lastName: "LastName",
            email: "email_id@something.com",
            phoneNumber: "1234567890"
        );

        FairmaticConfiguration fairmaticConfiguration = new FairmaticConfiguration(
            driverId: "driver_id",
            sdkKey: sdkKey,
            driverAttributes: fairmaticDriverAttributes
        );

        Fairmatic.SetupWithConfiguration(fairmaticConfiguration, (success, error) =>
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
        Fairmatic.StartDriveWithPeriod2("trackingId1-MAUI", (success, error) =>
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
    }

    public void OnStartPeriod3Clicked()
    {
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
        Fairmatic.GetSettingsWithCompletionHandler((settings) =>
        {
            Console.WriteLine("Get Settings Success");

            // Print settings errors from the object
            FairmaticSettingsError[] fairmaticSettingsError = settings.Errors;

            if (fairmaticSettingsError.Length == 0)
            {
                Console.WriteLine("No errors found in settings.");
                return;
            }

            //Concatenate the errors into a single string
            string errorMessage = string.Join(", ", fairmaticSettingsError.Select(e => e.ErrorType));
            Console.WriteLine($"Errors found in settings: {errorMessage}");
        });
    }
}
#endif