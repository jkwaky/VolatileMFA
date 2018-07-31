using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC
{
    class global
    {
        private static string domainName;
        private static Location coord;
        private static LocationCollection locations;

        private static bool net;
        private static bool gps;
        private static LocationCollection houston = new LocationCollection()
        {

                new Location(29.988989, -95.582433),
                new Location(29.989101, -95.582379),
                new Location(29.989185, -95.582478),
                new Location(29.989185, -95.582478),
                new Location(29.989716, -95.581992),
                new Location(29.989605, -95.581877),
                new Location(29.990213, -95.581535),
                new Location(29.990350, -95.581999),
                new Location(29.990285, -95.582023),
                new Location(29.990139, -95.582353),
                new Location(29.990390, -95.582567),
                new Location(29.990376, -95.582645),
                new Location(29.990478, -95.582739),
                new Location(29.990322, -95.583066),
                new Location(29.990590, -95.583260),
                new Location(29.990555, -95.583333),
                new Location(29.990644, -95.583400),
                new Location(29.990500, -95.583730),
                new Location(29.990987, -95.584024),
                new Location(29.990954, -95.583930),
                new Location(29.991003, -95.583922),
                new Location(29.991173, -95.583547),
                new Location(29.990873, -95.583353),
                new Location(29.990838, -95.583230),
                new Location(29.990973, -95.582844),
                new Location(29.990680, -95.582675),
                new Location(29.990636, -95.582541),
                new Location(29.990794, -95.582184),
                new Location(29.990423, -95.581969),
                new Location(29.990258, -95.581507),
                new Location(29.990437, -95.581459),
                new Location(29.990481, -95.581623),
                new Location(29.991213, -95.581403),
                new Location(29.991190, -95.581263),
                new Location(29.991292, -95.581247),
                new Location(29.991038, -95.580161),
                new Location(29.990070, -95.580475),
                new Location(29.990293, -95.581432),
                new Location(29.990295, -95.581427),
                new Location(29.990193, -95.581475),
                new Location(29.989589, -95.581816),
                new Location(29.989038, -95.581035),
                new Location(29.988432, -95.581593),
                new Location(29.988984, -95.582418)
        };
        private static LocationCollection vancouver = new LocationCollection()
        {
            new Location(45.613958, -122.501962),
            new Location(45.612727, -122.501899),
            new Location(45.612731, -122.500950),
            new Location(45.612978, -122.500960),
            new Location(45.613001, -122.500150),
            new Location(45.613470, -122.500166),
            new Location(45.613462, -122.500499),
            new Location(45.613984, -122.500531)
        };

        private static LocationCollection paloalto = new LocationCollection()
        {
               new Location(37.412394, -122.150357),
               new Location(37.411730, -122.150250),
               new Location(37.411806, -122.149317),
               new Location(37.411593, -122.149521),
               new Location(37.411480, -122.149432),
               new Location(37.411506, -122.148928),
               new Location(37.411421, -122.148912),
               new Location(37.411446, -122.148579),
               new Location(37.411531, -122.148595),
               new Location(37.411553, -122.148397),
               new Location(37.411084, -122.148327),
               new Location(37.411144, -122.147324),
               new Location(37.411246, -122.146428),
               new Location(37.412196, -122.146562),
               new Location(37.412128, -122.147453),
               new Location(37.412605, -122.147506),
               new Location(37.412563, -122.148386),
               new Location(37.413016, -122.148464),
               new Location(37.412982, -122.149451),
               new Location(37.412522, -122.149386)
            };
        public static LocationCollection PaloAlto
        {
            get => paloalto;
        }
        public static LocationCollection Vancouver
        {
            get => vancouver;
        }
        public static LocationCollection Houston
        {
            get => houston;
        }
        
        public static string DomainName { get => domainName; set => domainName = value; }
        public static LocationCollection Locations { get => locations; set => locations = value; }
        public static bool Net { get => net; set => net = value; }
        public static bool GPS { get => gps; set => gps = value; }
        public static Location Coord { get => coord; set => coord = value; }
    }
}
