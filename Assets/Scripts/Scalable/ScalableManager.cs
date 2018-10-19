using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles all Scalable Formation actions
public class ScalableManager : LevelManager {
    // Set prefabs
    [SerializeField]
    private GameObject PointPrefab;

    // Formation creation
    [SerializeField]
    private ScalableFormation formationFunction;
    [SerializeField]
    private int size;
    [SerializeField]
    private float spacing;
    private GameObject[] formation;

    // Leader
    private GameObject leader;
    private Vector2 leaderPos;

    // On initialization
    private void Awake() {
        // Create the formation
        formationFunction = GetComponent<ScalableFormation>();
        formation = formationFunction.CreateFormation(size, spacing);

        // Initialize Boids
        Boids = new List<GameObject>();
        for (int i = 0; i < size; i++) {
            SpawnBoid();
        }

        // Set leader
        leaderPos = BoidSpawner.transform.position;
        GetLeader();
    }

    // Update is called once per frame
    private void Update () {
		
	}

    protected void SpawnBoid() {
        Vector3 size = BoidSpawner.transform.localScale;
        Vector3 position = BoidSpawner.transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0);
        GameObject temp = Instantiate(BoidPrefab, position, BoidSpawner.transform.rotation);
        foreach (GameObject Boid in Boids) {
            Boid.GetComponent<Separate>().targets.Add(temp.GetComponent<NPCController>());
            temp.GetComponent<Separate>().targets.Add(Boid.GetComponent<NPCController>());
        }
        Boids.Add(temp);
    }

    private void GetLeader() {
        // Set the positions for each Boid on the formation
        List<GameObject> tempBoids = new List<GameObject>(Boids);
        foreach (GameObject Point in formation) {
            GameObject closeBoid = null;
            float distance = Mathf.Infinity;
            foreach (GameObject Boid in tempBoids) {
                float newdistance = Vector2.Distance(Boid.GetComponent<NPCController>().data.position,
                                           leaderPos+Point.GetComponent<NPCController>().data.position);
                if (newdistance < distance) {
                    distance = newdistance;
                    closeBoid = Boid;
                }
            }
            if (Point == formation[0]) {
                leader = closeBoid;
            } else {
                closeBoid.GetComponent<NPCController>().target = Point.GetComponent<NPCController>();
            }
            Point.transform.SetParent(leader.transform, false);
            tempBoids.Remove(closeBoid);
        }

        // Change the leader behavior
        leader.GetComponent<NPCController>().ai = leader.GetComponent<RayCastPath>();
        leader.GetComponent<PathFollow>().path = Path.GetComponent<PathPlacer>().path;
        leader.GetComponent<NPCController>().maxSpeedL /= 2;
    }

    //
    private void delete() {

    }
}
