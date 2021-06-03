using MyCollection.Models;
using MyCollection.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MovieCollection.Views
{
    /// <summary>
    /// Interaction logic for AddToCollection.xaml
    /// </summary>
    public partial class AddToCollection : Window
    {
        public AddToCollection(SingleCollection _SC)
        {
            InitializeComponent();
            this.DataContext = new AddToCollectionViewModel(_SC); 
        }
    }
}
