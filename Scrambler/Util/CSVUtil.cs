using Scrambler.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Util
{
    /// <summary>
    /// Utility class containing common utility functions for processing CSV files
    /// </summary>
    public static class CSVUtil
    {
        /// <summary>
        /// Utility function for reading values from a CSV file
        /// </summary>
        /// <param name="csvFilePath"></param>
        /// <returns></returns>
        public static List<string> ReadToEnd(String csvFilePath)
        {
            var list = new List<string>();

            using (var stream = new FileStream(csvFilePath, FileMode.Open))
            {
                var reader = new StreamReader(stream);
                stream.Seek(0, SeekOrigin.Begin);
                list.AddRange(reader.ReadToEnd().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList());
            }
            return list;
        }
        public static List<ICSVModel> ReadLine<TEntity>(String csvFilePath)
            where TEntity : class
        {
            Type t = typeof(TEntity);
            var result = new List<ICSVModel>();

            using (var stream = new FileStream(csvFilePath, FileMode.Open))
            {
                var reader = new StreamReader(stream);
                stream.Seek(0, SeekOrigin.Begin);

                string currentLine;
                switch (t.Name)
                {
                    case "NameCSVModel":
                        var name_entries = new List<NameCSVModel>();
                        var ecludeName = new List<string> { "FIRST_NAME", "LAST_NAME" };
                        // currentLine will be null when the StreamReader reaches the end of file
                        while ((currentLine = reader.ReadLine()) != null)
                        {
                            var list = currentLine.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            var query =
                               from item in list
                               where !(ecludeName.Any(item2 => item.Contains(item2)))
                               select item;
                            if (query.ToList().Count > 0)
                            {
                                name_entries.Add(new NameCSVModel
                                {
                                    FIRST_NAME = !String.IsNullOrEmpty(list[0]) ? list[0] : "",
                                    LAST_NAME = !String.IsNullOrEmpty(list[1]) ? list[1] : ""
                                });
                            }
                        }
                        result = name_entries.Select(x => (ICSVModel)x).ToList();
                        break;
                    case "AddressCSVModel":
                        var address_entries = new List<AddressCSVModel>();
                        var excludeAddress = new List<string> { "STREET_NUMBER", "STREET_NAME", "STREET_SUFFIX", "A.ADDR_LINE_ONE", "A.ADDR_LINE_TWO", "A.ADDR_LINE_THREE" };
                        // currentLine will be null when the StreamReader reaches the end of file
                        while ((currentLine = reader.ReadLine()) != null)
                        {
                            var list = currentLine.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            var query =
                               from item in list
                               where !(excludeAddress.Any(item2 => item.Contains(item2)))
                               select item;
                            if (query.ToList().Count > 0)
                            {
                                address_entries.Add(new AddressCSVModel
                                {
                                    STREET_NUMBER = !String.IsNullOrEmpty(list[0]) ? Convert.ToInt32(list[0]) : 0,
                                    STREET_NAME = !String.IsNullOrEmpty(list[1]) ? list[1] : "",
                                    STREET_SUFFIX = !String.IsNullOrEmpty(list[2]) ? list[2] : "",
                                    ADDR_LINE_ONE = !String.IsNullOrEmpty(list[3]) ? list[3] : "",
                                    ADDR_LINE_TWO = !String.IsNullOrEmpty(list[4]) ? list[4] : "",
                                    ADDR_LINE_THREE = !String.IsNullOrEmpty(list[5]) ? list[5] : ""
                                });
                            }
                        }
                        result = address_entries.Select(x => (ICSVModel)x).ToList();
                        break;
                }
            }
            return result;
        }
    }
}
