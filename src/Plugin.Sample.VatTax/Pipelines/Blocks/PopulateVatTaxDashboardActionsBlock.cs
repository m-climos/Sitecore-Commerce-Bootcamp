using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Plugin.Bootcamp.Exercises.VatTax.Entities;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Plugin.Bootcamp.Exercises.VatTax.EntityViews
{
    [PipelineDisplayName("EnsureActions")]
    public class PopulateVatTaxDashboardActionsBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        public override Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Condition.Requires(entityView).IsNotNull($"{this.Name}: The argument cannot be null");

            if (entityView.Name != "VatTaxDashboard")
            {
                return Task.FromResult(entityView);
            }
            
            var tableViewActionsPolicy = entityView.GetPolicy<ActionsPolicy>();
            tableViewActionsPolicy.Actions.Add(new EntityActionView
            {
                Name = "VatTaxDashboard-AddDashboardEntity",
                DisplayName = "Adds a new Vat Tax Entry",
                Description = "Adds a new Vat Tax Entry",
                IsEnabled = true,
                RequiresConfirmation = false,
                EntityView = "VatTaxDashboard-FormAddDashboardEntity",
                Icon = "add"
            });

            tableViewActionsPolicy.Actions.Add(new EntityActionView
            {
                Name = "VatTaxDashboard-RemoveDashboardEntity",
                DisplayName = "Remove Vat Tax Entry",
                Description = "Removes a Vat Tax Entry",
                IsEnabled = true,
                RequiresConfirmation = true,
                EntityView = string.Empty,
                Icon = "delete"
            });

            tableViewActionsPolicy.Actions.Add(new EntityActionView
            {
                Name = "VatTaxDashboard-EditDashboardEntity",
                DisplayName = "Edit Vat Tax Entry",
                Description = "Modifies a Vat Tax Entry",
                IsEnabled = true,
                EntityView = "VatTaxDashboard-FormEditDashboardEntity",
                Icon = "edit"
            });

            return Task.FromResult(entityView);
        }
    }
}
