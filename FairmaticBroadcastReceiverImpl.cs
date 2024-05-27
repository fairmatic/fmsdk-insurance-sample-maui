#if __ANDROID__

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Fairmatic.Sdk;
using Com.Fairmatic.Sdk.Classes;

public class FairmaticBroadcastReceiverImpl : FairmaticBroadcastReceiver
{
    public override void OnAccident(Context context, AccidentInfo accidentInfo)
    {
        Console.WriteLine("Fairmatic SDK : " + "CallBack From SDK: Accident Detected");
    }

    public override void OnDriveAnalyzed(Context context, AnalyzedDriveInfo analyzedDriveInfo)
    {
        Console.WriteLine("Fairmatic SDK : " + "CallBack From SDK: Drive Analyzed");
    }

    public override void OnDriveEnd(Context context, EstimatedDriveInfo estimatedDriveInfo)
    {
        Console.WriteLine("Fairmatic SDK : " + "CallBack From SDK: Drive End");
    }

    public override void OnDriveResume(Context context, DriveResumeInfo driveResumeInfo)
    {
        Console.WriteLine("Fairmatic SDK : " + "CallBack From SDK: Drive Resumed");
    }

    public override void OnDriveStart(Context context, DriveStartInfo startInfo)
    {
        Console.WriteLine("Fairmatic SDK : " + "CallBack From SDK: Drive Start");
    }

    public override void OnFairmaticSettingsConfigChanged(Context context, bool errorsFound, bool warningsFound)
    {
        Console.WriteLine("Fairmatic SDK : " + "CallBack From SDK: FairmaticSettingsChanged : " +
                    errorsFound + " : " + warningsFound);
    }
}

#endif