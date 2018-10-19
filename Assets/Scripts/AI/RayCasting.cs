using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Projects rays to detect wall collision
public class RayCasting : Seek {
    // Initialize necessary variables
    [SerializeField]
	private float avoidDistance;
    [SerializeField]
    private float lookahead;

    // Define Output
    override public Steering Output(Kinematic target) {
        // Ignore Boid layer
        int layermask = ~(1 << 9);

        // Set up main ray
        RaycastHit2D hit1 = Physics2D.Raycast(player.data.position, player.data.velocity, lookahead, layermask);

        // Set up whisker ray 1
        Vector2 whiskerposition = ((Vector3)player.data.velocity + transform.up + transform.right).normalized;
        RaycastHit2D hit2 = Physics2D.Raycast(player.data.position, whiskerposition, lookahead/2, layermask);

        // Set up whisker ray 2
        whiskerposition = ((Vector3)player.data.velocity + transform.up - transform.right).normalized;
        RaycastHit2D hit3 = Physics2D.Raycast(player.data.position, whiskerposition, lookahead/2, layermask);

        // Set up hit collision
        if (hit1.collider) {
            target.position += hit1.point + hit1.normal * avoidDistance;
        } else if (hit2.collider) {
            target.position += hit2.point + hit2.normal * avoidDistance;
        } else if (hit3.collider) {
            target.position += hit3.point + hit3.normal * avoidDistance;
        } else {
            return new Steering();
        }
        return base.Output(target);
    }
}
