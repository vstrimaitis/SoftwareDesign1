﻿namespace Common.Core
{
    public struct Participant
    {
        public string Username { get; private set; }

        public Participant(string username)
        {
            Username = username;
        }
    }
}
