using System.Collections.Generic;
using System.Linq;

namespace TemplateMethod.Core
{
    abstract class Contest
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
                sorted.Sort(Compare);
                return RenderScoreboard(RenderHeader(), sorted.Select(RenderParticipantEntry));
            }
        }

        public IEnumerable<string> RenderedTasks
        {
            get
            {
                return Tasks.Select(x => string.Format("{0}\n\n{1}", RenderTaskHeader(x), RenderTaskBody(x)));
            }
        }

        protected abstract int Compare(Participant p1, Participant p2);
        protected abstract string RenderHeader();
        protected abstract string RenderParticipantEntry(Participant p);
        protected abstract string RenderScoreboard(string header, IEnumerable<string> bodyEntries);

        protected abstract string RenderTaskHeader(Task t);
        protected abstract string RenderTaskBody(Task t);
    }
}
