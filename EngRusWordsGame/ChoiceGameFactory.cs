using System;
using System.Collections.Generic;
using System.Linq;

namespace EngRusWordsGame
{
    internal class ChoiceGameFactory : Factory
    {
        public override GameSession CreateSession(List<Core.WordCombination> dictionary)
        {
            int rnd = Random.Next(dictionary.Count());
            Core.WordCombination wc = dictionary[rnd];
            string originalWord = wc.Original;
            int rightAnswer = Random.Next(4);
            List<string> words = new List<string>();
            do
            {
                if (words.Count() == rightAnswer)
                {
                    words.Add(wc.Translation);
                    continue;
                }
                rnd = Random.Next(dictionary.Count());
                if (!words.Contains(dictionary[rnd].Translation) && !dictionary[rnd].Translation.Equals(wc.Translation))
                    words.Add(dictionary[rnd].Translation);
            } while (words.Count() < 4);
            return new ChoiceGameSession(originalWord, words[rightAnswer], words);
        }
    }
}
