using Sitecore.Commerce.Core;
using System;

namespace Plugin.Bootcamp.Exercises.Order.Export.Components
{
    public class ExportedOrderComponent : Component
    {
        /* STUDENT: Add properties to the component to meet the requirements. */
        public string ExportFilename { get; set; }
        public DateTime DateExported { get; set; }
        
    }
}
