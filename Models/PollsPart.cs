using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;
using Orchard.Fields.Fields;
using Piedone.HelpfulLibraries.Contents;
using Urbanit.Polls.Constants;

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
            get { return this.Retrieve(x => x.IsActive, true); }
            set { this.Store(x => x.IsActive, value); }
        }

        public string SerializedAnswers
        {
            get { return this.Retrieve(x => x.SerializedAnswers); }
            set { this.Store(x => x.SerializedAnswers, value); }
        }

        private readonly LazyField<IList<PollAnswer>> _answers = new LazyField<IList<PollAnswer>>();
        public LazyField<IList<PollAnswer>> AnswersField { get { return _answers; } }
        public IList<PollAnswer> Answers { get { return this._answers.Value; } }

        public DateTimeField PollStartField
        {
            get
            {
                return this.AsField<DateTimeField>(typeof(PollsPart).Name, FieldNames.StartDateTime);
            }
        }

        public DateTimeField PollEndField
        {
            get
            {
                return this.AsField<DateTimeField>(typeof(PollsPart).Name, FieldNames.EndDateTime);
            }
        }
    }
}