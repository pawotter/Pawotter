using System;
using System.Collections.Generic;
using UIKit;

namespace Pawotter.iOS
{
    public class SwipePageViewConfig
    {
        public nfloat TabHeight { get; set; } = 32;
        public nfloat TabMarginX { get; set; } = 12;
        public UIFont TabTitleFont { get; set; } = UIFont.SystemFontOfSize(12, UIFontWeight.Bold);
        public UIColor IndicatorColor { get; set; } = UIColor.Gray;
        public nfloat IndicatorWidth { get; set; } = 4;
        public UIColor TabBackgroundColor { get; set; } = UIColor.White;
        public IList<UIViewController> ViewControllers { get; set; } = new List<UIViewController>();
    }
}
