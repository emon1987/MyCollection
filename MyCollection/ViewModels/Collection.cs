using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollection.ViewModels
{
    public class Collection
    {
        public ObservableCollection<string> MainCollection { get; private set; }

        public Collection()
        {
            MainCollection = new ObservableCollection<string>();
        }
    }
}
