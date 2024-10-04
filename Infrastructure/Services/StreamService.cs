namespace Infrastructure.Services
{
    public class StreamService : IStreamService
    {
        public MemoryStream CreateMemoryStream()
        {
            return new MemoryStream();
        }
    }
}
