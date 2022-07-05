using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class BattlefieldScript : MonoBehaviour {
    private GameObject currentPlayer;
    private int playerCounter;
    public Text playerSetupText;

    void Start() {
        // Ensures the scene is repeated for a second player
        playerCounter = PlayerPrefs.GetInt("counter");
        if (playerCounter == 1) {
            currentPlayer = GameObject.Find("Player1Object");
            currentPlayer.GetComponent<PlayerScript>().setPlayerName(PlayerPrefs.GetString("p1"));
            playerSetupText.text = PlayerPrefs.GetString("p1") + ", Place Your Ships!";
        }else {
            currentPlayer = GameObject.Find("Player2Object");
            currentPlayer.GetComponent<PlayerScript>().setPlayerName(PlayerPrefs.GetString("p2"));
            playerSetupText.text = PlayerPrefs.GetString("p2") + ", Place Your Ships!";
        }
        Debug.Log("Current Player: " + currentPlayer.GetComponent<PlayerScript>().getPlayerName());
    }

    // Saves positions of player ships
    public void savePlayerShips() {
        GameObject[] allShips = GameObject.FindGameObjectsWithTag("Ship");
        foreach (GameObject ship in allShips) {
            List<Tile> listOfTiles = new List<Tile>();
            foreach (Vector3 pos in ship.GetComponent<shipBehavior>().occupiedCells){
                listOfTiles.Add(new Tile(pos));
            }
            Ship newShip = new Ship(ship.name, listOfTiles, ship.transform.position, ship.transform.rotation.z);
            currentPlayer.GetComponent<PlayerScript>().addShip(newShip);
        }
        if (playerCounter == 1) {
            PlayerPrefs.SetInt("counter", 2);
            SceneManager.LoadScene("Setup");
        }else {
            SceneManager.LoadScene("Battle");
        }
        
    }

    
    void Update() {
        
    }
}
