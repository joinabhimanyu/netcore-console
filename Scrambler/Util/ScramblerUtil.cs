using Scrambler.Model;
using Scrambler.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Xml.Linq;
using System.Configuration;

namespace Scrambler.Util
{
    /// <summary>
    /// Utility class containing utility functions for working with the input XML and Scramble the content
    /// </summary>
    public static class ScramblerUtil
    {
        public static ResourceManager mgr { get; set; }
        /// <summary>
        /// Function containing scrambling logic for name, address, email and ssn
        /// </summary>
        /// <param name="value"></param>
        /// <param name="nodetype"></param>
        /// <returns></returns>
        public static string Scramble(string value, UtilityTypes.NodeType nodetype)
        {
            //var response3 = AsyncUtil.RunAsync<DocumentRuleEntity>("http://localhost:56516/", "api/documentrule/6").GetAwaiter().GetResult();
            switch (nodetype)
            {
                case UtilityTypes.NodeType.Name:
                    // logic for scrambling Name
                    var namePath = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\navn.csv";
                    var nameCSVList = CSVUtil.ReadLine<NameCSVModel>(namePath).Select(x => (NameCSVModel)x).ToList();
                    value = ScrambleName(nameCSVList, value);

                    break;
                case UtilityTypes.NodeType.Address:
                    // logic for scrambling Address
                    var addressPath = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\adressse.csv";
                    var addressCSVList = CSVUtil.ReadLine<AddressCSVModel>(addressPath).Select(x => (AddressCSVModel)x).ToList();
                    value = ScrambleAddress(addressCSVList, value);

                    break;
                case UtilityTypes.NodeType.Email:
                    // logic for scrambling Email
                    var emailNamePath = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\navn.csv";
                    var emailNameList = CSVUtil.ReadLine<NameCSVModel>(emailNamePath).Select(x => (NameCSVModel)x).ToList();
                    value = ScrambleName(emailNameList, value);
                    break;
                case UtilityTypes.NodeType.SSN:
                    // logic for scrambling national identification number
                    break;
                case UtilityTypes.NodeType.Telephone:
                    value = ScrambleTelephone();
                    break;
                default:
                    throw new NotImplementedException();
            }
            return value;
        }
        /// <summary>
        /// Logic for scrambling telephone
        /// </summary>
        /// <returns></returns>
        public static string ScrambleTelephone()
        {
            return ConfigurationManager.AppSettings["scrabledTelephoneValue"].ToString();
        }
        /// <summary>
        /// Get NN for scrambling
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int64 GetScramblingNN(string value)
        {
            var nCandidate = String.Join(" ", value) .Substring(0, 3).ToCharArray();
            Func<char[], int, int> calculateParameter = null;
            calculateParameter = (char[] cArray, int acc) =>
            {
                var sValue = 0;
                if (cArray.Length == 1)
                {
                    char c = cArray[0];
                    sValue = GetScrambleValueFromAlphabet(c);
                    acc = acc + sValue;
                    return acc;
                }
                else
                {
                    char c = cArray[0];
                    sValue = GetScrambleValueFromAlphabet(c);
                    acc = acc + sValue;
                    return calculateParameter(cArray.Skip(1).ToArray(), acc);
                }
            };
            var N = calculateParameter(nCandidate, 0);
            Func<char[], List<string>, List<string>> concatenateScrambleValue = null;
            concatenateScrambleValue = (char[] cArray, List<string> l) =>
            {
                if (cArray.Length == 1)
                {
                    char c = cArray[0];
                    l.Add(GetScrambleValueFromAlphabet(c).ToString());
                    return l;
                }
                else
                {
                    char c = cArray[0];
                    l.Add(GetScrambleValueFromAlphabet(c).ToString());
                    return concatenateScrambleValue(cArray.Skip(1).ToArray(), l);
                }
            };
            Int64 NN = 0;
            var interNN = N.ToString() + String.Join("", concatenateScrambleValue(nCandidate, new List<string>()).ToArray());
            if (interNN.ToCharArray().Length <= 11)
            {
                NN = Convert.ToInt64(interNN);
            }
            else
            {
                NN = Convert.ToInt64(interNN.Substring(0, 11));
            }
            return NN;
        }
        /// <summary>
        /// Logic for scrambling name
        /// </summary>
        /// <param name="nameCSVList"></param>
        /// <param name="value"></param>
        public static string ScrambleName(List<NameCSVModel> nameCSVList, string value)
        {
            try
            {
                var NN = GetScramblingNN(value);
                var rows = nameCSVList.Count;
                var NC = NN % rows;
                var selectedRow = nameCSVList.Where(x => nameCSVList.IndexOf(x) == NC).FirstOrDefault();
                var unmasked_lastname = value.Split(' ')[1];
                if (unmasked_lastname.Trim()==selectedRow.LAST_NAME.Trim())
                {
                    if (NC + 1 == nameCSVList.Count)
                    {
                        selectedRow = nameCSVList.FirstOrDefault();
                    }
                    else
                    {
                        selectedRow = nameCSVList.Where(x => nameCSVList.IndexOf(x) == NC + 1).FirstOrDefault();
                    }
                }
                return $"{selectedRow.FIRST_NAME.Trim()} {selectedRow.LAST_NAME.Trim()}";
            }
            catch (Exception)
            {
                return value;
            }
        }
        /// <summary>
        /// Logic for scrambling address
        /// </summary>
        /// <param name="addressCSVList"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ScrambleAddress(List<AddressCSVModel> addressCSVList, string value)
        {
            try
            {
                var NN = GetScramblingNN(value);
                var rows = addressCSVList.Count;
                var NC = NN % rows;
                var selectedRow = addressCSVList.Where(x => addressCSVList.IndexOf(x) == NC).FirstOrDefault();
                if (value.Trim()==selectedRow.ADDR_LINE_ONE.Trim())
                {
                    if (NC+1==addressCSVList.Count)
                    {
                        selectedRow = addressCSVList.FirstOrDefault();
                    }
                    else
                    {
                        selectedRow = addressCSVList.Where(x => addressCSVList.IndexOf(x) == NC+1).FirstOrDefault();
                    }
                }
                return $"{selectedRow.ADDR_LINE_ONE.Trim()}";
            }
            catch (Exception)
            {
                return value;
            }
        }
        /// <summary>
        /// Logic for scrambling email
        /// </summary>
        public static string ScrambleEmail(List<NameCSVModel> nameCSVList, string value)
        {
            try
            {
                var NN = GetScramblingNN(value);
                var rows = nameCSVList.Count;
                var NC = NN % rows;
                var selectedRow = nameCSVList.Where(x => nameCSVList.IndexOf(x) == NC).FirstOrDefault();
                return $"test.{selectedRow.LAST_NAME.Trim()}@stb-noreply.no";
            }
            catch (Exception)
            {
                return value;
            }
        }
        /// <summary>
        /// Logic for scrambling ssn
        /// </summary>
        /// </summary>
        public static string ScrambleSSN()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get scramble value for characters
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int GetScrambleValueFromAlphabet(char c)
        {
            var value = 0;
            switch (c)
            {
                case 'A':
                case 'a':
                    value = 1;
                    break;
                case 'B':
                case 'b':
                    value = 2;
                    break;
                case 'C':
                case 'c':
                    value = 3;
                    break;
                case 'D':
                case 'd':
                    value = 4;
                    break;
                case 'E':
                case 'e':
                    value = 5;
                    break;
                case 'F':
                case 'f':
                    value = 6;
                    break;
                case 'G':
                case 'g':
                    value = 7;
                    break;
                case 'H':
                case 'h':
                    value = 8;
                    break;
                case 'I':
                case 'i':
                    value = 9;
                    break;
                case 'J':
                case 'j':
                    value = 10;
                    break;
                case 'K':
                case 'k':
                    value = 11;
                    break;
                case 'L':
                case 'l':
                    value = 12;
                    break;
                case 'M':
                case 'm':
                    value = 13;
                    break;
                case 'N':
                case 'n':
                    value = 14;
                    break;
                case 'O':
                case 'o':
                    value = 15;
                    break;
                case 'P':
                case 'p':
                    value = 16;
                    break;
                case 'Q':
                case 'q':
                    value = 17;
                    break;
                case 'R':
                case 'r':
                    value = 18;
                    break;
                case 'S':
                case 's':
                    value = 19;
                    break;
                case 'T':
                case 't':
                    value = 20;
                    break;
                case 'U':
                case 'u':
                    value = 21;
                    break;
                case 'V':
                case 'v':
                    value = 22;
                    break;
                case 'W':
                case 'w':
                    value = 23;
                    break;
                case 'X':
                case 'x':
                    value = 24;
                    break;
                case 'Y':
                case 'y':
                    value = 25;
                    break;
                case 'Z':
                case 'z':
                    value = 26;
                    break;
            }
            return value;
        }
        /// <summary>
        /// Function containing logic for traversing the input XML to find the specific nodename
        /// </summary>
        /// <param name="children"></param>
        /// <param name="nodename"></param>
        /// <param name="nodetype"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int TraverseXML(IEnumerable<XElement> children, string nodename, UtilityTypes.NodeType nodetype, int count)
        {
            try
            {
                foreach (var node in children)
                {
                    if (node.HasElements)
                    {
                        count = ScramblerUtil.TraverseXML(node.Elements(), nodename, nodetype, count);
                    }
                    else if (node.Name.ToString().ToUpper() == nodename.ToUpper())
                    {
                        node.Value = ScramblerUtil.Scramble(node.Value, nodetype);
                        count++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return count;
        }
        /// <summary>
        /// Function containing logic to save the modified XML to the output path
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="xml"></param>
        public static void Outputact(string filename, XDocument xml)
        {
            Console.WriteLine(mgr.GetString("enter_dest_path"));
            var dest = Console.ReadLine();
            if (!String.IsNullOrEmpty(dest))
            {
                xml.Save($"{dest}/{filename}");
                Console.WriteLine(mgr.GetString("xml_generation_success"));
            }
            else
                Console.WriteLine(mgr.GetString("invalid_path"));
        }
        /// <summary>
        /// Function containing logic to ask for repeat option
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="xml"></param>
        public static void OptionEntry(string filename, XDocument xml)
        {
            var option = Console.ReadLine();
            if (!String.IsNullOrEmpty(option.ToString()))
            {
                try
                {
                    switch (Convert.ToInt32(option))
                    {
                        case 1:
                            NodeNameAct(filename, xml);
                            break;
                        case 2:
                            ScramblerUtil.Outputact(filename, xml);
                            break;
                        default:
                            Console.WriteLine($"{mgr.GetString("invalid_option")}");
                            OptionEntry(filename, xml);
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"{mgr.GetString("invalid_option")}");
                    OptionEntry(filename, xml);
                }
            }
            else
            {
                Console.WriteLine(mgr.GetString("invalid_option"));
                OptionEntry(filename, xml);
            }
        }
        /// <summary>
        /// Function taking node type input
        /// </summary>
        /// <returns></returns>
        public static UtilityTypes.NodeType EnterNodeType()
        {
            var nodetype = Console.ReadLine();
            try
            {
                var pnodetype = Convert.ToInt32(nodetype);
                if (Enum.IsDefined(typeof(UtilityTypes.NodeType), pnodetype))
                {
                    return (UtilityTypes.NodeType)pnodetype;
                }
                else
                {
                    Console.WriteLine(mgr.GetString("invalid_nodetype"));
                    return EnterNodeType();
                }
                //return (UtilityTypes.NodeType)Enum.Parse(typeof(UtilityTypes.NodeType), nodetype);
            }
            catch (Exception)
            {
                Console.WriteLine(mgr.GetString("invalid_nodetype"));
                return EnterNodeType();
            }
        }
        /// <summary>
        /// Function taking node name input
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="xml"></param>
        public static void NodeNameAct(string filename, XDocument xml)
        {
            Console.WriteLine(mgr.GetString("enter_nodename"));
            var nodename = Console.ReadLine();
            Console.WriteLine($"{mgr.GetString("enter_nodetype")} ( {mgr.GetString("name")}, {mgr.GetString("address")}, {mgr.GetString("email")}, {mgr.GetString("ssn")} )");
            Console.WriteLine($"1: {mgr.GetString("name")}");
            Console.WriteLine($"2: {mgr.GetString("address")}");
            Console.WriteLine($"3: {mgr.GetString("email")}");
            Console.WriteLine($"4: {mgr.GetString("ssn")}");

            var type = ScramblerUtil.EnterNodeType();
            if (!String.IsNullOrEmpty(nodename))
            {
                var count = 0;
                Console.WriteLine(mgr.GetString("scrambling_progress"));
                var new_count = ScramblerUtil.TraverseXML(xml.Root.Elements(), nodename, type, count);
                if (new_count > 0)
                    Console.WriteLine(mgr.GetString("scrambling_success"));
                else
                    Console.WriteLine(mgr.GetString("invalid_nodename"));

                Console.WriteLine($"{mgr.GetString("nodename_option")}?");
                Console.WriteLine($"1: {mgr.GetString("yes")}");
                Console.WriteLine($"2: {mgr.GetString("no")}");


                ScramblerUtil.OptionEntry(filename, xml);
            }
            else
            {
                Console.WriteLine(mgr.GetString("invalid_nodename2"));
                NodeNameAct(filename, xml);
            }
        }
    }
}
