namespace TemplateMethod.Core.ReqA
{
    class AlphabeticParticipantList : ParticipantList
    {
        protected override int Compare(Participant p1, Participant p2)
        {
            return p1.Username.CompareTo(p2.Username);
        }
    }
}
