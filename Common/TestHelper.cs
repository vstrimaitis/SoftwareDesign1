using Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class TestHelper
    {
        private static Random random = new Random();

        public static IEnumerable<Participant> Participants
        {
            get
            {
                yield return new Participant("user1") ;
                yield return new Participant("user2") ;
                yield return new Participant("user3") ;
                yield return new Participant("user4") ;
                yield return new Participant("user5") ;
                yield return new Participant("user6") ;
                yield return new Participant("user7") ;
                yield return new Participant("user8") ;
                yield return new Participant("user9") ;
                yield return new Participant("user10");
            }
        }

        public static IEnumerable<Task> IcpcTasks
        {
            get
            {
                yield return new Task("A", "Description A", 1);
                yield return new Task("B", "Description B", 1);
                yield return new Task("C", "Description C", 1);
                yield return new Task("D", "Description D", 1);
                yield return new Task("E", "Description E", 1);
            }
        }

        public static IEnumerable<Task> CodeforcesTasks
        {
            get
            {
                yield return new Task("A", "Description A", 500);
                yield return new Task("B", "Description B", 1000);
                yield return new Task("C", "Description C", 1500);
                yield return new Task("D", "Description D", 2000);
                yield return new Task("E", "Description E", 2500);
            }
        }

        public static Dictionary<Participant, Dictionary<Task, Result>> IcpcResults
        {
            get
            {
                return Participants.ToDictionary(
                                    p => p,
                                    p => IcpcTasks.ToDictionary(t => t, t => GenerateResult(t)));
            }
        }

        public static Dictionary<Participant, Dictionary<Task, Result>> CodeforcesResults
        {
            get
            {
                return Participants.ToDictionary(
                                    p => p,
                                    p => CodeforcesTasks.ToDictionary(t => t, t => GenerateResult(t)));
            }
        }

        public static void RunIcpcScoreboardTest(Func<IEnumerable<Participant>,
                                                        IEnumerable<Task>,
                                                        Dictionary<Participant, Dictionary<Task, Result>>,
                                                        string
                                                     > scoreboardGenerator)
        {
            Console.WriteLine("ICPC contest scoreboard:");
            Console.WriteLine(scoreboardGenerator(Participants, IcpcTasks, IcpcResults));
        }

        public static void RunCodeforcesScoreboardTest(Func<IEnumerable<Participant>,
                                                            IEnumerable<Task>,
                                                            Dictionary<Participant, Dictionary<Task, Result>>,
                                                            string
                                                         > scoreboardGenerator)
        {
            Console.WriteLine("Codeforces contest scoreboard:");
            Console.WriteLine(scoreboardGenerator(Participants, CodeforcesTasks, CodeforcesResults));
        }

        public static void RunIcpcTaskRenderingTest(Func<IEnumerable<Participant>,
                                                            IEnumerable<Task>,
                                                            Dictionary<Participant, Dictionary<Task, Result>>,
                                                            IEnumerable<string>
                                                         > taskGenerator)
        {
            string tasks = string.Join("\n------------------------------\n", taskGenerator(Participants, IcpcTasks, IcpcResults));
            Console.WriteLine(tasks);
        }

        public static void RunCodeforcesTaskRenderingTest(Func< IEnumerable<Participant>,
                                                                IEnumerable<Task>,
                                                                Dictionary<Participant, Dictionary<Task, Result>>,
                                                                IEnumerable<string>
                                                             > taskGenerator)
        {
            string tasks = string.Join("\n------------------------------\n", taskGenerator(Participants, CodeforcesTasks, CodeforcesResults));
            Console.WriteLine(tasks);
        }

        public static void PrintSeparator()
        {
            Console.WriteLine("");
            Console.WriteLine(new string('*', Console.WindowWidth - 1));
            Console.WriteLine("");
        }

        static Result GenerateResult(Task task)
        {
            bool isSolved = random.Next(2) == 1;
            int time = random.Next(121);
            int wrongSubmissionCount = random.Next(20);
            return new Result(isSolved, time, wrongSubmissionCount);
        }
    }
}
