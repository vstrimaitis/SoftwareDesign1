namespace Strategy.Core
{
    class HtmlTaskRenderer : ITaskRenderer
    {
        public string RenderTaskBody(Task t)
        {
            return $"<p>Maximum score: <b>{t.MaxScore}</b></p>\n<p>{t.Description}</p>";
        }

        public string RenderTaskHeader(Task t)
        {
            return $"<h1>{t.Name}</h1>";
        }
    }
}
