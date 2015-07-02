using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Urbanit.Polls.Helpers;
using Urbanit.Polls.Models;

namespace Urbanit.Polls.Handlers
{
    public class PollsQuestionHandler : ContentHandler
    {
        public PollsQuestionHandler(IRepository<PollsPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));

            OnActivated<PollsPart>((context, part) => part.AnswersField.Loader(
                () => AnswerSerializerHelper.DeserializeAnswerList(part.SerializedAnswers)));
        }
    }
}