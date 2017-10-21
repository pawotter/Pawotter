using CoreGraphics;
using Pawotter.ViewModels;

namespace Pawotter.iOS.Views.Timeline
{
    public sealed class TimelineCell : BaseCollectionViewCell
    {
        readonly TimelineItemHeader header = new TimelineItemHeader();

        public TimelineCell() { CommonInit(); }
        public TimelineCell(System.IntPtr handle) : base(handle) { CommonInit(); }

        void CommonInit()
        {
            ContentView.AddSubviews(header);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            header.Frame = new CGRect(L.PaddingM, L.PaddingS, this.Width().WithPadding(), TimelineItemHeader.H);
        }

        public void Update(TimelineItemViewModel viewModel)
        {
            header.Update(viewModel);
            SetNeedsLayout();
        }
    }
}
