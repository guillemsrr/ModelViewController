using Mvc.Scripts.Models;

namespace Mvc.Scripts.Controllers
{
    public class RemoveTaskPressedController : Controller
    {
        public override void Execute()
        {
            GetModel<TaskCreatorModel>().OpenRemovePanel();
        }
    }
}