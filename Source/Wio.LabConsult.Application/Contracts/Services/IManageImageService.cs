using Wio.LabConsult.Application.Models.ImageManagement;

namespace Wio.LabConsult.Application.Contracts.Services;

public interface IManageImageService
{
    Task<ImageResponse> UploadImage(ImageData imageData);
}
