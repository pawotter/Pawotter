using UIKit;
using System;
using Pawotter.iOS.Views;
using CoreGraphics;

namespace Pawotter.iOS
{
    public static class UIExtensions
    {
        public static nfloat Width(this UIView view) => view.Frame.Width;
        public static nfloat Height(this UIView view) => view.Frame.Height;
        public static nfloat MaxX(this UIView view) => view.Frame.GetMaxX();
        public static nfloat MinY(this UIView view) => view.Frame.GetMinY();
        public static nfloat MaxY(this UIView view) => view.Frame.GetMaxY();
        public static nfloat X(this UIView view) => view.Frame.X;
        public static nfloat Y(this UIView view) => view.Frame.Y;

        public static nfloat MinusHalfPadding(this nfloat num) => num - L.PaddingM;
        public static nfloat MinusPadding(this nfloat num) => num - L.PaddingL;
        public static nfloat MinusDoublePadding(this nfloat num) => num - L.PaddingL * 2;
        public static nfloat PlusHalfPadding(this nfloat num) => num + L.PaddingM;
        public static nfloat PlusPadding(this nfloat num) => num + L.PaddingL;
        public static nfloat PlusDoublePadding(this nfloat num) => num + L.PaddingL * 2;

        public static UIColor Color(this int colorCode) => colorCode.Color(1.0f);
        public static UIColor Color(this int colorCode, nfloat alpha)
        {
            var r = (colorCode & 0xFF0000) >> 16;
            var g = (colorCode & 0x00FF00) >> 8;
            var b = colorCode & 0x0000FF;
            return UIColor.FromRGB(r, g, b).ColorWithAlpha(alpha);
        }
    }
}
