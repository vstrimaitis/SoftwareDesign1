namespace Strategy.Core
{
    public interface ITaskRenderer
    {
        string RenderTaskHeader(Task t);
        string RenderTaskBody(Task t);
    }
}
