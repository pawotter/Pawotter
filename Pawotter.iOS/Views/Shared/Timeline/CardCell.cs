using System;
using UIKit;
using CoreGraphics;
using Foundation;
using CoreText;

namespace Pawotter.iOS
{
    public class CardCell : UICollectionViewCell
    {
        public static nfloat Height(string text, nfloat width) => NMath.Max(
            UIConst.DefaultPadding * 2 + UIConst.DefaultLineSpace + TextLabelHeight(text, width),
            UIConst.DefaultPadding * 2 + UIConst.IconSize.Height
        );
        static nfloat TextLabelWidth(nfloat width) => width - UIConst.IconSize.Width - UIConst.DefaultPadding * 3;
        static nfloat TextLabelHeight(string text, nfloat width) => UIConst.DefaultFont.Height(text, TextLabelWidth(width));

        readonly UIImageView userIconView = new UIImageView();
        readonly UILabel displayNameLabel = new UILabel();
        readonly UILabel usernameLabel = new UILabel();
        readonly UILabel postedAtLabel = new UILabel();
        readonly UILabel textLabel = new UILabel();

        public CardCell(IntPtr handle) : base(handle) { Initialize(); }
        public CardCell() { Initialize(); }

        void Initialize()
        {
            BackgroundColor = UIConst.CellBackgroundColor;
            userIconView.BackgroundColor = UIColor.LightGray;
            displayNameLabel.Font = UIConst.BoldFont;
            displayNameLabel.TextColor = UIConst.DefaultFontColor;
            usernameLabel.Font = UIConst.DefaultFont;
            usernameLabel.TextColor = UIConst.MetaDataFontColor;
            postedAtLabel.Font = UIConst.DefaultFont;
            postedAtLabel.TextColor = UIConst.MetaDataFontColor;
            textLabel.Font = UIConst.DefaultFont;
            textLabel.TextColor = UIConst.DefaultFontColor;
            textLabel.Lines = 0;
            ContentView.AddSubviews(new UIView[] {
                userIconView,
                displayNameLabel,
                usernameLabel,
                postedAtLabel,
                textLabel
            });
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            userIconView.Frame = new CGRect(UIConst.LayoutOrigin, UIConst.IconSize);
            var postedAtLabelW = postedAtLabel.Font.Width(postedAtLabel.Text);
            postedAtLabel.Frame = new CGRect(Frame.Width - UIConst.DefaultPadding - postedAtLabelW, userIconView.Frame.Top, postedAtLabelW, postedAtLabel.Font.LineHeight);
            var displayNameLabelW = Math.Min(
                displayNameLabel.Font.Width(displayNameLabel.Text),
                Frame.Width - UIConst.DefaultPadding * 4 - UIConst.IconSize.Width - postedAtLabel.Frame.Width
            );
            displayNameLabel.Frame = new CGRect(
                userIconView.Frame.Right + UIConst.DefaultPadding, userIconView.Frame.Top,
                displayNameLabelW, displayNameLabel.Font.LineHeight
            );
            var usernameLabelX = displayNameLabel.Frame.Right + usernameLabel.Font.LineHeight / 2;
            usernameLabel.Frame = new CGRect(
                usernameLabelX, displayNameLabel.Frame.Top,
                NMath.Max(postedAtLabel.Frame.Left - usernameLabelX, 0), usernameLabel.Font.LineHeight
            );
            textLabel.Frame = new CGRect(
                displayNameLabel.Frame.Left, displayNameLabel.Frame.Top + UIConst.DefaultLineSpace,
                TextLabelWidth(Bounds.Width), TextLabelHeight(textLabel.Text, Bounds.Width)
            );
        }

        public void Update(string displayName, string username, DateTime postedAt, string text)
        {
            textLabel.Text = text;
            postedAtLabel.Text = postedAt.ToShortTimeString();
            displayNameLabel.Text = displayName;
            usernameLabel.Text = $"@{username}";
        }
    }
}
