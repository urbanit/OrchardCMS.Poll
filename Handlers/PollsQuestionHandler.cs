using Orchard.ContentManagement.Handlers;
using Urbanit.Polls.Helpers;
using Urbanit.Polls.Models;

namespace Urbanit.Polls.Handlers
{
    public class PollsQuestionHandler : ContentHandler
    {
        public PollsQuestionHandler()
        {
            OnActivated<PollsPart>((context, part) => part.AnswersField.Loader(
                () => AnswerSerializerHelper.DeserializeAnswerList(part.SerializedAnswers)));
        }
    }
}