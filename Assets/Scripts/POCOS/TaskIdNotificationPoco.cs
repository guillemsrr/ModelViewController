using Mvc.Scripts.Views;

namespace Mvc.Scripts.POCOS
{
    public class TaskIdNotificationPoco
    {
        public ViewID ViewID { get; }
        public string Title { get; }
        public string Description { get; }
        
        public TaskIdNotificationPoco(int viewId, string title, string description)
        {
            ViewID = new ViewID(viewId);
            Title = title;
            Description = description;
        }
    }
}