using Mvc.Scripts.Models;

namespace Mvc.Scripts.Controllers
{
    public class RemoveTaskController: Controller
    {
        public override void Execute()
        {
            GetModel<TaskCreatorModel>().RemoveTask();
        }
    }
}