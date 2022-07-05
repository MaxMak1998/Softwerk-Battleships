using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {
    public void changeToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
