#if __ANDROID__

using Com.Fairmatic.Sdk.Classes;

public class FairmaticSettingsCallbackImpl : Java.Lang.Object, IFairmaticSettingsCallback
{
    public void OnComplete(FairmaticSettings? fairmaticSettings)
    {
        if (fairmaticSettings != null)
        {
            FairmaticSettingError[] fairmaticSettingsError = fairmaticSettings.Errors.ToArray();

            Console.WriteLine($"Settings: {fairmaticSettings}");

            // If fairmaticSettingsError has elements, For each element in fairmaticSettingsError, print the error message
            if (fairmaticSettingsError.Length > 0)
            {
                foreach (var sdkError in fairmaticSettingsError)
                {
                    Console.WriteLine($"Error: {sdkError.Type}");
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