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


        public VotingController(IAuthorizer authorizer, IContentManager contentManager)
        {
            _authorizer = authorizer;
            _contentManager = contentManager;

            T = NullLocalizer.Instance;

        }


        public HttpResponseMessage Post(int voteId, int answerId)
        {
            var voteErrorMessage = "There is a problem with your vote. Please try again, or contact the site admin.";
            if (voteId == null) return Request.CreateResponse<string>(HttpStatusCode.NotFound, voteErrorMessage);
            if (answerId == null) return Request.CreateResponse<string>(HttpStatusCode.NotFound, voteErrorMessage);

            var pollsQuestionPart = _contentManager.Get<PollsPart>(voteId);

            IList<PollAnswer> answers = PollsAnswerSerializerHelper.DeserializeAnswerList(pollsQuestionPart.SerializedAnswers);
            answers[answerId].VoteCount++;
            pollsQuestionPart.SerializedAnswers = PollsAnswerSerializerHelper.SerializeAnswerList(answers);

            return Request.CreateResponse<string>(HttpStatusCode.OK, pollsQuestionPart.SerializedAnswers);
        }
    }
}