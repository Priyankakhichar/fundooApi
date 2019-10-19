////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "ImageCloudinary.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace CommonLayer
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Image cloudinary class
    /// </summary>
    public class ImageCloudinary
    {
        /// <summary>
        /// uploading image at cloudinary
        /// </summary>
        /// <param name="file"></param>
        /// <returns>returning the </returns>
        public string UploadImageAtCloudinary(IFormFile file)
        {
                ////name of the file
                var name = file.FileName;

                ////opening the file to read the content from file
                var stream = file.OpenReadStream();

                ////account information cloud name ,api key ,api secret
                Account account = new Account(
                        "priyankas-cloud",
                        "685742546695637",
                        "epl7e_EQNE_HiybiTZoFx9iTcRg");

                ////sending the account details to the cloudinary
                Cloudinary cloudinary = new Cloudinary(account);

                ////stroing the file parameters
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream)
                };

                ////uploading the details of file to the cloud
                var uploadResult = cloudinary.Upload(uploadParams);

                ////returning the url of the pic
                return uploadResult.Uri.ToString();
            }
    }
}
