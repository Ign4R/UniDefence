using System.Runtime.InteropServices;
using UnityEngine;

public class MobileDetector : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern int isMobile();
#endif

    public static bool IsMobile()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return isMobile() == 1;
#else
        return false;
#endif
    }
}
