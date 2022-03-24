using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infigo_api_sucks_solution.Models
{
    /// <summary>
    /// Fields for the JSON output that is taken from the Excel output from infigo
    /// </summary>
    public class ProductJsonModel // Sets the nodes that we will be looking for in our json and assigning them with JsonConver.Deserilizer
    {   // This class will be treated as an object with empyt values
        // Really easy to create this class from a json or xml. Copy the json to clipboard then Edit => Paste Special => Paste JSON as Classes
        // https://www.iditect.com/guide/csharp/csharp_howto_deserialize_json_into_dynamic_object.html
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string AdditionalDescription { get; set; }
        public string ProductTemplateId { get; set; }
        public string ShowOnHomePage { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }
        public string OpenGraphTitle { get; set; }
        public string OpenGraphDescription { get; set; }
        public string OpenGraphPictureId { get; set; }
        public string AllowCustomerReviews { get; set; }
        public string Published { get; set; }
        public string SKU { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string IsGiftCard { get; set; }
        public string GiftCardTypeId { get; set; }
        public string RequireOtherProducts { get; set; }
        public string AutomaticallyAddRequiredProducts { get; set; }
        public string IsDownload { get; set; }
        public string DownloadId { get; set; }
        public string UnlimitedDownloads { get; set; }
        public string MaxNumberOfDownloads { get; set; }
        public string DownloadActivationTypeId { get; set; }
        public string HasSampleDownload { get; set; }
        public string SampleDownloadId { get; set; }
        public string HasUserAgreement { get; set; }
        public string UserAgreementText { get; set; }
        public string IsRecurring { get; set; }
        public string RecurringCycleLength { get; set; }
        public string RecurringCyclePeriodId { get; set; }
        public string RecurringTotalCycles { get; set; }
        public string IsShipEnabled { get; set; }
        public string IsFreeShipping { get; set; }
        public string AdditionalShippingCharge { get; set; }
        public string IsTaxExempt { get; set; }
        public string TaxCategoryIds { get; set; }
        public string ManageInventoryMethodId { get; set; }
        public string StockQuantity { get; set; }
        public string DisplayStockAvailability { get; set; }
        public string DisplayStockQuantity { get; set; }
        public string MinStockQuantity { get; set; }
        public string LowStockActivityId { get; set; }
        public string NotifyAdminForQuantityBelow { get; set; }
        public string BackorderModeId { get; set; }
        public string OrderMinimumQuantity { get; set; }
        public string OrderMaximumQuantity { get; set; }
        public string DisableBuyButton { get; set; }
        public string DisableWishlistButton { get; set; }
        public string CallForPrice { get; set; }
        public string Price { get; set; }
        public string OldPrice { get; set; }
        public string ProductCost { get; set; }
        public string CustomerEntersPrice { get; set; }
        public string MinimumCustomerEnteredPrice { get; set; }
        public string MaximumCustomerEnteredPrice { get; set; }
        public string Weight { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string CreatedOnUtc { get; set; }
        public string[] CategoryIds { get; set; }
        public string ManufacturerIds { get; set; }
        public string PrintLocationId { get; set; }
        public string JpegDownloadVersion { get; set; }
        public string TeaserDetails { get; set; }
        public string[] Tags { get; set; }
        public string ProductType { get; set; }
        public string RequiredProductIds { get; set; }
        public string CrossSellProductIds { get; set; }
        public string RelatedProductIds { get; set; }
        public string AvailableStartDateTimeUtc { get; set; }
        public string AvailableEndDateTimeUtc { get; set; }
        public string AdminComment { get; set; }
        public string DownloadAndPrint { get; set; }
        public string MisGeneric { get; set; }
        public string spec_DepartmentCode { get; set; }
    }
}
