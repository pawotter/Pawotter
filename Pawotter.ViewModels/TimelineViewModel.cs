namespace Pawotter.ViewModels
{
    public interface ITimelineViewModel
    {
        string Title { get; }
    }

    public abstract class BaseTimelineViewModel : ITimelineViewModel
    {
        public abstract string Title { get; }
    }

    public class HomeTimelineViewModel : BaseTimelineViewModel
    {
        public override string Title => "Home";
    }

    public class LocalTimelineViewModel : BaseTimelineViewModel
    {
        public override string Title => "Local";
    }

    public class FederatedTimelineViewModel : BaseTimelineViewModel
    {
        public override string Title => "Federated";
    }
}
