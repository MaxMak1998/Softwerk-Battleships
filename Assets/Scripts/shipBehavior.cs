using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class shipBehavior : MonoBehaviour {
    [SerializeField] private Tilemap tilemap;
    bool isColliding;
    BoxCollider2D boxCollider2D;
    public float shipLength;
    public bool isVertical;
    public List<Vector3> occupiedCells;
    void Start() {
        isColliding = false;
        boxCollider2D = GetComponent<BoxCollider2D>();
        isVertical = true;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Ship"){
            Debug.Log("Collided with a ship");
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Ship"){
            Debug.Log("Not Colliding");
            isColliding = false;
        }
    }

    // Returns list of tile positions, which are occupied by a ship
    void getAllTilePositions() {
        Vector3 pivotPoint = gameObject.transform.position;
        float distanceToPeak = shipLength/2 - 1;
        Vector3 peakVector;

        if (isVertical) {
            peakVector = new Vector3(0, 1, 0);
        }else {
            peakVector = new Vector3(1, 0, 0);
        }
        Vector3 peakPoint = pivotPoint + distanceToPeak * peakVector;
        List<Vector3> listOfCells = new List<Vector3>();

        for (float i = 0f; i < shipLength; i++) {
            Vector3Int gridPos = tilemap.WorldToCell(peakPoint - peakVector * i);
            Vector3 cellPos = tilemap.GetCellCenterWorld(gridPos);
            listOfCells.Add(cellPos);
        }

        occupiedCells = listOfCells;
    }

    bool isLegalPlacement() {
        if (isColliding) {
            return false;
        }
        getAllTilePositions();
        foreach (Vector3 cell in occupiedCells) {
            if (cell.x < 1.0f || cell.x > 10.0f) {
                occupiedCells = null;
                return false;
            }
            if (cell.y < -4.5f || cell.y > 4.5f) {
                occupiedCells = null;
                return false;
            }
        }
        return true;
    }

    public void saveShipPos() {
        if (isLegalPlacement()) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        }else {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
        }
    }
    
    void Update() {
        
    }
}
