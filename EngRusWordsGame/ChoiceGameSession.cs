using System.Collections.Generic;

namespace EngRusWordsGame
{
    public class ChoiceGameSession : GameSession
    {
        public List<string> Words { get; }

        public ChoiceGameSession(string original, string translation, List<string> words) : base(original, translation)
        {
            this.Words = words;
        }

        public override bool EnterAsnwer(string answer)
        {
            if(isOver)
                return false;
            if (answer.Equals(this.Translation))
                return true;
            return false;
        }
    }
}
