using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFullStackTeam.CvPdfGenerator
{
    public interface ITemplateService
    {
        Task<string> RenderViewAsync<TViewModel>(string templateFileName, TViewModel model);
    }
}
