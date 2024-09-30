using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileDownloadPrototype.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        // Inject HttpClient and ILogger via constructor (dependency injection)
        public HomeController(HttpClient httpClient, ILogger<HomeController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;               //why ILogger
        }

        public IActionResult Index()
        {
            return View();
        }

        // Action to download a file from a 3rd-party API --> in frontend it fires to /DownloadFile in the forms
        [HttpGet]
        public async Task<IActionResult> DownloadFile
            //(int fileId)      --> for seperate baseURL and file url with id
            ()
        {
            
            //var fileUrl = "https://example.com/path-to-your-file";
            var fileUrl = "https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf\n";

            //USING BASE URL : 
            var baseURL = "https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/";

            try
            {
                // Send HTTP request to get the file with headers-only option to stream
                var response = await _httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Get content type and file name from response headers or URL
                var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
                var fileName = Path.GetFileName(fileUrl); // Extracting from URL or set dynamically

                // Stream the file to reduce memory overhead
                var stream = await response.Content.ReadAsStreamAsync();

                // Return the file for download
                return File(stream, contentType, fileName);
            }
            catch (Exception ex)
            {
                // Log the error with detailed information
                _logger.LogError(ex, "File download failed");

                // Show a generic error message to the user
                ViewBag.Error = "File download failed. Please try again later.";
                return View("Error");
            }
        }
    }
}
