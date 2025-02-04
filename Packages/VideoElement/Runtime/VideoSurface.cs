using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace HTML
{
    public enum VideoSurfaceType
    {
        /** 0: (Default) The renderer for rendering 3D GameObject, such as Cube、Cylinder and Plane.*/
        Renderer = 0,
        /** 1: The renderer for rendering Raw Image of the UI components. */
        RawImage = 1,
        /** 2: Renderer の拡張。aspectRatio に設定されたアスペクト比を維持してレンダリングします。 */
        RenderTexture = 2,
    };

    /** The definition of VideoSurface. */
    public class VideoSurface : MonoBehaviour
    {
        private Renderer _renderer = null;
        private Material _material = null;
        private Texture2D _nativeTexture = null;
        /* only one of the following should be set, depends on VideoSurfaceType */
        private RawImage _rawImage = null;
        public VideoElement Element { get; set; }

        [SerializeField]
        private VideoSurfaceType _videoSurfaceType = VideoSurfaceType.Renderer;
        [SerializeField]
        private VideoAspectRatio _aspectRatio = VideoAspectRatio.FitInside;
        [SerializeField]
        private RenderTexture _renderTexture = null;
        [SerializeField]
        private int _renderTextureWidth = 1280;
        [SerializeField]
        private int _renderTextureHeight = 720;

        private int _prevDrawingVideoWidth = 0;
        private int _prevDrawingVideoHeight = 0;
        private Rect _sampleRect = new Rect(0, 0, 1, 1);

        // Controls the rendering. False for nothing to be rendered.
        private bool _enable = false;

        public Texture GetTexture()
        {
            if (_videoSurfaceType == VideoSurfaceType.RenderTexture)
            {
                return _renderTexture;
            }
            return _nativeTexture;
        }

        void Start()
        {
            if (_renderer == null && (_videoSurfaceType == VideoSurfaceType.Renderer || _videoSurfaceType == VideoSurfaceType.RenderTexture))
            {
                _renderer = GetComponent<Renderer>();
            }

            if (_renderer == null || _videoSurfaceType == VideoSurfaceType.RawImage)
            {
                if (TryGetComponent(out _rawImage))
                {
                    _videoSurfaceType = VideoSurfaceType.RawImage;
                }
            }

            if (_videoSurfaceType == VideoSurfaceType.RenderTexture)
            {
                if (_renderTexture == null)
                {
                    _renderTexture = new RenderTexture(_renderTextureWidth, _renderTextureHeight, 24, RenderTextureFormat.ARGB32);
                    _renderTexture.Create();
                }
            }

            if (_renderer == null && _rawImage == null)
            {
                _enable = false;
                Debug.LogError("Unable to find surface render in VideoSurface component.");
            }
            else
            {
                UpdateMaterial();
            }
        }

        void Update()
        {
            if (_enable)
            {
                var isBlankTexture = IsBlankTexture();
                var width = Element.VideoWidth;
                var height = Element.VideoHeight;

                if (!isBlankTexture && (_nativeTexture.width != width || _nativeTexture.height != height))
                {
                    DestroyTexture();
                }
                if (isBlankTexture && width > 0 && height > 0)
                {
                    CreateTexture(width, height);
                }

                if (width > 0 && height > 0)
                {
                    var fetched = Element.FetchVideoTexture(_nativeTexture);
                    if (fetched && _videoSurfaceType == VideoSurfaceType.RenderTexture)
                    {
                        DrawRenderTexture(width, height);
                    }
                }
                else
                {
                    if (_videoSurfaceType == VideoSurfaceType.RenderTexture)
                    {
                        FillRenderTexture(Color.black);
                    }
                }
            }
            else
            {
                if (!IsBlankTexture())
                {
                    DestroyTexture();
                    ApplyTexture(null);
                    if (_videoSurfaceType == VideoSurfaceType.RenderTexture)
                    {
                        FillRenderTexture(Color.black);
                    }
                }
            }
        }

        void OnDestroy()
        {
            RenderingEnabled = false;

            DestroyTexture();

            if (_renderTexture != null)
            {
                _renderTexture.Release();
                Destroy(_renderTexture);
                _renderTexture = null;
            }

            if (_material != null)
            {
                Destroy(_material);
                _material = null;
            }
        }

        public Renderer Renderer
        {
            get => _renderer;
            set => _renderer = value;
        }

        public Material Material
        {
            get => _material;
            private set => _material = value;
        }

        public VideoSurfaceType VideoSurfaceType
        {
            get => _videoSurfaceType;
            set => _videoSurfaceType = value;
        }

        /** Starts/Stops the video rendering.
        *
        * @param enable Whether to start/stop the video rendering.
        * - true: (Default) Start.
        * - false: Stop.
        */
        public bool RenderingEnabled
        {
            get => _enable;
            set => _enable = (_renderer != null || _rawImage != null) && value;
        }

        private bool IsBlankTexture()
        {
            if (_videoSurfaceType == VideoSurfaceType.Renderer)
            {
                // if never assigned or assigned texture is not Texture2D, we will consider it blank and create a new one
                return _material.mainTexture == null || !(_material.mainTexture is Texture2D);
            }
            else if (_videoSurfaceType == VideoSurfaceType.RawImage)
            {
                return _rawImage.texture == null;
            }
            else if (_videoSurfaceType == VideoSurfaceType.RenderTexture)
            {
                return _material.mainTexture == null || !(_material.mainTexture is RenderTexture);
            }
            else
            {
                return true;
            }
        }

        private void CreateTexture(int width, int height)
        {
            _nativeTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
            _nativeTexture.wrapMode = TextureWrapMode.Clamp;
            _nativeTexture.Apply(false, false);
            Element.CreateVideoTexture(_nativeTexture);

            if (_videoSurfaceType == VideoSurfaceType.RenderTexture)
            {
                ApplyTexture(_renderTexture);
            }
            else
            {
                ApplyTexture(_nativeTexture);
            }
        }

        /// <summary>
        ///  Fetch the texture from Native and display Image Data. Method posts the relevant information to Surface renderer.
        /// </summary>
        private void ApplyTexture(Texture texture)
        {
            if (_videoSurfaceType == VideoSurfaceType.Renderer || _videoSurfaceType == VideoSurfaceType.RenderTexture)
            {
                _material.mainTexture = texture;
            }
            else if (_videoSurfaceType == VideoSurfaceType.RawImage)
            {
                _rawImage.texture = texture;
            }
        }

        private void DestroyTexture()
        {
            if (_nativeTexture != null)
            {
                Element.DeleteVideoTexture(_nativeTexture);
                Destroy(_nativeTexture);
                _nativeTexture = null;
            }
        }

        private void UpdateMaterial()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            if (_renderer != null)
            {
                // WebGL では UV マッピングが反転するため、Y 軸反転シェーダーを変更
                var shader = Shader.Find("Unlit/FlipY");
                if (shader != null)
                {
                    _renderer.material = _material = new Material(shader);
                }
                else
                {
                    Debug.LogWarning("Failed to find shader: Unlit/FlipY");
                    _material = new Material(_renderer.material);
                    _renderer.material = _material;
                }
            }
            else if (_rawImage == null)
            {
                // TODO: rawImage で Y 軸反転が必要か確認する
            }
#endif
        }

        private bool DrawRenderTexture(int width, int height)
        {
            if (width != _prevDrawingVideoWidth || height != _prevDrawingVideoHeight)
            {
                // 映像サイズとアスペクト比設定に応じて、表示領域を調整
                _sampleRect = GraphicUtils.CalculateSampleRect(width, height, _renderTexture.width, _renderTexture.height, _aspectRatio);
                _prevDrawingVideoWidth = width;
                _prevDrawingVideoHeight = height;
            }

            // VideoAspectRatio を考慮して RenderTexture に描画
            var at = RenderTexture.active;
            GraphicUtils.DrawRenderTexture(_nativeTexture, _sampleRect, _renderTexture);
            RenderTexture.active = at;

            return true;
        }

        private void FillRenderTexture(Color color)
        {
            var at = RenderTexture.active;
            GraphicUtils.FillRenderTexture(_renderTexture, color);
            RenderTexture.active = at;
        }
    }
}
