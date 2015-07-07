using Orchard.UI.Resources;

namespace Urbanit.Polls
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineStyle("Polls").SetUrl("polls.css");

            manifest.DefineScript("VotingScript").SetUrl("polls.js").SetDependencies("jQuery");

        }
    }
}