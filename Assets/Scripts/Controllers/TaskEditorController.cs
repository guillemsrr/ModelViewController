using Mvc.Scripts.Models;
using Mvc.Scripts.POCOS;
using Mvc.Scripts.Views;

namespace Mvc.Scripts.Controllers
{
    public class TaskEditorController: Controller<ViewID>
    {
        public override void Execute(ViewID taskPoco)
        {
            GetModel<TaskCreatorModel>().EditTask(taskPoco);
        }
    }
}