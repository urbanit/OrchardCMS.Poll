using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Widgets.Models;
using Orchard.Widgets.Services;
using Urbanit.Polls.Helpers;
using Urbanit.Polls.Models;

namespace Urbanit.Polls.Services
{
    public interface IPollsManager : IDependency
    {
        IEnumerable<PollsPart> GetPolls();
        ContentItem GetPollsWidget(int votingId);
        void CreatePollsWidget(ContentItem item);
        string Vote(int voteId, int answerId);
    }

    public class PollsManager : IPollsManager
    {
        private readonly IContentManager _contentManager;
        private readonly IWidgetsService _widgetsService;


        public PollsManager(IOrchardServices services, IWidgetsService widgetsService)
        {
            _contentManager = services.ContentManager;
            _widgetsService = widgetsService;
        }

        public IEnumerable<PollsPart> GetPolls()
        {
            var questions = _contentManager.Query<PollsPart>().List();

            return questions;
        }

        public ContentItem GetPollsWidget(int id)
        {
            if (id == 0)
            {
                ContentItem item = _contentManager.New("PollsWidget");

                var part = item.Parts.FirstOrDefault(p => p.GetType() == typeof(WidgetPart));

                if (part != null)
                {
                    //ASK: Nem lenne érdemes kesselni??

                    var widgetPart = part.As<WidgetPart>();
                    var availableLayers = _widgetsService.GetLayers();
                    var availableZones = _widgetsService.GetZones();
                    int widgetPosition = _widgetsService.GetWidgets().Count(widget => widget.Zone == widgetPart.Zone) + 1;
                    widgetPart.Position = widgetPosition.ToString(CultureInfo.InvariantCulture);

                    widgetPart.LayerPart = widgetPart.LayerPart ?? availableLayers.First();
                    widgetPart.Zone = widgetPart.Zone ?? availableZones.First();
                }

                return item;
            }
            return _contentManager.Get(id);
        }

        public void CreatePollsWidget(ContentItem model)
        {
            //if (_multiChoiceVotingRepository.Fetch(record => record.Name == model.Name).FirstOrDefault() != null)
            //{
            //    throw new Exception(String.Format("A voting with name '{0}' is already exists.", model.Name));
            //}
            //else
            //{
            //    var question = _contentManager.New<MultiChoiceVotingPart>("MultiChoiceQuestion");

            //    question.Name = model.Name;
            //    question.AnswerList = model.AnswerList;
            //    question.Comment = model.Comment;
            //    question.FinishDate = model.FinishDate;
            //    question.IsActive = model.IsActive;
            //    question.Question = model.Question;
            //    question.ReleaseDate = model.ReleaseDate;
            //    question.SerializedAnswers = SerializeAnswerList(GenerateAnswerList(model.AnswerList));

            //    _contentManager.Create(question);
            //}
        }

        public string Vote(int voteId, int answerId)
        {
            var pollsQuestionPart = _contentManager.Get<PollsPart>(voteId);

            IList<Answer> answers = AnswerSerializerHelper.DeserializeAnswerList(pollsQuestionPart.SerializedAnswers);
            answers[answerId].VoteCount++;
            pollsQuestionPart.SerializedAnswers = AnswerSerializerHelper.SerializeAnswerList(answers);

            //_contentManager.Flush();

            return pollsQuestionPart.SerializedAnswers;
        }
    }
}