using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Exceptions;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.UI.Notify;
using Orchard.Widgets.Models;
using Orchard.Widgets.Services;
using Urbanit.Polls.Constants;
using Urbanit.Polls.Models;
using Urbanit.Polls.ViewModels;

namespace Urbanit.Polls.Controllers
{
    public class PollsAdminController : Controller, IUpdateModel
    {
        private readonly IContentManager _contentManager;
        private readonly IOrchardServices _orchardServices;
        private readonly ITransactionManager _transactionManager;
        private readonly INotifier _notifier;
        private readonly IWidgetsService _widgetsService;


        public Localizer T { get; set; }
        

        public PollsAdminController(IOrchardServices orchardServices, IWidgetsService widgetsService)
        {
            _orchardServices = orchardServices;
            _contentManager = _orchardServices.ContentManager;
            _transactionManager = orchardServices.TransactionManager;
            _notifier = orchardServices.Notifier;
            _widgetsService = widgetsService;

            T = NullLocalizer.Instance;
        }

        public IEnumerable<PollsContentPart> GetPolls()
        {
            var questions = _contentManager.Query<PollsContentPart>().List();

            return questions;
        }

        public ContentItem GetPollsWidget(int id)
        {
            if (id == 0)
            {
                ContentItem item = _contentManager.New(PollsContentTypes.PollWidget);

                var part = item.Parts.FirstOrDefault(p => p.GetType() == typeof(WidgetPart));

                if (part != null)
                {
                    var widgetPart = part.As<WidgetPart>();
                    var availableLayers = _widgetsService.GetLayers();
                    var availableZones = _widgetsService.GetZones();
                    int widgetPosition = _widgetsService.GetWidgets().Count(widget => widget.Zone == widgetPart.Zone) + 1;
                    widgetPart.Position = widgetPosition.ToString(CultureInfo.InvariantCulture);

                    widgetPart.LayerPart = widgetPart.LayerPart ?? availableLayers.First();
                    widgetPart.Zone = widgetPart.Zone ?? availableZones.First();
                }
                return item;
            }
            return _contentManager.Get(id);
        }

        public ViewResult Index()
        {
            return View(new PollsIndexViewModel
            {
                Questions = GetPolls()
            });
        }

        public ActionResult PollsDashboard(int votingId = 0)
        {
            if (!IsAuthorized())
            {
                return new HttpUnauthorizedResult();
            }
            try
            {
                ContentItem voting = GetPollsWidget(votingId);

                if (voting == null)
                {
                    return new HttpNotFoundResult();
                }

                return PollsDashboardShapeResult(voting);
            }
            catch (Exception ex)
            {
                if (ex.IsFatal()) throw;

                _notifier.Information(T("Unable to display Polls Dashboard. Exception: {0}", ex.Message));

                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("PollsDashboard")]
        public ActionResult PollsDashboardPost(int votingId = 0)
        {
            if (!IsAuthorized())
            {
                return new HttpUnauthorizedResult();
            }

            try
            {
                ContentItem voting = GetPollsWidget(votingId);

                if (voting == null)
                {
                    return new HttpNotFoundResult();
                }

                if (votingId == 0)
                {
                    _contentManager.Create(voting);
                }

                _contentManager.UpdateEditor(voting, this);

                if (!ModelState.IsValid)
                {
                    _transactionManager.Cancel();

                    return PollsDashboardShapeResult(voting);
                }

                _notifier.Information(T("Polls was successfully saved."));

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _notifier.Information(T("Unable to save Polls. Exception: {0}", ex.Message));

                return RedirectToAction("Index");
            }
        }

        private ShapeResult PollsDashboardShapeResult(ContentItem item)
        {
            dynamic itemEditorShape = _contentManager.BuildEditor(item);

            dynamic editorShape = _orchardServices.New.PollsDashboard(EditorShape: itemEditorShape);

            return new ShapeResult(this, editorShape);
        }

        private bool IsAuthorized()
        {
            return _orchardServices.Authorizer.Authorize(Permissions.PollsManagePermission, T("Cannot view voting details."));
        }

        #region IUpdateModel Members

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.Text);
        }

        #endregion
    }
}