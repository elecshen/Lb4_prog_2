using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
