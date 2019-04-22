using Exercice4.Models;
using Exercice4.ViewModel;
using Exercice4.Views;
using LiveCoding1.Models;
using Storm.Mvvm;
using Storm.Mvvm.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LiveCoding1
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class HomePage : BaseContentPage
    {
        private HomePageViewModel binder = new HomePageViewModel();

        public HomePage()
        {
            InitializeComponent();
            Console.WriteLine("HomePage");
            BindingContext = binder;

        }

        /*public class CustomVeggieCell : ViewCell
        {
            public CustomVeggieCell()
            {
                //instantiate each of our views
                //var image = new Label();
                var nameLabel = new Label();
                var dateLabel = new Label();
                var textLabel = new Label();
                Button button = new Button();
                //<Button Text="" Clicked="jghdskfjg">  ->xml
                //private void pressMe(){           -> vue
                // Navigation.PushAsync(new MoonPage());
                //}

                var verticaLayout = new StackLayout();
                var horizontalLayout = new StackLayout() { BackgroundColor = Color.Olive };

                //set bindings
                nameLabel.SetBinding(Label.TextProperty, new Binding("Name"));
                dateLabel.SetBinding(Label.TextProperty, new Binding("Date"));
                textLabel.SetBinding(Label.TextProperty, new Binding("Text"));

                //image.SetBinding(Image.SourceProperty, new Binding("Text"));

                //Set properties for desired design
                horizontalLayout.Orientation = StackOrientation.Horizontal;
                horizontalLayout.HorizontalOptions = LayoutOptions.Fill;
                //image.HorizontalOptions = LayoutOptions.End;
                nameLabel.FontSize = 10;
                dateLabel.FontSize = 10;
                textLabel.FontSize = 10;

                //add views to the view hierarchy
                verticaLayout.Children.Add(nameLabel);
                verticaLayout.Children.Add(dateLabel);
                verticaLayout.Children.Add(textLabel);
                horizontalLayout.Children.Add(verticaLayout);
                horizontalLayout.Children.Add(button);
                //horizontalLayout.Children.Add(image);
                

                // add to parent view
                View = horizontalLayout;
            }
        }*/

        private PlaceItem findPlaceById(string classId) {
            IEnumerator<PlaceItem> enume = binder.places.GetEnumerator();
            while (enume.MoveNext()){
                if (enume.Current.Title.Equals(classId))
                {
                    return enume.Current;
                }
            }
            
            return enume.Current;
        }

        private void pressMeAction(object sender, EventArgs e)
        {
            Console.WriteLine("pressMeAction");
            var button = (Button)sender;
            Console.WriteLine("button : "+button);
            var classId = button.ClassId;
            Console.WriteLine("classId : "+classId);

            PlaceItem p = findPlaceById(classId);
            Console.WriteLine("Place : " + p);

            switch (classId) {
                case "Paris": break;
                default: break;
            }

            Navigation.PushAsync(new Page2(p));
            //binder.lieuCourant = sender.;
        }

        private async void pressMeAction2(object sender, EventArgs e)
        {
            Console.WriteLine("pressMeAction2");

            //binder.lieuCourant = sender.;
            JSONService j = new JSONService();
            j.CodeASupprimer();
            //await j.GetPlaces();
            PlaceItem t = await j.GetPlace(0);
            binder.places.Add(t);
        }
    }
}
