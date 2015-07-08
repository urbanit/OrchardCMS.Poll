using Orchard.UI.Resources;

namespace Urbanit.Polls
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineStyle("Urbanit-Polls").SetUrl("urbanit-polls.css");

            manifest.DefineScript("Urbanit-PollsScript").SetUrl("urbanit-polls.js").SetDependencies("jQuery");

        }
    }
}