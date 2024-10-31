using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
namespace Mendel.Core.Services.Cloudinary;



/*═══════════════════【 SERVICE 】═══════════════════*/
public record CloudinaryService(
ICloudinary Cloudinary
) : ICloudinaryService
{
	public async Task<string?> FindImageByFilename(string fileName)
	{
		var result = await Cloudinary.GetResourceAsync(fileName);
		return result.Url;
	}

	// public async Task<List<CloudinaryFile>> FindImagesWithKeyword(string name)
	// {
	//
	// 	var results = await Cloudinary.Search()
	// 		.Expression($"filename:*{name}* OR tags:{name}")
	// 		.SortBy("public_id", "desc")
	// 		.MaxResults(50)
	// 		.ExecuteAsync();
	//
	// return results.Resources
	// 		.Select(x => new CloudinaryFile(x.Url, x.PublicId)).ToList();
	// }

	public async Task<string> UploadImageRandomString(string imageURL, string fileName)
	{
		// var file = await Cloudinary.GetResourceAsync(fileName);
		//
		// if (file.Url is null)
		// {
			var uploadParams = new ImageUploadParams()
			{
				File = new FileDescription(@imageURL),
				Tags = fileName
			};

			var result = await Cloudinary.UploadAsync(uploadParams);
			return result.Url.ToString();
		}
		// else
		// {
		// 	return file.Url.ToString();
		// }
	// }

	public async Task<string> UploadImage(string imageURL, string fileName)
	{
		var file = await Cloudinary.GetResourceAsync(fileName);

		if (file.Url is null)
		{
			var uploadParams = new ImageUploadParams()
			{
				File = new FileDescription(fileName, @imageURL),
				PublicId = fileName
			};

			var result = await Cloudinary.UploadAsync(uploadParams);
			return result.Url.ToString();
		}
		else
		{
			return file.Url.ToString();
		}
	}

	public async Task<string> UploadImageFromBase64(string base64string, string fileName)
	{
		var file = await Cloudinary.GetResourceAsync(fileName);

		if (file.Url is null)
		{
			var uploadParams = new ImageUploadParams()
			{
				File = new FileDescription(fileName, "data:image/png;base64," + base64string),
				PublicId = fileName,
			};

			var result = await Cloudinary.UploadAsync(uploadParams);
			return result.Url.ToString();
		}
		else
		{
			return file.Url.ToString();
		}
	}

	public async Task DeleteImage(string url)
	{
		var publicId = url.Split('/').Last().Split('.').First();
		var result = await Cloudinary.DestroyAsync(new DeletionParams(publicId));
		//TODO: Check if it was successful
	}

	public async Task DeleteImageByPublicId(string publicId)
	{
		var result = await Cloudinary.DestroyAsync(new DeletionParams(publicId));
	}
}

/*═══════════════════【 INTERFACE 】═══════════════════*/
public interface ICloudinaryService
{
	Task<string?> FindImageByFilename(string fileName);
	Task<string> UploadImageRandomString(string imageURL, string fileName);
	Task<string> UploadImage(string imageURL, string fileName);
	Task<string> UploadImageFromBase64(string base64string, string fileName);
	Task DeleteImage(string url);
	Task DeleteImageByPublicId(string publicId);
	// Task<List<CloudinaryFile>> FindImagesWithKeyword(string name);
}