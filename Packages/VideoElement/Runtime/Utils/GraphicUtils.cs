using UnityEngine;
using UnityEngine.Video;

namespace HTML
{
    public static class GraphicUtils
    {
        public static void FillRenderTexture(RenderTexture dest, Color color)
        {
            Graphics.SetRenderTarget(dest);
            GL.Clear(true, true, color);
        }

        public static void DrawRenderTexture(Texture src, Rect targetRect, RenderTexture dest)
        {
            Graphics.SetRenderTarget(dest);
            GL.PushMatrix();
            GL.LoadPixelMatrix(0, 1, 1, 0);
            GL.Clear(true, true, new Color(0, 0, 0, 0));
            Graphics.DrawTexture(targetRect, src);
            GL.PopMatrix();
        }

        /**
         * コピー元の領域とコピー先の領域から、標本領域を計算
         */
        public static Rect CalculateSampleRect(float srcWidth, float srcHeight, float dstWidth, float dstHeight, VideoAspectRatio aspectRatio)
        {
            if (aspectRatio == VideoAspectRatio.NoScaling)
            {
                return new Rect(0, 0, srcWidth / dstWidth, srcHeight / dstHeight);
            }
            else if (aspectRatio == VideoAspectRatio.FitVertically)
            {
                var srcAR = srcWidth / srcHeight;
                var rect = new Rect(0, 0, dstHeight * srcAR / dstWidth, 1);
                // 中心を出す
                return new Rect((1.0f - rect.width) / 2.0f, (1.0f - rect.height) / 2.0f, rect.width, rect.height);
            }
            else if (aspectRatio == VideoAspectRatio.FitHorizontally)
            {
                var srcAR = srcWidth / srcHeight;
                var rect = new Rect(0, 0, 1, dstWidth / srcAR / dstHeight);
                // 中心に合わせる
                return new Rect((1.0f - rect.width) / 2.0f, (1.0f - rect.height) / 2.0f, rect.width, rect.height);
            }
            else if (aspectRatio == VideoAspectRatio.FitInside)
            {
                Rect rect;
                if (srcWidth / dstWidth > srcHeight / dstHeight)
                {
                    // 水平方向に Fit
                    var srcAR = srcWidth / srcHeight;
                    rect = new Rect(0, 0, 1, dstWidth / srcAR / dstHeight);
                }
                else
                {
                    // 垂直方向に Fit
                    var srcAR = srcWidth / srcHeight;
                    rect = new Rect(0, 0, dstHeight * srcAR / dstWidth, 1);
                }
                // 中心に合わせる
                return new Rect((1.0f - rect.width) / 2.0f, (1.0f - rect.height) / 2.0f, rect.width, rect.height);
            }
            else if (aspectRatio == VideoAspectRatio.FitOutside)
            {
                Rect rect;
                if (srcWidth / dstWidth > srcHeight / dstHeight)
                {
                    // 垂直方向に Fit
                    var srcAR = srcWidth / srcHeight;
                    rect = new Rect(0, 0, dstHeight * srcAR / dstWidth, 1);
                }
                else
                {
                    // 水平方向に Fit
                    var srcAR = srcWidth / srcHeight;
                    rect = new Rect(0, 0, 1, dstWidth / srcAR / dstHeight);
                }
                // 中心に合わせる
                return new Rect((1.0f - rect.width) / 2.0f, (1.0f - rect.height) / 2.0f, rect.width, rect.height);
            }

            // stretch mode
            return new Rect(0, 0, 1, 1);
        }
    }
}