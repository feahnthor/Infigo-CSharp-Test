using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infigo_api_sucks_solution.Config
{
    internal class Locators // Static fields for the site
    {

        #region Login
        public static string emailId = "LoginEmail"; // can use findElemnt(By.ID)
        public static string passwordId = "LoginPassword";
        public static string loginBtnClass = "loginbutton";
        #endregion

        #region Base Product/ Product admin home
        public static string adminBurgerId = "side-bar__burger";
        public static string productTabs = "//div[@id='product-edit']//ul//li"; // all tabs, the resulting IwebElement. ex: myEelemt[1] is the SEO tab
        public static string _metaTitle = "//input[@id='SeoMetaData_MetaTitle']";
        public static string metaTitle1 = "SeoMetaData_MetaTitle";
        #endregion

        public static string seoMetaTitle = "Meta title";
        public static string seoMetaDescription = "Meta description";
        public static string seoPageName = "Search engine friendly page name";
        public static string seoMetaKeywords = "Meta keywords";
        public static string seoGraphDataTitle = "Title";

        public static string seoPhotography = "Photography";
        public static string seoBackdrop = "Backdrop";
        public static string seoBackground = "Background";
        public static string seoFloor = "Floor";

        #region Variant Products
        public static string variantHomeTags = "//div[@id='productvariant-edit']//ul//li";
        public static string variantAttributeBox = "//div[@id='productvariantattributes-grid']//a[@class='t-button t-grid-add']";
        #endregion
    }
}
