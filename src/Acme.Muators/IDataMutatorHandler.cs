namespace Acme.Muators
{
    public interface IDataMutatorHandler
    {
        void BeforeAdd(object newState, IDataMutatorContext ctx);

        void BeforeModify(object oldState, object newState, IDataMutatorContext ctx);

        void BeforeRemove(object oldState, IDataMutatorContext ctx);

        bool CanHandle(IDataMutatorContext type);
    }
}