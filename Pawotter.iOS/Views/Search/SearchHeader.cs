using System;
using Pawotter.Core.Consts;
using UIKit;
using Pawotter.iOS.Views.Components;

namespace Pawotter.iOS.Views.Search
{
    public class SearchHeader : UICollectionReusableView
    {
        readonly UserIcon userIcon = new UserIcon();
        readonly FollowButton followButton = new FollowButton();
        readonly Label acct = new Label { TextColor = ColorConsts.Inactive.Color() };
        readonly Label description = new Label();

        public SearchHeader() { CommonInit(); }
        public SearchHeader(IntPtr handle) : base(handle) { CommonInit(); }

        void CommonInit()
        {
            BackgroundColor = ColorConsts.Background.Color();
            AddSubviews(userIcon, followButton, acct, description);
        }
    }
}
