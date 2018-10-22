using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Triggers tunnel behavior when both rays hit a tunnel
public class RayCastTunnel : RayCast {
    // Initialize necessary variables
    private GameObject manager;
    [HideInInspector]
    public bool active = true;
    [HideInInspector]
    public bool inTunnel = false;

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
        Vector2 direction = player.data.velocity - Vector2.Perpendicular(player.data.velocity) * 0.7f;
        RaycastHit2D hit1 = Physics2D.Raycast(player.data.position, direction, lookahead, layermask);
        Debug.DrawRay(player.data.position, direction.normalized * (lookahead), Color.black);

        // Set up left ray
        direction = player.data.velocity + Vector2.Perpendicular(player.data.velocity) * 0.7f;
        RaycastHit2D hit2 = Physics2D.Raycast(player.data.position, direction, lookahead, layermask);
        Debug.DrawRay(player.data.position, direction.normalized * (lookahead), Color.black);

        if (active && hit1.collider && hit2.collider) {
            if (manager.GetComponent<ScalableManager>()) {
                manager.GetComponent<ScalableManager>().TunnelOn();
                GetComponent<MultiBehavior>().ai[1] = GetComponent<RayCastRemove>();
            }
        }
        if (!inTunnel && hit1.collider && hit2.collider) {
            inTunnel = true;
        }
        if (inTunnel && hit1.collider == null && hit2.collider == null) {
            GetComponent<MultiBehavior>().ai[0] = GetComponent<Arrive>();
            GetComponent<MultiBehavior>().ai[1] = GetComponent<Align>();
            GetComponent<NPCController>().maxSpeedL *= 2;
        }
        return new Steering();
    }
}
