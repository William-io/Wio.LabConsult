using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System.Net;
using Wio.LabConsult.Application.Contracts.Services;
using Wio.LabConsult.Application.Models.ImageManagement;

namespace Wio.LabConsult.Infrastructure.ImageCloudinary;

public class ManageImageService : IManageImageService
{
    public CloudinarySettings _cloudinarySettings;

    public ManageImageService(IOptions<CloudinarySettings> cloudinarySettings)
    {
        _cloudinarySettings = cloudinarySettings.Value;
    }

    public async Task<ImageResponse> UploadImage(ImageData imageData)
    {
        //Implemente o codigo para upload de imagem no Cloudinary
        var account = new Account(
            _cloudinarySettings.CloudName,
            _cloudinarySettings.ApiKey,
            _cloudinarySettings.ApiSecret
        );

        var cloudinary = new Cloudinary(account);

        if (imageData.ImageStream == null || string.IsNullOrEmpty(imageData.Name))
        {
            throw new ArgumentException("Dados de imagem inválidos");
        }

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(imageData.Name, imageData.ImageStream)
        };

        var uploadResult = await cloudinary.UploadAsync(uploadParams);

        if(uploadResult.StatusCode == HttpStatusCode.OK)
        {
            return new ImageResponse
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.Url.ToString()
            };
        }

        throw new Exception("Erro ao fazer upload da imagem");
    }
}
