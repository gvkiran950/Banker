using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace BankerPro.Models
{

    public class MapData
    {
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public Position Position { get; set; }
        public MapLocation Location { get; set; }
    }

    public class MapPosition
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public class MapLocation
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

}
