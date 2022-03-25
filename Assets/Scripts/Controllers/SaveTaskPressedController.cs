using Mvc.Scripts.Models;

namespace Mvc.Scripts.Controllers
{
    public class SaveTaskPressedController: Controller
    {
        public override void Execute()
        {
            GetModel<TaskCreatorModel>().SaveTaskPressed();
        }
    }
}