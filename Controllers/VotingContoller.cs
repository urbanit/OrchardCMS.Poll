using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Themes;
using Urbanit.Polls.Services;

namespace Urbanit.Polls.Controllers {
    [Themed]
    public class VotingController : Controller {
        private readonly IOrchardServices _orchardServices;
        private readonly IWorkContextAccessor _wca;
        private readonly IPollsManager _pollsManager;

        public Localizer T { get; set; }
        public ILogger Logger { get; set; }

        public VotingController(
            IOrchardServices orchardServices,
            IWorkContextAccessor wca,
            IPollsManager pollsManager) {
            _orchardServices = orchardServices;
            _wca = wca;
            _pollsManager = pollsManager;

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;

        }

        public string Vote(int votingId, int answerId) {
            return _pollsManager.Vote(votingId, answerId);
        }

    }
}