using UnityEngine;
using System.Collections;

public class HoverDisplay : MonoBehaviour
{
    [Header("References")]
    public CanvasGroup infoPanel;    // Assign the CanvasGroup in the Inspector
    public string playerTag = "Player"; // Tag of the Player object

    [Header("Fade Settings")]
    public float fadeDuration = 0.5f; // Duration of fade in/out in seconds

    private Coroutine currentFade;

    private void Start()
    {
        // Ensure the panel starts hidden
        if (infoPanel != null)
        {
            infoPanel.alpha = 0f;
            infoPanel.interactable = false;
            infoPanel.blocksRaycasts = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (currentFade != null) StopCoroutine(currentFade);
            currentFade = StartCoroutine(FadeCanvasGroup(infoPanel, infoPanel.alpha, 1f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (currentFade != null) StopCoroutine(currentFade);
            currentFade = StartCoroutine(FadeCanvasGroup(infoPanel, infoPanel.alpha, 0f));
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end)
    {
        float elapsed = 0f;

        // Toggle interaction depending on visibility
        cg.interactable = (end > 0f);
        cg.blocksRaycasts = (end > 0f);

        while (elapsed < fadeDuration)
        {
            cg.alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cg.alpha = end;
    }
}
