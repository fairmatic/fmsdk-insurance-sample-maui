#if __ANDROID__

using Com.Fairmatic.Sdk.Classes;

public class FairmaticSettingsCallbackImpl : Java.Lang.Object, IFairmaticSettingsCallback
{
    public void OnComplete(IList<FairmaticSettingError> errors)
    {
        if (errors.Count > 0)
        {
            foreach (var sdkError in errors)
            {
                Console.WriteLine($"Error: {sdkError}");
            }
        }
    }
}

#endif