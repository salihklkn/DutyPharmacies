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
namespace DutyPharmacies
{
	public partial class MainPage : ContentPage
	{
		ObservableCollection<Cityes> cities = new ObservableCollection<Cityes>();
		ObservableCollection<Districts> ditricts = new ObservableCollection<Districts>();
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

			img_map.Markers.Add(new MapMarker() 
			{ 
				Label = "test1",
				Latitude = "40.997092",
				Longitude = "29.052004",
			});

			img_map.Markers.Add(new MapMarker()
			{
				Label = "test2",
				Latitude = "41.012504",
				Longitude = "29.014934",
			});

			img_map.Markers.Add(new MapMarker()
			{
				Label = "test2",
				Latitude = "41.005745",
				Longitude = "28.969475",
			});
		}

		private void cityPicker_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
		{
			DistrictsPicker.ItemsSource = new ObservableCollection<Districts>();
			ditricts.Clear();
			var selectedCity = e.AddedItems[0] as Cityes;

			foreach (Districts item in selectedCity.Districts)
			{
				ditricts.Add(item);
			}
			DistrictsPicker.ItemsSource = ditricts;
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			stk_dropdown.IsVisible = !stk_dropdown.IsVisible;
		}

		private void DistrictsPicker_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
		{
			stk_dropdown.IsVisible = !stk_dropdown.IsVisible;
			stk_map.IsVisible = true;
		}
	}
}
 