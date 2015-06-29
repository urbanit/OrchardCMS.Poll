using System;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Urbanit.Polls.Models;

namespace Urbanit.Polls {
    public class Migrations : DataMigrationImpl {
        public int Create() {
            SchemaBuilder.CreateTable("PollsPartRecord", table => table
                .ContentPartRecord()
                .Column<string>("Name")
                .Column<string>("Question")
                .Column<string>("AnswerList")
                .Column<string>("Comment")
                .Column<bool>("IsActive")
                .Column<string>("SerializedAnswers", column => column.Unlimited())
            );

            ContentDefinitionManager.AlterPartDefinition(typeof(PollsPart).Name,
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

            ContentDefinitionManager.AlterTypeDefinition("PollsWidget",
                cfg => cfg
                    .WithPart(typeof(PollsPart).Name)
                    .WithPart("WidgetPart")
                    .WithPart("CommonPart")
                    .WithSetting("Stereotype", "Widget")
            );

            return 1;
        }
    }
}