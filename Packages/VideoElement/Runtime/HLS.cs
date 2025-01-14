using System;
using UnityEngine;

namespace HTML
{
    public partial class HLS : IDisposable
    {
        static HLS()
        {
            HLS_init(EventHandle);
        }

        public static bool IsSupported
        {
            get => HLS_isSupported();
        }

        private string _id;

        public string Id => _id;

        public HLS(HLSOptions config = null)
        {
            var json = config != null ? JsonUtility.ToJson(config) : null;
            _id = HLS_newInstance(json);
        }

        public void Dispose()
        {
            HLS_destroy(_id);
        }

        public void LoadSource(string src)
        {
            HLS_loadSource(_id, src);
        }

        public bool AttachMediaById(string videoId)
        {
            return HLS_attachMediaById(_id, videoId);
        }

        public bool AttachMediaBySelector(string selector)
        {
            return HLS_attachMediaBySelector(_id, selector);
        }
    }
}
