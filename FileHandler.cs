using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json; // This is where our variables are stored
using Infigo_api_sucks_solution.Properties;
using Infigo_api_sucks_solution.Models;

namespace Infigo_api_sucks_solution
{
    internal class FileHandler
    {
        
        private Settings settings = Settings.Default;

        /// <summary>
        /// Parses a CSV that does not contains strings, meaning special characters will trip this up like a comma being part of a word
        /// </summary>
        /// <param name="filepath">Give it a directory and a file name</param>
        /// <returns>List of strings each row being an element</returns>
        public List<string[]> parseCSV(string filepath)
        {
            List<string[]> parseData = new List<string[]>(); // Everything in this list will be a string
            try
            {
                using StreamReader readFile = new StreamReader(filepath);
                string line;
                string[] row;

                while ((line = readFile.ReadLine()) != null)
                {
                    row = line.Split(new String[] { "," }, StringSplitOptions.None); // split each line by commas
                    Task updateList = Task.Run(() => parseData.Add(row)); // Asynchronously adds to list as the order shouldn't matter

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return parseData;
        }

        /// <summary>
        /// Parses CSV with Strings, this allos for things like html or special characters to be present so long as they are within ""
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>List of strings with each row being an element</returns>
        public List<string[]> ParseCSV_WithStrings(string filepath)
        {
            List<string[]> parseData = new List<string[]>();
            string line;
            string[] row;
            try
            {
                using StreamReader readFile = new StreamReader(filepath);


                while ((line = readFile.ReadLine()) != null)
                {
                    TextFieldParser parser = new TextFieldParser(new StringReader(line));
                    parser.HasFieldsEnclosedInQuotes = true; // This is the most important part
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        try
                        {
                            row = parser.ReadFields();
                            parseData.Add(row);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show($"Delimiter for data below in does not work, please copy the AdditionalDescription from another row into this one then save {line} ");
                            throw;
                        }
                        
                    }
                    parser.Close();
                    
                }
            }

            catch (IOException e)
            {
                MessageBox.Show($"Error parsing csv with string {filepath}\n{e.Message}");
                // Should also write entire row to a log file
            }

            return parseData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fieldsToBecomeList">Takes in a list of strings that will be keys in the json that has a list as value</param>
        /// <param name="delimerForString">Delimeter that separates strings</param>
        /// <returns></returns>
        public async Task CsvToJsonAsync(string filename, string[] fieldsToBecomeList, string delimerForString) // Should run a check to see if this file exists
        {
            int count = 0;
            string csvFieldThatJsonFilesWillGetName = Properties.Settings.Default.CSVToJsonRowToBaseJsonFileOutputName;
            var filehandler = new FileHandler(); // Compiler Error CS0120: In order to use a non-static field, method, or property, you must first create an object instance
            List<string[]> parsedCSV = filehandler.ParseCSV_WithStrings(filename);
            string[] headerRow = parsedCSV[0];

            parsedCSV.RemoveAt(0); // removes header

            foreach (string[] row in parsedCSV) // Row list, really sting[] means its an array
            {
                Guid guid = Guid.NewGuid(); // Generates a new (Global Unique Identifies) of 128-bit integer. Not used right now
                string randomString = Convert.ToBase64String(guid.ToByteArray());
                randomString = randomString.Replace("=", "")
                                            .Replace("+", "")
                                            .Replace("/", "")
                                            .Replace(@"\", "");  // https://stackoverflow.com/questions/15009423/way-to-generate-a-unique-number-that-does-not-repeat-in-a-reasonable-time
                Dictionary<string, dynamic> jsonObject = new Dictionary<string, dynamic>(); // Creates a new dictionary for each file to add
                int index = 0;

                // need to take care of index out of range errror
                using StreamWriter file = new($"{settings.CsvToJsonOutputFile}{row[1]}_{row[0]}.json"); // Replace  row[0] with variable that gives index of the word in the list

                foreach (string headerItem in headerRow) // Header Items, i.e; Name, Designer
                {
                    jsonObject.Add(headerItem, row[index]); // get the header column and index into the same number as row, so long as header row and all rows are the same length
                    index++;
                }

                // Since jsonObject is a dictionary, we can now use the keys to edit the values
                foreach (string key in fieldsToBecomeList)
                {
                    if (jsonObject.ContainsKey(key))
                    {
                        string valueString = jsonObject[key];
                        string[] value = valueString.Split(delimerForString); // split string on delimiter, returns a list ;
                        jsonObject[key] = value;
                    };
                }


                await file.WriteLineAsync(JsonConvert.SerializeObject(jsonObject).ReplaceLineEndings(",\n"));
                //if (count >= 10) { break; }; // for testing, limits how many files can be created
                
                count++;
            }
            
        }

        public ProductJsonModel GetBgtJsonData(string filename) //Parse json and get nodes assigned in ProductJson class below
        {
            string fileStream = File.ReadAllText(filename);

            ProductJsonModel jsonObject = JsonConvert.DeserializeObject<ProductJsonModel>(fileStream);
            
            return jsonObject;
        }

        public string MoveFile(string originalDir, string destinationDir) // move first file
        {
            string firstFileName = "";
            if (System.IO.Directory.Exists(originalDir))
            {
                string[] files = System.IO.Directory.GetFiles(originalDir, "*.json");
                try
                {
                    firstFileName = System.IO.Path.GetFileName(files[0]);
                    System.IO.Directory.Move($"{originalDir}{firstFileName}", $"{destinationDir}{firstFileName}");
                    
                }
                catch (Exception)
                {

                    throw;

                }
                
            }
            return $"{destinationDir}{firstFileName}";

        }

    }

    

}
