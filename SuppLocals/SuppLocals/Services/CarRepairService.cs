using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace SuppLocals.Services
{
    public class CarRepairService : Service
    {
        public CarRepairService(String address, double latitude, double longitude)
        {
            color = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            this.address = address;
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}
