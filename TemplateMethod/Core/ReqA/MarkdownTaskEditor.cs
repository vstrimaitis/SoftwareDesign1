namespace TemplateMethod.Core.ReqA
{
    class MarkdownTaskEditor : TaskEditor
    {
        public MarkdownTaskEditor(Task task) : base(task)
        { }

        protected override string RenderTaskBody(Task t)
        {
            return $"> Maximum score: {t.MaxScore} points\n> {t.Description}";
        }

        protected override string RenderTaskHeader(Task t)
        {
            return $"# {t.Name}";
        }
    }
}
