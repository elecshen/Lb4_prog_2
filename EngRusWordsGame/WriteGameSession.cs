using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngRusWordsGame
{
    public class WriteGameSession : GameSession
    {
        public WriteGameSession(string original, string translation) : base(original, translation) { }

        public override bool EnterAsnwer(string answer)
        {
            if (isOver)
                return false;
            if(answer.Equals(this.Translation))
                return true;
            return false;
        }
    }
}
