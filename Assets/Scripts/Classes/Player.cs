using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    public string name;
    public List<Ship> listOfShips;

    public Player(string name) {
        this.name = name;
        this.listOfShips = new List<Ship>();
    }
}
