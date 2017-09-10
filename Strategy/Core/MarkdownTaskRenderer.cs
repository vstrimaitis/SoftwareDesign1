namespace Strategy.Core
{
    class MarkdownTaskRenderer : ITaskRenderer
    {
        public string RenderTaskBody(Task t)
        {
            return $"> Maximum score: {t.MaxScore} points\n> {t.Description}";
        }

        public string RenderTaskHeader(Task t)
        {
            return $"# {t.Name}";
        }
    }
}
