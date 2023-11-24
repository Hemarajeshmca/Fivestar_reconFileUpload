using Microsoft.AspNetCore.Mvc;

namespace Recon_Filedownload.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string GetPost_data(Stream _data)
        {
            StreamReader reader = new StreamReader(_data);
            string post_data = reader.ReadToEnd();
            return post_data;
        }
    }
}
