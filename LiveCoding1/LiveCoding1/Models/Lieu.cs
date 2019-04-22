using Exercice4.Views;
using Storm.Mvvm.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace LiveCoding1.Models
{
    public class Lieu
    {
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PosGPS { get; set; }
        public ObservableCollection<Commentaire> commentaires { get; set; }

        /*Lieu(string imagePath, string name, string description, string posGPS) {

        }*/

        public Lieu() {
            commentaires = new ObservableCollection<Commentaire>();
        }

    }
}
