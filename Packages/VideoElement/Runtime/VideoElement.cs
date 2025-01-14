using System;
using UnityEngine;

namespace HTML
{
    public partial class VideoElement : IDisposable
    {
        static VideoElement()
        {
            VideoPlayer_init(VideoEvents.EventHandle);
        }

        public static VideoElement Create()
        {
            var id = Guid.NewGuid().ToString();
            VideoPlayer_createVideoElement(id);
            return new VideoElement(id, true);
        }

        public static VideoElement AttachById(string id)
        {
            if (!VideoPlayer_attachVideoElementById(id))
            {
                throw new ArgumentException($"VideoElement.AttachById: element not found: {id}");
            }
            return new VideoElement(id, false);
        }

        public static VideoElement AttachBySelector(string selector)
        {
            var id = VideoPlayer_attachVideoElementBySelector(selector);
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException($"VideoElement.AttachBySelector: element not found: {selector}");
            }
            return new VideoElement(id, false);
        }

        private string _id;
        private bool _needRemove;

        public string Id => _id;

        private VideoElement(string id, bool needRemove)
        {
            _id = id;
            _needRemove = needRemove;
            Events = new VideoEvents(this);
        }

        public void Dispose()
        {
            Events.Dispose();
            if (_needRemove)
            {
                VideoPlayer_removeElement(Id);
            }
        }

        public bool CanPlayType(string type)
        {
            return VideoPlayer_canPlayType(_id, type);
        }

        public void Load(Uri uri)
        {
            VideoPlayer_setSource(_id, uri.AbsoluteUri);
        }

        public void Play()
        {
            VideoPlayer_play(_id);
        }

        public void Pause()
        {
            VideoPlayer_pause(_id);
        }

        public double CurrentTime
        {
            get
            {
                return VideoPlayer_getCurrentTime(_id);
            }
            set
            {
                VideoPlayer_setCurrentTime(_id, value);
            }
        }

        public double Duration
        {
            get
            {
                return VideoPlayer_getDuration(_id);
            }
        }

        public bool IsEnded
        {
            get
            {
                return VideoPlayer_getEnded(_id);
            }
        }

        public bool Seekable
        {
            get
            {
                return VideoPlayer_getSeekable(_id);
            }
        }

        public bool Loop
        {
            get
            {
                return VideoPlayer_getLoop(_id);
            }
            set
            {
                VideoPlayer_setLoop(_id, value);
            }
        }

        public bool Muted
        {
            get
            {
                return VideoPlayer_getMuted(_id);
            }
            set
            {
                VideoPlayer_setMuted(_id, value);
            }
        }

        public double Volume
        {
            get
            {
                return VideoPlayer_getVolume(_id);
            }
            set
            {
                VideoPlayer_setVolume(_id, value);
            }
        }

        public ReadyState ReadyState
        {
            get
            {
                return (ReadyState)VideoPlayer_getReadyState(_id);
            }
        }

        public int VideoHeight
        {
            get
            {
                return VideoPlayer_getVideoHeight(_id);
            }
        }

        public int VideoWidth
        {
            get
            {
                return VideoPlayer_getVideoWidth(_id);
            }
        }

        public void CreateVideoTexture(Texture tex)
        {
            VideoPlayer_createVideoTexture(tex.GetNativeTexturePtr());
        }

        public void DeleteVideoTexture(Texture tex)
        {
            VideoPlayer_deleteVideoTexture(tex.GetNativeTexturePtr());
        }

        public bool FetchVideoTexture(Texture tex)
        {
            return VideoPlayer_fetchVideoTexture(_id, tex.GetNativeTexturePtr());
        }
    }
}
