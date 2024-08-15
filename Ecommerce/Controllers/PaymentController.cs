using Ecommerce.Data;
using Ecommerce.Helper;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentController> _logger;
        private readonly VnPayLibrary _vnpayLibrary;
        private readonly ProductService _service;

        public PaymentController(IConfiguration configuration, ProductService service, ILogger<PaymentController> logger, VnPayLibrary vnpayLibrary)
        {
            _configuration = configuration;
            _logger = logger;
            _vnpayLibrary = vnpayLibrary;
            _service = service;
        }

        private string CreateVnpayUrl(Bill model)
        {
            string vnp_Returnurl = "https://yourwebsite.com/Payment/VnpayReturn"; // Your return URL
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; // VNPay sandbox URL
            string vnp_TmnCode = "3EQ71TA1"; // Your TMN code
            string vnp_HashSecret = "YG9FFV5I51VDX6WQK6WABKIPNN31HB3C"; // Your secret key


            // Add request data
            _vnpayLibrary.AddRequestData("vnp_Version", "2.1.0"); // Updated version
            _vnpayLibrary.AddRequestData("vnp_Command", "pay");
            _vnpayLibrary.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            _vnpayLibrary.AddRequestData("vnp_Amount", ((int)(model.amount)).ToString()); // Convert amount to integer in cents
            _vnpayLibrary.AddRequestData("vnp_CurrCode", "VND");
            _vnpayLibrary.AddRequestData("vnp_TxnRef", Guid.NewGuid().ToString()); // Unique transaction reference
            _vnpayLibrary.AddRequestData("vnp_OrderInfo", "Thanh toan don hang");
            _vnpayLibrary.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);

            // Get IP address - placeholder IP if not using actual IP retrieval
            string ipAddress = "127.0.0.1"; // Replace with actual IP retrieval logic if needed
            _vnpayLibrary.AddRequestData("vnp_IpAddr", ipAddress);

            // Add the current date and time
            _vnpayLibrary.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));

            // Create the request URL
            string paymentUrl = _vnpayLibrary.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            // Log the URL for debugging purposes
            _logger.LogInformation("VNPay URL created: " + paymentUrl);

            return paymentUrl;
        }


        public IActionResult SendToPaymen()
        {
           Bill bill =  HttpContext.Session.Get<Bill>(SettingKey.Bill_KEY);
            if (bill == null)
            {
                return BadRequest();
            }
            return Redirect(CreateVnpayUrl(bill));
        }
        public async Task<IActionResult> VnpayReturn()
        {
            foreach (string s in Request.Query.Keys)
            {
                if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                {
                    _vnpayLibrary.AddResponseData(s, Request.Query[s]);
                }
            }

            string vnp_HashSecret = "YG9FFV5I51VDX6WQK6WABKIPNN31HB3C";
            bool checkSignature = _vnpayLibrary.ValidateSignature(Request.Query["vnp_SecureHash"], vnp_HashSecret);

            if (checkSignature)
            {
               Bill billInsert = HttpContext.Session.Get<Bill>(SettingKey.Bill_KEY);
                  await _service.Chackout(billInsert);
                return View("Success");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
