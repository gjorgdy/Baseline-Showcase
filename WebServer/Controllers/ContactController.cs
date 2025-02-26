using System.Diagnostics;
using Core;
using Core.Models;
using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebServer.Models;

namespace WebServer.Controllers;

public class ContactController : Controller
{
    
    private readonly HttpClient _httpClient;
    private readonly string _recaptchaSecret = DotEnv.Read()["RECAPTCHA_SECRET"];
    private readonly ILogger<HomeController> _logger;

    public ContactController(ILogger<HomeController> logger)
    {
        _httpClient = new HttpClient();
        // _httpClient.BaseAddress = new Uri(DotEnv.Read()["API_URL"]);
        _logger = logger;
    }

    [HttpGet]
    [Route("contact/{id:int}")]
    public IActionResult Index(int id)
    {
        return View(new MailFormModel(id));
    }

    [HttpGet]
    [Route("contact/success")]
    public IActionResult Success()
    {
        return View();
    }
    
    [HttpPost]
    [Route("contact/{id:int}")]
    public async Task<ActionResult> Index([FromForm] MailFormModel model, int id)
    {
        if (!await IsReCaptchaValid(Request.Form["g-recaptcha-response"]))
        {
            return View(new MailFormModel(id, false, "Recaptcha is invalid"));
        }
        
        if (!model.Validate()) return View(model);

        Baseline.MailService.SendMail(
            model.Email,
            "jordyreins@gmail.com",
            model.Subject,
            model.Body
        );
        return RedirectToAction("Success");
    }
    
    private async Task<bool> IsReCaptchaValid(string? recaptchaToken)
    {
        if (recaptchaToken == null)
        {
            return false;
        }
        
        var response = await _httpClient.PostAsync(
            $"https://www.google.com/recaptcha/api/siteverify?secret={_recaptchaSecret}&response={recaptchaToken}",
            null);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(jsonResponse)!;

        return result.success == "true";
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}