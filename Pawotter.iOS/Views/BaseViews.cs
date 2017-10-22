using UIKit;
using System;
using System.Reactive.Disposables;

namespace Pawotter.iOS.Views
{
    public abstract class BaseViewController : UIViewController
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (TabBarController?.NavigationItem != null)
            {
                TabBarController.NavigationItem.RightBarButtonItem = null;
                TabBarController.NavigationItem.LeftBarButtonItem = null;
            }
        }
    }

    public abstract class BaseNavigationController : UINavigationController
    {
        protected BaseNavigationController(UIViewController rootViewController) : base(rootViewController)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationBar.Translucent = false;
        }
    }

    public abstract class BaseView : UIView
    {
    }

    public abstract class BaseButton : UIButton
    {
        protected readonly CompositeDisposable disposeBag = new CompositeDisposable();
        protected readonly IRouter router = Application.Container.GetInstance<IRouter>();

        protected BaseButton()
        {
        }

        public override void RemoveFromSuperview()
        {
            base.RemoveFromSuperview();
            disposeBag.Dispose();
        }
    }

    public abstract class BaseCollectionViewCell : UICollectionViewCell
    {
        protected BaseCollectionViewCell() { }
        protected BaseCollectionViewCell(IntPtr handle) : base(handle) { }
    }
}
