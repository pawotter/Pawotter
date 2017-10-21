using System;
using Pawotter.Core;

namespace Pawotter.ViewModels
{
    public class TimelineItemViewModel
    {
        public string DisplayName { get; } = "display name";
        public string Acct { get; } = "acct";
        public string CreatedAt { get; } = DateTime.Now.CreatedAtString();
        public string Content { get; } = "Content";
    }
}
