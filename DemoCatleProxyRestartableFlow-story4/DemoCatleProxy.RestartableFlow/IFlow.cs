using System;
using System.Reflection.Metadata.Ecma335;

namespace DemoCatleProxy.RestartableFlow
{
    public interface IFlow
    {
        object UntypedModel { get; }
        void Execute();
        internal void SetModel(object model);
    }

    public interface IFlow<M> : IFlow
        where M : class
    {
        M Model { get; }
    }

    public abstract class Flow<M> : IFlow<M>
        where M : class, new()
    {
        protected M _model;

        public M Model => _model;

        public object UntypedModel => _model;

        public Flow()
        {
            _model = new M();
        }

        void IFlow.SetModel(object model)
        {
            _model = model as M;
        }

        public abstract void Execute();
    }
}
