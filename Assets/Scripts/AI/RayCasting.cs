using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Projects rays to detect wall collision
public class RayCasting : Seek {
    // Initialize necessary variables
    [SerializeField]
    private float radius;
    [SerializeField]
    private float offset;
    [SerializeField]
    private float avoidDistance;
    [SerializeField]
    private float lookahead;

    // Define Output
    override public Steering Output(Kinematic target) {
        // Ignore Boid layer
        int layermask = ~(1 << 9);

        // Set up main ray
        Vector2 position = player.data.position - player.data.velocity.normalized * offset;
        RaycastHit2D hit1 = Physics2D.Raycast(position, player.data.velocity, lookahead + radius, layermask);
        Debug.DrawRay(position, player.data.velocity.normalized * (lookahead + radius), Color.black);

        // Set up whisker ray 1
        Vector2 direction = (player.data.velocity - Vector2.Perpendicular(player.data.velocity) * 0.7f).normalized;
        RaycastHit2D hit2 = Physics2D.Raycast(position, direction, lookahead / 3 + radius, layermask);
        Debug.DrawRay(position, direction.normalized * (lookahead/3 + radius), Color.black);

        // Set up whisker ray 2
        direction = (player.data.velocity + Vector2.Perpendicular(player.data.velocity) * 0.7f).normalized;
        RaycastHit2D hit3 = Physics2D.Raycast(position, direction, lookahead / 3 + radius, layermask);
        Debug.DrawRay(position, direction.normalized * (lookahead/3 + radius), Color.black);

        // Set up hit collision
        if (hit1.collider || hit2.collider || hit3.collider) {
            target.position += hit1.point + (hit1.normal + hit2.normal + hit3.normal) * avoidDistance;
        } else {
            return new Steering();
        }
        return base.Output(target);
    }
}
