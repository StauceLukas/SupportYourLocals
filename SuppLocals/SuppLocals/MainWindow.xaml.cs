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

namespace SuppLocals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //By default
            InitializeComponent();

            //Our own methods
            InitMyMap();
        }

        private void InitMyMap()
        {
            //Activates the + and – keys to allow the user to manually zoom in and out of the map
            myMap.Focus();

        }

        public async void addPushPin(object sender, RoutedEventArgs e)
        {
            Geocoding.IGeocoder geocoder = new BingMapsGeocoder("vuOU7tN47KBhly1BAyhi~SKpEroFcVqMGYOJVSj-2HA~AhGXS-dV_H6Ofvn920LLMyvxfUUaLfjpZTD54fSc3WO-qRE7x6225O22AP_0XjDn");
            IEnumerable<Address> addresses = await geocoder.GeocodeAsync(addressText.Text);
            Pushpin pushpin = new Pushpin();
            pushpin.Location = new Microsoft.Maps.MapControl.WPF.Location(addresses.First().Coordinates.Latitude, addresses.First().Coordinates.Longitude);
            myMap.Children.Add(pushpin);

            adressesList.Items.Add(new String(addressText.Text + ".\nLat: " + addresses.First().Coordinates.Latitude + "\nLong: " + addresses.First().Coordinates.Longitude));

        }
    }
}
