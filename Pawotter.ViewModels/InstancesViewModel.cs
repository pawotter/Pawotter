using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using Pawotter.Core.Entities;
using SimpleInjector;
using Pawotter.API;
using Pawotter.Core.Exceptions;
using System.Reactive.Linq;

namespace Pawotter.ViewModels
{
    public class InstancesViewModel : BaseViewModel
    {
        static readonly IEnumerable<Uri> instanceUris = new[] {
            new Uri("https://friends.nico"),
            new Uri("https://mstdn.jp"),
            new Uri("https://pawoo.net"),
            new Uri("https://wug.fun"),
            new Uri("https://imastodon.net"),
        };

        readonly ReactiveProperty<IList<Instance>> instances = new ReactiveProperty<IList<Instance>>(new List<Instance>());

        public string Title => "Instances";
        public ReadOnlyReactiveProperty<IList<Instance>> Instances { get; }
        public int NumberOfRows => instances.Value.Count();

        public InstancesViewModel(Container container) : base(container)
        {
            Instances = instances.ToReadOnlyReactiveProperty();
        }

        public async void RefreshContent()
        {
            StartLoading();
            var xs = new List<Instance>();
            foreach (var uri in instanceUris)
            {
                try
                {
                    var api = container.GetInstance<IMastodonApiClient>();
                    var res = await api.GetInstanceAsync(uri);
                    xs.Add(res);
                }
                catch (Exception e)
                {
                    Logger.Info(e);
                }
            }
            if (!xs.Any()) exception.Value = new EmptyResultException();
            foreach (var x in xs) instances.Value = xs.ToList();
            isLoadComplete.Value = true;
            FinishLoading();
        }
    }
}
