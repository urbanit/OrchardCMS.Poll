using System.Collections.Generic;

namespace Urbanit.Polls.ViewModels
{
    public class PollsIndexViewModel
    {
        public PollsIndexViewModel()
        {
            Questions = new List<Models.PollsContentPart>();
        }

        public IEnumerable<Models.PollsContentPart> Questions { get; set; }
    }
}