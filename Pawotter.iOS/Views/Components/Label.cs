using UIKit;
using Pawotter.Core.Consts;

namespace Pawotter.iOS.Views.Components
{
    public class Label : UILabel
    {
        public Label()
        {
            TextColor = ColorConsts.Text.Color();
            Font = L.NormalFont;
            LineBreakMode = UILineBreakMode.WordWrap;
        }
    }
}
