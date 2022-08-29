using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecflowSeleniumEasy.Hooks
{
    public class Webelementsandvars
    {
        public ScenarioContext currentcontext;

        public Webelementsandvars(ScenarioContext sc)
        {
            currentcontext = sc;
        }
        public string findxpathid(string inputelementName)
        {
            try
            {
                string elementName = convert(inputelementName);
                string file = elementName.Split('.')[0];
                string MyFilePath = "PageObjects\\" + file + ".txt";
                string foundElement = "";
                foreach (var match in File.ReadLines(MyFilePath)
                              .Select((text, index) => new { text, lineNumber = index + 1 })
                              .Where(x => x.text.Contains(elementName)))
                {
                    Console.WriteLine("{0}: {1}", match.lineNumber, match.text);
                    foundElement = match.text;
                    break;
                }
                return convert(foundElement.Split(':')[1]); 
            }
            catch (Exception ex)
            {
                throw new Exception("Error when find web element. Detail: " + ex.Message);
            }

        }
        public string convert(string input)
        {
            try
            {
                if (input == null)
                {
                    return input;
                }

                Regex r2 = new Regex(@"%(.+?)%");
                MatchCollection mc2 = r2.Matches(input);
                foreach (Match m2 in mc2)
                {
                    var name = m2.Groups[1].Value;
                    var value = Environment.GetEnvironmentVariable(name);
                    //Console.WriteLine("debug: "+value);
                    if (value != null && value.Length > 0)
                    {
                        Console.WriteLine(DateTime.Now.ToString() + "--Assign value" + value + " Global variables named " + m2.Groups[0].Value);
                        input = input.Replace(m2.Groups[0].Value, value);
                    }

                }
                Regex r = new Regex(@"#(.+?)#");
                MatchCollection mc = r.Matches(input);
                foreach (Match m in mc)
                {
                    var name = m.Groups[1].Value;
                    var value = currentcontext.Get<string>(name);
                    //Console.WriteLine("debug: "+value);
                    if (value != null && value.Length > 0)
                    {
                        Console.WriteLine(DateTime.Now.ToString() + "--Assign value" + value + " local variables named " + m.Groups[0].Value);
                        input = input.Replace(m.Groups[0].Value, value);
                    }

                }

                

            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString() + "--Can not translate variable " + input + "error: " + ex.Message + ex.InnerException);
                return null;
                throw new Exception("Can not translate variable " + input + "error: " + ex.Message + ex.InnerException);
            }
            return input;
        }
    }
}
