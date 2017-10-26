using UIKit;
using System;
using System.Reactive.Disposables;
using Pawotter.Core.Consts;
using Pawotter.Core.Logger;

namespace Pawotter.iOS.Views
{
    public interface IScrollableViewController
    {
        void ScrollsToTop();
    }

    public abstract class BaseViewController : UIViewController
    {
        protected ILogger logger = Application.Container.GetInstance<ILogger>();

        protected BaseViewController()
        {
#if DEBUG
            logger.Debug($"[new] {GetType().Name}.{GetHashCode()}");
#endif
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = ColorConsts.Background.Color();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (TabBarController?.NavigationItem != null)
            {
                TabBarController.NavigationItem.RightBarButtonItem = null;
                TabBarController.NavigationItem.LeftBarButtonItem = null;
            }
        }

        ~BaseViewController()
        {
#if DEBUG
            logger.Debug($"[delete] {GetType().Name}.{GetHashCode()}");
#endif
        }
    }

    public abstract class BaseNavigationController : UINavigationController
    {
        protected ILogger logger = Application.Container.GetInstance<ILogger>();

        protected BaseNavigationController(UIViewController rootViewController) : base(rootViewController)
        {
#if DEBUG
            logger.Debug($"[new] {GetType().Name}.{GetHashCode()}");
#endif
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationBar.Translucent = false;
            View.BackgroundColor = ColorConsts.Background.Color();
        }

        ~BaseNavigationController()
        {
#if DEBUG
            logger.Debug($"[delete] {GetType().Name}.{GetHashCode()}");
#endif
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

    public abstract class BaseCollectionReusableView : UICollectionReusableView
    {
        protected BaseCollectionReusableView() { }
        protected BaseCollectionReusableView(IntPtr handle) : base(handle) { }
    }
}
