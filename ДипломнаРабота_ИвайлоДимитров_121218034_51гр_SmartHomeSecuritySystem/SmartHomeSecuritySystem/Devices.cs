using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSecuritySystem
{
    internal class Devices
    {
        int deviceId;
        string deviceType;
        string deviceBrand;
        string deviceLocation;

        public Devices()
        {
        }
        public Devices(int deviceId, string deviceType, string deviceBrand, string deviceLocation)
        {
            this.DeviceId = deviceId;
            this.DeviceType = deviceType;
            this.DeviceBrand = deviceBrand;
            this.DeviceLocation = deviceLocation;
        }

        public int DeviceId { get => deviceId; set => deviceId = value; }
        public string DeviceType { get => deviceType; set => deviceType = value; }
        public string DeviceBrand { get => deviceBrand; set => deviceBrand = value; }
        public string DeviceLocation { get => deviceLocation; set => deviceLocation = value; }
    }
}
