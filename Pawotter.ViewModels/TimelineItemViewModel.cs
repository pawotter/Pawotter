using System;
using Pawotter.Core;

namespace Pawotter.ViewModels
{
    public class TimelineItemViewModel
    {
        public string Title { get; } = "Toot";
        public string DisplayName { get; } = "display name";
        public string Acct { get; } = "acct";
        public string Status { get; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
        public string CreatedAt { get; } = DateTime.Now.CreatedAtString();
        public string Content { get; } = "Content";
    }
}
