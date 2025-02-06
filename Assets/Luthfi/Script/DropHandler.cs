using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DDExeDropHandler : MonoBehaviour, IDropHandler
{
    public string correctTag;
    public TextMeshProUGUI feedbackText;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null && droppedObject.CompareTag(correctTag))
        {
            droppedObject.transform.SetParent(transform);
            droppedObject.transform.position = transform.position;

            // Give feedback for correct answer
            Debug.Log("Correct!");
            feedbackText.text = "Correct!";

        }
        else
        {
            // Give feedback for incorrect answer
            Debug.Log("Incorrect!");
            feedbackText.text = "Incorrect!";

        }
    }
}