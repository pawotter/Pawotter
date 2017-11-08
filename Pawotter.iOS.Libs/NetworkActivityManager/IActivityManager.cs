using System;
namespace Pawotter.iOS.Libs.NetworkActivityManager
{
    public interface IActivityManager
    {
        void Attach();

        void Detach();
    }
}
