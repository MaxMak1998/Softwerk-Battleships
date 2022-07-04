using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship {
    public string name;
    public List<Tile> positions;
    
    public Ship(string name, List<Tile> positions) {
        this.name = name;
        this.positions = positions;
    }
}
