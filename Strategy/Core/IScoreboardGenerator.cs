using Common.Core;
using System.Collections.Generic;

namespace Strategy.Core
{
    public interface IScoreboardGenerator
    {
        int Compare(Participant p1, Dictionary<Task, Result> r1, Participant p2, Dictionary<Task, Result> r2);
        string RenderHeader(IEnumerable<Task> tasks);
        string RenderParticipantEntry(Participant p, Dictionary<Task, Result> results);
        string RenderScoreboard(string header, IEnumerable<string> bodyEntries);
    }
}
