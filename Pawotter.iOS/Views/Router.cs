using System;
using System.Linq;
using UIKit;

namespace Pawotter.iOS.Views
{
    public interface IRouter
    {
        void PresentViewController(UIViewController viewController, bool animated, Action completionHandler);

        void PushViewController(UIViewController viewController, bool animated);

        void DismissViewController(bool animated, Action completionHandler);
    }

    public class Router : IRouter
    {
        public void PresentViewController(UIViewController viewController, bool animated, Action completionHandler)
        {
            var currentVc = CurrentViewController;
            currentVc.PresentViewController(viewController, animated, completionHandler);
        }

        public void PushViewController(UIViewController viewController, bool animated)
        {
            var currentVc = CurrentViewController;
            currentVc?.NavigationController?.PushViewController(viewController, animated);
        }

        public void DismissViewController(bool animated, Action completionHandler)
        {
            var currentVc = CurrentViewController;
            currentVc?.DismissViewController(animated, completionHandler);
        }

        UIViewController CurrentViewController
        {
            get
            {
                var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
                while (true)
                {
                    switch (vc)
                    {
                        case UINavigationController nav:
                            var last = vc.ChildViewControllers.LastOrDefault();
                            if (last == null) break;
                            vc = last;
                            continue;
                        case UITabBarController tab:
                            var selected = tab.ViewControllers.ElementAtOrDefault((int) tab.SelectedIndex);
                            if (selected == null) break;
                            vc = selected;
                            continue;
                        case UIViewController c when c.PresentedViewController != null:
                            vc = c;
                            continue;
                    }
                    break;
                }
                return vc;
            }
        }
    }
}

