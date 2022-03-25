using Mvc.Scripts.Models;

namespace Mvc.Scripts.Controllers
{
    public class AddTaskController: Controller
    {
        public override void Execute()
        {
            GetModel<TaskCreatorModel>().OpenTaskCreator();
        }
    }
}