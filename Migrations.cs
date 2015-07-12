using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Urbanit.Polls.Constants;
using Urbanit.Polls.Models;
namespace Urbanit.Polls
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition(typeof(PollsContentPart).Name,
                builder => builder
                    .Attachable()
                    .WithField("StartDateTime",
                        cfg => cfg
                            .OfType("DateTimeField")
                            .WithDisplayName("Start of the Poll"))
                    .WithField("EndDateTime",
                        cfg => cfg
                            .OfType("DateTimeField")
                            .WithDisplayName("End of the Poll"))
                    );

            ContentDefinitionManager.AlterTypeDefinition(PollsContentTypes.PollWidget,
                cfg => cfg
                    .WithPart(typeof(PollsContentPart).Name)
                    .WithPart("WidgetPart")
                    .WithPart("CommonPart")
                    .WithSetting("Stereotype", "Widget")
                );

            return 1;
        }
    }
}