using Pawotter.ViewModels;

namespace Pawotter.iOS.Views.Account
{
    public class AccountViewController : BaseViewController
    {
        readonly AccountViewModel viewModel;

        public AccountViewController(AccountViewModel viewModel)
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
