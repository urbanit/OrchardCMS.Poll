using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Security;
using Orchard.Widgets.Services;
using Urbanit.Polls.Helpers;
using Urbanit.Polls.Models;

namespace Urbanit.Polls.Controllers.Api
{
    public class VotingController : ApiController
    {
        private readonly IAuthorizer _authorizer;
        private readonly IContentManager _contentManager;
        public Localizer T { get; set; }

        public VotingController(IAuthorizer authorizer, IContentManager contentManager, IWidgetsService widgetsService)
        {
            _authorizer = authorizer;
            _contentManager = contentManager;

            T = NullLocalizer.Instance;

        }

        public HttpResponseMessage Post(int voteId, int answerId)
        {
            var pollsQuestionPart = _contentManager.Get<PollsPart>(voteId);

            IList<Answer> answers = AnswerSerializerHelper.DeserializeAnswerList(pollsQuestionPart.SerializedAnswers);
            answers[answerId].VoteCount++;
            pollsQuestionPart.SerializedAnswers = AnswerSerializerHelper.SerializeAnswerList(answers);

            return Request.CreateResponse<string>(HttpStatusCode.OK, pollsQuestionPart.SerializedAnswers);
        }
    }
}