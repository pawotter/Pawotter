using Pawotter.ViewModels;

namespace Pawotter.iOS.Views.Timeline
{
    public sealed class StatusEditorViewController : BaseViewController
    {
        readonly StatusEditorViewModel viewModel;

        public StatusEditorViewController(StatusEditorViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
    }
}
