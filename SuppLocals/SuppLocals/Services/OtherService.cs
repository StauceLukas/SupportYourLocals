using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Media;

namespace SuppLocals.Services
{
    public class OtherService : Service
    {
        public OtherService(String address, double latitude, double longitude)
        {
            color = new SolidColorBrush(Color.FromRgb(0, 0, 255));
            this.address = address;
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}
