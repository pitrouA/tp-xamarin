using System.Collections.Generic;
using Exercice4.Models;
using Newtonsoft.Json;

namespace LiveCoding1.Models
{
	public class PlaceItem
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		
		[JsonProperty("title")]
		public string Title { get; set; }
		
		[JsonProperty("description")]
		public string Description { get; set; }
		
		[JsonProperty("image_id")]
		public int ImageId { get; set; }
		
		[JsonProperty("latitude")]
		public double Latitude { get; set; }
		
		[JsonProperty("longitude")]
		public double Longitude { get; set; }
		
		[JsonProperty("comments")]
		public List<CommentItem> Comments { get; set; }
	}
}