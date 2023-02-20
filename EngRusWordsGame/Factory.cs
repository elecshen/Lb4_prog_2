using System;
using System.Collections.Generic;

namespace EngRusWordsGame
{
    internal abstract class Factory
    {
        protected Random Random;
        public Factory()
        {
            Random = new Random();
        }

        public abstract GameSession CreateSession(List<Core.WordCombination> dictionary);
    }
}
