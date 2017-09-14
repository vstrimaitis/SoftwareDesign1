using Common.Core;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TemplateMethod.Core.Helpers
{
    public class IcpcHelper
    {
        private readonly int TimePenaltyPerSubmission;

        public IcpcHelper(int timePenaltyPerSubmission)
        {
            TimePenaltyPerSubmission = timePenaltyPerSubmission;
        }

        public int Compare(Participant p1, Dictionary<Task, Result> r1, Participant p2, Dictionary<Task, Result> r2)
        {
            IEnumerable<Result> solved1 = r1.Select(x => x.Value).Where(x => x.Solved);
            IEnumerable<Result> solved2 = r2.Select(x => x.Value).Where(x => x.Solved);
            int solvedCount1 = solved1.Where(x => x.Solved).Count();
            int solvedCount2 = solved2.Where(x => x.Solved).Count();
            if (solvedCount1 != solvedCount2)
            {
                return solvedCount2 - solvedCount1;
            }
            int penaltyTime1 = solved1.Select(x => x.Time + x.WrongSubmissionCount * TimePenaltyPerSubmission)
                                      .Sum();
            int penaltyTime2 = solved2.Select(x => x.Time + x.WrongSubmissionCount * TimePenaltyPerSubmission)
                                      .Sum();
            return penaltyTime1 - penaltyTime2;
        }

        public string RenderHeader(IEnumerable<Task> tasks, int taskColumnWidth, int userColumnWidth, int sumColumnWidth, int penaltyColumnWidth)
        {
            string renderedTasks = "";
            foreach (var t in tasks)
            {
                renderedTasks += t.Name.PadCenter(taskColumnWidth);
            }

            return string.Format("{0}{1}{2}{3}",
                                "User".PadCenter(userColumnWidth), renderedTasks, "Sum".PadCenter(sumColumnWidth), "Penalty".PadCenter(penaltyColumnWidth));
        }

        public string RenderParticipantEntry(Participant p, Dictionary<Task, Result> results, int userColumnWidth, int taskColumnWidth, int sumColumnWidth, int penaltyColumnWidth)
        {
            string result = p.Username.PadCenter(userColumnWidth);
            foreach (var r in results.Values)
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
                result += tmpResult.PadCenter(taskColumnWidth);
            }

            result += string.Format("{0}{1}",
                                results.Values.Where(x => x.Solved).Count().ToString().PadCenter(sumColumnWidth),
                                results.Values.Select(x => x.Time + x.WrongSubmissionCount * TimePenaltyPerSubmission).Sum().ToString().PadCenter(penaltyColumnWidth));

            return result;
        }

        public string RenderScoreboard(string header, IEnumerable<string> bodyEntries)
        {
            int len = header.Length;
            len = Math.Max(len, bodyEntries.Select(x => x.Length).Max());
            return string.Format("{0}\n{1}\n{2}", header, new string('-', len), string.Join("\n", bodyEntries));
        }
    }
}
