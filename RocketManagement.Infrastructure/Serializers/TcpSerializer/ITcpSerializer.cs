namespace RocketManagement.Infrastructure.Serializers.TcpSerializer
{
    public interface ITcpSerializer<T> where T : struct
    {
        T Serialize<T>(byte[] rawData);
    }
}
