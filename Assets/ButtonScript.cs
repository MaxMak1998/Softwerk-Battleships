using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
    public GameObject nameSelectPopup;
    public Button newGameButton;
    public Button startGameButton;
    // public Button cancelButton;
    public InputField player1name;
    public InputField player2name;

    void Start() {
        
    }

    public void exitGame() {
        Application.Quit();
    }

    public void newGame() {
        nameSelectPopup.SetActive(true);
        newGameButton.interactable = false;
    }

    public void startGame() {
        Debug.Log("Pretend the Game has Started!!!");
        PlayerPrefs.SetString("p1", player1name.text);
        PlayerPrefs.SetString("p2", player2name.text);
        PlayerPrefs.SetInt("counter", 1);
        SceneManager.LoadScene("Setup");
    }

    public void cancel() {
        nameSelectPopup.SetActive(false);
        newGameButton.interactable = true;
    }

    void Update() {
        if (player1name.text.Length > 0 && player2name.text.Length > 0) {
            startGameButton.interactable = true;
        }else {
            startGameButton.interactable = false;
        }
    }
}
