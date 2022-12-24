using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngRusWordsGame
{
    internal class WriteGameFactory : Factory
    {
        public override GameSession CreateSession(List<Core.WordCombination> dictionary)
        {
            Core.WordCombination wc = dictionary[Random.Next(dictionary.Count())];
            return new WriteGameSession(wc.Original, wc.Translation);
        }
    }
}
