using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour
{
    // Navigation Code

    public void Navigation(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

}
