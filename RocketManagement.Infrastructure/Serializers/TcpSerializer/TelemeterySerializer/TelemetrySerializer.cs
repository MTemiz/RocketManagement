using System.Runtime.InteropServices;

namespace RocketManagement.Infrastructure.Serializers.TcpSerializer.TelemeterySerializer
{
    public class TelemetrySerializer : ITcpSerializer<Telemetry>
    {
        public Telemetery Serialize<Telemetery>(byte[] rawData)
        {
            Telemetery result = default;

            RespectEndianness(typeof(Telemetery), rawData);

            GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);

            try
            {
                IntPtr rawDataPtr = handle.AddrOfPinnedObject();

                result = (Telemetery)Marshal.PtrToStructure(rawDataPtr, typeof(Telemetery));
            }
            finally
            {
                handle.Free();
            }

            return result;
        }

        private void RespectEndianness(Type type, byte[] data)
        {
            var fields = type.GetFields().Where(f => f.IsDefined(typeof(EndianAttribute), false))
                .Select(f => new
                {
                    Field = f,
                    Attribute = (EndianAttribute)f.GetCustomAttributes(typeof(EndianAttribute), false)[0],
                    Offset = Marshal.OffsetOf(type, f.Name).ToInt32()
                }).ToList();

            foreach (var field in fields)
            {
                if ((field.Attribute.Endianness == Endianness.BigEndian && BitConverter.IsLittleEndian) ||
                    (field.Attribute.Endianness == Endianness.LittleEndian && !BitConverter.IsLittleEndian))
                {
                    Array.Reverse(data, field.Offset, Marshal.SizeOf(field.Field.FieldType));
                }
            }
        }
    }
}
