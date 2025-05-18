using UnityEngine;

public class ChangeColorCoroutine : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TriggerColorChange()
    {
        StartCoroutine(ChangeToRedTemporarily(0.5f));
    }

    private System.Collections.IEnumerator ChangeToRedTemporarily(float duration)
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(duration);

        spriteRenderer.color = originalColor;
    }
}
