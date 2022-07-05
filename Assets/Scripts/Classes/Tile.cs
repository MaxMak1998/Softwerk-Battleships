using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    public Vector3 coordinates;
    public bool isHit;
    public Tile(Vector3 coordinates) {
        this.coordinates = coordinates;
        isHit = false;
    }
}
