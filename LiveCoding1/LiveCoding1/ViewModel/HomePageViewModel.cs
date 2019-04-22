using Exercice4.Models;
using Exercice4.Views;
using LiveCoding1.Models;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static LiveCoding1.HomePage;

namespace Exercice4.ViewModel{

    class HomePageViewModel : ViewModelBase {
        //private string nomFichier = "spring.jpg";
        //private DateTime currentDate = new DateTime(2019, 2, 09);
        public ObservableCollection<PlaceItem> places;
        
        public HomePageViewModel(){
            places = new ObservableCollection<PlaceItem>();

            JSONService j = new JSONService();
            //j.CodeASupprimer();

            //PlaceItem t = j.GetPlace(0);

            //places = t.Result;
            places.Add(new PlaceItem { Title = "Paris", Description = "embouteillé et polué", ImageId = 0, Id = 1 });

            /*places.Add(new PlaceItem { Title = "Bruxelles", Description = "un tres beau voyage", ImageId = 0, Id = 0 });
            
            places.Add(new PlaceItem { Title = "JeSaisPasOU", Description = "c'est moche", ImageId = 0, Id = 2});
            //places.Add(new Lieu { Name = "Paris", Description = "embouteillé et polué", ImagePath= "drawable/autumn.jpg" });
            //places.Add(new Lieu { Name = "JeSaisPasOU", Description = "c'est moche", ImagePath = "drawable/autumn.jpg" });
            PlaceItem lieu = new PlaceItem { Title = "JeSaisPasOU", Description = "c'est moche", ImageId = 0, Id = 3};
            List<CommentItem> list = new List<CommentItem>();
            list.Add(new CommentItem { Author = new UserItem(), Text = "text1", Date = new DateTime() });
            lieu.Comments = list;
            places.Add(lieu);*/
            //dt = new DataTemplate(typeof(CustomVeggieCell));
            //PressMeCommand = new Command(pressMeAction);
        }

        public ObservableCollection<PlaceItem> Places
        {
            get => places;
            set
            {
                SetProperty(ref places, value);
            }
        }
    }
}
