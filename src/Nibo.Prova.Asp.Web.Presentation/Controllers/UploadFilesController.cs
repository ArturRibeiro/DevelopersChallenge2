using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Nibo.Prova.Asp.Web.Presentation.Application.CommandHandlers.Transactions.Commands;
using Nibo.Prova.Asp.Web.Presentation.Application.Notifications;
using Nibo.Prova.Asp.Web.Presentation.Models.Upload;

namespace Nibo.Prova.Asp.Web.Presentation.Controllers
{
    public class UploadFilesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMediator _mediator;
        private readonly DomainNotificationHandler _domainNotification;

        public UploadFilesController(IHostingEnvironment webHostEnvironment, IMediator mediator,
            INotificationHandler<DomainNotification> notifications)
        {
            _hostingEnvironment = webHostEnvironment;
            _mediator = mediator;
            _domainNotification = (DomainNotificationHandler)notifications;
        }

        public IActionResult Index()
            => View();

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            var error = new ProcessingErrorViewModel();

            if (!CheckAllExtensions(files)) return View("Errors", error.AddError("a1"));

            if (!await ProcessUpload(files)) return View("Errors", error.AddError("There was an error uploading"));

            if (!await ExecuteUploadCommand()) return View("Errors", error.AddError(GetNotifications()));

            return RedirectToAction("Index", "Transactions");
        }

        #region Private Method

        private bool CheckAllExtensions(List<IFormFile> files)
            => files.Any(IsInvalidExtensionFile);

        private bool IsInvalidExtensionFile(IFormFile arg)
            => Path.GetExtension(arg.FileName) == ".ofx";

        private async Task<bool> ProcessUpload(List<IFormFile> files)
        {
            if (files.Count == 0) return false;
            try
            {
                foreach (var formFile in files)
                {
                    if (formFile.Length <= 0) continue;
                    var file = $@"{_hostingEnvironment.ContentRootPath}\Ofx\";

                    if (!Directory.Exists(file)) Directory.CreateDirectory(file);

                    file = $"{file}{formFile.FileName}";

                    using (var stream = new FileStream(file, FileMode.Create))
                        await formFile.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }


        private async Task<bool> ExecuteUploadCommand()
        {
            var originOfTheImport = Directory.GetFiles($@"{_hostingEnvironment.ContentRootPath}\Ofx", "*.ofx",
                SearchOption.TopDirectoryOnly);
            return await _mediator.Send(PrecessFileOFXCommand.Factory.Create(originOfTheImport));
        }

        private string[] GetNotifications()
        {
            if (_domainNotification.HasNotifications)
                return _domainNotification.GetNotifications()
                    .Select(x => x.Message).ToArray();

            return null;
        }

        #endregion
    }
}