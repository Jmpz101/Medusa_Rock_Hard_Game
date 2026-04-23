using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueLine : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CanvasGroup canvasGroup;

    public float lifetime = 4f;
    public float fadeDuration = 1f;
    public float typingSpeed = 0.02f;

    private float timer;
    private string fullText;


    public void Init(string content)
    {
        fullText = content;
        text.text = "";
        timer = lifetime;

        canvasGroup.alpha = 0;

        StartCoroutine(FadeIn());
        StartCoroutine(TypeText());
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < fadeDuration)
        {
            canvasGroup.alpha = timer / fadeDuration;
        }

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator FadeIn()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 4f;
            canvasGroup.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }
    }

    IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
