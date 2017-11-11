using UIKit;
using CoreGraphics;
using Foundation;
using System;
using Pawotter.iOS.Views.Instance;
using Pawotter.ViewModels;

namespace Pawotter.iOS.Views
{
    public class InstancesViewController : BaseViewController, IUITableViewDelegate, IUITableViewDataSource
    {
        readonly InstancesViewModel viewModel;

        readonly UITableView table = new UITableView(CGRect.Empty, UITableViewStyle.Grouped);

        public InstancesViewController(InstancesViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = viewModel.Title;
            EdgesForExtendedLayout = UIRectEdge.None;
            table.RegisterClassForCellReuse(typeof(InstanceCell), nameof(InstanceCell));
            View.AddSubviews(table);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            table.Frame = View.Bounds;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            table.WeakDelegate = this;
            table.WeakDataSource = this;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            table.WeakDelegate = null;
            table.WeakDataSource = null;
        }

        [Export("tableView:heightForHeaderInSection:")]
        public nfloat GetHeightForHeader(UITableView tableView, nint section) => 0.1f;

        [Export("tableView:heightForRowAtIndexPath:")]
        public nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) => InstanceCell.H;

        [Export("tableView:heightForFooterInSection:")]
        public nfloat GetHeightForFooter(UITableView tableView, nint section) => 0.1f;

        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
        }

        [Export("tableView:numberOfRowsInSection:")]
        public nint RowsInSection(UITableView tableView, nint section) => 100;

        [Export("tableView:cellForRowAtIndexPath:")]
        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(nameof(InstanceCell), indexPath) as InstanceCell;
            return cell ?? new UITableViewCell();
        }
    }
}
