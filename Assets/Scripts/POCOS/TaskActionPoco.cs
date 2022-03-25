using Mvc.Scripts.Views;

namespace Mvc.Scripts.POCOS
{
    public class TaskActionPoco
    {
        public string Title { get; }
        public string Description { get; }
        
        public TaskActionPoco(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}