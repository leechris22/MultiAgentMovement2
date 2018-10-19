using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all formations
// In this case, a circle
public class ScalableFormation : MonoBehaviour {
    // Define base formation to create
    public Kinematic[] CreateFormation(int size, float spacing) {
        // Initialize array
        Kinematic[] formation = new Kinematic[size];

        // Create a regular polygon with 'size' vertices and 'spacing' side length
        float radius = spacing / (2 * Mathf.Sin(Mathf.PI / size));
        float orientation = -180;
        for (int i = 0; i < size; i++) {
            float x = Mathf.Sin(Mathf.Deg2Rad * orientation) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * orientation) * radius + radius;
            formation[i] = new Kinematic(new Vector2(x, y), -orientation+360, Vector2.zero, 0);
            orientation += 360 / size;
        }
        return formation;
    }
}
