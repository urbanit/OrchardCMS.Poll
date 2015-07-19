using System.Net;
using System.Net.Http;
using System.Web.Http;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Security;
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


        public HttpResponseMessage Post(int voteId = 0, int answerId = 0)
        {
            var voteErrorMessage = "There is a problem with your vote. Please try again, or contact the site admin.";

            var pollsQuestionPart = _contentManager.Get<PollsPart>(voteId);
            if (pollsQuestionPart == null) return Request.CreateResponse<string>(HttpStatusCode.NotFound, voteErrorMessage);

            var answers = PollsAnswerSerializerHelper.DeserializeAnswerList(pollsQuestionPart.SerializedAnswers);

            var answerCount = answers.Count;
            if (answerId<0 && answerId>answerCount)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, voteErrorMessage);
            }
            answers[answerId].VoteCount++;
            pollsQuestionPart.SerializedAnswers = PollsAnswerSerializerHelper.SerializeAnswerList(answers);

            return Request.CreateResponse<string>(HttpStatusCode.OK, pollsQuestionPart.SerializedAnswers);
        }
    }
}