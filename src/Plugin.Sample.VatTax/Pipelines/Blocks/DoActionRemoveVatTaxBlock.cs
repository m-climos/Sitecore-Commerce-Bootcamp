using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Plugin.Bootcamp.Exercises.VatTax.Entities;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Pipelines;

namespace Plugin.Bootcamp.Exercises.VatTax.EntityViews
{
    [PipelineDisplayName("DoActionRemoveDashboardEntity")]
    public class DoActionRemoveVatTaxBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private readonly CommerceCommander _commerceCommander;

        public DoActionRemoveVatTaxBlock(CommerceCommander commerceCommander)
        {
            this._commerceCommander = commerceCommander;
        }

        public override async Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Contract.Requires(context != null);

            /* STUDENT: Add the necessary code to remove the selected Vat Tax configuration */
            if (entityView == null
                || !entityView.Action.Equals("VatTaxDashboard-RemoveDashboardEntity", StringComparison.OrdinalIgnoreCase))
            {
                return entityView;
            }
            var sampleDashboardEntities = await this._commerceCommander.Command<ListCommander>()
                .GetListItems<VatTaxEntity>(context.CommerceContext,
                    CommerceEntity.ListName<VatTaxEntity>(), 0, 99);
            try
            {
                if (entityView.ItemId != null)
                {
                    await this._commerceCommander.Command<DeleteEntityCommand>()
                        .Process(context.CommerceContext, entityView.ItemId);
                }
                else
                {
                    
                    if(sampleDashboardEntities != null)
                    {
                        //todo
                    }
                }
            }
            catch (Exception ex)
            {
                context.Logger.LogError($"Catalog.DoActionRemoveDashboardEntity.Exception: Message={ex.Message}");
            }

            return entityView;
        }
    }
}
