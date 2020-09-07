using System;
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
    [PipelineDisplayName("GetVatTaxDashboardViewBlock")]
    public class GetVatTaxDashboardViewBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        public override  Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Contract.Requires(entityView != null);
            Contract.Requires(context != null);
            Condition.Requires(entityView).IsNotNull($"{this.Name}: The argument cannot be null");

            /* STUDENT: Complete the body of the Run method. You should handle the 
             * entity view for both a new and existing entity. */
            var request = context.CommerceContext.GetObject<EntityViewArgument>();

            if (entityView.Name != "VatTaxDashboard-FormAddDashboardEntity" && entityView.Name != "VatTaxDashboard-FormEditDashboardEntity")
            {
                return Task.FromResult(entityView); 
            }
            var pluginPolicy = context.GetPolicy<PluginPolicy>();

            var vatTaxItem = ((VatTaxEntity)request.Entity);

            bool isEditView = !string.IsNullOrEmpty(entityView.Action) &&
                entityView.Action.Equals("VatTaxDashboard-EditDashboardEntity", StringComparison.OrdinalIgnoreCase);

            if (isEditView)
            {
                entityView.Properties.Add(
                new ViewProperty
                {
                    Name = "TaxTag",
                    DisplayName = "Tag",
                    IsHidden = false,
                    IsRequired = true,
                    RawValue = vatTaxItem.TaxTag
                });

                entityView.Properties.Add(
                    new ViewProperty
                    {
                        Name = "CountryCode",
                        DisplayName = "Country Code",
                        IsHidden = false,
                        IsRequired = true,
                        RawValue = vatTaxItem.CountryCode
                    });

                entityView.Properties.Add(
                    new ViewProperty
                    {
                        Name = "TaxPct",
                        DisplayName = "Tax Percentage",
                        IsHidden = false,
                        IsRequired = true,
                        RawValue = vatTaxItem.TaxPercentage
                    });
            }

            //var component = vatTaxItem.GetComponent<VatTaxEntity>(entityView.ItemId);
            else
            {
                entityView.Properties.Add(
                    new ViewProperty
                    {
                        Name = "TaxTag",
                        DisplayName = "Tag",
                        IsHidden = false,
                        IsRequired = true,
                        RawValue = string.Empty
                    });

                entityView.Properties.Add(
                    new ViewProperty
                    {
                        Name = "CountryCode",
                        DisplayName = "Country Code",
                        IsHidden = false,
                        IsRequired = true,
                        RawValue = string.Empty
                    });

                entityView.Properties.Add(
                    new ViewProperty
                    {
                        Name = "TaxPct",
                        DisplayName = "Tax Percentage",
                        IsHidden = false,
                        IsRequired = true,
                        RawValue = string.Empty
                    });
            }
            return Task.FromResult(entityView);
        }
    }
}
