using MovieCollection.Models;
using MyCollection.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using MyCollection.Views;
using System.IO;

namespace MyCollection.ViewModels
{
   
    class AddToCollectionViewModel
    {
        private string mTitle;

        public string MTitle
        {
            get { return mTitle; }
            set { mTitle = value; }
        }

        private string mFormat;

        public string MFormat
        {
            get { return mFormat; }
            set
            {
                mFormat = value == "System.Windows.Controls.ComboBoxItem: Blu-Ray" ? "Blu-Ray" : "DVD";
            }
        }

        private int uRating;

        public int URating
        {
            get { return uRating; }
            set
            {
                uRating = value;
                OnPropertyChanged("UserRating");
            }
        }


        public string MImageSource { get; set; }
        private SingleCollection passedSC;

        public SingleCollection PassedSC
        {
            get { return passedSC; }
            set { passedSC = value; }
        }


        private Movie movieAdded;

        public Movie MovieAdded
        {
            get { return movieAdded; }
            set
            {
                movieAdded = value;
                OnPropertyChanged("MovieAdded");
            }
        }


        public AddToCollectionViewModel(SingleCollection _SC) { PassedSC = _SC;}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void CreateNewMovie(object obj)
        {
            if ((MTitle is null) || (MFormat is null) || (URating == 0))
            {
                if(MTitle is null)
                {
                    MessageBox.Show("Please Enter a Movie Title", "Missing Information", MessageBoxButton.OK);
                }
                else if (MFormat is null)
                {
                    MessageBox.Show("Please Select a Format", "Missing Information", MessageBoxButton.OK);
                }
                else if (URating == 0)
                {
                    MessageBox.Show("Please Enter a Rating Between 1-5", "Missing Information", MessageBoxButton.OK);
                }
            }
            else
            {
                //string Path = $"C:/Users/emon1/Desktop/MyCollection/PosterDatabase/{MTitle}";
                MImageSource = (File.Exists($"C:/Users/emon1/Desktop/MyCollection/PosterDatabase/{MTitle}.jpg")) ? $"C:/Users/emon1/Desktop/MyCollection/PosterDatabase/{MTitle}.jpg" : "C:/Users/emon1/Desktop/MyCollection/PosterDatabase/DefaultMoviePoster.jpg";
                Movie TmpMovie = new Movie(MTitle, MFormat, MImageSource, URating);
                PassedSC.AddMovie(TmpMovie);
                CloseWindow();
            }
        }


        public ICommand SaveMovie
        {
            get
            {
                if (SaveButton == null)
                {
                    SaveButton = new DelegateCommand(CreateNewMovie);
                }
                return SaveButton;
            }
        }

        private DelegateCommand SaveButton;


        public void SetUserRating(object obj)
        {
            URating = Convert.ToInt16(obj);

        }

        public ICommand RatingBoxes
        {
            get
            {
                if (RatingBoxResult == null)
                {
                    RatingBoxResult = new DelegateCommand(SetUserRating);
                }
                return RatingBoxResult;
            }
        }

        private DelegateCommand RatingBoxResult;

        public void CancelADDMovie(object obj) { CloseWindow(); }

        public ICommand CancelMovie
        {
            get
            {
                if (CancelButton is null)
                {
                    CancelButton = new DelegateCommand(CancelADDMovie);
                }
                return CancelButton;
            }
        }

        private DelegateCommand CancelButton;

        private void CloseWindow()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }
    }
}
