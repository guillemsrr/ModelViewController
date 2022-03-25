using Mvc.Scripts.Models.Interfaces;

namespace Mvc.Scripts.Controllers.Interfaces
{
    public interface IGetModel
    {
        T GetModel<T>(string id = null) where T : class, IModel;
    }
}