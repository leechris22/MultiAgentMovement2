using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles all Scalable Formation actions
public class ScalableManager : LevelManager {
    // Formation creation
    [SerializeField]
    private Formation formationFunction;
    [SerializeField]
    private int size;
    public float spacing;
    private GameObject[] formation;

    // Leader
    private GameObject leader;
    private Vector2 leaderPos;
    private int leaderIndex = 0;

    // On initialization
    private void Start() {
        // Get the formation creator
        formationFunction = GetComponent<Formation>();

        // Initialize Boids
        Boids = new List<GameObject>();
        for (int i = 0; i < size; i++) {
            SpawnBoid();
        }

        // Set the formation
        leaderPos = BoidSpawner.transform.position;
        SetLeader();
        SetFollowers();
    }

    // Update is called once per frame
    /*private void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            foreach (GameObject Boid in Boids) {
                Boid.GetComponent<PathFollow>().current = leader.GetComponent<PathFollow>().current;
                Boid.GetComponent<MultiBehavior>().ai[0] = Boid.GetComponent<PathFollow>();
                Boid.GetComponent<MultiBehavior>().ai[1] = Boid.GetComponent<FaceForward>();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            foreach (GameObject Boid in Boids) {
                Boid.GetComponent<MultiBehavior>().ai[0] = Boid.GetComponent<Arrive>();
                Boid.GetComponent<MultiBehavior>().ai[1] = Boid.GetComponent<Align>();
            }
        }
    }*/

    // Spawns Scalable Boids
    protected void SpawnBoid() {
        Vector3 size = BoidSpawner.transform.localScale;
        Vector3 position = BoidSpawner.transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0);
        GameObject temp = Instantiate(BoidPrefab, position, BoidSpawner.transform.rotation);
        temp.GetComponent<PathFollow>().path = Path.GetComponent<PathPlacer>().path;
        Boids.Add(temp);
    }

    // Sets the leader
    private void SetLeader() {
        // Finds the closest Boid from the last leader's position
        GameObject closeBoid = null;
        float distance = Mathf.Infinity;
        foreach (GameObject Boid in Boids) {
            if (Vector2.Distance(Boid.transform.position, leaderPos) < distance) {
                distance = Vector2.Distance(Boid.transform.position, leaderPos);
                closeBoid = Boid;
            }
        }

        // Set the leader and its behavior
        leader = closeBoid;
        leader.GetComponent<MultiBehavior>().ai[0] = leader.GetComponent<RayCastPath>();
        leader.GetComponent<MultiBehavior>().ai[1] = leader.GetComponent<RayCastTunnel>();
        leader.GetComponent<NPCController>().maxSpeedL /= 2;
        leader.GetComponent<PathFollow>().current = leaderIndex;
    }

    // Sets the position of all Boids except the leader
    public void SetFollowers() {
        // Delete the old formation and create a new one
        if (formation != null) {
            foreach (GameObject Point in formation) {
                Destroy(Point);
            }
        }
        formation = formationFunction.CreateFormation(size, spacing, false);
        leader.GetComponent<RayCastGroup>().radius = formationFunction.radius;
        leader.GetComponent<RayCastGroup>().offset = formationFunction.radius;

        // Set the positions for each Boid on the formation except the leader
        List<GameObject> tempBoids = new List<GameObject>(Boids);
        tempBoids.Remove(leader);
        foreach (GameObject Point in formation) {
            Point.transform.SetParent(leader.transform, false);

            // Find the closest Boid
            GameObject closeBoid = null;
            float distance = Mathf.Infinity;
            foreach (GameObject Boid in tempBoids) {
                if (Vector2.Distance(Boid.transform.position, Point.transform.position) < distance) {
                    distance = Vector2.Distance(Boid.transform.position, Point.transform.position);
                    closeBoid = Boid;
                }
            }
            closeBoid.GetComponent<NPCController>().target = Point.GetComponent<NPCController>();
            tempBoids.Remove(closeBoid);
        }
    }

    // Destroys Boid and changes formation
    public void Reorganize(GameObject DeadBoid) {
        size--;
        Boids.Remove(DeadBoid);
        if (size != 0) {
            if (DeadBoid == leader) {
                leaderPos = leader.transform.position;
                leaderIndex = leader.GetComponent<PathFollow>().current;
                SetLeader();
            }
            SetFollowers();
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
                Boid.GetComponent<RayCastTunnel>().active = false;
                Boid.GetComponent<NPCController>().maxSpeedL /= 2;
            }
        }
    }
}
