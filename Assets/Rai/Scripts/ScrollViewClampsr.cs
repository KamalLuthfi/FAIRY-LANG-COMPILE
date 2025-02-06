using UnityEngine;
using UnityEngine.UI;

public class ScrollViewClamp : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float topLimit = 0f;  // Top limit for the scroll position
    public float bottomLimit = -100f; // Bottom limit for the scroll position (adjust as needed)

    void Update()
    {
        Vector2 currentPosition = scrollRect.content.anchoredPosition;

        // Clamp the vertical position
        float clampedY = Mathf.Clamp(currentPosition.y, bottomLimit, topLimit);

        // Apply the clamped position back to the scroll content
        scrollRect.content.anchoredPosition = new Vector2(currentPosition.x, clampedY);
    }
}
