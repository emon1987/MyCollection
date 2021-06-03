using MovieCollection.Models;
using MyCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyCollection.ViewModels
{
    class MoiveDetailsViewModel
    {
        public Movie SelectedMovie { get; set; }

        public MoiveDetailsViewModel(Movie selectedMovie)
        {
            SelectedMovie = selectedMovie;
        }


        public void CloseMovieDetails(object obj)
        {
            CloseWindow();
        }

        public ICommand CloseDetails
        {
            get
            {
                if (CloseButton is null)
                {
                    CloseButton = new DelegateCommand(CloseMovieDetails);
                }
                return CloseButton;
            }
        }

        private DelegateCommand CloseButton;

        private void CloseWindow()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }

    }
}
