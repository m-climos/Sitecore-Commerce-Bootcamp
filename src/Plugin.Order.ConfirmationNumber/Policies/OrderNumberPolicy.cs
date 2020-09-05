using Sitecore.Commerce.Core;

namespace Plugin.Bootcamp.Exercises.Order.ConfirmationNumber.Policies
{
    public class OrderNumberPolicy : Policy
    {
        public OrderNumberPolicy()
        {
            /* STUDENT: Complete the constructor to initialize the properties */
            this.Prefix = string.Empty;
            this.Suffix = string.Empty;
            this.IncludeDate = false;
        }
        /* STUDENT: Add read/write properties as specified in the requirements */
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public bool IncludeDate { get; set; }
    }
}
