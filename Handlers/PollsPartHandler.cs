using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement.Handlers;
using Urbanit.Polls.Helpers;
using Urbanit.Polls.Models;

namespace Urbanit.Polls.Handlers
{
    public class PollsPartHandler : ContentHandler
    {
        public PollsPartHandler()
        {
            OnActivated<PollsPart>((context, part) =>
                {

                    part.AnswersField.Loader(
                    () =>
                    {
                        if (string.IsNullOrWhiteSpace(part.AnswerList) || part.AnswerList == "")
                        {
                            return new List<PollAnswer>();
                        }

                        if (string.IsNullOrEmpty(part.SerializedAnswers))
                        {
                            part.SerializedAnswers = PollsAnswerSerializerHelper.GenerateDefaultAnswerList(part.AnswerList);
                        }
                        else
                        {
                            var newDefalultAnswerList = PollsAnswerSerializerHelper.DeserializeAnswerList(PollsAnswerSerializerHelper.GenerateDefaultAnswerList(part.AnswerList));
                            var originalAnswerList = PollsAnswerSerializerHelper.DeserializeAnswerList(part.SerializedAnswers);

                            originalAnswerList.RemoveAll(originalAnswer =>
                                originalAnswerList
                                    .Where(answer =>
                                        !newDefalultAnswerList
                                            .Select(newDefalultAnswer => newDefalultAnswer.Text).Contains(answer.Text))
                                    .Select(answer => answer.Text)
                                    .Contains(originalAnswer.Text));

                            originalAnswerList.AddRange(
                                newDefalultAnswerList
                                    .Where(newDefalultAnswer =>
                                        !originalAnswerList
                                            .Select(originalAnswer => originalAnswer.Text)
                                            .Contains(newDefalultAnswer.Text)));

                            part.SerializedAnswers = PollsAnswerSerializerHelper.SerializeAnswerList(originalAnswerList);
                        }
                        return PollsAnswerSerializerHelper.DeserializeAnswerList(part.SerializedAnswers);
                    }
                    );
                });
        }

    }
}
