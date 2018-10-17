using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds the character data
[System.Serializable]
public struct Kinematic {
    // Static data
    public Vector2 position;
    public float orientation;

    public Vector2 velocity;
    public float rotation;

    // Constructor
    public Kinematic(Vector2 pos, float ori, Vector2 vel, float rot) {
        position = pos;
        orientation = ori;
        velocity = vel;
        rotation = rot;
    }
}
