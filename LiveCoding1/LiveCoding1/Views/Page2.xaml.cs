using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Exercice4.ViewModel;
using LiveCoding1.Models;

using fivenine.UnifiedMaps;

namespace Exercice4.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page2 : ContentPage
	{
        //Map map;

        private void OnAppearing(object sender, EventArgs e)
        {
            Console.Write("on appearing");
            Map.MoveToRegion();
            ((Page2ViewModel)BindingContext).Map = Map;
        }

        private void hhh()
        {
            Console.Write("on appearing");
            
            Map.MoveToRegion(new MapRegion(new Position(48.853401, 2.348779), 400), true);

            ((Page2ViewModel)BindingContext).Map = Map;
        }

        

        public Page2 (PlaceItem currentLieu)
		{
			InitializeComponent ();
            Console.Write("--------------Page2 constructeur--------------");

            BindingContext = new Page2ViewModel(currentLieu);
            Map.PinClicked += (sender, e) => Debug.WriteLine("Pin Clicked");
            Map.PinInfoViewClicked += (sender, e) => Debug.WriteLine("Info Window Clicked");
            Map.PinInfoViewLongClicked += (sender, e) => Debug.WriteLine("Info Window Long Clicked");
            Map.MapClicked += (sender, e) => Debug.WriteLine($"Map Clicked, {{latitude: {((Position)e.Value).Latitude} longitude: {((Position)e.Value).Longitude}}}");
            Map.MapLongClicked += (sender, e) => Debug.WriteLine($"Map Long Clicked, {{latitude: {((Position)e.Value).Latitude} longitude: {((Position)e.Value).Longitude}}}");
            Map.PinLongClicked += (sender, e) => Debug.WriteLine("Pin Long Clicked");
            Map.PinDragStart += (sender, e) => Debug.WriteLine($"Pin Drag Start, {{latitude: {((MapPin)e.Value).Location.Latitude} longitude: {((MapPin)e.Value).Location.Longitude}}}");
            Map.PinDragging += (sender, e) => Debug.WriteLine("Pin Dragging");
            Map.PinDragEnd += (sender, e) => Debug.WriteLine($"Pin Drag End, {{latitude: {((MapPin)e.Value).Location.Latitude} longitude: {((MapPin)e.Value).Location.Longitude}}}");

            //hhh();
            //Xamarin.FormsMaps.Init();
            /*map = new Map
            {

                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };


            map.MoveToRegion(new MapSpan(new Position(0, 0), 360, 360));


            var slider = new Slider(1, 18, 1);
            slider.ValueChanged += (sender, e) => {
                var zoomLevel = e.NewValue;
                var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
                Debug.WriteLine(zoomLevel + " -> " + latlongdegrees);
                if (map.VisibleRegion != null)
                    map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            };


            var street = new Button { Text = "Street" };
            var hybrid = new Button { Text = "Hybrid" };
            var satellite = new Button { Text = "Satellite" };
            street.Clicked += HandleClicked;
            hybrid.Clicked += HandleClicked;
            satellite.Clicked += HandleClicked;
            var segments = new StackLayout
            {
                Spacing = 30,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { street, hybrid, satellite }
            };


            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            stack.Children.Add(slider);
            stack.Children.Add(segments);
            Content = stack;


            map.PropertyChanged += (sender, e) => {
                Debug.WriteLine(e.PropertyName + " just changed!");
                if (e.PropertyName == "VisibleRegion" && map.VisibleRegion != null)
                    CalculateBoundingCoordinates(map.VisibleRegion);
            };*/

        }
        /*void HandleClicked(object sender, EventArgs e)
        {
            var b = sender as Button;
            switch (b.Text)
            {
                case "Street":
                    map.MapType = MapType.Street;
                    break;
                case "Hybrid":
                    map.MapType = MapType.Hybrid;
                    break;
                case "Satellite":
                    map.MapType = MapType.Satellite;
                    break;
            }
        }


        static void CalculateBoundingCoordinates(MapSpan region)
        {
            var center = region.Center;
            var halfheightDegrees = region.LatitudeDegrees / 2;
            var halfwidthDegrees = region.LongitudeDegrees / 2;

            var left = center.Longitude - halfwidthDegrees;
            var right = center.Longitude + halfwidthDegrees;
            var top = center.Latitude + halfheightDegrees;
            var bottom = center.Latitude - halfheightDegrees;

            if (left < -180) left = 180 + (180 + left);
            if (right > 180) right = (right - 180) - 180;

            Debug.WriteLine("Bounding box:");
            Debug.WriteLine("                    " + top);
            Debug.WriteLine("  " + left + "                " + right);
            Debug.WriteLine("                    " + bottom);
        }*/

        
    }
}