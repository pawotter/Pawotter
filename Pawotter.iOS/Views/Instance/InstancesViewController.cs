using UIKit;
using CoreGraphics;
using Foundation;
using System;
using Pawotter.iOS.Views.Instance;
using Pawotter.ViewModels;
using System.Linq;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;

namespace Pawotter.iOS.Views
{
    public class InstancesViewController : BaseViewController, IUITableViewDelegate, IUITableViewDataSource
    {
        readonly InstancesViewModel viewModel;

        readonly UITableView table = new UITableView(CGRect.Empty, UITableViewStyle.Grouped);
        readonly UIRefreshControl refreshControl = new UIRefreshControl();

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
            table.RefreshControl = refreshControl;
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
            BindFromUI();
            BindFromViewModel();
            viewModel.RefreshContent();
        }

        void BindFromUI()
        {
            viewModel.Instances
                     .Subscribe(_ => table.ReloadData())
                     .AddTo(disposeBag);
            viewModel.IsLoading
                     .Where(x => !x)
                     .Subscribe(_ => refreshControl.EndRefreshing())
                     .AddTo(disposeBag);
        }

        void BindFromViewModel()
        {
            Observable.FromEventPattern(x => refreshControl.ValueChanged += x, x => refreshControl.ValueChanged -= x)
                      .Subscribe(_ => viewModel.RefreshContent())
                      .AddTo(disposeBag);
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
        public nint RowsInSection(UITableView tableView, nint section) => viewModel.NumberOfRows;

        [Export("tableView:cellForRowAtIndexPath:")]
        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(nameof(InstanceCell), indexPath) as InstanceCell;
            cell.Update(viewModel.Instances.Value.ElementAtOrDefault(indexPath.Row));
            return cell ?? new UITableViewCell();
        }
    }
}
