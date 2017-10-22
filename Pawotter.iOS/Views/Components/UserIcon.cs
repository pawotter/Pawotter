using UIKit;
using System.Reactive.Linq;
using System;
using Reactive.Bindings.Extensions;
using Pawotter.iOS.Views.Account;
using Pawotter.ViewModels;

namespace Pawotter.iOS.Views.Components
{
    public class UserIcon : BaseButton
    {
        public UserIcon()
        {
            ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            BackgroundColor = UIColor.Orange; // fixme
            Layer.CornerRadius = L.UserIconRadius;
            ClipsToBounds = true;
        }

        public override void WillMoveToSuperview(UIView newsuper)
        {
            base.WillMoveToSuperview(newsuper);
            if (newsuper == null) return;
            Observable.FromEventPattern(x => TouchUpInside += x, x => TouchUpInside -= x)
                      .Subscribe(_ => router.PushViewController(new AccountViewController(new AccountViewModel()), true))
                      .AddTo(disposeBag);
        }
    }
}
