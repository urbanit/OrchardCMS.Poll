using System.Web;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Urbanit.Polls.Helpers;
using Urbanit.Polls.Models;

namespace Urbanit.Polls.Drivers
{
    public class PollsPartDriver : ContentPartDriver<PollsContentPart>
    {
        private readonly IOrchardServices _orchardServices;


        public PollsPartDriver(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }


        protected override DriverResult Display(PollsContentPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_PollsContent", () =>
                {
                    var userName = _orchardServices.WorkContext.CurrentUser;
                    var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

                    return shapeHelper.Parts_Polls(UserName: userName);
                });
        }

        protected override DriverResult Editor(PollsContentPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_PollsContent_Edit", () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/PollsContent",
                    Model: part,
                    Prefix: Prefix));
        }

        protected override DriverResult Editor(PollsContentPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            part.SerializedAnswers = PollsAnswerSerializerHelper.SerializeAnswerList(part.AnswerList);

            return Editor(part, shapeHelper);
        }
    }
}