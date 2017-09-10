using System.Collections.Generic;
using System.Linq;

namespace TemplateMethod.Core.ReqA
{
    abstract class ParticipantList
    {
        public IEnumerable<Participant> ReorderParticipants(IEnumerable<Participant> participants)
        {
            var copy = participants.ToList();
            copy.Sort(Compare);
            return copy;
        }

        protected abstract int Compare(Participant p1, Participant p2);
    }
}
