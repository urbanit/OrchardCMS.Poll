using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.UI.Admin;
using Urbanit.Polls.Models;

namespace Urbanit.Polls.Controllers
{
    [Admin]
    public class AdminController : Controller
    {
        private readonly IContentManager _contentManager;
        private readonly IOrchardServices _orchardServices;


        public Localizer T { get; set; }


        public AdminController(IOrchardServices orchardServices)
        {

            _contentManager = orchardServices.ContentManager;
            _orchardServices = orchardServices;

            T = NullLocalizer.Instance;
        }


        public ViewResult Index()
        {
            return View(_orchardServices.New.ViewModel(
                Questions: _contentManager.Query<PollsPart>().List()));
        }
    }
}