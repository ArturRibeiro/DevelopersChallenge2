using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.Prova.Asp.Web.Presentation.Models.Upload
{
    public class ProcessingErrorViewModel
    {
        public List<string> Errors { get; set; }

        public ProcessingErrorViewModel AddError(params string[] error)
        {
            this.Errors = error.ToList();
            return this;
        }
    }
}
