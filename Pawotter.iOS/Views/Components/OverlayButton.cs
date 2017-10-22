using Pawotter.Core.Consts;
using UIKit;
using System.Reactive.Linq;
using System;
using Reactive.Bindings.Extensions;

namespace Pawotter.iOS.Views.Components
{
    public class OverlayButton : BaseButton
    {
        public OverlayButton()
        {
            BackgroundColor = ColorConsts.Overlay.Color().ColorWithAlpha(ColorConsts.Alpha);
            Layer.CornerRadius = L.Radius;
            ClipsToBounds = true;
            TitleLabel.Font = L.BoldFont;
            SetTitleColor(ColorConsts.White.Color(), UIControlState.Normal);
        }

        public override void WillMoveToSuperview(UIView newsuper)
        {
            base.WillMoveToSuperview(newsuper);
            if (newsuper == null) return;
            Observable.FromEventPattern(x => TouchUpInside += x, x => TouchUpInside -= x)
                      .Subscribe(_ => Hidden = true)
                      .AddTo(disposeBag);
        }

        ~OverlayButton()
        {
            Console.WriteLine("deinit");
        }
    }
}
