using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class CombatControllerScript : MonoBehaviour {
    private GameObject player1;
    private GameObject player2;
    private GameObject currentPlayer;
    [SerializeField] private Tilemap tilemap;
    public GameObject player1board;
    public GameObject player2board;
    public GameObject missedShotPrefab;

    void Start() {
        player1 = GameObject.Find("Player1Object");
        player2 = GameObject.Find("Player2Object");
        player1.GetComponent<PlayerScript>().setLimits(10, 1, 4.5f, -4.5f);
        player2.GetComponent<PlayerScript>().setLimits(-1, -10, 4.5f, -4.5f);

        player1.GetComponent<PlayerScript>().placeShips(new Vector3(-11, 0, 0));
        player2.GetComponent<PlayerScript>().placeShips(new Vector3(0, 0, 0));

        player1.GetComponent<PlayerScript>().canShoot = true;
        player2.GetComponent<PlayerScript>().canShoot = false;
        currentPlayer = player1;
        player2board.transform.position += new Vector3(0, 0, -1);
    }

    public void switchPlayerTurn() {
        if (currentPlayer == player1) {
            player2board.transform.position += new Vector3(0, 0, 1);
            player1board.transform.position += new Vector3(0, 0, -1);
            player2.GetComponent<PlayerScript>().canShoot = true;
            currentPlayer = player2;
        }else {
            player2board.transform.position += new Vector3(0, 0, -1);
            player1board.transform.position += new Vector3(0, 0, 1);
            player1.GetComponent<PlayerScript>().canShoot = true;
            currentPlayer = player1;
        }
        Debug.Log(currentPlayer.GetComponent<PlayerScript>().getPlayerName());
    }

    GameObject getOpponentPlayer(GameObject currentPlayer) {
        if (currentPlayer == player1) {
            return player2;
        }else {
            return player1;
        }
    }

    Vector3 getCellPos() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = tilemap.WorldToCell(mousePos);
        Vector3 highlightPos = tilemap.GetCellCenterWorld(gridPos);
        return highlightPos;
    }

    void shoot(Vector3 mouseTarget) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null) {
            GameObject target = hit.collider.gameObject;
            if (target.name == "ShipModel(Clone)" && target.GetComponent<SpriteRenderer>().color != new Color(1, 0, 0, 1)) {
                target.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                target.transform.position += new Vector3(0, 0, -2);
            }
        }else {
            Instantiate(missedShotPrefab, mouseTarget + new Vector3(0, 0, -2), Quaternion.identity);
            currentPlayer.GetComponent<PlayerScript>().canShoot = false;
            Debug.Log("No Shots Left");
        }
    }

    // Update is called once per frame
    void Update() {
        if (currentPlayer.GetComponent<PlayerScript>().canShoot) {
            if (Input.GetMouseButtonDown(0)) {
                if (currentPlayer.GetComponent<PlayerScript>().targetWithinLimits(getCellPos())) {
                    Debug.Log("Legal Target");
                    shoot(getCellPos());
                }else {
                    Debug.Log("Illegal Target");
                }
            }
        }
    }
}
