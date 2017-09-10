using Common.Core;
using System.Collections.Generic;
using System.Linq;

namespace Strategy.Core
{
    public class Contest
    {
        public IEnumerable<Participant> Participants { get; private set; }
        public IEnumerable<Task> Tasks { get; private set; }
        public Dictionary<Participant, Dictionary<Task, Result>> Results;

        private IScoreboardGenerator _scoreboardGenerator;
        private ITaskRenderer _taskRenderer;
        
        public Contest(IScoreboardGenerator scoreboardGenerator,
                       ITaskRenderer taskRenderer,
                       IEnumerable<Participant> participants,
                       IEnumerable<Task> tasks,
                       Dictionary<Participant, Dictionary<Task, Result>> results)
        {
            _scoreboardGenerator = scoreboardGenerator;
            _taskRenderer = taskRenderer;
            Participants = participants;
            Tasks = tasks;
            Results = results;
        }

        public string Scoreboard
        {
            get
            {
                var sorted = Participants.ToList();
                sorted.Sort((a,b) => _scoreboardGenerator.Compare(a, Results[a], b, Results[b]));
                return _scoreboardGenerator.RenderScoreboard(
                            _scoreboardGenerator.RenderHeader(Tasks),
                            sorted.Select(p => _scoreboardGenerator.RenderParticipantEntry(p, Results[p])));
            }
        }

        public IEnumerable<string> RenderedTasks
        {
            get
            {
                return Tasks.Select(x => string.Format("{0}\n\n{1}", 
                        _taskRenderer.RenderTaskHeader(x),
                        _taskRenderer.RenderTaskBody(x)));
            }
        }
    }
}
