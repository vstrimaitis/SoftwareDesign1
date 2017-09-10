namespace TemplateMethod.Core
{
    struct Participant
    {
        public string Username { get; private set; }

        public Participant(string username)
        {
            Username = username;
        }
    }
}
