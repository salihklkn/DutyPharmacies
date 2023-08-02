using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DutyPharmacies
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NGaF1cWGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEZjUX9ccXRUQGRfWERzXg==");
			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
