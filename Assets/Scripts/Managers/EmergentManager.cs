using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles all Emergent Formation actions
public class EmergentManager : LevelManager {
    // Set the number of Boids
    [SerializeField]
    private int size;

    // Leader
    private GameObject leader;
    private int leaderIndex = 0;

    // On initialization
    private void Start() {
        // Initialize Boids
        Boids = new List<GameObject>();
        for (int i = 0; i < size; i++) {
            SpawnBoid();
        }

        // Set the formation
        SetLeader(null);
        SetFollowers();
    }

    // Spawns Scalable Boids
    protected void SpawnBoid() {
        Vector3 size = BoidSpawner.transform.localScale;
        Vector3 position = BoidSpawner.transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0);
        GameObject temp = Instantiate(BoidPrefab, position, BoidSpawner.transform.rotation);
        temp.GetComponent<PathFollow>().path = Path.GetComponent<PathPlacer>().path;
        foreach (GameObject Boid in Boids) {
            Boid.GetComponent<Separate>().targets.Add(temp.GetComponent<NPCController>());
            temp.GetComponent<Separate>().targets.Add(Boid.GetComponent<NPCController>());
        }
        Boids.Add(temp);
    }

    // Sets the leader
    private void SetLeader(NPCController oldLeader) {
        // Sets the first Boid with the oldLeader as its target as the leader
        foreach (GameObject Boid in Boids) {
            if (Boid.GetComponent<NPCController>().target == oldLeader) {
                leader = Boid;
                break;
            }
        }

        // Set the leader behavior
        leader.GetComponent<MultiBehavior>().ai[0] = leader.GetComponent<RayCastPath>();
        leader.GetComponent<MultiBehavior>().ai[1] = leader.GetComponent<RayCastTunnel>();
        leader.GetComponent<MultiBehavior>().weights[2] = 0;
        leader.GetComponent<MultiBehavior>().weights[3] = 0;
        leader.GetComponent<NPCController>().target = leader.GetComponent<NPCController>();
        leader.GetComponent<PathFollow>().current = leaderIndex;
    }

    // Sets the targets of all Boids except the leader
    private void SetFollowers() {
        // Set a list of potential targets
        List<GameObject> targets = new List<GameObject>();
        targets.Add(leader);

        // Sets the target for each Boid as the closest Boid with a target
        foreach (GameObject Boid in Boids) {
            if (Boid != leader) {
                GameObject closeBoid = null;
                float distance = Mathf.Infinity;
                foreach (GameObject target in targets) {
                    if (Vector2.Distance(Boid.transform.position, target.transform.position) < distance) {
                        distance = Vector2.Distance(Boid.transform.position, target.transform.position);
                        closeBoid = target;
                    }
                }
                Boid.GetComponent<NPCController>().target = closeBoid.GetComponent<NPCController>();
                targets.Add(Boid);
            }
        }
    }

    // Changes targets for the Boids with DeadBoid as the target
    private void SetNoTargets(NPCController DeadBoid) {
        // Set a list of potential targets
        List<GameObject> targets = new List<GameObject>();
        targets.Add(leader);

        // Sets the target for Boids targeting DeadBoid as the closest Boid that target chains to the leader
        foreach (GameObject Boid in Boids) {
            if (Boid.GetComponent<NPCController>().target == DeadBoid) {
                GameObject closeBoid = null;
                float distance = Mathf.Infinity;
                foreach (GameObject target in targets) {
                    if (target != Boid && Vector2.Distance(Boid.transform.position, target.transform.position) < distance) {
                        distance = Vector2.Distance(Boid.transform.position, target.transform.position);
                        closeBoid = target;
                    }
                }
                targets.Add(Boid);
                Boid.GetComponent<NPCController>().target = closeBoid.GetComponent<NPCController>();
            } else {
                foreach (GameObject target in targets) {
                    if (Boid.GetComponent<NPCController>().target == target.GetComponent<NPCController>()) {
                        targets.Add(Boid);
                        break;
                    }
                }
            }
        }
    }

    // Destroys Boid and change targets
    public void Reorganize(GameObject DeadBoid) {
        size--;
        Boids.Remove(DeadBoid);
        if (size != 0) {
            if (DeadBoid == leader) {
                leaderIndex = leader.GetComponent<PathFollow>().current;
                SetLeader(DeadBoid.GetComponent<NPCController>());
            }
            SetNoTargets(DeadBoid.GetComponent<NPCController>());
        }
        Destroy(DeadBoid);
    }

    // Change the Boid behavior to move into the tunnel
    public void TunnelOn() {
        foreach (GameObject Boid in Boids) {
            if (Boid != leader) {
                Boid.GetComponent<PathFollow>().current = leader.GetComponent<PathFollow>().current - 12;
                Boid.GetComponent<PathFollow>().GetNearest();
                Boid.GetComponent<MultiBehavior>().ai[0] = Boid.GetComponent<PathFollow>();
                Boid.GetComponent<MultiBehavior>().ai[1] = Boid.GetComponent<RayCastTunnel>();
                Boid.GetComponent<MultiBehavior>().weights[3] = 0;
                Boid.GetComponent<RayCastTunnel>().active = false;
                Boid.GetComponent<NPCController>().maxSpeedL /= 2;
            }
        }
    }
}
