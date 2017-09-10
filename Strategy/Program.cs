using Common;
using Common.Core;
using Strategy.Core;
using Strategy.Core.ReqA;
using System;
using System.Collections.Generic;

namespace Strategy
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            TestHelper.PrintTestSuiteHeader(" Strategy tests ", '>', '<');
            IScoreboardGenerator icpcScoreboardGenerator = new IcpcScoreboardGenerator();
            IScoreboardGenerator cfScoreboardGenerator = new CodeforcesScoreboardGenerator();
            ITaskRenderer htmlTaskRenderer = new HtmlTaskRenderer();
            ITaskRenderer markdownTaskRenderer = new MarkdownTaskRenderer();

            TestHelper.PrintTestHeader("Scoreboard test");
            TestHelper.RunIcpcScoreboardTest((p, t, r) => CreateIcpcContest(p, t, r).Scoreboard);
            TestHelper.PrintSubtestSeparator();
            TestHelper.RunCodeforcesScoreboardTest((p, t, r) => CreateCodeforcesContest(p, t, r).Scoreboard);

            //======================================================================================================================
            TestHelper.PrintTestHeader("Contest task rendering test");
            TestHelper.RunIcpcTaskRenderingTest((p, t, r) => CreateIcpcContest(p, t, r).RenderedTasks);
            TestHelper.PrintSubtestSeparator();
            TestHelper.RunCodeforcesTaskRenderingTest((p, t, r) => CreateCodeforcesContest(p, t, r).RenderedTasks);

            //======================================================================================================================
            TestHelper.PrintTestHeader("Task editor test");
            Task oldTask = new Task("Old task", "This is the old, unedited task", 1500);
            Task newTask = new Task("New task", "This is the new and shiny task.", 2000);
            TaskEditor htmlEditor = new TaskEditor(htmlTaskRenderer, oldTask);
            TaskEditor markdownEditor = new TaskEditor(markdownTaskRenderer, oldTask);
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

        private static Contest CreateIcpcContest(IEnumerable<Participant> p, IEnumerable<Task> t, Dictionary<Participant, Dictionary<Task, Result>> r)
        {
            IScoreboardGenerator icpcScoreboardGenerator = new IcpcScoreboardGenerator();
            ITaskRenderer markdownTaskRenderer = new MarkdownTaskRenderer();
            return new Contest(icpcScoreboardGenerator, markdownTaskRenderer, p, t, r);
        }

        private static Contest CreateCodeforcesContest(IEnumerable<Participant> p, IEnumerable<Task> t, Dictionary<Participant, Dictionary<Task, Result>> r)
        {
            IScoreboardGenerator cfScoreboardGenerator = new CodeforcesScoreboardGenerator();
            ITaskRenderer htmlTaskRenderer = new HtmlTaskRenderer();
            return new Contest(cfScoreboardGenerator, htmlTaskRenderer, p, t, r);
        }
    }
}
