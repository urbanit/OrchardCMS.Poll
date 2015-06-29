using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.UI.Resources;

namespace Urbanit.Polls {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            manifest.DefineStyle("Polls").SetUrl("polls.css");
            manifest.DefineScript("VotingScript").SetUrl("polls.js").SetDependencies("jQuery");

            //manifest.DefineScript("jQuery").SetUrl("jquery-1.8.2.min.js", "jquery-1.8.2.js").SetVersion("1.8.2")
            //    .SetCdn("//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js", "//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js", true);
            //manifest.DefineScript("jQueryUI").SetUrl("jquery-ui.min.js", "jquery-ui.js").SetVersion("1.8.23").SetDependencies("jQuery")
            //    .SetCdn("//ajax.aspnetcdn.com/ajax/jquery.ui/1.8.23/jquery-ui.js", "//ajax.aspnetcdn.com/ajax/jquery.ui/1.8.23/jquery-ui.js", true);
            //manifest.DefineScript("jQueryUI_Core").SetUrl("jquery.ui.core.min.js", "jquery.ui.core.js").SetVersion("1.8.23").SetDependencies("jQuery");
            //manifest.DefineScript("jQueryUI_DatePicker").SetUrl("jquery.ui.datepicker.min.js", "jquery.ui.datepicker.js").SetVersion("1.8.23").SetDependencies("jQueryUI_Core");
        }
    }
}