using Orchard.ContentManagement.Handlers;
using Urbanit.Polls.Helpers;
using Urbanit.Polls.Models;

namespace Urbanit.Polls.Handlers
{
    public class PollsPartHandler : ContentHandler
    {
        public PollsPartHandler()
        {
            OnActivated<PollsPart>((context, part) => part.AnswersField.Loader(
                () => PollsAnswerSerializerHelper.DeserializeAnswerList(part.SerializedAnswers)));
        }
    }
}