namespace Domain.Services
{
    public interface IUploadImageService
    {
        string UploadImage(byte[] bytes);
    }
}