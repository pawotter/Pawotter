using System.Collections.Generic;
using UIKit;

namespace Pawotter.iOS
{
    /// <summary>
    /// Home(Notificcations/Home/Local/Federated timelines) view controller
    /// </summary>
    public class HomeSwipePageController : UIViewController
    {
        readonly SwipePageViewConfig config = new SwipePageViewConfig();
        SwipePageViewController pageViewController;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            EdgesForExtendedLayout = UIRectEdge.None;
            View.BackgroundColor = UIConst.BackgroundColor;
            config.ViewControllers = GetViewControllers();
            pageViewController = new SwipePageViewController(config);
            AddChildViewController(pageViewController);
            View.AddSubview(pageViewController.View);
            pageViewController.View.Frame = View.Bounds;
            pageViewController.DidMoveToParentViewController(this);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            pageViewController.View.Frame = View.Bounds;
        }

        static IList<UIViewController> GetViewControllers()
        {
            return new List<UIViewController>
            {
                new TimelineViewController { Title = "Notifications" },
                new TimelineViewController { Title = "Home" },
                new TimelineViewController { Title = "Local" },
                new TimelineViewController { Title = "Federated" },
            };
        }
    }
}
