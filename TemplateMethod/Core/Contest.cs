using System.Collections.Generic;
using System.Linq;

namespace TemplateMethod.Core
{
    public abstract class Contest
    {
        public List<Participant> Participants { get; private set; }
        public List<Task> Tasks { get; private set; }
        public Dictionary<Participant, Dictionary<Task, Result>> Results;
        
        public Contest(List<Participant> participants,
                       List<Task> tasks,
                       Dictionary<Participant, Dictionary<Task, Result>> results)
        {
            Participants = participants;
            Tasks = tasks;
            Results = results;
        }

        public string Scoreboard
        {
            get
            {
                var sorted = Participants.ToList();
                sorted.Sort((a, b) => Compare(a, Results[a], b, Results[b]));
                return RenderScoreboard(RenderHeader(Tasks), sorted.Select(x => RenderParticipantEntry(x, Results[x])));
            }
        }

        public IEnumerable<string> RenderedTasks
        {
            get
            {
                return Tasks.Select(x => string.Format("{0}\n\n{1}", RenderTaskHeader(x), RenderTaskBody(x)));
            }
        }

        protected abstract int Compare(Participant p1, Dictionary<Task, Result> r1, Participant p2, Dictionary<Task, Result> r2);
        protected abstract string RenderHeader(IEnumerable<Task> tasks);
        protected abstract string RenderParticipantEntry(Participant p, Dictionary<Task, Result> results);
        protected abstract string RenderScoreboard(string header, IEnumerable<string> bodyEntries);

        protected abstract string RenderTaskHeader(Task t);
        protected abstract string RenderTaskBody(Task t);
    }
}
