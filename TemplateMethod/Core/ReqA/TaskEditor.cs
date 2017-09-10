namespace TemplateMethod.Core.ReqA
{
    public abstract class TaskEditor
    {
        private Task _task;

        public string RenderedTask
        {
            get
            {
                return $"{RenderTaskHeader(_task)}\n{RenderTaskBody(_task)}";
            }
        }

        public TaskEditor(Task task)
        {
            _task = task;
        }

        public void EditTask(Task newTask)
        {
            _task = newTask;
        }

        protected abstract string RenderTaskHeader(Task t);
        protected abstract string RenderTaskBody(Task t);
    }
}
