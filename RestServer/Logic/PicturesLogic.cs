using System;
using System.Threading.Tasks;
using RestServer.Modules;
using RestServer.Utilities;

namespace RestServer.Logic
{
    public class PicturesLogic
    {
        public static GetPicturesResponse GetPictures(GetPicturesRequest model)
        {
            S3Utilities s3 = new S3Utilities();
            var objects = s3.GetObjectsInBucket("image-manager-pictures");

            return new GetPicturesResponse()
            {
                Pictures=objects
            };
        }
    }
}