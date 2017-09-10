namespace TemplateMethod.Core.ReqA
{
    class UsernameLengthBasedParticipantList : ParticipantList
    {
        protected override int Compare(Participant p1, Participant p2)
        {
            return p1.Username.Length.CompareTo(p2.Username.Length);
        }
    }
}
