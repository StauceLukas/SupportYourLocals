using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Media;

namespace SuppLocals
{
    public abstract class Service
    {
        //Constructor
        public Service()
        {
            color = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        // PushPin Color in the map
        public SolidColorBrush color;

        //Address
        public String address;

        //Latitude and Longitude
        public double latitude;
        public double longitude;

    }
}
