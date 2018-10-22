using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Raycast configuration that
public class RayCastRemove : RayCast {
    // Initialize necessary variables
    private GameObject manager;
    [HideInInspector]
    public bool active = true;

    // On initialization
    override protected void Awake() {
        player = GetComponent<NPCController>();
        manager = GameObject.FindWithTag("Manager");
    }

    // Define Output
    override public Steering Output(Kinematic target) {
        // Ignore Boid layer
        int layermask = ~(1 << 9);

        // Set up right ray
        Vector2 position = player.data.position - player.data.velocity.normalized * 0.5f - Vector2.Perpendicular(player.data.velocity).normalized * 0.5f;
        RaycastHit2D hit1 = Physics2D.Raycast(position, player.data.velocity, lookahead, layermask);
        Debug.DrawRay(position, player.data.velocity.normalized * lookahead, Color.black);

        // Set up right ray
        RaycastHit2D hit2 = Physics2D.Raycast(position, -Vector2.Perpendicular(player.data.velocity), lookahead, layermask);
        Debug.DrawRay(position, -Vector2.Perpendicular(player.data.velocity).normalized * lookahead, Color.black);

        // Set up left ray
        position = player.data.position - player.data.velocity.normalized * 0.5f + Vector2.Perpendicular(player.data.velocity).normalized * 0.5f;
        RaycastHit2D hit3 = Physics2D.Raycast(position, player.data.velocity, lookahead, layermask);
        Debug.DrawRay(position, player.data.velocity.normalized * lookahead, Color.black);

        // Set up left ray
        RaycastHit2D hit4 = Physics2D.Raycast(position, Vector2.Perpendicular(player.data.velocity), lookahead, layermask);
        Debug.DrawRay(position, Vector2.Perpendicular(player.data.velocity).normalized * lookahead, Color.black);

        // Set up hit collision
        if (hit1.collider || hit3.collider) {
            target.position = player.data.position + player.data.velocity.normalized * avoidDistance;
            if (hit1.collider) {
                target.position += Vector2.Perpendicular(player.data.velocity).normalized * avoidDistance;
            }
            if (hit3.collider) {
                target.position -= Vector2.Perpendicular(player.data.velocity).normalized * avoidDistance;
            }
            if (active) {
                GetComponent<NPCController>().ai = GetComponent<RayCastPath>();
                GetComponent<RayCastPath>().raycast = this;
                active = false;
            }
        } else if (!active && hit2.collider && hit4.collider) {
            if (manager.GetComponent<ScalableManager>() && manager.GetComponent<ScalableManager>().spacing != 1) {
                manager.GetComponent<ScalableManager>().spacing = 1;
                manager.GetComponent<ScalableManager>().SetFollowers();
            }
            return new Steering();
        } else {
            return new Steering();
        }

        return GetComponent<Seek>().Output(target) + GetComponent<Face>().Output(target);
    }
}
