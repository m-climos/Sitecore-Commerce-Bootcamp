using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;

namespace Plugin.Bootcamp.Exercises.Order.Export.Pipelines.Arguments
{
    public class ExportOrderArgument : PipelineArgument
    {
        public string OrderId { get; set; }

        public ExportOrderArgument(string orderId)
        {
            /* STUDENT: Complete the constructor. You should check that a valid orderId
             * has been provided and set the OrderId property. */
            Condition.Requires<string>(orderId, "orderId").IsNotNullOrEmpty();

            this.OrderId = orderId;

        }
    }
}