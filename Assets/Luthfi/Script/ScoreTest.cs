using UnityEngine;
using UnityEngine.UI;

public class StarUI : MonoBehaviour
{
    public Image[] starImages; // Assign star UI elements in the Inspector
    public Sprite activeStarSprite; // Highlighted star
    public Sprite inactiveStarSprite; // Greyed-out star

    void Start()
    {
        UpdateStarUI();
    }

    public void UpdateStarUI()
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            if (ProgressManager.Instance.levelsCompleted[i])
            {
                starImages[i].sprite = activeStarSprite;
            }
            else
            {
                starImages[i].sprite = inactiveStarSprite;
            }
        }
    }
}
