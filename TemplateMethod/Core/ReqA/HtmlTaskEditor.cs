using Common.Core;

namespace TemplateMethod.Core.ReqA
{
    public class HtmlTaskEditor : TaskEditor
    {
        public HtmlTaskEditor(Task task) : base(task)
        { }

        protected override string RenderTaskBody(Task t)
        {
            return $"<p>Maximum score: <b>{t.MaxScore}</b></p>\n<p>{t.Description}</p>";
        }

        protected override string RenderTaskHeader(Task t)
        {
            return $"<h1>{t.Name}</h1>";
        }
    }
}
