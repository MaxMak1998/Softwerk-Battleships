using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class DragnDrop : MonoBehaviour {
    GameObject target;
    bool isMouseDrag;
    Vector3 screenPosition;
    Vector3 offset;
    [SerializeField] private Tilemap tilemap;

    void Start() {
        target  = null;
        isMouseDrag =  false;
    }

    GameObject ReturnClickedObject() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null) {
            target = hit.collider.gameObject;
        }
        return target;
    }

    Vector3 onGridPosition(Vector3 mousePos) {
        Vector3Int gridPos = tilemap.WorldToCell(mousePos);
        Vector3 snapPos = tilemap.GetCellCenterWorld(gridPos);
        return snapPos;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            target = ReturnClickedObject();
            if (target != null) {
                isMouseDrag = true;
                screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
            }
        }
 

 
        if (isMouseDrag) {
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;
            if (target.GetComponent<shipBehavior>().shipLength % 2 == 0) {
                target.transform.position = onGridPosition(currentPosition) + new Vector3(0, -0.5f, 0);
                if (target.GetComponent<shipBehavior>().isVertical == false) {
                    target.transform.position = target.transform.position + new Vector3(0.5f, 0.5f, 0);
                }
            }else {
                target.transform.position = onGridPosition(currentPosition);
            }

            // Rotation
            if (Input.GetMouseButtonDown(1)) {
                target.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                if (target.GetComponent<shipBehavior>().isVertical == true) {
                    target.GetComponent<shipBehavior>().isVertical = false;
                }else {
                    target.GetComponent<shipBehavior>().isVertical = true;
                }
            }
            if (Input.GetMouseButtonUp(0)) {
                target.GetComponent<shipBehavior>().saveShipPos();
                isMouseDrag = false;
                target = null;

                // Chek if all ships are on the map and unlock the "Save Position" Button
                GetComponent<GameControllerScript>().allShipsPlaced();
            }
        }
    }
}
