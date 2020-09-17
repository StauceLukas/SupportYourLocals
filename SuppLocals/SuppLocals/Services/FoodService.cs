using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace SuppLocals.Services
{
    public class FoodService : Service
    {
        public FoodService(String address, double latitude ,double longitude)
        {
            color = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            this.address = address;
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}
