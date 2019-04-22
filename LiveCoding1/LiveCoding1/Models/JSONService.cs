using System;

using Xamarin.Forms;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TD.Api.Dtos;
using System.Text;
using Fourplaces.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Collections.ObjectModel;
using LiveCoding1.Models;

namespace Exercice4.Models

{
    public class JSONService : ContentPage
    {
        HttpClient client;
        public String url = "https://td-api.julienmialon.com/";
        public JSONService()
        {

            Console.WriteLine("RestService");
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

        }

        public async void CodeASupprimer() {
            Console.WriteLine("\n------------------Code a tester--------------------\n\n\n");
            HttpClient client = new HttpClient();
            byte[] imageData = await client.GetByteArrayAsync("https://bnetcmsus-a.akamaihd.net/cms/blog_header/x6/X6KQ96B3LHMY1551140875276.jpg");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/images");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "__access__token__");

            MultipartFormDataContent requestContent = new MultipartFormDataContent();

            var imageContent = new ByteArrayContent(imageData);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            // Le deuxième paramètre doit absolument être "file" ici sinon ça ne fonctionnera pas
            requestContent.Add(imageContent, "file", "file.jpg");

            request.Content = requestContent;

            HttpResponseMessage response = await client.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Image uploaded!");
            }
            else
            {
                Debugger.Break();
            }
        }

