using System.Collections.Generic;
using System.Xml.Serialization;

public class Quest {
    [XmlArray("Nodes")]
    [XmlArrayItem("Node")]
    public List<Node> Nodes;
}

public class Node
{
    [XmlAttribute("Id")]
    public int Id;

    [XmlAttribute("Type")]
    public string Type;

    [XmlAttribute("Text")]
    public string Text;

    [XmlElement("Options")]
    public Options Options;

    [XmlElement("ItemsForQuest")]
    public ItemsForQuest ItemsForQuest;
}

public class Options
{
    [XmlElement("Option")]
    public List<Option> OptionsList;
}

public class Option
{
    [XmlAttribute("Text")]
    public string Text;

    [XmlAttribute("NextNode")]
    public int NextNode;
}

public class ItemsForQuest
{
    [XmlElement("Item")]
    public List<Item> Items;
}

public class Item
{
    [XmlAttribute("Name")]
    public string Name;

    [XmlAttribute("Amount")]
    public int Amount;
}