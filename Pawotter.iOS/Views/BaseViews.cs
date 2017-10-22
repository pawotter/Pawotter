using UIKit;
using Pawotter.Core.Consts;
using System;

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
        }
    }

    public sealed class AppNavigationController : BaseNavigationController
    {
        public static AppNavigationController OfModalStyle(UIViewController rootViewController)
        {
            var nav = new AppNavigationController(rootViewController);
            var dismissButton = new UIBarButtonItem(UIBarButtonSystemItem.Stop, (sender, e) =>
            {
                rootViewController.DismissViewController(true, null);
                rootViewController.NavigationItem.RightBarButtonItem.Dispose();
            });
            rootViewController.NavigationItem.SetRightBarButtonItem(dismissButton, false);
            return nav;
        }

        public AppNavigationController(UIViewController rootViewController) : base(rootViewController)
        {
        }
    }

    public abstract class BaseView : UIView
    {

    }

    public abstract class BaseCollectionViewCell : UICollectionViewCell
    {
        protected BaseCollectionViewCell() { }
        protected BaseCollectionViewCell(System.IntPtr handle) : base(handle) { }
    }

    public sealed class Border : UIView
    {
        public Border()
        {
            BackgroundColor = ColorConsts.Border.Color();
        }
    }
}
