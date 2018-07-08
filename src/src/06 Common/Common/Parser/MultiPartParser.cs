
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// MultipartParser http://multipartparser.codeplex.com
    /// Reads a multipart form data stream and returns the filename, content type and contents as a stream.
    /// 2009 Anthony Super http://antscode.blogspot.com
    /// </summary>
namespace MyDiary.Common.Parser
{
    public class MultiPartParser
    {  
        #region PUBLIC PROPERTIES

        public bool Success
        {
            get;
            private set;
        }

        public string ContentType
        {
            get;
            private set;
        }

        public string Filename
        {
            get;
            private set;
        }

        public byte[] FileContents
        {
            get;
            private set;
        }

        #endregion 

        #region CONSTRUCTORS

        public MultiPartParser(Stream stream)
        {
            this.Parse(stream, Encoding.UTF8);
        }

        public MultiPartParser(Stream stream, Encoding encoding)
        {
            this.Parse(stream, encoding);
        }

        #endregion            
        
        #region PRIVATE METHODS

        private void Parse(Stream stream, Encoding encoding)
        {
            this.Success = false;

            // Read the stream into a byte array
            byte[] data = ToByteArray(stream);

            // Copy to a string for header parsing
            string content = encoding.GetString(data);

            // The first line should contain the delimiter
            int delimiterEndIndex = content.IndexOf("\r\n");

            if (delimiterEndIndex > -1)
            {
                string delimiter = content.Substring(0, content.IndexOf("\r\n"));

                // Look for Content-Type
                Regex re = new Regex(@"(?<=Content\-Type:)(.*?)(?=\r\n\r\n)");
                Match contentTypeMatch = re.Match(content);

                // Look for filename
                re = new Regex(@"(?<=filename\=\"")(.*?)(?=\"")");
                Match filenameMatch = re.Match(content);

                // Did we find the required values?
                if (contentTypeMatch.Success && filenameMatch.Success)
                {
                    // Set properties
                    this.ContentType = contentTypeMatch.Value.Trim();
                    this.Filename = filenameMatch.Value.Trim();

                    // Get the start & end indexes of the file contents
                    int startIndex = contentTypeMatch.Index + contentTypeMatch.Length + "\r\n\r\n".Length;

                    byte[] delimiterBytes = encoding.GetBytes("\r\n" + delimiter);
                    int endIndex = IndexOf(data, delimiterBytes, startIndex);

                    int contentLength = endIndex - startIndex;

                    // Extract the file contents from the byte array
                    byte[] fileData = new byte[contentLength];

                    Buffer.BlockCopy(data, startIndex, fileData, 0, contentLength);

                    this.FileContents = fileData;
                    this.Success = true;
                }
            }
        }

        private int IndexOf(byte[] searchWithin, byte[] serachFor, int startIndex)
        {
            int index = 0;
            int startPos = Array.IndexOf(searchWithin, serachFor[0], startIndex);

            if (startPos != -1)
            {
                while ((startPos + index) < searchWithin.Length)
                {
                    if (searchWithin[startPos + index] == serachFor[index])
                    {
                        index++;
                        if (index == serachFor.Length)
                        {
                            return startPos;
                        }
                    }
                    else
                    {
                        startPos = Array.IndexOf<byte>(searchWithin, serachFor[0], startPos + index);
                        if (startPos == -1)
                        {
                            return -1;
                        }
                        index = 0;
                    }
                }
            }

            return -1;
        }

        private byte[] ToByteArray(Stream stream)
        {
            //byte[] buffer = new byte[32768];
            byte[] buffer = new byte[stream.Length];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }

        #endregion

        //Getting byte[] from HttpContext.Current.Request.InputStream works 'fine' except one little thing: in the stream , I get not only binary data from the file user 
        //selected but also bunch of headers and encoded fields: ==> for this reason new third party class MultiPartParser.cs is used
        // MultiPartParser parser = new MultiPartParser(HttpContext.Current.Request.InputStream);
        //if (parser.Success)
        //{
        //    // Save the file to db
        //    Application.Services.Abstract.DTO.IImage image = new Application.Services.DTO.Image();
        //    image.UserImage = parser.FileContents;
        //    uploadImageId = _imageService.UploadImage(image);
        //    imageJson.UploadedImageId = uploadImageId;
        //}
        //else
        //{
        //    //System.Net.HttpStatusCode.UnsupportedMediaType,
        //    throw new WebException("The posted file was not recognised.", WebExceptionStatus.SendFailure);
        //}  
    }
}

