using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;
using MyCollection.Models;
using MyCollection.Views;
using System.IO;
using System.Windows.Input;
using MovieCollection.Models;
using System.ComponentModel;
using System.Windows;
using MovieCollection.Views;
using System.Windows.Controls;
using MyCollection.ViewModels;

namespace MovieCollection.ViewModels
{
    public class MainWindowViewModel
    {
        #region MovieCollection Objects
        public string FilePath { get; set; } = $"{AppDomain.CurrentDomain.BaseDirectory}\\Imports\\CollectionDatabase.xml";                                                                                    
        public string ClosingFilePath { get; set; } = $"{Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()))}\\Imports\\CollectionDatabase.xml";
        public MovieDatabase Collections { get; set; } = new MovieDatabase();
        public XmlSerializer XReader { get; set; } = new XmlSerializer(typeof(MovieDatabase));

    
        private SingleCollection selectedSingleCollection;

        public SingleCollection SelectedSingleCollection
        {
            get { return selectedSingleCollection; }
            set
            {
                selectedSingleCollection = value;
                OnPropertyChanged("SelectedSingleCollection");

                SaveMovieDatabase(FilePath);
            }
        }

        private string chooseCollectionText = "Select Collection";

        public string ChooseCollectionText
        {
            get { return chooseCollectionText; }
            set
            {
                if (value == null || chooseCollectionText == value)
                {
                    chooseCollectionText = "Select Collection";
                    return;
                }
                //chooseCollectionText = value;
                OnPropertyChanged("ChooseCollectionText");
            }
        }

        private Movie sMovie;

