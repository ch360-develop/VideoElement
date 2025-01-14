using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;

namespace HTML
{
    public partial class VideoElement
    {
        public VideoEvents Events { get; private set; }

        public class VideoEvents : IDisposable
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            [DllImport("__Internal")]
            private static extern void VideoPlayer_addEventListener(string id, string eventName);
            [DllImport("__Internal")]
            private static extern void VideoPlayer_removeEventListener(string id, string eventName);
#else
            private static void VideoPlayer_addEventListener(string id, string eventName) { }
            private static void VideoPlayer_removeEventListener(string id, string eventName) { }
#endif

            private static Dictionary<string, VideoEvents> _eventHandlers = new();

            public static void RegisterHandler(string id, VideoEvents handler)
            {
                _eventHandlers[id] = handler;
            }

            public static void UnregisterHandler(string id)
            {
                _eventHandlers.Remove(id);
            }

            [MonoPInvokeCallback(typeof(Action<string, string>))]
            internal static void EventHandle(string id, string eventName)
            {
                if (_eventHandlers.TryGetValue(id, out var handler))
                {
                    handler.OnEvent(eventName);
                }
            }

            private VideoElement _parent;

            public VideoEvents(VideoElement parent)
            {
                _parent = parent;
                RegisterHandler(_parent.Id, this);
            }

            public void Dispose()
            {
                UnregisterHandler(_parent.Id);
            }

            public virtual void OnEvent(string eventName)
            {
                switch (eventName)
                {
                    case "abort":
                        _abort?.Invoke(_parent);
                        break;
                    case "canplay":
                        _canplay?.Invoke(_parent);
                        break;
                    case "canplaythrough":
                        _canplaythrough?.Invoke(_parent);
                        break;
                    case "durationchange":
                        _durationchange?.Invoke(_parent);
                        break;
                    case "emptied":
                        _emptied?.Invoke(_parent);
                        break;
                    case "encrypted":
                        _encrypted?.Invoke(_parent);
                        break;
                    case "ended":
                        _ended?.Invoke(_parent);
                        break;
                    case "error":
                        _error?.Invoke(_parent);
                        break;
                    case "loadeddata":
                        _loadeddata?.Invoke(_parent);
                        break;
                    case "loadedmetadata":
                        _loadedmetadata?.Invoke(_parent);
                        break;
                    case "loadstart":
                        _loadstart?.Invoke(_parent);
                        break;
                    case "pause":
                        _pause?.Invoke(_parent);
                        break;
                    case "play":
                        _play?.Invoke(_parent);
                        break;
                    case "playing":
                        _playing?.Invoke(_parent);
                        break;
                    case "progress":
                        _progress?.Invoke(_parent);
                        break;
                    case "ratechange":
                        _ratechange?.Invoke(_parent);
                        break;
                    case "seeked":
                        _seeked?.Invoke(_parent);
                        break;
                    case "seeking":
                        _seeking?.Invoke(_parent);
                        break;
                    case "stalled":
                        _stalled?.Invoke(_parent);
                        break;
                    case "suspend":
                        _suspend?.Invoke(_parent);
                        break;
                    case "timeupdate":
                        _timeupdate?.Invoke(_parent);
                        break;
                    case "volumechange":
                        _volumechange?.Invoke(_parent);
                        break;
                    case "waiting":
                        _waiting?.Invoke(_parent);
                        break;
                }
            }

            #region Event

