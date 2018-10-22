using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles all Two Level Formation actions
public class TwoLevelManager : LevelManager {
    // Formation creation
    [SerializeField]
    private Formation formationFunction;
    [SerializeField]
    private int size;
    public float spacing;
    private GameObject[] formation;

    // Leader
    [SerializeField]
    private GameObject leader;

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
        SetLeader();
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

    // Sets the properties of the leader
    private void SetLeader() {
        leader.GetComponent<PathFollow>().path = Path.GetComponent<PathPlacer>().path;
        leader.transform.position = BoidSpawner.transform.position;
    }

    // Sets the position of all Boids
    public void SetFollowers() {
        // Delete the old formation and create a new one
        if (formation != null) {
            foreach (GameObject Point in formation) {
                Destroy(Point);
            }
        }
        formation = formationFunction.CreateFormation(size, spacing, true);

        // Set the positions for each Boid on the formation
        List<GameObject> tempBoids = new List<GameObject>(Boids);
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
            SetFollowers();
        }
        Destroy(DeadBoid);
    }

    // Change the Boid behavior to move into the tunnel
    public void TunnelOn() {
        foreach (GameObject Boid in Boids) {
            Boid.GetComponent<PathFollow>().current = leader.GetComponent<PathFollow>().current - 12;
            Boid.GetComponent<PathFollow>().GetNearest();
            Boid.GetComponent<PathFollow>().current -= 5;
            Boid.GetComponent<MultiBehavior>().ai[0] = Boid.GetComponent<PathFollow>();
            Boid.GetComponent<MultiBehavior>().ai[1] = Boid.GetComponent<RayCastTunnel>();
            Boid.GetComponent<MultiBehavior>().weights[3] = 0;
            Boid.GetComponent<RayCastTunnel>().active = false;
            Boid.GetComponent<NPCController>().maxSpeedL /= 2;
        }
    }
}