        public Movie SMovie
        {
            get { return sMovie; }
            set
            {
                sMovie = value;
            }
        }
        #endregion
        public MainWindowViewModel()
        {
            if (File.Exists(FilePath))
            {
                LoadMovieDatabase(FilePath);
            }
          

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region XML Saving and Loading
        public void LoadMovieDatabase(string fileName)
        {
            using (FileStream FStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                Collections = XReader.Deserialize(FStream) as MovieDatabase;
            }
        }

        public void SaveMovieDatabase(string fileName)
        {
            if (File.Exists(FilePath))
            {
                using (FileStream FStream = new FileStream(fileName, FileMode.Open, FileAccess.Write))
                {
                    XReader.Serialize(FStream, Collections);
                }
            }
        }
        #endregion

        #region SelectCollection
        public void DisplaySelectedCollection(object obj)
        {
           

        }

        public ICommand ChooseCollection
        {
            get
            {
                if (CollectionChanged == null)
                {
                    CollectionChanged = new DelegateCommand(DisplaySelectedCollection);
                }
                return CollectionChanged;
            }
        }

        private DelegateCommand CollectionChanged;
        #endregion

        #region MovieDetails
        public void MovieDetailsWin(object obj)
        {
            if (SMovie is null)
            {
                MessageBox.Show("Must choose a movie first to view details", "Missing Information", MessageBoxButton.OK);
                //var win = new MissingInformation("Must choose a movie first to view details");
                //win.ShowDialog();
            }
            else
            {
                var win = new MovieDetails(SMovie);
                win.ShowDialog();
                //SaveMovieDatabase(FilePath);
            }

        }

        public ICommand MovieDetails
        {
            get
            {
                if (MovieDetailsButton is null)
                {
                    MovieDetailsButton = new DelegateCommand(MovieDetailsWin);
                }
                return MovieDetailsButton;
            }
        }

        private DelegateCommand MovieDetailsButton;
        #endregion

        #region ADD_Movie
        public void AddCommand(object obj)
        {
            if (SelectedSingleCollection is null)
            {
                MessageBox.Show("Please Select a Collection to complete ADD", "Missing Information", MessageBoxButton.OK);
            }
            else
            {
                var win = new AddToCollection(SelectedSingleCollection);
                win.ShowDialog();
                SaveMovieDatabase(FilePath);
            }
        }

        public ICommand ADDMovie
        {
            get
            {
                if (AddNewMovie == null)
                {
                    AddNewMovie = new DelegateCommand(AddCommand);
                }
                return AddNewMovie;
            }
        }

        private DelegateCommand AddNewMovie;

        #endregion

        #region Remove_Movie
        public void RemoveMovieWindow(object obj)
        {
            if ((SelectedSingleCollection is null) || (SMovie is null))
            {
                if (SelectedSingleCollection is null)
                {
                    MessageBox.Show("Please Select a Collection", "Missing Information", MessageBoxButton.OK);


                    //var win = new MissingInformation("Please Select A Collection");
                    //win.ShowDialog();
                }
                else if (SMovie is null)
                {
                    MessageBox.Show("Please choose a movie to remove", "Missing Information", MessageBoxButton.OK);
                    //var win = new MissingInformation("Please choose a movie to remove");
                    //win.ShowDialog();
                }

            }
            else
            {
                var result = MessageBox.Show($"Are you sure you want to delete {SMovie.MovieTitle}?", $"Remove Movie from {SelectedSingleCollection.CollectionName}", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    SelectedSingleCollection.MCollection.Remove(SMovie);
                    SaveMovieDatabase(FilePath);
                }
                //var win = new RemoveWindow(SelectedSingleCollection, SMovie);
                //win.ShowDialog();
                //SaveMovieDatabase(FilePath);

            }

        }

        public ICommand RemoveSingleTitle
        {
            get
            {
                if (RemoveMovie == null)
                {
                    RemoveMovie = new DelegateCommand(RemoveMovieWindow);
                }
                return RemoveMovie;
            }
        }

        private DelegateCommand RemoveMovie;
        #endregion

        #region Order_Selected_Collection
        public void OrderSelectedCollection(object obj)
        {
            if (SelectedSingleCollection is null)
            {
                MessageBox.Show("You must choose a collection before sorting", "Missing Information", MessageBoxButton.OK);
                //var win = new MissingInformation("Must choose a collection before sorting");
                //win.ShowDialog();
            }
            else
            {
                if (obj.ToString() == "Alphabetical")
                {
                    var TempMDatabase = new ObservableCollection<Movie>(SelectedSingleCollection.MCollection.OrderBy(x => x.MovieTitle).ToList());
                    SelectedSingleCollection.MCollection.Clear();
                    foreach (var TempMovie in TempMDatabase)
                    {
                        SelectedSingleCollection.AddMovie(TempMovie);
                    }
                }
                else
                {
                    var TempMDatabase = new ObservableCollection<Movie>(SelectedSingleCollection.MCollection.OrderByDescending(x => x.UserRating).ToList());
                    SelectedSingleCollection.MCollection.Clear();
                    foreach (var TempMovie in TempMDatabase)
                    {
                        SelectedSingleCollection.AddMovie(TempMovie);
                    }
                }
            }
        }

        public ICommand OrderByCommand
        {
            get
            {
                if (OrderBy is null)
                {
                    OrderBy = new DelegateCommand(OrderSelectedCollection);
                }
                return OrderBy;
            }
        }
        private DelegateCommand OrderBy;
        #endregion

        #region Create_New_Collection
        public void ADDCollectionCommand(object obj)
        {
            var win = new CreateNewCollection(Collections);
            win.ShowDialog();
            SaveMovieDatabase(FilePath);
        }

        public ICommand ADDCollection
        {
            get
            {
                if (CreateNewCollectionButton == null)
                {
                    CreateNewCollectionButton = new DelegateCommand(ADDCollectionCommand);
                }
                return CreateNewCollectionButton;
            }
        }

        private DelegateCommand CreateNewCollectionButton;
        #endregion

        #region Close_Program
        public void CloseCommand(object obj)
        {
            SaveMovieDatabase(FilePath);
            SaveMovieDatabase(ClosingFilePath);
            Application.Current.Shutdown();
            return;
        }


        public ICommand CloseMD
        {
            get
            {
                if (CloseProgram == null)
                {
                    CloseProgram = new DelegateCommand(CloseCommand);
                }
                return CloseProgram;
            }
        }

        private DelegateCommand CloseProgram;
        #endregion
    }
}

