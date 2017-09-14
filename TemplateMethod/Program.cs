using Common;
using Common.Core;
using System;
using TemplateMethod.Core;
using TemplateMethod.Core.ReqA;

namespace TemplateMethod
{
    class Program
    {
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            TestHelper.PrintTestSuiteHeader(" Template method tests ", '>', '<');

            TestHelper.PrintTestHeader("Scoreboard test");
            TestHelper.RunIcpcScoreboardTest((p, t, r) => new IcpcMarkdownContest(p, t, r).Scoreboard);
            TestHelper.PrintSubtestSeparator();
            TestHelper.RunIcpcScoreboardTest((p, t, r) => new IcpcHtmlContest(p, t, r).Scoreboard);
            TestHelper.PrintSubtestSeparator();
            TestHelper.RunCodeforcesScoreboardTest((p, t, r) => new CodeforcesMarkdownContest(p, t, r).Scoreboard);
            TestHelper.PrintSubtestSeparator();
            TestHelper.RunCodeforcesScoreboardTest((p, t, r) => new CodeforcesHtmlContest(p, t, r).Scoreboard);
            //======================================================================================================================
            TestHelper.PrintTestHeader("Contest task rendering test");
            TestHelper.RunIcpcTaskRenderingTest((p, t, r) => new IcpcMarkdownContest(p, t, r).RenderedTasks);
            TestHelper.PrintSubtestSeparator();
            TestHelper.RunIcpcTaskRenderingTest((p, t, r) => new IcpcHtmlContest(p, t, r).RenderedTasks);
            TestHelper.PrintSubtestSeparator();
            TestHelper.RunCodeforcesTaskRenderingTest((p, t, r) => new CodeforcesMarkdownContest(p, t, r).RenderedTasks);
            TestHelper.PrintSubtestSeparator();
            TestHelper.RunCodeforcesTaskRenderingTest((p, t, r) => new CodeforcesHtmlContest(p, t, r).RenderedTasks);
            //======================================================================================================================
            TestHelper.PrintTestHeader("Task editor test");
            Task oldTask = new Task("Old task", "This is the old, unedited task", 1500);
            Task newTask = new Task("New task", "This is the new and shiny task.", 2000);
            TaskEditor htmlEditor = new HtmlTaskEditor(oldTask);
            TaskEditor markdownEditor = new MarkdownTaskEditor(oldTask);
            Console.WriteLine("HTML before:");
            Console.WriteLine(htmlEditor.RenderedTask);
            htmlEditor.EditTask(newTask);
            Console.WriteLine();
            Console.WriteLine("HTML after:");
            Console.WriteLine(htmlEditor.RenderedTask);
            TestHelper.PrintSubtestSeparator();
            Console.WriteLine("Markdown before:");
            Console.WriteLine(markdownEditor.RenderedTask);
            markdownEditor.EditTask(newTask);
            Console.WriteLine();
            Console.WriteLine("Markdown after:");
            Console.WriteLine(markdownEditor.RenderedTask);
        }
    }
}
