using System;
using System.Collections.Generic;
using System.Linq;
using TemplateMethod.Extensions;

namespace TemplateMethod.Core
{
    class CodeforcesContest : Contest
    {
        private const int ScorePenaltyPerWrongSubmission = 50;
        private const int UserColumnWidth = 8;
        private const int TaskColumnWidth = 16;
        private const int SumColumnWidth = 8;

        public CodeforcesContest(List<Participant> participants,
                                 List<Task> tasks,
                                 Dictionary<Participant, Dictionary<Task, Result>> results)
            : base(participants, tasks, results)
        { }

        protected override int Compare(Participant p1, Dictionary<Task, Result> r1, Participant p2, Dictionary<Task, Result> r2)
        {
            double score1 = r1.Select(x => CalculateTaskScore(x.Key, x.Value)).Sum();
            double score2 = r2.Select(x => CalculateTaskScore(x.Key, x.Value)).Sum();
            return Math.Sign(score2 - score1);
        }

        protected override string RenderHeader(IEnumerable<Task> tasks)
        {
            string result = "User".PadCenter(UserColumnWidth);
            foreach (var t in tasks)
            {
                string tmp = string.Format("{0}({1})", t.Name, t.MaxScore);
                result += tmp.PadCenter(TaskColumnWidth);
            }
            result += "Sum".PadCenter(SumColumnWidth);
            return result;
        }

        protected override string RenderParticipantEntry(Participant p, Dictionary<Task, Result> results)
        {
            string result = p.Username.PadCenter(UserColumnWidth);
            int totalScore = 0;
            foreach (var r in results)
            {
                int score = 0;
                if (r.Value.Solved)
                {
                    score = (int)CalculateTaskScore(r.Key, r.Value);
                    totalScore += score;
                }
                result += (r.Value.Solved ? score.ToString() : "---").PadCenter(TaskColumnWidth);
            }
            result += totalScore.ToString().PadCenter(SumColumnWidth);
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
            return t.Description;
        }

        protected override string RenderTaskHeader(Task t)
        {
            return $"Task name: {t.Name} ({t.MaxScore} pts.)";
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
