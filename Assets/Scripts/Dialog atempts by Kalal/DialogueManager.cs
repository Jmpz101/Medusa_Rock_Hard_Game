using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour 
{
    public GameObject dialoguePrefab;
    public Transform container;

    public float moveSpeed = 200f;
    public float spacing = 60f;

    private List<RectTransform> lines = new List<RectTransform>();
    
    void Start()
    {
        AddLine("Hello there.");
        AddLine("This is a test.");
        AddLine("It should stack upward.");
    }

    public void AddLine(string text)
    {
        GameObject obj = Instantiate(dialoguePrefab, container);
        DialogueLine line = obj.GetComponent<DialogueLine>();
        line.Init(text);

        RectTransform rect = obj.GetComponent<RectTransform>();

        // Start at bottom
        rect.anchoredPosition = new Vector2(0, 0);

        lines.Insert(0, rect);

        UpdatePositions();
    }

    void UpdatePositions()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            float targetY = i * spacing;
            StartCoroutine(MoveTo(lines[i], targetY));
        }
    }

    System.Collections.IEnumerator MoveTo(RectTransform rect, float targetY)
    {
        Vector2 start = rect.anchoredPosition;
        Vector2 target = new Vector2(start.x, targetY);

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 5f;
            rect.anchoredPosition = Vector2.Lerp(start, target, t);
            yield return null;
        }

        rect.anchoredPosition = target;
    }
}