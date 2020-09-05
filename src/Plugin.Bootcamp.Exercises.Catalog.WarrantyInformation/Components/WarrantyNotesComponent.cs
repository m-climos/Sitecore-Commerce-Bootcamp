using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Catalog;

namespace Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Components
{
    /* STUDENT: Add properties as specified in the requirements */
    public class WarrantyNotesComponent : Component
    {
        /* STUDENT: Add properties as specified in the requirements */
        public string WarrantyInformation { get; set; } = string.Empty;
        public int NumberOfYears { get; set; } = 1;
    }

}
