using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Pawotter.iOS.Views
{
    public static class L
    {
        public static nfloat PaddingS => 4.0f;
        public static nfloat PaddingM => 8.0f;
        public static nfloat PaddingL => 16.0f;

        public static nfloat LineSpace => 17;
        public static nfloat BorderW => 0.5f;
        public static nfloat Banner => 44;

        public static CGSize UserIcon => new CGSize(48, 48);
        public static nfloat UserIconRadius => 4.0f;
        public static CGSize Icon => new CGSize(18, 18);

        public static nfloat H(this UIFont font, String text, nfloat width)
        {
            var size = new CGSize(width, nfloat.MaxValue);
            var drawingOptions = NSStringDrawingOptions.UsesLineFragmentOrigin;
            var attributes = new UIStringAttributes { ParagraphStyle = new NSParagraphStyle { LineBreakMode = UILineBreakMode.WordWrap }, Font = font }.Dictionary;
            return new NSString(text ?? "").WeakGetBoundingRect(size, drawingOptions, attributes, null).Height;
        }

        public static nfloat W(this UIFont font, String text)
        {
            var size = new CGSize(nfloat.MaxValue, font.PointSize);
            var drawingOptions = NSStringDrawingOptions.UsesLineFragmentOrigin;
            var attributes = new UIStringAttributes { ParagraphStyle = new NSParagraphStyle { LineBreakMode = UILineBreakMode.WordWrap }, Font = font }.Dictionary;
            return new NSString(text ?? "").WeakGetBoundingRect(size, drawingOptions, attributes, null).Width;
        }
    }
}
