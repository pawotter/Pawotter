using UIKit;
using CoreGraphics;
using Foundation;
using System;
using Pawotter.iOS.Views.Account;
using Pawotter.ViewModels;

namespace Pawotter.iOS.Views.Others
{
    public sealed class OthersViewController : BaseViewController, IUITableViewDelegate, IUITableViewDataSource, IScrollableViewController
    {
        UITableView tableView = new UITableView(CGRect.Empty, UITableViewStyle.Grouped);

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            EdgesForExtendedLayout = UIRectEdge.None;
            tableView.WeakDelegate = this;
            tableView.WeakDataSource = this;
            tableView.AlwaysBounceVertical = true;
            tableView.CellLayoutMarginsFollowReadableWidth = false;
            tableView.RegisterClassForCellReuse(typeof(UITableViewCell), nameof(UITableViewCell));
            View.AddSubviews(tableView);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            tableView.Frame = View.Bounds;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            TabBarController.Title = "Others";
        }

        #region IUITableViewDataSource
        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(nameof(UITableViewCell), indexPath) as UITableViewCell;
            cell.TextLabel.Text = indexPath.Row.ToString();
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            return cell;
        }

        public nint RowsInSection(UITableView tableview, nint section) => 10;

        [Export("tableView:heightForRowAtIndexPath:")]
        public nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) => L.Banner;
        #endregion

        #region IUITableViewDelegate
        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            Application.Router.PushViewController(new AccountsViewController(new AccountsViewModel()), true);
            tableView.DeselectRow(indexPath, true);
        }

        [Export("tableView:heightForHeaderInSection:")]
        public nfloat GetHeightForHeader(UITableView tableView, nint section) => 0.1f;


        [Export("tableView:heightForFooterInSection:")]
        public nfloat GetHeightForFooter(UITableView tableView, nint section) => 0.1f;
        #endregion

        public void ScrollsToTop() => tableView.SetContentOffset(CGPoint.Empty, true);
    }
}
