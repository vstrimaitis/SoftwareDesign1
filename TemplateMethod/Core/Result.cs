namespace TemplateMethod.Core
{
    public struct Result
    {
        public bool Solved { get; private set; }
        public int Time { get; private set; }
        public int WrongSubmissionCount { get; private set; }

        public Result(bool solved, int time, int wrongSubmissionCount)
        {
            Solved = solved;
            Time = time;
            WrongSubmissionCount = wrongSubmissionCount;
        }
    }
}
