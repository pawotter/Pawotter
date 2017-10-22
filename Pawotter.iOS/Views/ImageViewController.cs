using UIKit;
using CoreGraphics;

namespace Pawotter.iOS.Views
{
    public class ImageViewController : BaseViewController
    {
        readonly UIScrollView scrollView = new UIScrollView();
        readonly UIImageView imageView = new UIImageView();

        public ImageViewController()
        {
            ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
            ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            scrollView.AddSubviews(imageView);
            View.AddSubviews(scrollView);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            scrollView.Frame = View.Bounds;
            imageView.Frame = new CGRect();
        }
    }
}
