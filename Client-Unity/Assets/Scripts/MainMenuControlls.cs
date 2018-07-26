using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControlls : MonoBehaviour {

    public GameObject MMCamera;
    public GameObject ControllsCamera;
    public GameObject CreditsCamera;

	// Use this for initialization
	void Start () {
        // Set the main menu camera to active.
        MMCamera.SetActive(true);
        // Set all other cameras to inactive.
        ControllsCamera.SetActive(false);
        CreditsCamera.SetActive(false);
	}

    // This function will set all cameras off.
    private void allCamerasOff() {
        MMCamera.SetActive(false);
        ControllsCamera.SetActive(false);
        CreditsCamera.SetActive(false);
    }

    // Build 1 function to make active each camera.
    public void MMCamOn() {
        // First turn off any active camera.
        allCamerasOff();
        // Then turn MMCamera on.
        MMCamera.SetActive(true);
    }

    public void ControllsCamOn() {
        // First turn off any active camera.
        allCamerasOff();
        // Then turn ControllsCamera on.
        ControllsCamera.SetActive(true);
    }

    public void CreditsCamOn() {
        // First turn off any active camera.
        allCamerasOff();
        // Then turn CreditsCamera on.
        CreditsCamera.SetActive(true);
    }

    // Function to exit aplication.
    public void Exit() {
        Application.Quit();
    }

    // Load scenes for math module
    public void LoadMathGrapher() {
        SceneManager.LoadScene(1);
    }
}
