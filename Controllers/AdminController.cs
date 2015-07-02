using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Exceptions;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.UI.Notify;
using Urbanit.Polls.Services;
using Urbanit.Polls.ViewModels;

namespace Urbanit.Polls.Controllers
{
    public class AdminController : Controller, IUpdateModel
    {
        private readonly IPollsManager _pollsManager;
        private readonly IContentManager _contentManager;
        private readonly IOrchardServices _orchardServices;
        private readonly ITransactionManager _transactionManager;
        private readonly INotifier _notifier;

        public Localizer T { get; set; }


        public AdminController(
            IOrchardServices orchardServices,
            IPollsManager pollsQuestionManager)
        {
            _orchardServices = orchardServices;
            _pollsManager = pollsQuestionManager;
            _contentManager = _orchardServices.ContentManager;
            _transactionManager = orchardServices.TransactionManager;
            _notifier = orchardServices.Notifier;

            T = NullLocalizer.Instance;
        }

        public ViewResult Index()
        {
            return View(new PollsIndexViewModel
            {
                Questions = _pollsManager.GetPolls()
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
                ContentItem voting = _pollsManager.GetPollsWidget(votingId);

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
                ContentItem voting = _pollsManager.GetPollsWidget(votingId);

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
            return _orchardServices.Authorizer.Authorize(Permissions.ManagePolls, T("Cannot view voting details."));
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