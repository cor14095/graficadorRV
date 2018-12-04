using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GraphControls : MonoBehaviour {

    public GameObject player;
    public GameObject mainCamara;

    // Load scenes for main menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Change camera view
    public void ViewGraph()
    {
        player.SetActive(true);
        mainCamara.SetActive(false);
    }

    // Change camera view
    public void ViewScreen()
    {
        player.SetActive(false);
        mainCamara.SetActive(true);
    }
}
