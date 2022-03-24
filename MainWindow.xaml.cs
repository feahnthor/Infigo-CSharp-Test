/*
Author: Henry Feahn
Description: This is a program that seeks to automate a few of the things that Infigo a company whose software we currently use to create sites such as
        https://backgroungtown.com and https://imagebuggy.com. It has a pretty good CMS and customization is very good, however, there are several things
        we do not have access to as their api has the most basic features imaginable for product creation. As of this writing this is one of a few solutions
        to make things easier on myself by adding SEO to our SEO starved site, that contains 10,000+ products. This is my first attempt at C#, so things were figured out by Goolge
Date-Created: 2022/02/01
Version: 1.0
 */
using Infigo_api_sucks_solution.Pages;
using Infigo_api_sucks_solution.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
//Settings File stores the data/variables https://docs.microsoft.com/en-us/visualstudio/ide/reference/settings-page-project-designer?f1url=%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(ApplicationSettingsOverview)%26rd%3Dtrue&view=vs-2022

namespace Infigo_api_sucks_solution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// int i : determines number of drivers to open
    /// </summary>

    public partial class MainWindow : Window
    {

        private readonly List<IWebDriver> webDrivers = new List<IWebDriver>(); // empty list that must contain webdriver objects
        private Object[] urls = new object[] { };
        private Dictionary<string, ProductJsonModel> productDict = new Dictionary<string, ProductJsonModel>();
        private ProductJsonModel products = new ProductJsonModel();
        private bool baseProductComplete = false;
        WorkOrder worker = new WorkOrder();  // attempt to use a worker
        //int i = Environment.ProcessorCount - 13; //sets max numbers of browsers to cpu processor count
        int i = 40; // this sets how many browswers will be opened up initially. 
        ListBoxItem item = new ListBoxItem();
        public string[] filesInSearch { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {

            StopButton.Visibility = Visibility.Visible;
            StartButton.Visibility = Visibility.Collapsed;
            FileHandler fileHandler = new FileHandler();

            CheckStart();
            CheckForFiles();
            await UpdateStatusList($"{i} Drivers to start");
            OpenDrivers();
            CheckFiles();

            if (CsvToJsonRadioButton.IsChecked == true && CheckFolderExist()) // only run if button is checked
            {
                string[] fieldToMakeList = { "Tags", "CategoryIds" };
                await fileHandler.CsvToJsonAsync(@"\\172.16.2.44\Websites\intranet\v_1.0\temp\jobs\bgtown\csv_imports\products_2022-03-04-18-02-18_7933.csv", fieldToMakeList, @";");

                List<string[]> parsedCSV = fileHandler.ParseCSV_WithStrings($"{Constants.bgtCsvImportFolder}");
                int me = 0;
                foreach (var row in parsedCSV)
                {
                    //MessageBox.Show(row.Length.ToString());
                    //Console.WriteLine(row);
                    //Task addNames = Task.Run(() => UpdateStatusList(row[1]));
                    try
                    {
                        await UpdateStatusList(row[0]);
                        me++;
                        if (me == 10) { break; }
                    }
                    catch (Exception ex)
                    {

                        await UpdateStatusList(ex.ToString());
                        Console.Write(row);
                    }
                    
                }
                
            }
            MoveToProcessing(); // must go ahead of Login as it moves the files to where Login.LogMeIn searches
            string[] processingDir = Directory.GetFiles(Constants.BgtProcessingDir);

            if (processingDir.Length > 0)
            {
                Login login = new Login();
                login.LogMeIn(webDrivers);
            }

            string[] searchDir = Directory.GetFiles(Constants.bgtJsonTestFolder);
            //while (searchDir.Length > 0)
            //{
            while (searchDir.Length > 0)
            {


                try
                {
                    
                    BaseProduct baseProduct = new BaseProduct();
                    baseProduct.productDict = productDict;
                    await baseProduct.UpdateSeo(webDrivers);
                    await UpdateStatusList($"Done with batch. {searchDir.Length} left"); // same as DateTime.Now.ToString("HH:mm:ss")

                    //File.Move($"{Variables.bgtProcessingFolder}{Path.GetFileName(worker.jsonFile)}", $"{Variables.bgtDoneFolder}{Path.GetFileName(worker.jsonFile)}");

                    //Variants variants = new Variants();
                    //variants.productDict = productDict;


                    searchDir = Directory.GetFiles(Constants.bgtJsonTestFolder); // check how many files left now
                    //if (baseProduct.IsComplete)
                    //{

                    //    File.Move($"{Variables.bgtProcessingFolder}{Path.GetFileName(worker.jsonFile)}", $"{Variables.bgtDoneFolder}{Path.GetFileName(worker.jsonFile)}"); // get file name and concatenate it to the folder name in order to move it there
                    //}
                    MoveToProcessing();
                    if (!baseProduct.IsComplete) // checks if function status, then moves file
                    {

                    }
                }
                catch (Exception ex)
                {
                    processingDir = Directory.GetFiles(Constants.BgtProcessingDir);
                    foreach (var file in processingDir)
                    {
                        File.Move($"{Variables.bgtProcessingFolder}{Path.GetFileName(worker.jsonFile)}", $"{Variables.bgtErrorFolder}{Path.GetFileName(worker.jsonFile)}");
                    }
                }

            }


            //}





        }

        private void VariantWorker()
        {

        }

        private void OpenDrivers()
        {
            /***
             * Summary:
                    Opens driver based on the cpu count, unless the number of files left in the folder at start is less than the cpu count

            ***/
            filesInSearch = Directory.GetFiles(Constants.bgtJsonTestFolder);
            if (i > filesInSearch.Length) i = filesInSearch.Length;
            while (i > 0)
            {
                // Start writing code In IDriverSetup.cs -> DiverSetup.cs -> This
                IDriverSetup driverInstance = new DriverSetup(); // Instantiating an interface https://softwareengineering.stackexchange.com/questions/167808/instantiating-interfaces-in-c
                driverInstance.InitializeDriver("edge", "headless", Constants.BgtProductListUrl);

                webDrivers.Add(driverInstance.GetDriver());
                //MessageBox.Show($"Started Vale: {myrand}");
                listStatus.Items.Insert(0, $"{DateTime.Now:HH:mm:ss} {Constants.BgtProductListUrl}"); // same as DateTime.Now.ToString("HH:mm:ss")
                i--; // keep subtracting from i 

            }
        }

        /// <summary>
        /// This is the function that moves the files to the processing folder, then redirects the browser to the right url.
        ///     This is not done asynchronously to avoid 500 errors from backgroundtown. Each driver will load is respective design up one at a time
        /// </summary>
        private void MoveToProcessing()
        {
            int count = 0;


            foreach (IWebDriver driver in webDrivers) // try to move files equal to the total amount of drivers available
            {
                string myFile = Directory.GetFiles(Variables.bgtJsonTestFolder, "*.json")[count]; // get first file
                UpdateStatusList($"Moving {Path.GetFileName(myFile)} to Processing folder...");
                Directory.Move(myFile, $"{Variables.bgtProcessingFolder}{Path.GetFileName(myFile)}"); // get file name and concatenate it to the folder name in order to move it there
                UpdateStatusList($"{Path.GetFileName(myFile)} moved sucessfully");
                FileHandler fileHandler = new FileHandler();

                ProductJsonModel ProductJsonModel = fileHandler.GetBgtJsonData($"{Variables.bgtProcessingFolder}{Path.GetFileName(myFile)}"); // since fileHandler has the nodes in the ProductJsonModel class we can access them using Intellisense. For a dynamic file

                driver.Navigate().GoToUrl($"{Constants.BaseBgtProdUrl}{ProductJsonModel.ProductId}"); // if this  goes to a 500 error, it maybe the product has been deleted

                if (!productDict.ContainsKey(driver.CurrentWindowHandle))
                {
                    productDict.Add(driver.CurrentWindowHandle, ProductJsonModel); // add webdriver handle to dict if it doesn't exist
                }
                else
                {
                    productDict[driver.CurrentWindowHandle] = ProductJsonModel; // update the value of the key with a new design/product
                }
                UpdateStatusList($"Driver {count} - Product:");
                count++;
                UpdateStatusList($"Driver {count}: { Constants.BaseBgtProdUrl}");
                //worker.ProductJsonModel = ProductJsonModel;
                worker.jsonFile = myFile;
                //worker.driver = driver;
            }

        }

        //Button Randomize Start
        #region 
        private void btnRandomize_Click(object sender, RoutedEventArgs e)
        {
            foreach (var driver in webDrivers)
            {
                Task t = Task.Run(() => driver.Navigate().GoToUrl($"https://{urls[new Random().Next() % 5]}")); // Asynchronously switches to a different url, so all browsers switch at together
            }
        }
        #endregion


        private async void btnStop_Click(object sender, RoutedEventArgs e)
        {
            Task task = Task.Run(async () =>
            {
                StartButton.Visibility = Visibility.Visible; StopButton.Visibility = Visibility.Collapsed;
                await CloseDrivers();
            });
           
            //await CloseDrivers();
            await task;
            
        }

        public async Task CloseDrivers()
        {
            foreach (var driver in webDrivers)
            {
                Task q = Task.Run(() =>  driver.Quit());
                await q;
            }
            
           
        }

        private bool CheckFolderExist()
        {

            if (!Directory.Exists(Variables.bgtJsonSearchFolder)) { UpdateStatusList("ERROR: BGT JSON Search folder not found"); return false; }
            if (!Directory.Exists(Variables.bgtJsonTestFolder)) { UpdateStatusList("ERROR: BGT JSON Search folder not found"); return false; }
            if (!Directory.Exists(Variables.bgtProcessingFolder)) { UpdateStatusList("ERROR: BGT JSON Search folder not found"); return false; }
            if (!Directory.Exists(Variables.bgtDoneFolder)) { UpdateStatusList("ERROR: BGT JSON Search folder not found"); return false; }
            return true;
        }

        private bool CheckStart()
        {
            if (!CheckFolderExist()) return false;
            return true;
        }

        private async void CheckForFiles()
        {
            if (!CheckFolderExist())
            {
                return;
            }
            await UpdateStatusList("Searching...");

        }


        private async Task UpdateStatusList(string msg) // Updates the listbox visible to the user
        {
            //ListBoxItem item = new ListBoxItem();
            //item.Content = $"{ DateTime.Now:HH: mm: ss}   { msg}";
            //listStatus.Items.Add(item);
            Task task = Task.Run(() =>
            {
                this.Dispatcher.Invoke(() => // Makes it so that if a different thread tries to call this ascynchronously, it switches it to synchronously https://stackoverflow.com/questions/9732709/the-calling-thread-cannot-access-this-object-because-a-different-thread-owns-it
                {


                    listStatus.Items.Insert(0, $"{DateTime.Now:HH:mm:ss} {msg}");

                    if (listStatus.Items.Count > 1000) listStatus.Items.RemoveAt(listStatus.Items.Count - 1); //Removes last item in the listbox when it gets full
                });
            });
            await task;

        }

        private async void CheckFiles()
        {
            if (!CheckFolderExist())
            {
                await UpdateStatusList("Could not find folder");
            }

            await UpdateStatusList("Searching...");
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseDrivers();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (!baseProductComplete)
            {

            }
            Application.Current.Shutdown();

        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {


            MoveToProcessing(); // must go ahead of Login as it moves the files to where Login.LogMeIn searches
            string[] processingDir = Directory.GetFiles(Constants.BgtProcessingDir);

            if (processingDir.Length > 0)
            {
                Login login = new Login();
                login.LogMeIn(webDrivers);
            }

            string[] searchDir = Directory.GetFiles(Constants.BgtAddFolder);
            //while (searchDir.Length > 0)
            //{
            try
            {
                BaseProduct baseProduct = new BaseProduct();
                baseProduct.productDict = productDict;
                baseProduct.UpdateSeo(webDrivers);
                searchDir = Directory.GetFiles(Constants.BgtAddFolder); // check how many files left now
                if (!baseProduct.IsComplete) // checks if function status, then moves file
                {

                }
            }
            catch (Exception ex)
            {
                processingDir = Directory.GetFiles(Constants.BgtProcessingDir);
                foreach (var file in processingDir)
                {
                    Directory.Move(Constants.BgtProcessingDir, Constants.BgtErrorDir);
                }
            }
        }

        private void listStatus_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }

    public class WorkOrder
    {
        public IWebDriver driver;
        public string jsonFile;
        public ProductJsonModel ProductJsonModel;

        public WorkOrder()
        {
            driver = null;
            jsonFile = "";
            ProductJsonModel = new ProductJsonModel();


        }
    }
}
