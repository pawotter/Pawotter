using UIKit;
using Pawotter.ViewModels;
using CoreGraphics;
using System;

namespace Pawotter.iOS.Views.Timeline
{
    public sealed class TimelineItemHeader : BaseView
    {
        public static nfloat H => L.LineSpace;

        readonly UILabel headline = new UILabel();

        public TimelineItemHeader()
        {
            AddSubviews(headline);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            headline.Frame = new CGRect(0, 0, this.Width(), L.LineSpace);
        }

        public void Update(TimelineItemViewModel viewModel)
        {
            headline.Text = viewModel.DisplayName;
            headline.SizeToFit();
        }
    }
}
