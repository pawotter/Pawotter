using UIKit;
using Pawotter.iOS.Views.Timeline;
using Pawotter.ViewModels;

namespace Pawotter.iOS.Views.Libs.SwipePage
{
    public class SwipeSampleTabViewController : BaseViewController
    {
        readonly SwipePageViewConfig config = new SwipePageViewConfig();
        readonly SwipePageViewController pageViewController;

        public SwipeSampleTabViewController()
        {
            pageViewController = new SwipePageViewController(config);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            EdgesForExtendedLayout = UIRectEdge.None;
            config.ViewControllers = new UIViewController[]
            {
                new TimelineViewController(new HomeTimelineViewModel(), TimelineViewController.DisplayMode.MainTab) { Title = "friends.nico" },
                new TimelineViewController(new HomeTimelineViewModel(), TimelineViewController.DisplayMode.MainTab) { Title = "mstdn.jp" }
            };
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
    }
}
