using MovieCollection.Models;
using MyCollection.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using MovieCollection.Views;
using MyCollection.Views;


namespace MyCollection.ViewModels
{
    class CreateNewCollectionViewModel
    {
        private string newCollectionName;

        public string NewCollectionName
        {
            get { return newCollectionName; }
            set { newCollectionName = value; }
        }

        public MovieDatabase MD { get; set; }
        

        public CreateNewCollectionViewModel(MovieDatabase mD)
        {
            MD = mD;
        }
        public void CreateNewCollection(object obj)
        {
            if (NewCollectionName is null)
            {
                MessageBox.Show("Please Enter a collection name", "Missing Information", MessageBoxButton.OK);
            }
            else
            {
                SingleCollection TEMP = new SingleCollection
                {
                    CollectionName = NewCollectionName
                };
                MD.AddCollectionToDatabase(TEMP);
                CloseWindow();
            }
        }


        public ICommand SaveCollection
        {
            get
            {
                if (CreateNewCollectionButton == null)
                {
                    CreateNewCollectionButton = new DelegateCommand(CreateNewCollection);
                }
                return CreateNewCollectionButton;
            }
        }

        private DelegateCommand CreateNewCollectionButton;

        public void CancelCreateNewCollection(object obj)
        {
            CloseWindow();
        }
        public ICommand CancelNewCollection
        {
            get
            {
                if (CancelButton is null)
                {
                    CancelButton = new DelegateCommand(CancelCreateNewCollection);
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
