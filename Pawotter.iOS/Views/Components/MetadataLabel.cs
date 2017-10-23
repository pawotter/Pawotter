using Pawotter.Core.Consts;
using UIKit;

namespace Pawotter.iOS.Views.Components
{
    public class MetadataLabel : UILabel
    {
        public MetadataLabel()
        {
            TextColor = ColorConsts.Inactive.Color();
            Font = L.NormalFont;
            LineBreakMode = UILineBreakMode.WordWrap;
        }
    }
}
