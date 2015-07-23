using Orchard.UI.Resources;

namespace Urbanit.Polls
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineStyle("Urbanit.Polls").SetUrl("urbanit-polls-pollspart.css");

            manifest.DefineScript("Urbanit.Polls").SetUrl("urbanit-polls-pollspart.js").SetDependencies("jQuery");
        }
    }
}