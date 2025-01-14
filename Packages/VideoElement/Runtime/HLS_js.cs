using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace HTML
{
    public partial class HLS
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void HLS_init(Action<string, string, string> action);
        [DllImport("__Internal")]
        private static extern bool HLS_isSupported();
        [DllImport("__Internal")]
        private static extern string HLS_newInstance(string config);
        [DllImport("__Internal")]
        private static extern void HLS_destroy(string id);
        [DllImport("__Internal")]
        private static extern void HLS_loadSource(string id, string src);
        [DllImport("__Internal")]
        private static extern bool HLS_attachMediaById(string id, string videoId);
        [DllImport("__Internal")]
        private static extern bool HLS_attachMediaBySelector(string id, string selector);
#else
        private static void HLS_init(Action<string, string, string> action) { }
        private static bool HLS_isSupported() { return false; }
        private static string HLS_newInstance(string config)
        {
            Debug.LogWarning("HLS is not supported in this platform.");
            return null;
        }
        private static void HLS_destroy(string id) { }
        private static void HLS_loadSource(string id, string src) { }
        private static bool HLS_attachMediaById(string id, string videoId) { return false; }
        private static bool HLS_attachMediaBySelector(string id, string selector) { return false; }
#endif
    }
}