using UIKit;
using Pawotter.Core.Consts;

namespace Pawotter.iOS.Views
{
    public abstract class BaseViewController : UIViewController
    {
    }

    public abstract class BaseNavigationController : UINavigationController
    {
        public BaseNavigationController(UIViewController rootViewController) : base(rootViewController)
        {
        }
    }

    public sealed class NavigationController : BaseNavigationController
    {
        public NavigationController(UIViewController rootViewController) : base(rootViewController)
        {
        }
    }

    public abstract class BaseView : UIView
    {

    }

    public abstract class BaseCollectionViewCell : UICollectionViewCell
    {
        public BaseCollectionViewCell() { }
        public BaseCollectionViewCell(System.IntPtr handle) : base(handle) { }
    }

    public sealed class Border : UIView
    {
        public Border()
        {
            BackgroundColor = ColorConsts.Border.Color();
        }
    }
}
