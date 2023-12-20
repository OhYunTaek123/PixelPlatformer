using System.Collections;
using UnityEngine;

public class GhostAttackEffect : MonoBehaviour
{
    public float fadeSpeed = 1f;
    private SpriteRenderer spriteRenderer;
    private Color startColor;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
    }
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * fadeSpeed;

            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1f, 0f, elapsedTime));

            yield return null;
        }

        Destroy(gameObject);
    }
}
