using System;
using Pawotter.Core.Consts;
using UIKit;

namespace Pawotter.iOS.Views.Account
{
    public class AccountHeader : UICollectionReusableView
    {
        public AccountHeader() { CommonInit(); }
        public AccountHeader(IntPtr handle) : base(handle) { CommonInit(); }

        void CommonInit()
        {
            BackgroundColor = ColorConsts.Background.Color();
        }
    }
}
