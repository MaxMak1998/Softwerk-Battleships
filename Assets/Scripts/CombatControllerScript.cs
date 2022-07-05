using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine;

public class CombatControllerScript : MonoBehaviour {
    private GameObject player1;
    private GameObject player2;
    private GameObject currentPlayer;
    [SerializeField] private Tilemap tilemap;
    public GameObject player1board;
    public GameObject player2board;
    public GameObject missedShotPrefab;
    public GameObject explosion;
    public Button switchPlayerButton;
    public Text winnerText;
    public Text player1text;
    public Text player2text;
    public Text eventsText;

    void Start() {
        // Initialize Players
        player1 = GameObject.Find("Player1Object");
        player2 = GameObject.Find("Player2Object");

        // Set Player's Names
        player1text.text = player1.GetComponent<PlayerScript>().getPlayerName();
        player2text.text = player2.GetComponent<PlayerScript>().getPlayerName();

        // Allows to "shoot" only at opponent's board
        player1.GetComponent<PlayerScript>().setLimits(10, 1, 4.5f, -4.5f);
        player2.GetComponent<PlayerScript>().setLimits(-1, -10, 4.5f, -4.5f);

        // Place ships
        player1.GetComponent<PlayerScript>().placeShips(new Vector3(-11, 0, 0));
        player2.GetComponent<PlayerScript>().placeShips(new Vector3(0, 0, 0));

        player1.GetComponent<PlayerScript>().canShoot = true;
        player2.GetComponent<PlayerScript>().canShoot = false;
        currentPlayer = player1;

        // Puts the board over the ships, to hind from opponent
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
    }

    GameObject getOpponentPlayer(GameObject currentPlayer) {
        if (currentPlayer == player1) {
            return player2;
        }else {
            return player1;
        }
    }

    // Returns center of the cell which mouse clicked on
    Vector3 getCellPos() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = tilemap.WorldToCell(mousePos);
        Vector3 highlightPos = tilemap.GetCellCenterWorld(gridPos);
        return highlightPos;
    }

    void shoot(Vector3 mouseTarget) {
        // Detects if the mouse clicked on a ship tile (grey)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null) {
            GameObject target = hit.collider.gameObject;

            // if ship tile was not previously hit
            if (target.name == "ShipModel(Clone)" && target.GetComponent<SpriteRenderer>().color != new Color(1, 0, 0, 1)) {

                // simple explosion animation
                Instantiate(explosion, mouseTarget + new Vector3(0, 0, -2), Quaternion.identity);

                // changes color to red (is hit)
                target.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                target.transform.position += new Vector3(0, 0, -2);
                eventsText.text = "Hit!";
                checkWin();
            }
        }else {
            // creates a "missed" tile
            Instantiate(missedShotPrefab, mouseTarget + new Vector3(0, 0, -2), Quaternion.identity);
            currentPlayer.GetComponent<PlayerScript>().canShoot = false;
            eventsText.text = "Miss! Switch Player!";
            Debug.Log("No Shots Left");
        }
    }

    // Checks the color of all opponent's ships. If all of them are red, player wins
    void checkWin() {
        GameObject opponent = getOpponentPlayer(currentPlayer);
        bool allHit = true;
        foreach (Transform child in opponent.transform) {
            if (child.GetComponent<SpriteRenderer>().color != new Color(1, 0, 0, 1)) {
                allHit = false;
            }
        }
        if (allHit) {
            Debug.Log("Winner Test");
            winnerText.gameObject.SetActive(true);
            winnerText.text = "Game Over! Winner: " + currentPlayer.GetComponent<PlayerScript>().getPlayerName() + "!";
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
            switchPlayerButton.interactable = false;
        }else {
            switchPlayerButton.interactable = true;
        }

    }
}
