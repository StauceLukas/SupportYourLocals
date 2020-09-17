using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net;
using System.Xml.Linq;
using Geocoding.Microsoft;
using Geocoding;
using SuppLocals.Services;
using System.Numerics;

namespace SuppLocals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public List<Service> servicesList = new List<Service>();

        public MainWindow()
        {
            //By default
            InitializeComponent();

            servicesList = new List<Service>();

            //Activates the + and – keys to allow the user to manually zoom in and out of the map
            myMap.Focus();

            //Add Service Types to the ComboBox
            selectedServiceCreate.ItemsSource = Enum.GetValues(typeof(ServiceType));
            selectedServiceCreate.SelectedIndex = 0;

            //Add Filters
            List<String> types = new List<String>();
            types.Add("ALL");
            foreach (var type in Enum.GetValues(typeof(ServiceType)))
            {
                types.Add(type.ToString());
            }

            filterListBox.ItemsSource = types;
            filterListBox.SelectedIndex = 0;

            updateServiceListAndMap(null, null);

        }

        public async void addPushPin(object sender, RoutedEventArgs e)
        {
            Service newService = null;
            Pushpin pushPin = new Pushpin();

            Geocoding.IGeocoder geocoder = new BingMapsGeocoder("vuOU7tN47KBhly1BAyhi~SKpEroFcVqMGYOJVSj-2HA~AhGXS-dV_H6Ofvn920LLMyvxfUUaLfjpZTD54fSc3WO-qRE7x6225O22AP_0XjDn");
            IEnumerable<Address> addresses = await geocoder.GeocodeAsync(addressText.Text);

            double lati = addresses.First().Coordinates.Latitude;
            double longi = addresses.First().Coordinates.Longitude;

            pushPin.Location = new Microsoft.Maps.MapControl.WPF.Location(lati , longi);
            myMap.Children.Add(pushPin);
            myMap.Center = pushPin.Location;

 
            switch (selectedServiceCreate.SelectedIndex)
            {
                //Food
                case 0:
                    {
                        newService = new FoodService(addressText.Text, lati, longi);
                        pushPin.Background = newService.color;
                        break;
                    }
                //Car Repair
                case 1:
                    {
                        newService = new CarRepairService(addressText.Text, lati , longi );
                        pushPin.Background = newService.color;
                        break;
                    }
                //Other
                case 2:
                    {
                        newService = new OtherService(addressText.Text, lati, longi);
                        pushPin.Background = newService.color;
                        break;
                    }
            }
            servicesList.Add(newService);
            updateServiceListAndMap(null, null);
        }

        private void updateServiceListAndMap(object sender, SelectionChangedEventArgs args)
        {
            myMap.Children.Clear();
            servicesListBox.Items.Clear();

            switch (filterListBox.SelectedIndex)
            {
                //ALL
                case 0:
                    {
                        foreach(Service service in servicesList)
                        {
                            Pushpin pushpin = new Pushpin();
                            pushpin.Location = new Microsoft.Maps.MapControl.WPF.Location(service.latitude, service.longitude);
                            pushpin.Background = service.color;
                            myMap.Children.Add(pushpin);
                            servicesListBox.Items.Add(service.address + "\nLat:" + service.latitude + "\nLong:" + service.longitude);
                        }
                        break;
                    }
                //Food
                case 1:
                    {
                        foreach (Service service in servicesList)
                        {

                            if(typeof(FoodService) == service.GetType() ) {
                                Pushpin pushpin = new Pushpin();
                                pushpin.Location = new Microsoft.Maps.MapControl.WPF.Location(service.latitude, service.longitude);
                                pushpin.Background = service.color;
                                myMap.Children.Add(pushpin);
                                servicesListBox.Items.Add(service.address + "\nLat:" + service.latitude + "\nLong:" + service.longitude);
                            }
                        }
                        break;
                    }
                //Car Repair
                case 2:
                    {
                        foreach (Service service in servicesList)
                        {

                            if (typeof(CarRepairService) == service.GetType())
                            {
                                Pushpin pushpin = new Pushpin();
                                pushpin.Location = new Microsoft.Maps.MapControl.WPF.Location(service.latitude, service.longitude);
                                pushpin.Background = service.color;
                                myMap.Children.Add(pushpin);
                                servicesListBox.Items.Add(service.address + "\nLat:" + service.latitude + "\nLong:" + service.longitude);
                            }
                        }
                        break;
                    }
                //Other
                case 3:
                    {
                        foreach (Service service in servicesList)
                        {

                            if (typeof(OtherService) == service.GetType())
                            {
                                Pushpin pushpin = new Pushpin();
                                pushpin.Location = new Microsoft.Maps.MapControl.WPF.Location(service.latitude, service.longitude);
                                pushpin.Background = service.color;
                                myMap.Children.Add(pushpin);
                                servicesListBox.Items.Add(service.address + "\nLat:" + service.latitude + "\nLong:" + service.longitude);
                            }
                        }

                        break;
                    }
            }

        }
       
    }
}
