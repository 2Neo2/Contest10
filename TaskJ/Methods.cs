using System;
using System.Collections.Generic;
using System.Xml;

public class Methods
{
    public static XmlDocument GetDocument(string company, List<string> persons)
    {
       XmlDocument doc = new XmlDocument();
       var bosses = new Dictionary<string, List<XmlNode>>();
       var _persons = new Dictionary<string, XmlNode>();
       
       XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
       doc.AppendChild(docNode);
       
       XmlNode companyNode = doc.CreateElement("company");
       XmlAttribute companyAttribute = doc.CreateAttribute("name");
       companyAttribute.Value = $"{company}";
       companyNode.Attributes.Append(companyAttribute);
       doc.AppendChild(companyNode);
      
       var data = persons[0].Split('\t');
       bosses.Add(data[0], new List<XmlNode>());
       XmlNode boss = doc.CreateElement("person");
       XmlAttribute personAttribute = doc.CreateAttribute("id");
       personAttribute.Value = $"{data[0]}";
       XmlAttribute personAttribute1 = doc.CreateAttribute("name");
       personAttribute1.Value = $"{data[3]}";
       XmlAttribute personAttribute2 = doc.CreateAttribute("position");
       personAttribute2.Value = $"{data[2]}";
       boss.Attributes.Append(personAttribute);
       boss.Attributes.Append(personAttribute1);
       boss.Attributes.Append(personAttribute2);
       _persons.Add(data[0], boss);
       
       for (int i = 1; i < persons.Count; i++)
       {
           data = persons[i].Split('\t');
           XmlNode person = doc.CreateElement("person");
           XmlAttribute personAttribute0 = doc.CreateAttribute("id");
           personAttribute0.Value = $"{data[0]}";
           XmlAttribute personAttribute01 = doc.CreateAttribute("name");
           personAttribute01.Value = $"{data[3]}";
           XmlAttribute personAttribute02 = doc.CreateAttribute("position");
           personAttribute02.Value = $"{data[2]}";
           person.Attributes.Append(personAttribute0);
           person.Attributes.Append(personAttribute01);
           person.Attributes.Append(personAttribute02);
           _persons.Add(data[0], person);

           if (bosses.ContainsKey(data[1]))
           {
               bosses[data[1]].Add(person);
           }
           else
           {
               bosses.Add(data[1], new List<XmlNode>());
               bosses[data[1]].Add(person);
           }
           
       }
       
       foreach (var el in bosses)
       {
           foreach (var value in el.Value)
           {
               _persons[el.Key].AppendChild(value);
           }
       }

       companyNode.AppendChild(boss);
       return doc;
    }

    public static string CreateTextSummary(string text, int maxLenght = 100)
    {
        string result = "";

        foreach (var item in text)
        {
            if (item == '\n')
                result += ' ';
            else
                result += item;
        }

        return result;
    }
}

