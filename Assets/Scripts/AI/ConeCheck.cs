using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a cone to detect collisions
public class ConeCheck : Separate {
    // Initialize necessary variables
    [HideInInspector]
    public List<NPCController> targets;
    [SerializeField]
    private float threshold;

    // Define Output
    override public Steering Output(Kinematic lead) {
        // Define variables
        Vector2 orientation = new Vector2(Mathf.Sin(player.data.rotation), Mathf.Cos(player.data.rotation));
        NPCController nearest = null;

        // Obtain the data for nearest boid and calculate separation velocity
        foreach (NPCController target in targets) {
            if (Vector2.Dot(orientation, (target.data.position - player.data.position)) > threshold) {
                if (nearest == null || Vector2.Distance(player.data.position, target.data.position) < Vector2.Distance(player.data.position, nearest.data.position)) {
                    nearest = target;
                }
            }
        }

        // Return no change if no nearby boid found
        if (nearest == null) {
            return new Steering();
        }

        // Otherwise, calculate separate
        return base.Output(nearest.data);
    }
}
