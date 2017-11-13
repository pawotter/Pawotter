using System;
using SimpleInjector;
using Pawotter.Core.Logger;
using Reactive.Bindings;

namespace Pawotter.ViewModels
{
    public abstract class BaseViewModel
    {
        protected readonly Container container;
        protected ILogger Logger => container.GetInstance<ILogger>();
        protected readonly ReactiveProperty<Exception> exception = new ReactiveProperty<Exception>();
        protected readonly ReactiveProperty<bool> isLoading = new ReactiveProperty<bool>(false);
        protected readonly ReactiveProperty<bool> isLoadComplete = new ReactiveProperty<bool>(false);

        public ReadOnlyReactiveProperty<Exception> Exception;
        public ReadOnlyReactiveProperty<bool> IsLoading;
        public ReadOnlyReactiveProperty<bool> IsLoadComplete;

        public BaseViewModel(Container container)
        {
            this.container = container;
            this.Exception = exception.ToReadOnlyReactiveProperty();
            this.IsLoading = isLoading.ToReadOnlyReactiveProperty();
            this.IsLoadComplete = isLoadComplete.ToReadOnlyReactiveProperty();
        }

        protected void StartLoading() => isLoading.Value = true;
        protected void FinishLoading() => isLoading.Value = false;
    }
}
