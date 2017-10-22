using Pawotter.Core.Consts;
using UIKit;

namespace Pawotter.iOS.Views.Components
{
    public sealed class Border : UIView
    {
        public Border()
        {
            BackgroundColor = ColorConsts.Border.Color();
        }
    }
}
