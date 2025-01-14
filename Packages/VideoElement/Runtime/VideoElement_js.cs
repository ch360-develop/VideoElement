using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace HTML
{
    public enum ReadyState
    {
        HAVE_NOTHING = 0,
        HAVE_METADATA = 1,
        HAVE_CURRENT_DATA = 2,
        HAVE_FUTURE_DATA = 3,
        HAVE_ENOUGH_DATA = 4,
    }

    public partial class VideoElement
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void VideoPlayer_init(Action<string, string> action);
        [DllImport("__Internal")]
        private static extern bool VideoPlayer_attachVideoElementById(string id);
        [DllImport("__Internal")]
        private static extern string VideoPlayer_attachVideoElementBySelector(string selector);
        [DllImport("__Internal")]
        private static extern void VideoPlayer_createVideoElement(string id);
        [DllImport("__Internal")]
        private static extern void VideoPlayer_removeElement(string id);

        [DllImport("__Internal")]
        private static extern bool VideoPlayer_canPlayType(string id, string type);
        [DllImport("__Internal")]
        private static extern void VideoPlayer_setSource(string id, string uri);
        [DllImport("__Internal")]
        private static extern void VideoPlayer_play(string id);
        [DllImport("__Internal")]
        private static extern void VideoPlayer_pause(string id);
        [DllImport("__Internal")]
        private static extern double VideoPlayer_getCurrentTime(string id);
        [DllImport("__Internal")]
        private static extern void VideoPlayer_setCurrentTime(string id, double time);
        [DllImport("__Internal")]
        private static extern double VideoPlayer_getDuration(string id);
        [DllImport("__Internal")]
        private static extern bool VideoPlayer_getMuted(string id);
        [DllImport("__Internal")]
        private static extern void VideoPlayer_setMuted(string id, bool value);
        [DllImport("__Internal")]
        private static extern int VideoPlayer_getReadyState(string id);
        [DllImport("__Internal")]
        private static extern bool VideoPlayer_getEnded(string id);
        [DllImport("__Internal")]
        private static extern bool VideoPlayer_getSeekable(string id);
        [DllImport("__Internal")]
        private static extern bool VideoPlayer_getLoop(string id);
        [DllImport("__Internal")]
        private static extern void VideoPlayer_setLoop(string id, bool value);
        [DllImport("__Internal")]
        private static extern double VideoPlayer_getVolume(string id);
        [DllImport("__Internal")]
        private static extern void VideoPlayer_setVolume(string id, double value);
        [DllImport("__Internal")]
        private static extern int VideoPlayer_getVideoHeight(string id);
        [DllImport("__Internal")]
        private static extern int VideoPlayer_getVideoWidth(string id);

        [DllImport("__Internal")]
        private static extern void VideoPlayer_createVideoTexture(IntPtr texture);
        [DllImport("__Internal")]
        private static extern void VideoPlayer_deleteVideoTexture(IntPtr texture);
        [DllImport("__Internal")]
        private static extern bool VideoPlayer_fetchVideoTexture(string id, IntPtr texture);
#else
        private static void VideoPlayer_init(Action<string, string> action) { }
        private static bool VideoPlayer_attachVideoElementById(string id)
        {
            Debug.LogWarning("VideoPlayer_attachVideoElementById not supported in this platform.");
            return true;
        }
        private static string VideoPlayer_attachVideoElementBySelector(string selector)
        {
            Debug.LogWarning("VideoPlayer_attachVideoElementBySelector not supported in this platform.");
            return "";
        }
        private static void VideoPlayer_createVideoElement(string id) { }
        private static void VideoPlayer_removeElement(string id) { }
        private static bool VideoPlayer_canPlayType(string id, string type) { return false; }
        private static void VideoPlayer_setSource(string id, string uri) { }
        private static void VideoPlayer_play(string id) { }
        private static void VideoPlayer_pause(string id) { }
        private static double VideoPlayer_getCurrentTime(string id) { return 0; }
        private static void VideoPlayer_setCurrentTime(string id, double time) { }
        private static double VideoPlayer_getDuration(string id) { return 0; }
        private static bool VideoPlayer_getMuted(string id) { return false; }
        private static void VideoPlayer_setMuted(string id, bool value) { }
        private static int VideoPlayer_getReadyState(string id) { return 0; }
        private static bool VideoPlayer_getEnded(string id) { return false; }
        private static bool VideoPlayer_getSeekable(string id) { return false; }
        private static bool VideoPlayer_getLoop(string id) { return false; }
        private static void VideoPlayer_setLoop(string id, bool value) { }
        private static double VideoPlayer_getVolume(string id) { return 0; }
        private static void VideoPlayer_setVolume(string id, double value) { }
        private static int VideoPlayer_getVideoHeight(string id) { return 0; }
        private static int VideoPlayer_getVideoWidth(string id) { return 0; }
        private static void VideoPlayer_createVideoTexture(IntPtr texture) { }
        private static void VideoPlayer_deleteVideoTexture(IntPtr texture) { }
        private static bool VideoPlayer_fetchVideoTexture(string id, IntPtr texture) { return false; }
#endif
    }
}
