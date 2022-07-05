using System.Runtime.InteropServices;

namespace RocketManagement.Infrastructure.Serializers.TcpSerializer.TelemeterySerializer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Telemetry
    {
        public byte packageStartByte;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string rocketSystemId;
        public byte packageNumber;
        public byte packageSize;
        public float altitude;
        public float speed;
        public float acceleration;
        public float thrust;
        public float temperature;
    }
}
