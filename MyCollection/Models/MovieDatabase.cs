using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

namespace MyCollection.Models
{
    [Serializable]
    public class MovieDatabase
    {
        [XmlAttribute]
        public string DatabaseName { get; set; }

        [XmlElement("SingleCollection")]
        public ObservableCollection<SingleCollection> MDatabase { get; set; } = new ObservableCollection<SingleCollection>();
        public MovieDatabase() { }

        public void AddCollectionToDatabase(SingleCollection singleCollection)
        {
            MDatabase.Add(singleCollection);
        }
    }

    [Serializable]
    public class SingleCollection
    {
        [XmlElement("Movie")]
        public ObservableCollection<Movie> MCollection { get; set; } = new ObservableCollection<Movie>();

        [XmlAttribute]
        public string CollectionName { get; set; }

        public SingleCollection() { }

        public void AddMovie(Movie movie)
        {
            MCollection.Add(movie);
        }
    }
    
    [Serializable]
    public class Movie
    {
        [XmlAttribute]
        public string MovieTitle { get; set; }

        [XmlAttribute]
        public string Format { get; set; }

        [XmlAttribute]
        public string ImageSource { get; set; }

        [XmlAttribute]
        public int UserRating { get; set; }

        public Movie() { }

        public Movie(string movieTitle, string format, string imageSource, int userRating)
        {
            MovieTitle = movieTitle ?? throw new ArgumentNullException(nameof(movieTitle));
            Format = format ?? throw new ArgumentNullException(nameof(format));
            ImageSource = imageSource ?? throw new ArgumentNullException(nameof(imageSource));
            UserRating = userRating;
        }
    }
}

