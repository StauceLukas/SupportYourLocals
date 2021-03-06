﻿using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace SuppLocals.Services
{
    public class CarRepairService : Service
    {
        public CarRepairService(String address, Location location)
        {
            color = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            this.address = address;
            this.location = location;
        }
    }
}
