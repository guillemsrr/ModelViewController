using Mvc.Scripts.Contexts;
using Mvc.Scripts.Controllers.Interfaces;
using Mvc.Scripts.Models.Interfaces;

namespace Mvc.Scripts.Controllers
{
    public abstract class Controller : IController, IGetModel
    {
        public abstract void Execute();
        
        public T GetModel<T>(string id = null) where T : class, IModel
        {
            return Context.Instance.GetModel<T>(id);
        }
    }
    
    public abstract class Controller<U> : IController<U>, IGetModel
    {
        public abstract void Execute(U taskPoco);
        
        public T GetModel<T>(string id = null) where T : class, IModel
        {
            return Context.Instance.GetModel<T>(id);
        }
    }
}