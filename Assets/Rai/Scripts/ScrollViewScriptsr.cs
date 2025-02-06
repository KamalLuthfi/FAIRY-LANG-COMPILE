using UnityEngine;
using UnityEngine.UI;

public class ScrollViewScript : MonoBehaviour
{
    public GameObject content;
    public GameObject itemPrefab;  // Prefab for the items to add to the scroll view

    void Start()
    {
        PopulateScrollView();
    }

    void PopulateScrollView()
    {
        for (int i = 0; i < 20; i++) // Example: Add 20 items
        {
            GameObject newItem = Instantiate(itemPrefab, content.transform);
            newItem.GetComponentInChildren<Text>().text = "Item " + (i + 1);
        }
    }
}