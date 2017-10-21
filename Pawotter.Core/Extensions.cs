using System;

namespace Pawotter.Core
{
    public static class Extensions
    {
        public static string CreatedAtString(this DateTime dateTime) => dateTime.ToString("yyyy/MM/dd HH:mm");
    }
}
