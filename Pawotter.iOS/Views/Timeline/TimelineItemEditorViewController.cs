using Pawotter.ViewModels;

namespace Pawotter.iOS.Views.Timeline
{
    public sealed class TimelineItemEditorViewController : BaseViewController
    {
        readonly TimelineItemEditorViewModel viewModel;

        public TimelineItemEditorViewController(TimelineItemEditorViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
    }
}
