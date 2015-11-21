using RestServer.Common;
using RestServer.Common.Nancy;
using RestServer.Logic;
using RestServer.Utilities;

namespace RestServer.Modules
{
    public class PicturesModule : BaseModule
    {
        public PicturesModule():base("api/pictures")
        {
            Get["/"] = (_) =>
            {
                var model = ValidateRequest<GetPicturesRequest>();
                var response = PicturesLogic.GetPictures(model);
                return this.Success(response);
            };
        }

    }


    public class GetPicturesRequest
    {
    }
    public class GetPicturesResponse
    {
        public S3Utilities.S3File[] Pictures { get; set; }
    }

}