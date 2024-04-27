using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem Instance;
    public string questFileName = "quest1.xml";
    public GameObject player;
    public GameObject questGiver;
    public GameObject OptionPrefab, InventoryItemPrefab;
    public int currentQuestNodeId = 1;
    public Quest currentQuest;
    public List<GameObject> questItems;
    public bool questPanelActive, questIsActive, gatheredAllItems, questIsComplete = false;

    // UI elements
    public GameObject questPanel;
    public GameObject questText;
    public GameObject optionPanel, objectivePanel, inventoryPanel, itemPanel;
    public GameObject defaultOption;
    public TextMeshProUGUI objectiveText;


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
                questIsActive = true;
                gatheredAllItems = false;

                // For each item in the current node, get the item from resources and instantiate it
                foreach (Item item in currentNode.ItemsForQuest.Items)
                {
                    GameObject itemGO = Resources.Load<GameObject>("Items/" + item.Name);
                    if (itemGO != null)
                    {
                        for (int i = 0; i < item.Amount; i++)
                        {
                            GameObject itemInstance = Instantiate(itemGO, new Vector3(-100, 1, -100), Quaternion.identity);
                            questItems.Add(itemInstance);
                        }
                    }
                }
            }
            else if (currentNode.Type == "EndQuest")
            {
                questIsComplete = true;
            }
            // If the currentNode if of type "End" reset the iD to 1
            if (currentNode.Type == "End")
            {
                currentQuestNodeId = 1;
            }
        }
    }

    void Update()
    {
        // Objective text to display in the UI
        if (!questIsActive)
        {
            objectiveText.text = "Objective: Talk to the quest giver";
        }
        else if (questIsActive && !gatheredAllItems)
        {
            objectiveText.text = "Objective: Gather all these items:";
            foreach (GameObject item in questItems)
            {
                objectiveText.text += "\n" + item.name;
                // Remove "(Clone)" from the item name
                objectiveText.text = objectiveText.text.Replace("(Clone)", "");
            }
        }
        else if (questIsActive && gatheredAllItems && !questIsComplete)
        {
            objectiveText.text = "Objective: Return to the quest giver";
        }
        else if (questIsComplete)
        {
            ClearInventory();
            objectiveText.text = "Objective: Quest is complete. Continue to the next level";
        }

        // If Tab key is pressed, show/hide the inventory panel
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }

        // If questPanel is active let the player use the mouse to interact with the UI
        if (questPanelActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            objectivePanel.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            objectivePanel.SetActive(true);
        }

        // If the player has not gathered all items, set the current node to the node of type "QuestIsActive"
        if (!gatheredAllItems && questIsActive)
        {
            SetCurrentNode(currentQuest.Nodes.Find(x => x.Type == "QuestIsActive").Id);
        }
        
        // Check if player presses the "E" key to interact with the quest giver
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(player.transform.position, questGiver.transform.position);
            if (distance < 3)
            {
                // If the player has gathered all items, and they talk to the quest giver, set the current node to the node of type "EndQuest"
                if (gatheredAllItems && questIsActive)
                {
                    SetCurrentNode(currentQuest.Nodes.Find(x => x.Type == "EndQuest").Id);
                }

                questPanelActive = true;

                questPanel.SetActive(true);
                UpdateQuestUI();
            }
        }

        // If questPanel is active, check if player presses the "Q" key to close the quest panel
        if (questPanel.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            questPanelActive = false;

            questPanel.SetActive(false);
        }

    }

    public void CheckItem(string itemName)
    {
        if (questIsActive)
        {
            GameObject item = questItems.Find(x => x.name == itemName);
            if (item != null)
            {
                bool itemExists = false;

                string itemNameWithoutClone = itemName.Replace("(Clone)", "");

                // Add the item to the inventory panel, if it already exists, increase the amount
                foreach (Transform child in itemPanel.transform)
                {
                    if (child.GetComponent<InventoryItem>().itemName == itemNameWithoutClone && !itemExists)
                    {
                        child.GetComponent<InventoryItem>().AddAmount(1);
                        itemExists = true;
                    }
                }

                if (!itemExists)
                {
                    GameObject inventoryItem = Instantiate(InventoryItemPrefab, itemPanel.transform);
                    inventoryItem.GetComponent<InventoryItem>().SetItem(itemNameWithoutClone, 1);
                }

                questItems.Remove(item);
                Destroy(item);

                if (questItems.Count == 0)
                {
                    gatheredAllItems = true;
                }
            }
        }
    }

    public void ClearInventory()
    {
        foreach (Transform child in itemPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
    
}
