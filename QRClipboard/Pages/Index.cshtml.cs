using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QRCoder;

namespace QRClipboard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public string GroupId { get; set; }
        public string QRUrl { get; set; }
        public string QRCode { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Guid guid;
            if (!string.IsNullOrEmpty(Id) && Guid.TryParse(Id, out guid))
            {
                GroupId = guid.ToString();
            }
            else
            {
                GroupId = Guid.NewGuid().ToString();
            }

            QRUrl = Request.Scheme + "://" + Request.Host + "/" + GroupId;

            var qrGenerator = new QRCodeGenerator();
            var urlPayload = new PayloadGenerator.Url(QRUrl);
            var qrCodeData = qrGenerator.CreateQrCode(urlPayload, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new Base64QRCode(qrCodeData);
            QRCode = qrCode.GetGraphic(4);
        }
    }
}
