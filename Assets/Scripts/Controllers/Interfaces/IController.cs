namespace Mvc.Scripts.Controllers.Interfaces
{
    public interface IController<T>
    {
        void Execute(T taskPoco);
    }

    public interface IController
    {
        void Execute();
    }
}