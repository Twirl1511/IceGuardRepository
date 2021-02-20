using UnityEngine;

public static class Vibrator 
{
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentAcivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentAcivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentAcivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate(long milliseconds = 200)
    {
        if (IsAndroid())
        {
            try
            {
                vibrator.Call("vibrate", milliseconds);
            }
            catch
            {

            }
            
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    public static void Cancel()
    {
        vibrator.Call("cancel");
    }

    public static bool IsAndroid()
    {
#if UNITY_ANDROID
        return true;
#else
        return false;
#endif
    }

}
