using System;
using System.Threading;

namespace Pawotter.iOS.Libs.NetworkActivityManager
{
    /// <summary>
    /// If any activity exists, given indicator will be activated.
    /// </summary>
    sealed class AnyActivityIndicatorManager : IActivityManager
    {
        /// <summary>
        /// activity count
        /// </summary>
        int count;

        readonly IIndicator indicator;
        readonly Object thisObject = new Object();

        internal AnyActivityIndicatorManager(IIndicator indicator)
        {
            this.indicator = indicator;
        }

        /// <summary>
        /// Increment activity count. When count equals 0, indicator will be activated.
        /// </summary>
        void IActivityManager.Attach()
        {
            lock (thisObject)
            {
                if (count == 0) indicator.Activate();
                Interlocked.Increment(ref count);
            }
        }

        /// <summary>
        /// Decrement activity count. When count equals 1, indicator will be inactivated.
        /// </summary>
        void IActivityManager.Detach()
        {
            lock (thisObject)
            {
                if (count == 1) indicator.Inactivate();
                if (count > 0) Interlocked.Decrement(ref count);
            }
        }
    }
}
