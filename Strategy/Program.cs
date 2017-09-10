using Common;
using Common.Core;
using Strategy.Core;
using Strategy.Core.ReqA;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Strategy
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            List<Task> icpcTasks = TestHelper.IcpcTasks.ToList();
            List<Participant> icpcParticipants = TestHelper.Participants.ToList();
            List<Task> cfTasks = TestHelper.CodeforcesTasks.ToList();
            List<Participant> cfParticipants = TestHelper.Participants.ToList();
            var icpcResults = TestHelper.IcpcResults;
            var cfResults = TestHelper.CodeforcesResults;

            IScoreboardGenerator icpcScoreboardGenerator = new IcpcScoreboardGenerator();
            IScoreboardGenerator cfScoreboardGenerator = new CodeforcesScoreboardGenerator();
            ITaskRenderer htmlTaskRenderer = new HtmlTaskRenderer();
            ITaskRenderer markdownTaskRenderer = new MarkdownTaskRenderer();

            Contest icpcContest = new Contest(icpcScoreboardGenerator, markdownTaskRenderer,icpcParticipants, icpcTasks, icpcResults);
            Contest cfContest = new Contest(cfScoreboardGenerator, htmlTaskRenderer, cfParticipants, cfTasks, cfResults);

            Console.WriteLine("ICPC contest scoreboard: ");
            Console.WriteLine(icpcContest.Scoreboard);
            Console.WriteLine("");
            Console.WriteLine(new string('*', Console.WindowWidth - 1));
            Console.WriteLine("");
            Console.WriteLine("Codeforces contest scoreboard: ");
            Console.WriteLine(cfContest.Scoreboard);

            //======================================================================================================================
            string allIcpcTasks = string.Join("\n------------------------------\n", icpcContest.RenderedTasks);
            string allCfTasks = string.Join("\n------------------------------\n", cfContest.RenderedTasks);

            Console.WriteLine(allIcpcTasks);
            Console.WriteLine("");
            Console.WriteLine(new string('*', Console.WindowWidth - 1));
            Console.WriteLine("");
            Console.WriteLine(allCfTasks);

            //======================================================================================================================
            Console.WriteLine("");
            Console.WriteLine(new string('*', Console.WindowWidth - 1));
            Console.WriteLine("");
            Task oldTask = new Task("Old task", "This is the old, unedited task", 1500);
            Task newTask = new Task("New task", "This is the new and shiny task.", 2000);
            TaskEditor htmlEditor = new TaskEditor(htmlTaskRenderer, oldTask);
            TaskEditor markdownEditor = new TaskEditor(markdownTaskRenderer, newTask);
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
