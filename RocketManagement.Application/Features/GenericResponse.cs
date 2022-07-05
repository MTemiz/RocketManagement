namespace RocketManagement.Application.Features
{
    public sealed class GenericResponse<T> : BaseResponse where T : class, new()
    {
        public T Model { get; set; }
    }
}
