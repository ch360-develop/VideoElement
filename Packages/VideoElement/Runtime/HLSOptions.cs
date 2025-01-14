using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace HTML
{
    [Serializable]
    public class HLSOptions : ISerializationCallbackReceiver
    {
        #region JSON attributes

        [SerializeField]
        private bool lowLatencyMode;

        #endregion

        #region Properties

        public bool LowLatencyMode { get => lowLatencyMode; set => lowLatencyMode = value; }

        #endregion

        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            OnAfterDeserialize();
        }

        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            OnBeforeSerialize();
        }
    }
}
