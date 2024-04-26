using System.Xml.Serialization;

public class QuestNode
{
    [XmlElementAttribute("NodeTitle")]
    public string nodeTitle;

    [XmlElementAttribute("NodeText")]
    public string nodeText;

    [XmlElementAttribute("NodeID")]
    public int nodeID;

    public override string ToString()
    {
        return string.Format("NodeID: {0}, NodeTitle: {1}, NodeText: {2}", nodeID, nodeTitle, nodeText);
    }
}
