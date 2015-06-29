using System;
using Orchard.ContentManagement.Records;
using Orchard.Data.Conventions;

namespace Urbanit.Polls.Models {
    public class PollsPartRecord : ContentPartRecord {
        public virtual string Name { get; set; }
        public virtual string Question { get; set; }
        public virtual string AnswerList { get; set; }
        public virtual string Comment { get; set; }
        public virtual bool IsActive { get; set; }
        [StringLengthMax]
        public virtual string SerializedAnswers { get; set; }
    }
}