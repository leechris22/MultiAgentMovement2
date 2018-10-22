using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Raycast configuration that takes into account the group
public class RayCastGroup : RayCast {
    // Initialize necessary variables
    [HideInInspector]
    public float radius = 0;
    [HideInInspector]
    public float offset = 0;

    // Define Output
    override public Steering Output(Kinematic target) {
        // Ignore Boid layer
        int layermask = ~(1 << 9);
        
        // Set up ray 1
        Vector2 position = player.data.position - player.data.velocity.normalized * offset - Vector2.Perpendicular(player.data.velocity).normalized * radius;
        RaycastHit2D hit1 = Physics2D.Raycast(position, player.data.velocity, lookahead + radius, layermask);
        Debug.DrawRay(position, player.data.velocity.normalized * (lookahead + radius), Color.black);

        // Set up ray 2
        position = player.data.position - player.data.velocity.normalized * offset + Vector2.Perpendicular(player.data.velocity).normalized * radius;
        RaycastHit2D hit2 = Physics2D.Raycast(position, player.data.velocity, lookahead + radius, layermask);
        Debug.DrawRay(position, player.data.velocity.normalized * (lookahead + radius), Color.black);

        // Set up ray 3
        position = player.data.position - player.data.velocity.normalized * offset;
        Vector2 direction = player.data.velocity - Vector2.Perpendicular(player.data.velocity);
        RaycastHit2D hit3 = Physics2D.Raycast(position, direction, lookahead + radius, layermask);
        Debug.DrawRay(position, direction.normalized * (lookahead + radius), Color.black);

        // Set up ray 4
        direction = player.data.velocity + Vector2.Perpendicular(player.data.velocity);
        RaycastHit2D hit4 = Physics2D.Raycast(position, direction, lookahead + radius, layermask);
        Debug.DrawRay(position, direction.normalized * (lookahead + radius), Color.black);

        // Set up hit collision
        if (hit1.collider || hit2.collider || hit3.collider || hit4.collider) {
            target.position = player.data.position + player.data.velocity.normalized * avoidDistance;
            if (hit1.collider) {
                target.position += Vector2.Perpendicular(player.data.velocity).normalized * avoidDistance;
            }
            if (hit2.collider) {
                target.position -= Vector2.Perpendicular(player.data.velocity).normalized * avoidDistance;
            }
        } else {
            return new Steering();
        }

        return GetComponent<Seek>().Output(target) + GetComponent<Face>().Output(target);
    }
}

