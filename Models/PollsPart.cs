using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;
using Orchard.Fields.Fields;
using Piedone.HelpfulLibraries.Contents;

namespace Urbanit.Polls.Models
{
    public class PollsPart : ContentPart
    {

        [Required]
        public string Name
        {
            get { return this.Retrieve(x => x.Name); }
            set { this.Store(x => x.Name, value); }
        }

        [Required]
        public string Question
        {
            get { return this.Retrieve(x => x.Question); }
            set { this.Store(x => x.Question, value); }
        }

        [Required]
        public string AnswerList
        {
            get { return this.Retrieve(x => x.AnswerList); }
            set { this.Store(x => x.AnswerList, value); }
        }

        public string Comment
        {
            get { return this.Retrieve(x => x.Comment); }
            set { this.Store(x => x.Comment, value); }
        }

        [Required]
        public bool IsActive
        {
            get { return this.Retrieve(x => x.IsActive); }
            set { this.Store(x => x.IsActive, value); }
        }

        public string SerializedAnswers
        {
            get { return this.Retrieve(x => x.SerializedAnswers); }
            set { this.Store(x => x.SerializedAnswers, value); }
        }

        private readonly LazyField<IList<Answer>> _answers = new LazyField<IList<Answer>>();
        public LazyField<IList<Answer>> AnswersField { get { return _answers; } }
        public IList<Answer> Answers
        {
            get { return this._answers.Value; }
        }

        public DateTimeField PollStart
        {
            get
            {
                return this.AsField<DateTimeField>(typeof(PollsPart).Name, "StartDateTime");
            }
        }
        public DateTimeField PollEnd
        {
            get
            {
                return this.AsField<DateTimeField>(typeof(PollsPart).Name, "EndDateTime");
            }
        }
    }
}