using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    Vector3 coordinates;
    bool isHit;
    public Tile(Vector3 coordinates) {
        this.coordinates = coordinates;
        isHit = false;
    }
}
