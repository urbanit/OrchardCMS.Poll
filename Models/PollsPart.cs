using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;
using Piedone.HelpfulLibraries.Contents;
using Orchard.Fields.Fields;

namespace Urbanit.Polls.Models {
    public class PollsPart : ContentPart<PollsPartRecord> {

        [Required]
        public string Name {
            get { return Retrieve(x => x.Name); }
            set { Store(x => x.Name, value); }
        }

        [Required]
        public string Question {
            get { return Retrieve(x => x.Question); }
            set { Store(x => x.Question, value); }
        }

        [Required]
        public string AnswerList {
            get { return Retrieve(x => x.AnswerList); }
            set { Store(x => x.AnswerList, value); }
        }

        public string Comment {
            get { return Retrieve(x => x.Comment); }
            set { Store(x => x.Comment, value); }
        }

        [Required]
        public bool IsActive {
            get { return Retrieve(x => x.IsActive); }
            set { Store(x => x.IsActive, value); }
        }

        public string SerializedAnswers {
            get { return Retrieve(x => x.SerializedAnswers); }
            set { Store(x => x.SerializedAnswers, value); }
        }

        private readonly LazyField<IList<Answer>> _answers = new LazyField<IList<Answer>>();
        public LazyField<IList<Answer>> AnswersField { get { return _answers; } }
        public IList<Answer> Answers {
            get { return _answers.Value; }
        }

        public DateTimeField PollStart {
            get {
                return this.AsField<DateTimeField>(typeof(PollsPart).Name, "StartDateTime");
            }
        }
        public DateTimeField PollEnd {
            get {
                return this.AsField<DateTimeField>(typeof(PollsPart).Name, "EndDateTime");
            }
        }
    }
}