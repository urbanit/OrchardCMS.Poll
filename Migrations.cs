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
            ContentDefinitionManager.AlterPartDefinition(typeof(PollsPart).Name,
                builder => builder
                    .Attachable()
                    .WithField(FieldNames.StartDateTime,
                        cfg => cfg
                            .OfType("DateTimeField")
                            .WithDisplayName("Start of the Poll"))
                    .WithField(FieldNames.EndDateTime,
                        cfg => cfg
                            .OfType("DateTimeField")
                            .WithDisplayName("End of the Poll"))
                    );

            ContentDefinitionManager.AlterTypeDefinition(ContentTypes.PollsWidget,
                cfg => cfg
                    .WithPart(typeof(PollsPart).Name)
                    .WithPart("WidgetPart")
                    .WithPart("CommonPart")
                    .WithPart("IdentityPart")
                    .WithSetting("Stereotype", "Widget")
                    .DisplayedAs("Polls Widget")
                );

            return 1;
        }
    }
}