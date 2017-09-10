using System;
using System.Collections.Generic;
using System.Linq;
using TemplateMethod.Extensions;

namespace TemplateMethod.Core
{
    class IcpcContest : Contest
    {
        private const int TimePenaltyPerSubmission = 20; // Number of minutes of penalty per wrong submission
        private const int UserColumnWidth = 8;
        private const int TaskColumnWidth = 16;
        private const int SumColumnWidth = 8;
        private const int PenaltyColumnWidth = 8;

        public IcpcContest(List<Participant> participants,
                           List<Task> tasks,
                           Dictionary<Participant, Dictionary<Task, Result>> results)
            : base(participants, tasks, results)
        { }

        protected override int Compare(Participant p1, Participant p2)
        {
            IEnumerable<Result> solved1 = Results[p1].Select(x => x.Value).Where(x => x.Solved);
            IEnumerable<Result> solved2 = Results[p2].Select(x => x.Value).Where(x => x.Solved);
            int solvedCount1 = solved1.Where(x => x.Solved).Count();
            int solvedCount2 = solved2.Where(x => x.Solved).Count();
            if(solvedCount1 != solvedCount2)
            {
                return solvedCount2 - solvedCount1;
            }
            int penaltyTime1 = solved1.Select(x => x.Time + x.WrongSubmissionCount * TimePenaltyPerSubmission)
                                      .Sum();
            int penaltyTime2 = solved2.Select(x => x.Time + x.WrongSubmissionCount * TimePenaltyPerSubmission)
                                      .Sum();
            return penaltyTime1 - penaltyTime2;
        }

        protected override string RenderHeader()
        {
            string tasks = "";
            foreach(var t in Tasks)
            {
                tasks += t.Name.PadCenter(TaskColumnWidth);
            }

            return string.Format("{0}{1}{2}{3}",
                                "User".PadCenter(UserColumnWidth), tasks, "Sum".PadCenter(SumColumnWidth), "Penalty".PadCenter(PenaltyColumnWidth));
        }

        protected override string RenderParticipantEntry(Participant p)
        {
            string result = p.Username.PadCenter(UserColumnWidth);
            foreach (var r in Results[p].Values)
            {
                string tries = "-";
                string tmpResult;
                if (r.Solved)
                {
                    tries = string.Format("+{0:00}", r.WrongSubmissionCount);
                }
                int hrs = r.Time / 60;
                int mins = r.Time - hrs * 60;
                string time = string.Format("{0:00}:{1:00}", hrs, mins);
                if (r.Solved)
                    tmpResult = string.Format("{0} ({1})", tries, time);
                else
                    tmpResult = string.Format("{0}", tries);
                result += tmpResult.PadCenter(TaskColumnWidth);
            }
            
            result += string.Format("{0}{1}",
                                Results[p].Values.Where(x => x.Solved).Count().ToString().PadCenter(SumColumnWidth),
                                Results[p].Values.Select(x => x.Time + x.WrongSubmissionCount*TimePenaltyPerSubmission).Sum().ToString().PadCenter(PenaltyColumnWidth));

            return result;
        }

        protected override string RenderScoreboard(string header, IEnumerable<string> bodyEntries)
        {
            int len = header.Length;
            len = Math.Max(len, bodyEntries.Select(x => x.Length).Max());
            return string.Format("{0}\n{1}\n{2}", header, new string('-', len), string.Join("\n", bodyEntries));
        }

        protected override string RenderTaskBody(Task t)
        {
            return $"{t.Description.Replace(' ', '_')}";
        }

        protected override string RenderTaskHeader(Task t)
        {
            return $"Task name: \"{t.Name}\"";
        }
    }
}
