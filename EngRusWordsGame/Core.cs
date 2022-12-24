using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EngRusWordsGame
{
    public class Core
    {
        public class WordCombination
        {
            public string Original { get; set; }
            public string Translation { get; set; }

            [JsonConstructor]
            public WordCombination(string original = "", string translation = "")
            {
                Original = original;
                Translation = translation;
            }
        }

        private ObservableCollection<WordCombination> dictionary;
        public ReadOnlyObservableCollection<WordCombination> Dictionary { get; }

        private readonly ChoiceGameFactory cgFactory;
        private readonly WriteGameFactory wgFactory;

        public Core()
        {
            cgFactory = new ChoiceGameFactory();
            wgFactory = new WriteGameFactory();

            dictionary = new ObservableCollection<WordCombination>();
            Dictionary = new ReadOnlyObservableCollection<WordCombination>(dictionary);
        }
        
        public void AddWord(string original, string translation)
        {
            if (IsContainWord(original)) return;
            dictionary.Add(new WordCombination(original, translation));
        }

        public bool IsContainWord(string original)
        {
            var wc = FindWordCombination(original);
            if (!wc.Translation.Equals(""))
                return true;
            return false;
        }

        public bool IsContainWord(string original, out WordCombination wc)
        {
            wc = FindWordCombination(original);
            if (!wc.Translation.Equals(""))
                return true;
            return false;
        }

        public void RemoveWord(string original)
        {
            WordCombination wc;
            if (!IsContainWord(original, out wc)) return;
            dictionary.Remove(wc);
        }

        private WordCombination FindWordCombination(string original)
        {
            var wc = dictionary.Where(w => w.Original == original);
            if (wc.Count() > 0) return wc.First();
            return new WordCombination();
        }

        public GameSession GetChoiceSession()
        {
            return cgFactory.CreateSession(dictionary.ToList());
        }

        public GameSession GetWriteSession()
        {
            return wgFactory.CreateSession(dictionary.ToList());
        }

        public void ImportDictionary(string path)
        {
            string json = File.ReadAllText(path);
            var list = JsonSerializer.Deserialize<WordCombination[]>(json);
            foreach(var l in list)
            {
                AddWord(l.Original, l.Translation);
            }
        }

        public void ExportDictionary(string path)
        {
            string json = JsonSerializer.Serialize(dictionary.ToList(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
    }
}
