using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public int nextNodeIndex;
}

[System.Serializable]
public class DialogueNode
{
    [TextArea]
    public string text;

    public DialogueChoice[] choices;
}

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    public DialogueNode[] nodes;

    private int currentNode = 0;
    private bool isActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isActive)
            {
                StartDialogue();
            }
            else
            {
                NextNode(0); // default choice
            }
        }
    }

    void StartDialogue()
    {
        isActive = true;
        currentNode = 0;
        ShowNode();
    }

    public void NextNode(int choiceIndex)
    {
        if (nodes[currentNode].choices.Length == 0)
        {
            EndDialogue();
            return;
        }

        currentNode = nodes[currentNode].choices[choiceIndex].nextNodeIndex;
        ShowNode();
    }

    void ShowNode()
    {
        dialogueText.text = nodes[currentNode].text;
    }

    void EndDialogue()
    {
        isActive = false;
        dialogueText.text = "";
    }
}