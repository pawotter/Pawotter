using Pawotter.ViewModels;
namespace Pawotter.iOS.Views.Timeline
{
    /// <summary>
    /// Detailed view for timeline item.
    /// </summary>
    public class TimelineItemViewController : BaseViewController
    {
        readonly TimelineItemViewModel viewModel;

        public TimelineItemViewController(TimelineItemViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = viewModel.Title;
        }
    }
}
