using System;
using CoreGraphics;
using UIKit;
using Foundation;
namespace Pawotter.iOS
{
    internal static class UIConst
    {
        #region layout
        internal static CGPoint LayoutOrigin => new CGPoint(DefaultPadding, DefaultPadding);
        internal static nfloat DefaultPadding => 8;

        internal static CGSize IconSize => new CGSize(48, 48);
        #endregion

        #region line space
        internal static nfloat DefaultLineSpace => 18;
        #endregion

        #region font size
        internal static nfloat ExtraSmallFontSize => 11;
        internal static nfloat SmallFontSize => 12;
        internal static nfloat DefaultFontSize => 13;
        internal static nfloat LargeFontSize => 14;
        internal static nfloat ExtraLargeFontSize => 15;
        #endregion

        #region font
        internal static UIFont DefaultFont => UIFont.SystemFontOfSize(DefaultFontSize);
        internal static UIFont BoldFont => UIFont.BoldSystemFontOfSize(DefaultFontSize);
        internal static nfloat Width(this UIFont font, String text) => new NSString(text ?? "").StringSize(font, new CGSize(nfloat.MaxValue, nfloat.MaxValue)).Width;
        internal static nfloat Height(this UIFont font, String text, nfloat width) => new NSString(text ?? "").StringSize(font, new CGSize(width, nfloat.MaxValue)).Height;
        #endregion

        #region color
        internal static UIColor DefaultFontColor => ColorCodeToUIColor(0x222222);
        internal static UIColor MetaDataFontColor => ColorCodeToUIColor(0x999999);
        internal static UIColor BackgroundColor => UIColor.White;
        internal static UIColor TimelineBackgroundColor => ColorCodeToUIColor(0xDDDDDD);
        internal static UIColor CellBackgroundColor => UIColor.White;
        #endregion

        static UIColor ColorCodeToUIColor(int code)
        {
            var r = (code & 0xFF0000) >> 16;
            var g = (code & 0x00FF00) >> 8;
            var b = code & 0x0000FF;
            return UIColor.FromRGB(r, g, b);
        }
    }
}
