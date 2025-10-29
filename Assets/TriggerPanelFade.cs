using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TriggerPanelFade : MonoBehaviour
{
    public CanvasGroup panel; // Reference your panel's CanvasGroup
    public float fadeDuration = 1f; // seconds

    private void Start()
    {
        if (panel != null)
            panel.alpha = 0f; // start hidden
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (panel != null)
                StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            panel.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
    }
}
