using Orchard.ContentManagement.Handlers;
using Urbanit.Polls.Helpers;
using Urbanit.Polls.Models;

namespace Urbanit.Polls.Handlers
{
    public class PollsQuestionHandler : ContentHandler
    {
        public PollsQuestionHandler()
        {
            OnActivated<PollsContentPart>((context, part) => part.AnswersField.Loader(
                () => PollsAnswerSerializerHelper.DeserializeAnswerList(part.SerializedAnswers)));
        }
    }
}