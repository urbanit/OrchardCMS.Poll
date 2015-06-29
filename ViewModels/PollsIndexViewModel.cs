using System.Collections.Generic;

namespace Urbanit.Polls.ViewModels {
    public class PollsIndexViewModel {
        public PollsIndexViewModel() {
            Questions = new List<Models.PollsPart>();
        }

        public IEnumerable<Models.PollsPart> Questions { get; set; }
    }
}