        public async Task<ObservableCollection<PlaceItem>> GetPlaces()
        {
            Console.WriteLine("\n\n\n------------------\n\n\n------------------Code a tester : GetPlaces--------------------\n\n\n------------------\n\n\n");
            HttpClient client = new HttpClient();
            ObservableCollection<PlaceItem> places = null;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url + "/places");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "__access__token__");

            var uri = new Uri(string.Format(url + "places", string.Empty));
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Response<ObservableCollection<PlaceItem>> r = JsonConvert.DeserializeObject<Response<ObservableCollection<PlaceItem>>>(content);
                Console.WriteLine("Dev_is_sucess:" + r.IsSuccess);
                Console.WriteLine("Dev_error_code:" + r.ErrorCode);
                Console.WriteLine("Dev_error_message:" + r.ErrorMessage);

                return r.Data;
            }

            
            return places;
        }

        public async Task<PlaceItem> GetPlace(int id)
        {
            Console.WriteLine("\n\n\n------------------\n\n\n------------------Code a tester : GetPlace--------------------\n\n\n------------------\n\n\n");
            HttpClient client = new HttpClient();
            PlaceItem place = null;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url + "/places/"+id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "__access__token__");

            var uri = new Uri(string.Format(url + "place/" + id, string.Empty));
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Response<PlaceItem> r = JsonConvert.DeserializeObject<Response<PlaceItem>>(content);
                Console.WriteLine("Dev_is_sucess:" + r.IsSuccess);
                Console.WriteLine("Dev_error_code:" + r.ErrorCode);
                Console.WriteLine("Dev_error_message:" + r.ErrorMessage);

                return r.Data;
            }


            return place;
        }


        public async Task<List<PlaceItemSummary>> RefreshDataAsync()
        {

            var uri = new Uri(string.Format(url + "places", string.Empty));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                AllData<List<PlaceItemSummary>> d = JsonConvert.DeserializeObject<AllData<List<PlaceItemSummary>>>(content);
                Console.WriteLine("Dev_is_sucess:" + d.is_success);
                Console.WriteLine("Dev_error_code:" + d.error_code);
                Console.WriteLine("Dev_error_message:" + d.error_message);
                return d.data;


            }

            return null;



        }

        public async Task<PlaceItem> PlaceItemDataAsync(int id)
        {

            var uri = new Uri(string.Format(url + "places/" + id, string.Empty));

            Console.WriteLine("Dev_BefResp:");

            var response = await client.GetAsync(uri);

            Console.WriteLine("Dev_statusCode:" + response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                AllData<PlaceItem> d = JsonConvert.DeserializeObject<AllData<PlaceItem>>(content);
                Console.WriteLine("Dev_is_sucess:" + d.is_success);
                Console.WriteLine("Dev_error_code:" + d.error_code);
                Console.WriteLine("Dev_error_message:" + d.error_message);

                return d.data;
            }

            return null;

        }

        public void SendCommentDataAsync(int id, String comment)
        {

            var uri = new Uri(string.Format(url + "places/" + id + "/comments", string.Empty));

            Console.WriteLine("Dev_SCDBefResp:");

            CreateCommentRequest ccr = new CreateCommentRequest();
            ccr.Text = comment;
            var jsonRequest = JsonConvert.SerializeObject(ccr);

            try
            {
                var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");
                var response = client.PostAsync(uri, content).Result;

                Console.WriteLine("Dev_SCDResponse:" + response);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*public async Task<bool> SendPlaceDataAsync(String nom, String description, string lattitude, string longitude, LoginResult lr, bool camera)
        {

            var uri = new Uri(string.Format(url + "places/", string.Empty));



            double lattitudeD = double.Parse(lattitude, System.Globalization.CultureInfo.InvariantCulture);
            double longitudeD = double.Parse(longitude, System.Globalization.CultureInfo.InvariantCulture);


            CreatePlaceRequest cpr = new CreatePlaceRequest();
            cpr.Title = nom;
            cpr.Description = description;

            ImageItem iItem = await UploadImage(lr, camera);
            cpr.ImageId = iItem.Id;

            //cpr.ImageId = 37;
            cpr.Latitude = lattitudeD;
            cpr.Longitude = longitudeD;

            Console.WriteLine("Dev_SendPlaceData:lattitudeD:" + lattitudeD + "|longitudeD:" + longitudeD + "|imageId:" + 37);

            var jsonRequest = JsonConvert.SerializeObject(cpr);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");


            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", lr.AccessToken);
            request.Content = content;
            HttpResponseMessage response = await client.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Dev_RDResponse:" + result);
            Console.WriteLine("Dev_RDStatusCode:" + response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                Response r = JsonConvert.DeserializeObject<Response>(result);
                Console.WriteLine("Dev_is_sucess:" + r.IsSuccess);
                Console.WriteLine("Dev_error_code:" + r.ErrorCode);
                Console.WriteLine("Dev_error_message:" + r.ErrorMessage);

                return true;
                /*if (d.is_success)
                {

                    //return d.data;
                }

            }
            else
            {
                Debugger.Break();

            }

            return false;
            //return null;



        }

        public async Task<Stream> SendPicture(bool camera)
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                //Supply media options for saving our photo after it's taken.
                var pictureMediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Receipts",
                    Name = $"{DateTime.UtcNow}.jpg",
                    PhotoSize = PhotoSize.Small

                };

                var galleryMediaOptions = new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = PhotoSize.Small

                };

                // Take a photo of the business receipt.
                MediaFile file;
                if (camera)
                {
                    file = await CrossMedia.Current.TakePhotoAsync(pictureMediaOptions);

                }
                else
                {
                    file = await CrossMedia.Current.PickPhotoAsync(galleryMediaOptions);

                }
                Console.WriteLine("picture:");
                Console.WriteLine("picture:" + file);

                var stream = file.GetStream();
                file.Dispose();
                return stream;

                //return file;
            }

            return null;
        }

        private byte[] GetImageStreamAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public async Task<ImageItem> UploadImage(LoginResult lr, bool camera)
        {
            Stream mf = await SendPicture(camera);
            byte[] imageData = GetImageStreamAsBytes(mf);
            //byte[] imageData = await client.GetByteArrayAsync("https://bnetcmsus-a.akamaihd.net/cms/blog_header/x6/X6KQ96B3LHMY1551140875276.jpg");
            //byte[] imageData = mf.GetStream;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/images");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", lr.AccessToken);

            MultipartFormDataContent requestContent = new MultipartFormDataContent();

            var imageContent = new ByteArrayContent(imageData);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            // Le deuxième paramètre doit absolument être "file" ici sinon ça ne fonctionnera pas
            requestContent.Add(imageContent, "file", "file.jpg");

            request.Content = requestContent;

            HttpResponseMessage response = await client.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Dev_RDResponse:" + result);
            Console.WriteLine("Dev_RDStatusCode:" + response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Image uploaded!");
                Response<ImageItem> r = JsonConvert.DeserializeObject<Response<ImageItem>>(result);
                Console.WriteLine("Dev_is_sucess:" + r.IsSuccess);
                Console.WriteLine("Dev_error_code:" + r.ErrorCode);
                Console.WriteLine("Dev_error_message:" + r.ErrorMessage);

                return r.Data;
            }
            else
            {
                Debugger.Break();
                return null;
            }
        }*/

    }


}


