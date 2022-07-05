using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    private string playerName;
    List<Ship> listOfShips;
    public GameObject shipPlaceholderPrefab;
    public bool canShoot;
    float maxXvalue;
    float minXvalue;
    float maxYvalue;
    float minYvalue;

    void Start() {
        listOfShips = new List<Ship>();
        DontDestroyOnLoad(this.gameObject);
        canShoot = false;
    }

    public void setLimits(float maxX, float minX, float maxY, float minY) {
        this.maxXvalue = maxX;
        this.minXvalue = minX;
        this.maxYvalue = maxY;
        this.minYvalue = minY;
    }

    public bool targetWithinLimits(Vector3 target) {
        if (target.x <= maxXvalue && target.x >= minXvalue && target.y <= maxYvalue && target.y >= minYvalue) {
            return true;
        }
        return false;
    }

    public void setPlayerName(string newName) {
        this.playerName = newName;
    }

    public string getPlayerName() {
        return this.playerName;
    }

    public void addShip(Ship ship) {
        listOfShips.Add(ship);
    }

    public void placeShips(Vector3 positionShift) {
        foreach(Ship ship in listOfShips) {
            placeOneShip(ship, positionShift);
        }
    }

    private void placeOneShip(Ship ship, Vector3 positionShift) {
        foreach(Tile tile in ship.positions) {
            Instantiate(shipPlaceholderPrefab, tile.coordinates + positionShift, Quaternion.identity);
        }
    }

    public List<Ship> getShips() {
        return listOfShips;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
