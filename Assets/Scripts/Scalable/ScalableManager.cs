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
    private Kinematic[] formation;

    // Leader
    private GameObject leader;
    private Vector2 leaderPos;

    // Points
    private List<GameObject> Points;

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

        // Initialize Points
        Points = new List<GameObject>();
        for (int i = 0; i < size; i++) {
            Points.Add(Instantiate(PointPrefab, formation[i].position, Quaternion.Euler(0, 0, formation[i].orientation)));
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
        GameObject temp = Instantiate(BoidPrefab, position, Quaternion.identity);

        foreach (GameObject Boid in Boids) {
            //Boid.GetComponent<Flock>().targets.Add(temp.GetComponent<NPCController>());
            //temp.GetComponent<Flock>().targets.Add(Boid.GetComponent<NPCController>());
        }
        Boids.Add(temp);
    }

    private void GetLeader() {
        List<GameObject> tempBoids = new List<GameObject>(Boids);
        foreach (GameObject Point in Points) {
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
            if (Point == Points[0]) {
                leader = closeBoid;
            } else {
                closeBoid.GetComponent<NPCController>().target = Point.GetComponent<NPCController>();
            }
            Point.transform.parent = leader.transform;
            tempBoids.Remove(closeBoid);
        }
/*        for (int i = 0; i < size; i++) {
            Points[i].transform.position = formation[i].position;
            Points[i].transform.rotation = Quaternion.Euler(0, 0, formation[i].orientation);
        }*/
    }
}
