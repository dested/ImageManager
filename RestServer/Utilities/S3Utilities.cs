using System.Collections.Generic;
using System.IO;
using System.Linq;
using Amazon.S3;
using Amazon.S3.Model;
using RestServer.Common;

namespace RestServer.Utilities
{
    public   class S3Utilities
    {
        private static IAmazonS3 _client;

        public S3Utilities()
        {
            _client = new AmazonS3Client(ApplicationConstants.AWS.AccessKey, ApplicationConstants.AWS.SecretKey,
                Amazon.RegionEndpoint.USWest2);

        }

        public IList<string> GetBuckets()
        {
            var buckets = new List<string>();
            using (_client)
            {
                var response = _client.ListBuckets();
                buckets.AddRange(response.Buckets.Select(bucket => bucket.BucketName));
            }
            return buckets;
        }

        public object GetObject(string bucketName, string keyName)
        {
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = keyName
            };

            using (_client)
            {
                using (var response = _client.GetObject(request))
                {
                    var reader = new StreamReader(response.ResponseStream);
                    return reader.ReadToEnd();
                }
            }
        }

        public byte[] DownloadStream(string bucketName, string keyName)
        {
            var getObjectRequest = new GetObjectRequest()
            {
                BucketName = bucketName,
                Key = keyName
            };

            var getObjectResponse = _client.GetObject(getObjectRequest);
            var s = getObjectResponse.ResponseStream;
            var buffer = new byte[16 * 1026];
            var retStream = new MemoryStream();
            int readStream;
            while ((readStream = s.Read(buffer, 0, buffer.Length)) > 0)
            {
                retStream.Write(buffer, 0, readStream);
            }
            return retStream.ToArray();
        }


        public class S3File
        {
            public string Name { get; set; }
            public string Url { get; set; }

            public S3File() { }

            public S3File(string name, string url)
            {
                this.Name = name;
                this.Url = url;
            }

            public string GetDisplayName()
            {
                return this.Name.Replace("/", "");
            }

            public string GetExtension()
            {
                var ext = "";
                var fileExtPos = this.Name.LastIndexOf(".");
                if (fileExtPos >= 0)
                    ext = this.Name.Substring(fileExtPos + 1, this.Name.Length - fileExtPos - 1);
                return ext;
            }
        }

        public S3File[] GetObjectsInBucket(string bucketName, string folder="")
        {
            var items = new List<S3File>();
            using (_client)
            {
                var request = new ListObjectsRequest();
                request.BucketName = bucketName; 
                if (!string.IsNullOrEmpty(folder))
                {
                    request.Prefix = folder;
                }


                do
                {
                    var response = _client.ListObjects(request);
                    // Process response.
                    foreach (var file in response.S3Objects)
                    {
                        if (file.Key.Length < 1) continue;
                        var url = "https://" + bucketName + ".s3.amazonaws.com/" + file.Key;
                        items.Add(new S3File(file.Key, url));
                    }

                    // If response is truncated, set the marker to get the next set of keys.
                    if (response.IsTruncated)
                    {
                        request.Marker = response.NextMarker;
                    }
                    else
                    {
                        request = null;
                    }
                } while (request != null);
            }
            return items.ToArray();
        }


    }
}