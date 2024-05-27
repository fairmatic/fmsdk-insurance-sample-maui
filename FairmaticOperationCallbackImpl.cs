#if __ANDROID__

using Com.Fairmatic.Sdk.Classes;
public class FairmaticOperationCallbackImpl : Java.Lang.Object, IFairmaticOperationCallback
{
    public void OnCompletion(IFairmaticOperationResult result)
    {
        Android.Util.Log.Info("Fairmatic SDK", "OnCompletion called");
        if (result is IFairmaticOperationResult.Failure)
        {
            Android.Util.Log.Info("Fairmatic SDK", "Result : " + (result as IFairmaticOperationResult.Failure).ErrorMessage);
        } else {
            Android.Util.Log.Info("Fairmatic SDK", "Result : " + (result as IFairmaticOperationResult.Success));
        }
    }
}

#endif