namespace Strategy.Core
{
    public struct Task
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int MaxScore { get; private set; }

        public Task(string name, string description, int maxScore)
        {
            Name = name;
            Description = description;
            MaxScore = maxScore;
        }
    }
}
