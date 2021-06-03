using MyCollection.Models;
using MyCollection.ViewModels;
using System.Windows;

namespace MyCollection.Views
{
    /// <summary>
    /// Interaction logic for MovieDetails.xaml
    /// </summary>
    public partial class MovieDetails : Window
    {
        public MovieDetails(Movie _SelectedTitle)
        {
            InitializeComponent();
            this.DataContext = new MoiveDetailsViewModel(_SelectedTitle);
        }
    }
}
