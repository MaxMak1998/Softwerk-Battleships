using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BattlefieldScript : MonoBehaviour {
    private GameObject currentPlayer;
    private int playerCounter;
    void Start() {
        playerCounter = PlayerPrefs.GetInt("counter");
        if (playerCounter == 1) {
            currentPlayer = GameObject.Find("Player1Object");
            currentPlayer.GetComponent<PlayerScript>().setPlayerName(PlayerPrefs.GetString("p1"));
        }else {
            currentPlayer = GameObject.Find("Player2Object");
            currentPlayer.GetComponent<PlayerScript>().setPlayerName(PlayerPrefs.GetString("p2"));
        }
    }

    void savePlayerShips() {
        GameObject[] allShips = GameObject.FindGameObjectsWithTag("Ship");
        foreach (GameObject ship in allShips) {
            List<Tile> listOfTiles = new List<Tile>();
            foreach (Vector3 pos in ship.GetComponent<shipBehavior>().occupiedCells){
                listOfTiles.Add(new Tile(pos));
            }
            Ship newShip = new Ship(ship.name, listOfTiles);
            currentPlayer.GetComponent<PlayerScript>().addShip(newShip);
        }
        if (playerCounter == 1) {
            PlayerPrefs.SetInt("counter", 2);
            SceneManager.LoadScene("Setup");
        }else {
            //  Load Battle Scene!!!
        }
        
    }

    
    void Update() {
        
    }
}
