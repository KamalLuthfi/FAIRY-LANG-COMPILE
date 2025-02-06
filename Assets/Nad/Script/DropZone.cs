using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public static bool correctDrop = false;
    public int points = 10; // Points for correct drop

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null)
        {
            // Check if this is the correct zone
            if (draggedObject.CompareTag("CorrectItem"))
            {
                Debug.Log("Correct Drop!");
                GameManager.Instance.AddScore(points);
                correctDrop = true;
            }
            else
            {
                Debug.Log("Wrong Drop!");
                GameManager.Instance.AddScore(-points); // Penalize for incorrect drop
                correctDrop = false;
            }
        }
    }
}
