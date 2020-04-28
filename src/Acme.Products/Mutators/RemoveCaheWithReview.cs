using Acme.Caching;
using Acme.Data.DataModels;
using Acme.Muators;
using Acme.Toolkit.Extensions;

namespace Acme.Products.Mutators
{
    public class RemoveCaheWithReview : IDataMutatorHandler
    {
        private readonly ICacheProvider _cacheProvider;

        public RemoveCaheWithReview(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }
        public void BeforeAdd(object newState, IDataMutatorContext context)
        {
            newState.ThrowIfNull(nameof(newState));
            ExpireCache(newState);
        }

        public void BeforeModify(object oldState, object newState, IDataMutatorContext context)
        {
            oldState.ThrowIfNull(nameof(oldState));
            ExpireCache(oldState);
        }

        public void BeforeRemove(object oldState, IDataMutatorContext context)
        {
            oldState.ThrowIfNull(nameof(oldState));
            ExpireCache(oldState);
        }

        public bool CanHandle(IDataMutatorContext ctx)
        {
            return ctx.DataSubjectType == typeof(ProductDataModel);
        }

        private void ExpireCache(object subject)
        {
            var s = subject as ProductDataModel;
            _cacheProvider.Expire($"productwithreview_{s.Id}");
        }
    }
}