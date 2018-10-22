using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves the player along a path
[RequireComponent(typeof(Arrive), typeof(Face))]
public class PathFollow : AI {
    // Initialize necessary variables
    [HideInInspector]
    public Kinematic[] path;
    [SerializeField]
    private float pathRadius;
    [HideInInspector]
    public int current = 0;

    // Define Output
    override public Steering Output(Kinematic target) {
        Steering steering = new Steering();
 
        // If no path to follow, do nothing
        if (current >= path.Length) {
            return steering;
        }
    
        // Move to point until player reaches point, then target next point
        if (Vector2.Distance(path[current].position, player.data.position) > pathRadius) {
            steering += GetComponent<Arrive>().Output(path[current]);
            steering += GetComponent<Face>().Output(path[current]);
        } else {
            current++;
        }

        // Return output
        return steering;
    }

    // Obtain the nearest point in the path
    public void GetNearest() {
        float distance = Mathf.Infinity;
        for (int i = current; i < current+30; i++) {
            if (i < path.Length && Vector2.Distance(path[i].position, player.data.position) < distance) {
                distance = Vector2.Distance(path[i].position, player.data.position);
                current = i;

            }
        }
    }

}
