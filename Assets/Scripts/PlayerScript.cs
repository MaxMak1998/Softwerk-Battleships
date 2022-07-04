using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    private string playerName;
    List<Ship> listOfShips;

    void Start() {
        DontDestroyOnLoad(this.gameObject);
    }

    public void setPlayerName(string newName) {
        this.playerName = newName;
    }

    public void addShip(Ship ship) {
        listOfShips.Add(ship);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
