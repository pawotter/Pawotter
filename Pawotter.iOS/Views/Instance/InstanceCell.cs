using System;
using CoreGraphics;
using Pawotter.iOS.Views.Components;
using UIKit;

namespace Pawotter.iOS.Views.Instance
{
    public class InstanceCell : BaseTableViewViewCell
    {
        public static nfloat H => L.SmallUserIcon.Height.PlusDoublePadding();

        readonly UserIcon instanceIcon = new UserIcon();
        readonly Label instanceName = new Label { Font = L.BoldFont, Text = "DisplayName" };
        readonly MetadataLabel acct = new MetadataLabel { Text = "userid@instance" };

        public InstanceCell() { CommonInit(); }
        public InstanceCell(IntPtr handle) : base(handle) { CommonInit(); }

        void CommonInit()
        {
            instanceIcon.BackgroundColor = UIColor.Orange;
            AddSubviews(instanceIcon, instanceName, acct);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            var textW = this.Width() - 100;
            instanceIcon.Frame = new CGRect(L.PaddingL, L.PaddingL, L.SmallUserIcon.Width, L.SmallUserIcon.Height);
            instanceName.Frame = new CGRect(instanceIcon.MaxX().PlusPadding(), instanceIcon.MinY(), textW, L.NormalFont.LineHeight);
            acct.Frame = new CGRect(instanceName.X(), instanceName.MaxY(), textW, L.NormalFont.LineHeight);
        }

        public void Update(Core.Entities.Instance instance)
        {
            instanceName.Text = instance?.Title;
        }
    }
}
