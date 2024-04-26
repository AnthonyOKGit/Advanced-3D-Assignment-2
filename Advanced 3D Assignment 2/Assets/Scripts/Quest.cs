using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public class Quest
{
    [XmlArray("Nodes")]
    [XmlArrayItem("Node")]
    public List<QuestNode> Nodes;

    [XmlAttribute("QuestTitle")]
    public string questTitle;

    [System.NonSerialized]
    private QuestNode currentNode;

    [System.NonSerialized]
    private int currentNodeIndex;

    public QuestNode Progress()
    {
        if (hasNextNode()) {
            currentNode = Nodes[currentNodeIndex++];
            return currentNode;
        }
        return null;
    }

    public bool hasNextNode()
    {
        return Nodes != null && currentNodeIndex < Nodes.Count;
    }

    public static Quest Load(string fileName)
    {
        var serializer = new XmlSerializer(typeof(Quest));
        var stream = new FileStream(fileName, FileMode.Open);
        var quest = serializer.Deserialize(stream) as Quest;
        stream.Close();

        return quest;
    }
    
}
