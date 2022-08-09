using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLibrary
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private string _filePath = @$"{Directory.GetCurrentDirectory()}\Logs\";

        public RequestLoggerMiddleware(RequestDelegate Next)
        {
            next = Next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);

            } while (readChunkLength > 0);

            return textWriter.ToString();
        }

        /* -------------------------------------- GET REQUEST BODY METHOD BEGIN --------------------------------------------- */
        private async Task<string> GetRequestBody(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            string reqBody = ReadStreamInChunks(requestStream);
            context.Request.Body.Position = 0;
            return reqBody;
        }
        /* -------------------------------------- GET REQUEST BODY METHOD END --------------------------------------------- */


        public async Task InvokeAsync(HttpContext httpContext)
        {
            string requestText = await GetRequestBody(httpContext);

            //if (string.IsNullOrEmpty(requestText))
            //{
            //    await next.Invoke(httpContext);
            //    return;
            //}


            var originalBodyStream = httpContext.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            httpContext.Response.Body = responseBody;

            /* -------------------------------------- RESPONSE OBJECT CREATE BEGIN --------------------------------------------- */


            await next.Invoke(httpContext);// RESPONSE OCCURS HERE


            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            String responseText = await new StreamReader(httpContext.Response.Body, Encoding.UTF8).ReadToEndAsync();
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);


            /* -------------------------------------- RESPONSE OBJECT CREATE END --------------------------------------------- */



            /* -------------------------------------- LOGS STAT --------------------------------------------- */

            string requestLog = requestText;
            string path = httpContext.Request.Path;
            await Task.Run(() =>
            {
                using (StreamWriter writer = new StreamWriter(FilePathGenerator(_filePath), true))
                {
                    writer.WriteLine($"\n**************************** \n Date => {DateTime.Now} \n  RequestText => {requestText} \n ResponseText => {responseText} \n Path => {path} \n ****************************\n");
                }
            });

            /* -------------------------------------- LOGS END --------------------------------------------- */

            await responseBody.CopyToAsync(originalBodyStream);
        }
        private string FilePathGenerator(string filePath)
        {
            string willAddFilePath = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt";
            return filePath + willAddFilePath;
        }
    }

}
