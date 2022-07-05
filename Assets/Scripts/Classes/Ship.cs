using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship {
    public string name;
    public List<Tile> positions;
    public Vector3 pivotPoint;
    public float rotation;

    public Ship(string name, List<Tile> positions, Vector3 pivotPoint, float rotation) {
        this.name = name;
        this.positions = positions;
        this.pivotPoint = pivotPoint;
        this.rotation = rotation;
    }
}
