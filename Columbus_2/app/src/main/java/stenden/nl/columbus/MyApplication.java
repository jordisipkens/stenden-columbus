package stenden.nl.columbus;

import android.app.Application;

/**
 * Created by Jordi on 6/9/2015.
 */
public class MyApplication extends Application{
    public static boolean isActivityVisible() {
        return activityVisible;
    }

    public static void activityResumed() {
        activityVisible = true;
    }

    public static void activityPaused() {
        activityVisible = false;
    }

    private static boolean activityVisible;
}
