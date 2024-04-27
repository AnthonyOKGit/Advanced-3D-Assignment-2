using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionButton : MonoBehaviour
{
    public TextMeshProUGUI optionText;
    public int nextNodeId;

    public void SetOption(string text, int nextNode)
    {
        optionText.text = text;
        nextNodeId = nextNode;
    }

    public void OnOptionSelected()
    {
        QuestSystem.Instance.SetCurrentNode(nextNodeId);
    }
}
