﻿using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Plugin.Bootcamp.Exercises.VatTax.EntityViews
{
    [PipelineDisplayName("GetVatTaxNavigationViewBlock")]
    public class GetVatTaxNavigationViewBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        public override Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Contract.Requires(entityView != null);
            Contract.Requires(context != null);

            Condition.Requires(entityView).IsNotNull($"{this.Name}: The argument cannot be null");

            /* STUDENT: Add the necessary code to show your new Vat Tax dashboard in the Business Tools navigation */
            if (entityView.Name != "ToolsNavigation")
            {
                return Task.FromResult(entityView);
            }

            var pluginPolicy = context.GetPolicy<PluginPolicy>();

            entityView.ChildViews.Add(new EntityView()
            {
                Name = "VatTaxDashboard",
                ItemId = "VatTaxDashboard",
                DisplayName = "Vat Tax",
                Icon = "link",
                DisplayRank = 9
            });

            return Task.FromResult(entityView);
        }
    }
}
