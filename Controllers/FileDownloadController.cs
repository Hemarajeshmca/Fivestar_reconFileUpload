using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.IO.Compression;
using System.Net.Mime;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace Recon_Filedownload.Controllers
{
    
    public class FileDownloadController : Controller
    {
        CommonController objcommon = new CommonController();
        Fileservice _fileService = new Fileservice();

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("filesss")]
        public async Task<ActionResult> DownloadFiles(FileDownload_modal filedownload)
        {
            
            // ... code for validation and get the file
            string filePath = filedownload.jobName;
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return  File(bytes, contentType, Path.GetFileName(filePath));
        }

        [HttpPost("filestrs")]


        public Task<byte[]> DownloadFileAsync([FromBody] FileDownload_modal filedownload)
        //public async Task<ActionResult> Dow
        //nloadFileAsync(FileDownload_modal filedownload)
        {

            // ... code for validation and get the file
            //  string filePath = "D:/DMSdinesh/ReconfiledownloadTest/recontest";// "D:/test.csv";//filedownload.Job_Name;
            string filePath = filedownload.filePath + filedownload.jobGid.ToString() + ".csv";
           // string filePath = filedownload.filePath + ".txt";

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytesAsync(filePath);



            return bytes;//File(bytes, contentType, Path.GetFileName(filePath));
            }

        [HttpPost("Singlefiles")]
         public async Task<IActionResult> Download([FromBody] FileDownload_modal filedownload)
        {
            var filename = "D:/DMSdinesh/ReconfiledownloadTest/file/776.csv";

           // string filename = filedownload.filePath + ".csv";

            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            //[] bytes = memory.ToArray();
            //return bytes;

           return File(memory, "text/csv", Path.GetFileName(filename));

            
        }
        [HttpPost("files")]
        public IActionResult Downloads([FromBody] FileDownload_modal filedownload)
        {
            string jobid =filedownload.jobGid.ToString();
            string subDirectory = filedownload.filePath;
           // string subDirectory = "MyFiles";
            try
            {
                var (fileType, archiveData, archiveName) = _fileService.DownloadFiles(subDirectory,jobid);

                return File(archiveData, fileType, archiveName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        [HttpGet("file")]
        public async Task<ActionResult> DownloadFiles(String filePath)
        {
            // ... code for validation and get the file
            //string filePath = "D:/test.csv";
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }
    }
}
