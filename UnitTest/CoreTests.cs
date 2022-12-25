using EngRusWordsGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void IsContainWord_Find1_1and11WordCombination()
        {
            Core core = new Core();
            string original = "1", translation = "11";
            core.AddWord(original, translation);
            Core.WordCombination wc;
            string expected = original + translation;

            core.IsContainWord(original, out wc);

            Assert.AreEqual(expected, wc.Original + wc.Translation);
        }

        [TestMethod]
        public void IsContainWord_FindInNotEmpty_EmptyWordCombination()
        {
            Core core = new Core();
            core.AddWord("1", "11");
            Core.WordCombination wc;
            string expected = "";

            core.IsContainWord("", out wc);

            Assert.AreEqual(expected, wc.Original + wc.Translation);
        }

        [TestMethod]
        public void IsContainWord_FindInEmpty_EmptyWordCombination()
        {
            Core core = new Core();
            Core.WordCombination wc;
            string expected = "";

            core.IsContainWord("", out wc);

            Assert.AreEqual(expected, wc.Original + wc.Translation);
        }

        [TestMethod]
        public void IsContainWord_Is1inList_True()
        {
            Core core = new Core();
            core.AddWord("1", "11");

            bool actual = core.IsContainWord("1");

            Assert.IsTrue(actual);
        }


        [TestMethod]
        public void IsContainWord_Is2inList_False()
        {
            Core core = new Core();
            core.AddWord("1", "11");

            bool actual = core.IsContainWord("2");

            Assert.IsTrue(actual);
        }
    }
}
