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
using Syncfusion.SfMaps.XForms;
using DutyPharmacies.Common;

namespace DutyPharmacies
{
	public partial class MainPage : ContentPage
	{
		ObservableCollection<Cityes> cities = new ObservableCollection<Cityes>();
		ObservableCollection<Districts> ditricts = new ObservableCollection<Districts>();

		string selectedCityText;
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

			cityPicker.ItemsSource = cities;


		}

		private async void cityPicker_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
		{
			try
			{
				var selectedCity = e.AddedItems[0] as Cityes;

				selectedCityText = selectedCity.Name;

				await RequestCityDistrictMap(selectedCity.Name, string.Empty);


				DistrictsPicker.ItemsSource = new ObservableCollection<Districts>();
				ditricts.Clear();


				foreach (Districts item in selectedCity.Districts)
				{
					ditricts.Add(item);
				}
				DistrictsPicker.ItemsSource = ditricts;
			}
			catch (Exception ex)
			{

			}

		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			stk_dropdown.IsVisible = !stk_dropdown.IsVisible;
		}

		private async void DistrictsPicker_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
		{
			stk_dropdown.IsVisible = !stk_dropdown.IsVisible;
			stk_map.IsVisible = true;

			var selectedDist = e.AddedItems[0] as Districts;

			await RequestCityDistrictMap(selectedCityText, selectedDist.DistrictName);
		}

		public async Task RequestCityDistrictMap(string cityName, string distcrictName)
		{
			var options = new RestClientOptions(Consts.ApiUrl)
			{
				MaxTimeout = -1,
			};
			var client = new RestClient(options);


			RestRequest req = new RestRequest();

			if (string.IsNullOrEmpty(distcrictName) == true)
			{
				req = new RestRequest("/health/dutyPharmacy?il=" + cityName + "", Method.Get);
				
			}
			else
			{
				req = new RestRequest("/health/dutyPharmacy?ilce=" + distcrictName + "&il=" + cityName, Method.Get);
			}

			req.AddHeader("authorization", "apikey 2zKWWeULMALuZS51lChHr6:70sFGAjhMGTJbW69c4yosx");


			RestResponse response = await client.ExecuteAsync(req);

			PharmaciesResponse cityInfo = JsonConvert.DeserializeObject<PharmaciesResponse>(response.Content);

			string loc = cityInfo.result.First().loc;

			string[] locCoordinates = loc.Split(',');
			if (locCoordinates.Length == 2 && double.TryParse(locCoordinates[0], out double latitudeOutput) && double.TryParse(locCoordinates[1], out double longitudeOutput))
			{
				img_map.GeoCoordinates = new Point(latitudeOutput, longitudeOutput);
			}

			foreach (var item in cityInfo.result)
			{
				string location = item.loc;
				string[] coordinates = location.Split(',');


				string latitude = String.Empty;
				string longitude = String.Empty;

				if (coordinates.Length == 2)
				{
					latitude = coordinates[0].Trim();
					longitude = coordinates[1].Trim();
				}


				img_map.Markers.Add(new MapMarker()
				{
					Label = item.name + " " + "ECZANESİ",
					Latitude = latitude,
					Longitude = longitude,
				});
			}
		}
	}
}
