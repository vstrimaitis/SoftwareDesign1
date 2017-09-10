namespace Strategy.Core.ReqA
{
    class TaskEditor
    {
        private Task _task;
        private ITaskRenderer _renderer;

        public string RenderedTask
        {
            get
            {
                return $"{_renderer.RenderTaskHeader(_task)}\n{_renderer.RenderTaskBody(_task)}";
            }
        }

        public TaskEditor(ITaskRenderer renderer, Task task)
        {
            _renderer = renderer;
            _task = task;
        }

        public void EditTask(Task newTask)
        {
            _task = newTask;
        }
    }
}
