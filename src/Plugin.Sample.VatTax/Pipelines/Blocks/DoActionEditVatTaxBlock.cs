using Plugin.Bootcamp.Exercises.VatTax.Entities;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Pipelines;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Plugin.Bootcamp.Exercises.VatTax.Pipelines.Blocks
{
    [PipelineDisplayName("DoActionEditVatTaxBlock")]
    public class DoActionEditVatTaxBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private readonly CommerceCommander _commerceCommander;

        public DoActionEditVatTaxBlock(CommerceCommander commerceCommander)
        {
            this._commerceCommander = commerceCommander;
        }
        public override async Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Contract.Requires(context != null);

            /* STUDENT: Add the necessary code to remove the selected Vat Tax configuration */
            var request = context.CommerceContext.GetObject<EntityViewArgument>();

            if (string.IsNullOrEmpty(arg.Action) ||
                !arg.Action.Equals("VatTaxDashboard-EditDashboardEntity", StringComparison.OrdinalIgnoreCase))
            {
                return arg;
            }
            var pluginPolicy = context.GetPolicy<PluginPolicy>();

            var sampleDashboardEntities = await this._commerceCommander.Command<ListCommander>()
                .GetListItems<VatTaxEntity>(context.CommerceContext,
                    CommerceEntity.ListName<VatTaxEntity>(), 0, 99);

            var vatTaxItem = sampleDashboardEntities.Where(x => x.Id.Equals(arg.ItemId)).FirstOrDefault();
            var vatTaxItemToEdit = vatTaxItem;
            
            vatTaxItemToEdit.TaxTag = arg.Properties.FirstOrDefault(x =>
            x.Name.Equals("TaxTag", StringComparison.OrdinalIgnoreCase)).Value.ToString();

            vatTaxItemToEdit.CountryCode = arg.Properties.FirstOrDefault(x =>
            x.Name.Equals("CountryCode", StringComparison.OrdinalIgnoreCase))?.Value;

            vatTaxItemToEdit.TaxPct = Convert.ToDecimal(arg.Properties.FirstOrDefault(x =>
            x.Name.Equals("TaxPct", StringComparison.OrdinalIgnoreCase))?.Value);

            await this._commerceCommander.Pipeline<IPersistEntityPipeline>().Run(new PersistEntityArgument(vatTaxItem), context);

            return arg;
        }
    }
}
