using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GameControllerScript : MonoBehaviour {
    [SerializeField] private Camera cam;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Button button;

    void Start() {
        
    }

    Vector3 getCellPos() {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = tilemap.WorldToCell(mousePos);
        Vector3 highlightPos = tilemap.GetCellCenterWorld(gridPos);
        Debug.Log(highlightPos);
        return highlightPos;
    }

    public void allShipsPlaced() {
        GameObject[] allShips = GameObject.FindGameObjectsWithTag("Ship");
        bool areAllShipsPlaced = true;
        foreach (GameObject ship in allShips) {
            if (ship.GetComponent<SpriteRenderer>().color == new Color(1,0,0,1)) {
                areAllShipsPlaced = false;
            }
        }
        if (areAllShipsPlaced) {
            button.interactable = true;
        }else {
            button.interactable = false;
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // getCellPos();
        }
    }
}
