using Common.Core;
using System.Collections.Generic;
using TemplateMethod.Core.Helpers;

namespace TemplateMethod.Core
{
    public class CodeforcesHtmlContest : Contest
    {
        private const int ScorePenaltyPerWrongSubmission = 50;
        private const int UserColumnWidth = 8;
        private const int TaskColumnWidth = 16;
        private const int SumColumnWidth = 8;

        private CodeforcesHelper _helper = new CodeforcesHelper(ScorePenaltyPerWrongSubmission);

        public CodeforcesHtmlContest(IEnumerable<Participant> participants,
                                 IEnumerable<Task> tasks,
                                 Dictionary<Participant, Dictionary<Task, Result>> results)
            : base(participants, tasks, results)
        { }

        protected override int Compare(Participant p1, Dictionary<Task, Result> r1, Participant p2, Dictionary<Task, Result> r2)
        {
            return _helper.Compare(p1, r1, p2, r2);
        }

        protected override string RenderHeader(IEnumerable<Task> tasks)
        {
            return _helper.RenderHeader(tasks, UserColumnWidth, TaskColumnWidth, SumColumnWidth);
        }

        protected override string RenderParticipantEntry(Participant p, Dictionary<Task, Result> results)
        {
            return _helper.RenderParticipantEntry(p, results, UserColumnWidth, TaskColumnWidth, SumColumnWidth);
        }

        protected override string RenderScoreboard(string header, IEnumerable<string> bodyEntries)
        {
            return _helper.RenderScoreboard(header, bodyEntries);
        }

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
