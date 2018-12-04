using Scrambler.Util;
using System;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Scrambler.Model;
using System.Threading;
using Scrambler.Model.BusinessEntities;

namespace Scrambler
{
    class Program
    {
        static void Main(string[] args)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("sv-SE");
            //Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            ResourceManager mgr = new ResourceManager("Scrambler.ScramblerMessages", Assembly.GetExecutingAssembly());
            ScramblerUtil.mgr = mgr;

            Console.WriteLine(mgr.GetString("enter_source_path"));
            Action filefeed = null;
            filefeed = () =>
            {
                var source = Console.ReadLine();
                if (!String.IsNullOrEmpty(source))
                {
                    if (File.Exists(source))
                    {
                        // read input xml
                        var xml = XDocument.Load(source);
                        if (xml != null)
                        {
                            var filename = Path.GetFileName(source);

                            if (!String.IsNullOrEmpty(filename))
                            {
                                ScramblerUtil.NodeNameAct(filename, xml);
                            }
                            else
                            {
                                Console.WriteLine(mgr.GetString("invalid_path2"));
                                filefeed();
                            }
                        }
                        else
                        {
                            Console.WriteLine(mgr.GetString("invalid_path2"));
                            filefeed();
                        }
                    }
                    else
                    {
                        Console.WriteLine(mgr.GetString("invalid_path2"));
                        filefeed();
                    }
                }
                else
                {
                    Console.WriteLine(mgr.GetString("invalid_path2"));
                    filefeed();
                }
            };
            filefeed();
            Console.ReadLine();
        }

    }
}
