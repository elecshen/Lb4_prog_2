using EngRusWordsGame;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Lb4_prog_2
{
    internal class VM : INotifyPropertyChanged
    {
        private Visibility returnButtonVisibility;
        public Visibility ReturnButtonVisibility
        {
            get
            {
                return returnButtonVisibility;
            }
            set
            {
                returnButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility cgsButtonVisibility;
        public Visibility CGSButtonVisibility
        {
            get
            {
                return cgsButtonVisibility;
            }
            set
            {
                cgsButtonVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility wgsButtonVisibility;
        public Visibility WGSButtonVisibility
        {
            get
            {
                return wgsButtonVisibility;
            }
            set
            {
                wgsButtonVisibility = value;
                OnPropertyChanged();
            }
        }
        private List<Page> pages;
        private readonly Frame MainFrame;

        public Core.WordCombination Word2Edit { get; set; }
        public Core Core { get; }
        public Core.WordCombination SelectedWC { get; set; }

        private GameSession cgSession;
        public GameSession CGSession
        {
            get
            {
                return cgSession;
            }
            set
            {
                cgSession = value;
                OnPropertyChanged();
            }
        }
        private GameSession wgSession;
        public GameSession WGSession
        {
            get
            {
                return wgSession;
            }
            set
            {
                wgSession = value;
                OnPropertyChanged();
            }
        }

        private string cgsSelected;
        public string CGSSelected
        {
            get
            {
                return cgsSelected;
            }
            set
            {
                cgsSelected = value;
                OnPropertyChanged();
            }
        }
        private string wgsSelected;
        public string WGSSelected
        {
            get
            {
                return wgsSelected;
            }
            set
            {
                wgsSelected = value;
                OnPropertyChanged();
            }
        }

        private string cgsButtonText;
        public string CGSButtonText
        {
            get
            {
                return cgsButtonText;
            }
            set
            {
                cgsButtonText = value;
                OnPropertyChanged();
            }
        }
        private string wgsButtonText;
        public string WGSButtonText
        {
            get
            {
                return wgsButtonText;
            }
            set
            {
                wgsButtonText = value;
                OnPropertyChanged();
            }
        }

        private string cgsAnswer;
        public string CGSAnswer
        {
            get
            {
                return cgsAnswer;
            }
            set
            {
                cgsAnswer = value;
                OnPropertyChanged();
            }
        }
        private string wgsAnswer;
        public string WGSAnswer
        {
            get
            {
                return wgsAnswer;
            }
            set
            {
                wgsAnswer = value;
                OnPropertyChanged();
            }
        }

        private string filePath;
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
                OnPropertyChanged();
            }
        }

        public VM(Frame mFrame)
        {
            MainFrame= mFrame;
            returnButtonVisibility = Visibility.Collapsed;
            cgsButtonVisibility = Visibility.Collapsed;
            wgsButtonVisibility = Visibility.Collapsed;
            pages = new List<Page>()
            {
                new PageMain(),
                new PageDictionaryEditor(),
                new PageChoiceGame(),
                new PageWriteGame()
            };
            foreach(var page in pages)
            {
                page.DataContext= this;
            }
            MainFrame.Navigate(pages[0]);
            Word2Edit = new Core.WordCombination();
            Core = new Core();
            Core.AddWord("1", "11");
            Core.AddWord("2", "22");
            Core.AddWord("3", "33");
            Core.AddWord("4", "44");
            Core.AddWord("5", "55");
            Core.AddWord("6", "66");
        }

        private void goChoice(bool start)
        {
            if(start)
            {
                CGSButtonText = "Проверить";
                CGSButtonVisibility = Visibility.Collapsed;
                CGSession = Core.GetChoiceSession();
                CGSSelected = "";
            }
            else
            {
                CGSButtonText = "Далее";
                CGSButtonVisibility = Visibility.Visible;
                if (CGSSelected != null && CGSession.EnterAsnwer(CGSSelected))
                {
                    CGSAnswer = "Ответ верный!";
                }
                else
                {
                    CGSAnswer = CGSession.GetAnswer();
                }
            }
        }
        private void goWrite(bool start)
        {
            if (start)
            {
                WGSButtonText = "Проверить";
                WGSButtonVisibility = Visibility.Collapsed;
                WGSession = Core.GetWriteSession();
                WGSSelected = "";
            }
            else
            {
                WGSButtonText = "Далее";
                WGSButtonVisibility = Visibility.Visible;
                if (WGSSelected != null && WGSession.EnterAsnwer(WGSSelected))
                {
                    WGSAnswer = "Ответ верный!";
                }
                else
                {
                    WGSAnswer = WGSession.GetAnswer();
                }
            }
        }

        private Command returnCommand;
        public Command ReturnCommand
        {
            get
            {
                return returnCommand ??
                    (returnCommand = new Command(obj =>
                    {
                        MainFrame.Navigate(pages[0]);
                        ReturnButtonVisibility = Visibility.Collapsed;
                    }));
            }
        }

        private Command openEditorCommand;
        public Command OpenEditorCommand
        {
            get
            {
                return openEditorCommand ??
                    (openEditorCommand = new Command(obj =>
                    {
                        MainFrame.Navigate(pages[1]);
                        ReturnButtonVisibility = Visibility.Visible;
                    }));
            }
        }

        private Command openChoiceGameCommand;
        public Command OpenChoiceGameCommand
        {
            get
            {
                return openChoiceGameCommand ??
                    (openChoiceGameCommand = new Command(obj =>
                    {
                        goChoice(true);
                        MainFrame.Navigate(pages[2]);
                        ReturnButtonVisibility = Visibility.Visible;
                    }));
            }
        }

        private Command openWriteGameCommand;
        public Command OpenWriteGameCommand
        {
            get
            {
                return openWriteGameCommand ??
                    (openWriteGameCommand = new Command(obj =>
                    {
                        goWrite(true);
                        MainFrame.Navigate(pages[3]);
                        ReturnButtonVisibility = Visibility.Visible;
                    }));
            }
        }

        private Command addWCCommand;
        public Command AddWCCommand
        {
            get
            {
                return addWCCommand ??
                    (addWCCommand = new Command(obj =>
                    {
                        if(Word2Edit.Original != "" && Word2Edit.Translation != "")
                            Core.AddWord(Word2Edit.Original, Word2Edit.Translation);
                    }));
            }
        }

        private Command removeWCCommand;
        public Command RemoveWCCommand
        {
            get
            {
                return removeWCCommand ??
                    (removeWCCommand = new Command(obj =>
                    {
                        if(SelectedWC != null)
                            Core.RemoveWord(SelectedWC.Original);
                    }));
            }
        }

        private Command cgsNextCommand;
        public Command CGSNextCommand
        {
            get
            {
                return cgsNextCommand ??
                    (cgsNextCommand = new Command(obj =>
                    {
                        if (CGSButtonText != "Проверить")
                            goChoice(true);
                        else
                            goChoice(false);
                    }));
            }
        }
        private Command wgsNextCommand;
        public Command WGSNextCommand
        {
            get
            {
                return wgsNextCommand ??
                    (wgsNextCommand = new Command(obj =>
                    {
                        if (WGSButtonText != "Проверить")
                            goWrite(true);
                        else
                            goWrite(false);
                    }));
            }
        }

        private Command selectFilePathCommand;
        public Command SelectFilePathCommand
        {
            get
            {
                return selectFilePathCommand ??
                    (selectFilePathCommand = new Command(obj =>
                    {
                        OpenFileDialog ofd = new OpenFileDialog();
                        if (ofd.ShowDialog() != true)
                            return;
                        FilePath = ofd.FileName;
                    }));
            }
        }

        private Command importCommand;
        public Command ImportCommand
        {
            get
            {
                return importCommand ??
                    (importCommand = new Command(obj =>
                    {
                        try
                        {
                            new Uri(FilePath);
                        } catch(UriFormatException)
                        {
                            FilePath = "Неверный путь";
                            return;
                        }
                        Core.ImportDictionary(FilePath);
                    }));
            }
        }

        private Command exportCommand;
        public Command ExportCommand
        {
            get
            {
                return exportCommand ??
                    (exportCommand = new Command(obj =>
                    {
                        Core.ExportDictionary(@"d:\export.json");
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}