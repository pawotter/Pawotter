using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Pawotter.iOS.Views
{
    public static class L
    {
        public static UIFont NormalFont => UIFont.SystemFontOfSize(14);
        public static UIFont BoldFont => UIFont.BoldSystemFontOfSize(14);
        public static UIFont LargeBoldFont => UIFont.BoldSystemFontOfSize(16);
        public static UIFont ExtraLargeBoldFont => UIFont.BoldSystemFontOfSize(18);

        public static nfloat PaddingS => 4.0f;
        public static nfloat PaddingM => 8.0f;
        public static nfloat PaddingL => 16.0f;

        public static nfloat LineSpace => 17;
        public static nfloat BorderW => 0.5f;
        public static nfloat Banner => 44;

        public static CGSize UserIcon => new CGSize(48, 48);
        public static nfloat UserIconRadius => 4.0f;
        public static nfloat Radius => 6.0f;
        public static CGSize Icon => new CGSize(18, 18);

        public static nfloat ImageAspectRatio => 506.0f / 254.0f;

        public static nfloat H(this UIFont font, String text, nfloat width)
        {
            var size = new CGSize(width, nfloat.MaxValue);
            var drawingOptions = NSStringDrawingOptions.UsesLineFragmentOrigin;
            var dict = new NSMutableDictionary();
            var paragraphStyle = new NSMutableParagraphStyle();
            paragraphStyle.LineBreakMode = UILineBreakMode.WordWrap;
            dict.Add(UIStringAttributeKey.ParagraphStyle, paragraphStyle);
            dict.Add(UIStringAttributeKey.Font, font);
            return new NSString(text ?? "").WeakGetBoundingRect(size, drawingOptions, dict, null).Height;
        }
    }
}
