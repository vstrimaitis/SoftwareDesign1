using Common;
using Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using TemplateMethod.Core;
using TemplateMethod.Core.ReqA;

namespace TemplateMethod
{
    class Program
    {
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            TestHelper.RunIcpcScoreboardTest((p, t, r) => new IcpcContest(p.ToList(), t.ToList(), r).Scoreboard);
            TestHelper.PrintSeparator();
            TestHelper.RunCodeforcesScoreboardTest((p, t, r) => new CodeforcesContest(p.ToList(), t.ToList(), r).Scoreboard);
            //======================================================================================================================
            TestHelper.RunIcpcTaskRenderingTest((p, t, r) => new IcpcContest(p.ToList(), t.ToList(), r).RenderedTasks);
            TestHelper.PrintSeparator();
            TestHelper.RunCodeforcesTaskRenderingTest((p, t, r) => new CodeforcesContest(p.ToList(), t.ToList(), r).RenderedTasks);
            TestHelper.PrintSeparator();
            //======================================================================================================================
            Task oldTask = new Task("Old task", "This is the old, unedited task", 1500);
            Task newTask = new Task("New task", "This is the new and shiny task.", 2000);
            TaskEditor htmlEditor = new HtmlTaskEditor(oldTask);
            TaskEditor markdownEditor = new MarkdownTaskEditor(newTask);
            Console.WriteLine("HTML before:");
            Console.WriteLine(htmlEditor.RenderedTask);
            htmlEditor.EditTask(newTask);
            Console.WriteLine("HTML after:");
            Console.WriteLine(htmlEditor.RenderedTask);
            Console.WriteLine();
            Console.WriteLine("Markdown before:");
            Console.WriteLine(markdownEditor.RenderedTask);
            markdownEditor.EditTask(newTask);
            Console.WriteLine("Markdown after:");
            Console.WriteLine(markdownEditor.RenderedTask);
        }
    }
}
