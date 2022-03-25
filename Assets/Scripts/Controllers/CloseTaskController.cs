using Mvc.Scripts.Models;

namespace Mvc.Scripts.Controllers
{
    public class CloseTaskController: Controller
    {
        public override void Execute()
        {
            GetModel<TaskCreatorModel>().CloseTask();
        }
    }
}