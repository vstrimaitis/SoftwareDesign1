using Common.Core;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TemplateMethod.Core.Helpers
{
    public class CodeforcesHelper
    {
        private readonly int ScorePenaltyPerWrongSubmission;

        public CodeforcesHelper(int scorePenaltyPerWrongSubmission)
        {
            ScorePenaltyPerWrongSubmission = scorePenaltyPerWrongSubmission;
        }

        public int Compare(Participant p1, Dictionary<Task, Result> r1, Participant p2, Dictionary<Task, Result> r2)
        {
            double score1 = r1.Select(x => CalculateTaskScore(x.Key, x.Value)).Sum();
            double score2 = r2.Select(x => CalculateTaskScore(x.Key, x.Value)).Sum();
            return Math.Sign(score2 - score1);
        }

        public string RenderHeader(IEnumerable<Task> tasks, int userColumnWidth, int taskColumnWidth, int sumColumnWidth)
        {
            string result = "User".PadCenter(userColumnWidth);
            foreach (var t in tasks)
            {
                string tmp = string.Format("{0}({1})", t.Name, t.MaxScore);
                result += tmp.PadCenter(taskColumnWidth);
            }
            result += "Sum".PadCenter(sumColumnWidth);
            return result;
        }

        public string RenderParticipantEntry(Participant p, Dictionary<Task, Result> results, int userColumnWidth, int taskColumnWidth, int sumColumnWidth)
        {
            string result = p.Username.PadCenter(userColumnWidth);
            int totalScore = 0;
            foreach (var r in results)
            {
                int score = 0;
                if (r.Value.Solved)
                {
                    score = (int)CalculateTaskScore(r.Key, r.Value);
                    totalScore += score;
                }
                result += (r.Value.Solved ? score.ToString() : "---").PadCenter(taskColumnWidth);
            }
            result += totalScore.ToString().PadCenter(sumColumnWidth);
            return result;
        }

        public string RenderScoreboard(string header, IEnumerable<string> bodyEntries)
        {
            int len = header.Length;
            len = Math.Max(len, bodyEntries.Select(x => x.Length).Max());
            return string.Format("{0}\n{1}\n{2}", header, new string('-', len), string.Join("\n", bodyEntries));
        }

        private double CalculateTaskScore(Task task, Result result)
        {
            if (!result.Solved)
                return 0;
            double ScorePenaltyPerMinute = task.MaxScore / 250.0;
            return Math.Max(0, task.MaxScore
                                - result.WrongSubmissionCount * ScorePenaltyPerWrongSubmission
                                - ScorePenaltyPerMinute * result.Time);
        }
    }
}
