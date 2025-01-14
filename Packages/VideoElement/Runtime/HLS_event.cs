using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AOT;

namespace HTML
{
    public partial class HLS
    {
        public enum Events
        {
            MediaAttaching,
            MediaAttached,
            MediaDetaching,
            MediaDetached,
            BufferReset,
            BufferCodecs,
            BufferCreated,
            BufferAppending,
            BufferAppended,
            BufferEos,
            BufferFlushing,
            BufferFlushed,
            BackBufferReached,
            ManifestLoading,
            ManifestLoaded,
            ManifestParsed,
            SteeringManifestLoaded,
            LevelSwitching,
            LevelSwitched,
            LevelLoading,
            LevelLoaded,
            LevelUpdated,
            LevelPtsUpdated,
            LevelsUpdated,
            AudioTracksUpdated,
            AudioTrackSwitching,
            AudioTrackSwitched,
            AudioTrackLoading,
            AudioTrackLoaded,
            SubtitleTracksUpdated,
            SubtitleTrackSwitch,
            SubtitleTrackLoading,
            SubtitleTrackLoaded,
            SubtitleFragProcessed,
            InitPtsFound,
            FragLoading,
            FragLoadProgress,
            FragLoadEmergencyAborted,
            FragLoaded,
            FragDecrypted,
            FragParsingInitSegment,
            FragParsingUserdata,
            FragParsingMetadata,
            FragParsingData,
            FragParsed,
            FragBuffered,
            FragChanged,
            FpsDrop,
            FpsDropLevelCapping,
            Error,
            Destroying,
            KeyLoading,
            KeyLoaded,
            StreamStateTransition,
            NonNativeTextTracksFound,
            CuesParse,
        }

        public static string ToSnake(string str)
        {
            return str.Select((c, i) => i > 0 && char.IsUpper(c) ? $"_{c}" : char.ToUpperInvariant(c).ToString())
                .Aggregate(string.Empty, (s1, s2) => s1 + s2);
        }

#if !UNITY_EDITOR && UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void HLS_events_on(string id, string eventName);
        [DllImport("__Internal")]
        private static extern void HLS_events_off(string id, string eventName);
#else
        private static void HLS_events_on(string id, string eventName) { }
        private static void HLS_events_off(string id, string eventName) { }
#endif

        private static void HLS_events_on(string id, Events eventName)
        {
            HLS_events_on(id, ToSnake(eventName.ToString()));
        }

        private static void HLS_events_off(string id, Events eventName)
        {
            HLS_events_off(id, ToSnake(eventName.ToString()));
        }

        private static Dictionary<string, HLS> _eventHandlers = new();

        public static void RegisterHandler(string id, HLS handler)
        {
            _eventHandlers[id] = handler;
        }

        public static void UnregisterHandler(string id)
        {
            _eventHandlers.Remove(id);
        }

        [MonoPInvokeCallback(typeof(Action<string, string, string>))]
        private static void EventHandle(string id, string eventName, string data)
        {
            if (_eventHandlers.TryGetValue(id, out var handler))
            {
                var @event = Enum.Parse<Events>(eventName);
                handler.OnEvent(@event, data);
            }
        }

        public virtual void OnEvent(Events @event, string data = null)
        {
            switch (@event)
            {
                case Events.MediaAttaching:
                    _mediaAttaching?.Invoke();
                    break;
            }
        }

        #region Event

        private event Action _mediaAttaching;
        public event Action MediaAttaching
        {
            add
            {
                if (_mediaAttaching == null)
                {
                    HLS_events_on(_id, Events.MediaAttaching);
                }
                _mediaAttaching += value;
            }
            remove
            {
                _mediaAttaching -= value;
                if (_mediaAttaching == null)
                {
                    HLS_events_off(_id, Events.MediaAttaching);
                }
            }
        }
    }

    #endregion
}
