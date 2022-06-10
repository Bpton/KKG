using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Jsonclass
/// </summary>
public class Jsonclass
{
    public int Roll { get; set; }
    // Name of the student
    public string name { get; set; }
    // The List of courses studying
    public List<string> courses { get; set; }
    public Jsonclass()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    static void Main()
    {
        // Creating a new instance of class studentInfo
        Jsonclass student1 = new Jsonclass()
        {
            // Roll number
            Roll = 110,
            // Name
            name = "Alex",
            //list of courses
            courses = new List<string>()
      {
        "Math230",
        "Calculus1",
        "CS100",
        "ML"
      }
        };
        Console.WriteLine("JSON converted string: ");
        // convert to Json string by seralization of the instance of class.
        string stringjson = JsonConvert.SerializeObject(student1);
        Console.WriteLine(stringjson);
        string filepath = "../js/jsdata.json/";
        string updatedJson = JsonConvert.SerializeObject(stringjson, Formatting.Indented);
        File.WriteAllText(filepath, updatedJson);
    }
}
