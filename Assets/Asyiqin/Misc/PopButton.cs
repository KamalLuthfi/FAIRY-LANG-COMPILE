using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonPop : MonoBehaviour, IPointerClickHandler
{
    public float popScale = 1.2f; // The scale when the button pops
    public float popDuration = 0.1f; // Duration of the pop effect

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale; // Store the original size
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(PopEffect());
    }

    IEnumerator PopEffect()
    {
        transform.localScale = originalScale * popScale; // Scale up
        yield return new WaitForSeconds(popDuration);
        transform.localScale = originalScale; // Return to normal size
    }
}
