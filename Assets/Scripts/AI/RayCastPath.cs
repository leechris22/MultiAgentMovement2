using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
[RequireComponent(typeof(PathFollow), typeof(RayCast))]
public class RayCastPath : AI {
    // Initialize necessary variables
    public RayCast raycast;
    private bool avoiding = false;

    // Define Output
    override public Steering Output(Kinematic target) {
        Steering steering = raycast.Output(target);
        if (avoiding && steering.angular == 0 && steering.linear == Vector2.zero) {
            GetComponent<PathFollow>().GetNearest();
        }
        if (steering.angular == 0 && steering.linear == Vector2.zero) {
            avoiding = false;
            steering += GetComponent<PathFollow>().Output(target);
        } else {
            avoiding = true;
        }
        return steering;
    }
}
