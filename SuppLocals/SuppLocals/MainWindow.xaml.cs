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
        //serviceList[0] - FOOD |  [1] - Car Repair |  [2] - OTHER
        public List<List<Service>> servicesList = new List<List<Service>>();

        public MainWindow()
        {
            //By default
            InitializeComponent();

            for (int i = 0; i < Enum.GetNames(typeof(ServiceType)).Length; i++)
            {
                servicesList.Add(new List<Service>());
            }
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

        private void buttonClick(object sender, RoutedEventArgs e)
        {
            if (sPan.Visibility == Visibility.Collapsed)
            {
                sPan.Visibility = Visibility.Visible;
                (sender as Button).Content = "☰";
            }
            else
            {
                sPan.Visibility = Visibility.Collapsed;
                (sender as Button).Content = "☰";
            }
        }

        private void hyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
           //true if the shell should be used when starting the process; false if the process should be created directly from the executable file.
           //The default is true on.NET Framework apps and false on.NET Core apps.
           Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
           e.Handled = true;
        }

        public async void addPushPin(object sender, RoutedEventArgs e)
        {
            
            if (!validFields())
            {
                MessageBox.Show("Please fill all text fields");
                return;
            }

            Service newService = null;
            Pushpin pushPin = new Pushpin();

            Geocoding.IGeocoder geocoder = new BingMapsGeocoder("vuOU7tN47KBhly1BAyhi~SKpEroFcVqMGYOJVSj-2HA~AhGXS-dV_H6Ofvn920LLMyvxfUUaLfjpZTD54fSc3WO-qRE7x6225O22AP_0XjDn");
            IEnumerable<Address> addresses = await geocoder.GeocodeAsync(addressText.Text);

            if(addresses.Count() == 0)
            {
                MessageBox.Show("We couldn't find that place, please try to clarify the address");
                return;
            }

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
                        servicesList[(int)ServiceType.FOOD].Add(newService);
                        break;
                    }
                //Car Repair
                case 1:
                    {
                        newService = new CarRepairService(addressText.Text, lati , longi );
                        pushPin.Background = newService.color;
                        servicesList[(int)ServiceType.CAR_REPAIR].Add(newService);
                        break;
                    }
                //Other
                case 2:
                    {
                        newService = new OtherService(addressText.Text, lati, longi);
                        pushPin.Background = newService.color;
                        servicesList[(int)ServiceType.OTHER].Add(newService);
                        break;
                    }
            }
            updateServiceListAndMap(null, null);
        }

        private bool validFields()   //True if valid fields , false - invalid
        {
            if(addressText.Text == "")
            {
                return false;
            }

            return true;
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
                        foreach (List<Service> serviceListTemp in servicesList)
                        {
                            foreach (Service service in serviceListTemp) {
                                Pushpin pushpin = new Pushpin();
                                pushpin.Location = new Microsoft.Maps.MapControl.WPF.Location(service.latitude, service.longitude);
                                pushpin.Background = service.color;
                                myMap.Children.Add(pushpin);
                                servicesListBox.Items.Add(service.address + "\nLat:" + service.latitude + "\nLong:" + service.longitude);
                            }
                        }
                        break;
                    }
                //Food
                case 1:
                    {
                        foreach (Service service in servicesList[(int)ServiceType.FOOD])
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
                        foreach (Service service in servicesList[(int)ServiceType.CAR_REPAIR])
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
                        foreach (Service service in servicesList[(int)ServiceType.OTHER])
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
       

        public void serviceChanged(object sender , SelectionChangedEventArgs args)
        {
            if(servicesListBox.SelectedIndex < 0)
            {
                return;
            }

            //ALL services
            if(filterListBox.SelectedIndex == 0)
            {
                List<Service> tempList = servicesList.SelectMany(x => x).ToList();
                myMap.Center = new Microsoft.Maps.MapControl.WPF.Location(tempList[servicesListBox.SelectedIndex].latitude, tempList[servicesListBox.SelectedIndex].longitude);
            }
            else
            {
                myMap.Center = new Microsoft.Maps.MapControl.WPF.Location(servicesList[(int)filterListBox.SelectedIndex -1 ][servicesListBox.SelectedIndex].latitude, servicesList[(int)filterListBox.SelectedIndex - 1][servicesListBox.SelectedIndex].longitude);

            }
        }

    }


}
