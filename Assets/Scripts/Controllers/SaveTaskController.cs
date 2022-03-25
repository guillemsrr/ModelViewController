using Mvc.Scripts.Models;
using Mvc.Scripts.POCOS;

namespace Mvc.Scripts.Controllers
{
    public class SaveTaskController: Controller<TaskActionPoco>
    {
        public override void Execute(TaskActionPoco taskActionPoco)
        {
            GetModel<TaskCreatorModel>().SaveTask(taskActionPoco);
        }
    }
}