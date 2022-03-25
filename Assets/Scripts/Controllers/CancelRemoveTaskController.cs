using Mvc.Scripts.Models;

namespace Mvc.Scripts.Controllers
{
    public class CancelRemoveTaskController: Controller
    {
        public override void Execute()
        {
            GetModel<TaskCreatorModel>().CancelRemoveTask();
        }
    }
}