using System;
using System.Collections.Generic;
using UIKit;

namespace Pawotter.iOS.Views.Libs.SwipePage
{
    public class SwipePageViewConfig
    {
        public UIFont TitleFont { get; set; } = UIFont.SystemFontOfSize(14, UIFontWeight.Bold);
        public UIColor InactiveColor { get; set; } = new UIColor(127f / 256, 140f / 256, 141f / 256, 1.0f);
        public UIColor ActiveColor { get; set; } = new UIColor(27f / 256, 149f / 256, 224f / 256, 1.0f);
        public UIColor BackgroundColor { get; set; } = UIColor.White;
        public UIColor BorderColor { get; set; } = new UIColor(127f / 256, 140f / 256, 141f / 256, 1.0f);

        public nfloat TabHeight { get; set; } = 34;
        public nfloat TabMarginX { get; set; } = 16;
        public nfloat IndicatorWidth { get; set; } = 2;
        public nfloat BorderHeight { get; set; } = 0.5f;

        public IList<UIViewController> ViewControllers { get; set; } = new List<UIViewController>();
    }
}
