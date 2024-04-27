using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem Instance;
    public string questFileName = "quest1.xml";
    public GameObject player;
    public GameObject questGiver;
    public GameObject OptionPrefab;
    public int currentQuestNodeId = 1;
    public Quest currentQuest;
    public List<Item> itemsNeededForQuest = new List<Item>();
    public bool questPanelActive, gatheredAllItems, questIsComplete = false;

    // UI elements
    public GameObject questPanel;
    public GameObject questText;
    public GameObject optionPanel;
    public GameObject defaultOption;


    void Start()
    {
        Instance = this;

        LoadQuest();
    }

    void LoadQuest()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Quest));
        using (FileStream stream = new FileStream("Assets/Resources/Quests/" + questFileName, FileMode.Open))
        {
            Quest quest = serializer.Deserialize(stream) as Quest;
            if (quest != null)
            {
                foreach (Node node in quest.Nodes)
                {
                    Debug.Log("Node Id: " + node.Id);
                    Debug.Log("Node Type: " + node.Type);
                    Debug.Log("Node Text: " + node.Text);
                    if (node.Options != null)
                    {
                        foreach (Option option in node.Options.OptionsList)
                        {
                            Debug.Log("Option Text: " + option.Text);
                            Debug.Log("Option NextNode: " + option.NextNode);
                        }
                    }
                    if (node.ItemsForQuest != null)
                    {
                        foreach (Item item in node.ItemsForQuest.Items)
                        {
                            Debug.Log("Item Name: " + item.Name);
                            Debug.Log("Item Amount: " + item.Amount);
                        }
                    }
                }

                currentQuest = quest;
            }
        }
    }

    public void SetCurrentNode(int nodeId)
    {
        currentQuestNodeId = nodeId;
        UpdateQuestUI();
    }

    void UpdateQuestUI()
    {
        Node currentNode = currentQuest.Nodes.Find(x => x.Id == currentQuestNodeId);
        if (currentNode != null)
        {
            questText.GetComponent<TMPro.TextMeshProUGUI>().text = currentNode.Text;

            foreach (Transform child in optionPanel.transform)
            {
                // If child contains the OptionButton script, destroy it
                if (child.GetComponent<OptionButton>())
                {
                    Destroy(child.gameObject);
                }
            }

            if (currentNode.Options != null)
            {
                defaultOption.SetActive(false);

                foreach (Option option in currentNode.Options.OptionsList)
                {
                    GameObject optionGO = Instantiate(OptionPrefab, optionPanel.transform);
                    optionGO.GetComponent<OptionButton>().SetOption(option.Text, option.NextNode);
                }
            }
            else
            {
                defaultOption.SetActive(true);
            }

            if (currentNode.Type == "StartQuest")
            {
                itemsNeededForQuest = currentNode.ItemsForQuest.Items;
            }
            else if (currentNode.Type == "EndQuest")
            {
                questIsComplete = true;
            }
        }
    }

    void Update()
    {
        // If questPanel is active let the player use the mouse to interact with the UI
        if (questPanelActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        // Check if player presses the "E" key to interact with the quest giver
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(player.transform.position, questGiver.transform.position);
            if (distance < 3)
            {
                questPanelActive = true;

                questPanel.SetActive(true);
                UpdateQuestUI();
            }
        }

        // If questPanel is active, check if player presses the "Q" key to close the quest panel
        if (questPanel.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            questPanel.SetActive(false);
        }
    }

    public void CheckItem(string itemName)
    {
        Item item = itemsNeededForQuest.Find(x => x.Name == itemName);
        if (item != null)
        {
            item.Amount--;
            if (item.Amount <= 0)
            {
                itemsNeededForQuest.Remove(item);
            }
        }

        if (itemsNeededForQuest.Count == 0)
        {
            gatheredAllItems = true;
        }
    }


    
}
