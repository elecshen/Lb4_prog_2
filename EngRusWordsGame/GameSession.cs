namespace EngRusWordsGame
{
    public abstract class GameSession
    {
        public string Original { get; }
        protected string Translation;
        protected bool isOver;

        public GameSession(string original, string translation)
        {
            Original = original;
            Translation = translation;
            isOver = false;
        }

        public string GetAnswer()
        {
            isOver = true;
            return Translation;
        }

        abstract public bool EnterAsnwer(string answer);
    }
}
