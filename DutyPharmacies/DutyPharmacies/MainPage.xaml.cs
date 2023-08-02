using DutyPharmacies.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DutyPharmacies
{
	public partial class MainPage : ContentPage
	{
		ObservableCollection<Cityes> cities = new ObservableCollection<Cityes>();
		public MainPage()
		{
			InitializeComponent();

			
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();


			var options = new RestClientOptions("http://salihkalkantest.site")
			{
				MaxTimeout = -1,
			};

			var client = new RestClient(options);
			var request = new RestRequest("/OtherProjects/Locations.json", Method.Get);
			RestResponse response = await client.ExecuteAsync(request);
			var citiesData = JsonConvert.DeserializeObject<CitiesListResponse>(response.Content);


			foreach (Cityes item in citiesData.data)
			{
				cities.Add(item);

			}

		}
	}
}
 