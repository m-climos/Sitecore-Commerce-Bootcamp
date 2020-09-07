using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Components;

namespace Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Pipelines.Blocks
{
    [PipelineDisplayName("GetWarrantyNotesViewBlock")]
    public class GetWarrantyNotesViewBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        public override Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{Name}: The argument cannot be null.");
            var catalogViewsPolicy = context.GetPolicy<KnownCatalogViewsPolicy>();

            /* STUDENT: Complete the Run method as specified in the requirements */
            var isVariationView = arg.Name.Equals(catalogViewsPolicy.Variant, StringComparison.OrdinalIgnoreCase);
            var isDetailsView = arg.Name.Equals(catalogViewsPolicy.Master, StringComparison.OrdinalIgnoreCase);
            var isConnectView = arg.Name.Equals(catalogViewsPolicy.ConnectSellableItem, StringComparison.OrdinalIgnoreCase);
            var isWarrantyNotesView = arg.Name.Equals("WarrantyNotes", StringComparison.OrdinalIgnoreCase);
            var request = context.CommerceContext.GetObject<EntityViewArgument>();

            if (string.IsNullOrEmpty(arg.Name) ||
                !isDetailsView &&
                !isWarrantyNotesView &&
                !isVariationView &&
                !isConnectView)
            {
                return Task.FromResult(arg);
            }
            if (!(request.Entity is SellableItem))
            {
                return Task.FromResult(arg);
            }
            var sellableItem = (SellableItem)request.Entity;
            var variationId = string.Empty;

            if (isVariationView && !string.IsNullOrEmpty(arg.ItemId))
            {
                variationId = arg.ItemId;
            }

            var isEditView = !string.IsNullOrEmpty(arg.Action) &&
                arg.Action.Equals("WarrantyNotes-Edit", StringComparison.OrdinalIgnoreCase);

            var componentView = arg;
            if (!isEditView)
            {
                componentView = new EntityView
                {
                    Name = "WarrantyNotes",
                    DisplayName = "Warranty Information",
                    EntityId = arg.EntityId,
                    EntityVersion = request.EntityVersion == null ? 1 : (int)request.EntityVersion,
                    ItemId = variationId
                };

                arg.ChildViews.Add(componentView);

                System.Diagnostics.Debug.WriteLine($"Get Entity View in Master view. Version from the argument is {arg.EntityVersion}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Get Entity View in Edit view. Version from the argument is {arg.EntityVersion}");
            }

            if (sellableItem != null && (sellableItem.HasComponent<WarrantyNotesComponent>(variationId) || isConnectView || isEditView))
            {
                var component = sellableItem.GetComponent<WarrantyNotesComponent>(variationId);

                componentView.Properties.Add(
                    new ViewProperty
                    {
                        Name = nameof(WarrantyNotesComponent.WarrantyInformation),
                        DisplayName = "WarrantyInformation",
                        RawValue = component.WarrantyInformation,
                        IsReadOnly = !isEditView,
                        IsRequired = false
                    });
                componentView.Properties.Add(
                    new ViewProperty
                    {
                        Name = nameof(WarrantyNotesComponent.NumberOfYears),
                        DisplayName = "NumberOfYears",
                        RawValue = component.NumberOfYears,
                        IsReadOnly = !isEditView,
                        IsRequired = false
                    });
                System.Diagnostics.Debug.WriteLine($"Description is {component.WarrantyInformation}, term is {component.NumberOfYears}");
            }

            return Task.FromResult(arg);
        }
    }
}