            private event Action<VideoElement> _abort;
            public event Action<VideoElement> Abort
            {
                add
                {
                    if (_abort == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "abort");
                    }
                    _abort += value;
                }
                remove
                {
                    _abort -= value;
                    if (_abort == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "abort");
                    }
                }
            }
            private event Action<VideoElement> _canplay;
            public event Action<VideoElement> CanPlay
            {
                add
                {
                    if (_canplay == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "canplay");
                    }
                    _canplay += value;
                }
                remove
                {
                    _canplay -= value;
                    if (_canplay == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "canplay");
                    }
                }
            }

            private event Action<VideoElement> _canplaythrough;
            public event Action<VideoElement> CanPlayThrough
            {
                add
                {
                    if (_canplaythrough == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "canplaythrough");
                    }
                    _canplaythrough += value;
                }
                remove
                {
                    _canplaythrough -= value;
                    if (_canplaythrough == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "canplaythrough");
                    }
                }
            }

            private event Action<VideoElement> _durationchange;
            public event Action<VideoElement> DurationChange
            {
                add
                {
                    if (_durationchange == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "durationchange");
                    }
                    _durationchange += value;
                }
                remove
                {
                    _durationchange -= value;
                    if (_durationchange == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "durationchange");
                    }
                }
            }

            private event Action<VideoElement> _emptied;
            public event Action<VideoElement> Emptied
            {
                add
                {
                    if (_emptied == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "emptied");
                    }
                    _emptied += value;
                }
                remove
                {
                    _emptied -= value;
                    if (_emptied == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "emptied");
                    }
                }
            }

            private event Action<VideoElement> _encrypted;
            public event Action<VideoElement> Encrypted
            {
                add
                {
                    if (_encrypted == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "encrypted");
                    }
                    _encrypted += value;
                }
                remove
                {
                    _encrypted -= value;
                    if (_encrypted == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "encrypted");
                    }
                }
            }

            private event Action<VideoElement> _ended;
            public event Action<VideoElement> Ended
            {
                add
                {
                    if (_ended == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "ended");
                    }
                    _ended += value;
                }
                remove
                {
                    _ended -= value;
                    if (_ended == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "ended");
                    }
                }
            }

            private event Action<VideoElement> _error;
            public event Action<VideoElement> Error
            {
                add
                {
                    if (_error == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "error");
                    }
                    _error += value;
                }
                remove
                {
                    _error -= value;
                    if (_error == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "error");
                    }
                }
            }

            private event Action<VideoElement> _loadeddata;
            public event Action<VideoElement> LoadedData
            {
                add
                {
                    if (_loadeddata == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "loadeddata");
                    }
                    _loadeddata += value;
                }
                remove
                {
                    _loadeddata -= value;
                    if (_loadeddata == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "loadeddata");
                    }
                }
            }

            private event Action<VideoElement> _loadedmetadata;
            public event Action<VideoElement> LoadedMetadata
            {
                add
                {
                    if (_loadedmetadata == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "loadedmetadata");
                    }
                    _loadedmetadata += value;
                }
                remove
                {
                    _loadedmetadata -= value;
                    if (_loadedmetadata == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "loadedmetadata");
                    }
                }
            }

            private event Action<VideoElement> _loadstart;
            public event Action<VideoElement> LoadStart
            {
                add
                {
                    if (_loadstart == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "loadstart");
                    }
                    _loadstart += value;
                }
                remove
                {
                    _loadstart -= value;
                    if (_loadstart == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "loadstart");
                    }
                }
            }

            private event Action<VideoElement> _pause;
            public event Action<VideoElement> Pause
            {
                add
                {
                    if (_pause == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "pause");
                    }
                    _pause += value;
                }
                remove
                {
                    _pause -= value;
                    if (_pause == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "pause");
                    }
                }
            }

            private event Action<VideoElement> _play;
            public event Action<VideoElement> Play
            {
                add
                {
                    if (_play == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "play");
                    }
                    _play += value;
                }
                remove
                {
                    _play -= value;
                    if (_play == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "play");
                    }
                }
            }

            private event Action<VideoElement> _playing;
            public event Action<VideoElement> Playing
            {
                add
                {
                    if (_playing == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "playing");
                    }
                    _playing += value;
                }
                remove
                {
                    _playing -= value;
                    if (_playing == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "playing");
                    }
                }
            }

            private event Action<VideoElement> _progress;
            public event Action<VideoElement> Progress
            {
                add
                {
                    if (_progress == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "progress");
                    }
                    _progress += value;
                }
                remove
                {
                    _progress -= value;
                    if (_progress == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "progress");
                    }
                }
            }

            private event Action<VideoElement> _ratechange;
            public event Action<VideoElement> RateChange
            {
                add
                {
                    if (_ratechange == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "ratechange");
                    }
                    _ratechange += value;
                }
                remove
                {
                    _ratechange -= value;
                    if (_ratechange == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "ratechange");
                    }
                }
            }

            private event Action<VideoElement> _seeked;
            public event Action<VideoElement> Seeked
            {
                add
                {
                    if (_seeked == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "seeked");
                    }
                    _seeked += value;
                }
                remove
                {
                    _seeked -= value;
                    if (_seeked == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "seeked");
                    }
                }
            }

            private event Action<VideoElement> _seeking;
            public event Action<VideoElement> Seeking
            {
                add
                {
                    if (_seeking == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "seeking");
                    }
                    _seeking += value;
                }
                remove
                {
                    _seeking -= value;
                    if (_seeking == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "seeking");
                    }
                }
            }

            private event Action<VideoElement> _stalled;
            public event Action<VideoElement> Stalled
            {
                add
                {
                    if (_stalled == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "stalled");
                    }
                    _stalled += value;
                }
                remove
                {
                    _stalled -= value;
                    if (_stalled == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "stalled");
                    }
                }
            }

            private event Action<VideoElement> _suspend;
            public event Action<VideoElement> Suspend
            {
                add
                {
                    if (_suspend == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "suspend");
                    }
                    _suspend += value;
                }
                remove
                {
                    _suspend -= value;
                    if (_suspend == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "suspend");
                    }
                }
            }

            private event Action<VideoElement> _timeupdate;
            public event Action<VideoElement> TimeUpdate
            {
                add
                {
                    if (_timeupdate == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "timeupdate");
                    }
                    _timeupdate += value;
                }
                remove
                {
                    _timeupdate -= value;
                    if (_timeupdate == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "timeupdate");
                    }
                }
            }

            private event Action<VideoElement> _volumechange;
            public event Action<VideoElement> VolumeChange
            {
                add
                {
                    if (_volumechange == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "volumechange");
                    }
                    _volumechange += value;
                }
                remove
                {
                    _volumechange -= value;
                    if (_volumechange == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "volumechange");
                    }
                }
            }

            private event Action<VideoElement> _waiting;
            public event Action<VideoElement> Waiting
            {
                add
                {
                    if (_waiting == null)
                    {
                        VideoPlayer_addEventListener(_parent.Id, "waiting");
                    }
                    _waiting += value;
                }
                remove
                {
                    _waiting -= value;
                    if (_waiting == null)
                    {
                        VideoPlayer_removeEventListener(_parent.Id, "waiting");
                    }
                }
            }
        }
    }

    #endregion
}
