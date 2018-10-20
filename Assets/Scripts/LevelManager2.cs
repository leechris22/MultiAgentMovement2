using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager2 : MonoBehaviour {
    // Set prefabs
    [SerializeField]
    private GameObject GreenBoidPrefab;
    [SerializeField]
    private GameObject RedBoidPrefab;
    [SerializeField]
    private GameObject GreenBoidLead;
    [SerializeField]
    private GameObject RedBoidLead;

    // Set instantiated game object references
    private List<GameObject> GreenBoids;
    private List<GameObject> RedBoids;

    // Set level loading material
    [SerializeField]
    private GameObject GreenBoidPath;
    [SerializeField]
    private GameObject RedBoidPath;

    // Set text
    [SerializeField]
    private Text ConeText;
    [SerializeField]
    private Text PredictionText;
    private bool ConeToggle = false;
    private bool PredictionToggle = false;

    // On initialization
    void Start() {
        GreenBoids = new List<GameObject>();
        RedBoids = new List<GameObject>();

        // Spawn 2 groups of Boids
        for (int i = 0; i < 6; i++) {
            SpawnBoids(GreenBoidPrefab, GreenBoidLead, GreenBoidLead.GetComponent<NPCController>(), GreenBoids);
            SpawnBoids(RedBoidPrefab, RedBoidLead, RedBoidLead.GetComponent<NPCController>(), RedBoids);
        }

        // Set leads to follow path
        GreenBoidLead.GetComponent<PathFollow>().path = GreenBoidPath.GetComponent<PathPlacer>().path;
        RedBoidLead.GetComponent<PathFollow>().path = RedBoidPath.GetComponent<PathPlacer>().path;

        // Set collision detection
        foreach (GameObject GBoid in GreenBoids) {
            foreach (GameObject RBoid in RedBoids) {
                GBoid.GetComponent<ConeCheck>().targets.Add(RBoid.GetComponent<NPCController>());
                GBoid.GetComponent<CollisionPrediction>().targets.Add(RBoid.GetComponent<NPCController>());
                RBoid.GetComponent<ConeCheck>().targets.Add(GBoid.GetComponent<NPCController>());
                RBoid.GetComponent<CollisionPrediction>().targets.Add(GBoid.GetComponent<NPCController>());
            }
        }
    }

    // Update is called once per frame
    void Update() {
        ToggleCollision();
        MovetoScene();
    }

    // Spawn a boid and set necessary targets
    private void SpawnBoids(GameObject boid, GameObject spawner, NPCController lead, List<GameObject> Boids) {
        Vector3 size = spawner.transform.localScale;
        Vector3 position = spawner.transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0);
        GameObject temp = Instantiate(boid, position, Quaternion.identity);

        temp.GetComponent<NPCController>().target = lead;
        foreach (GameObject Boid in Boids) {
            Boid.GetComponent<Flock>().targets.Add(temp.GetComponent<NPCController>());
            temp.GetComponent<Flock>().targets.Add(Boid.GetComponent<NPCController>());
        }
        Boids.Add(temp);
    }

    // Spawn a boid and set necessary targets
    private void ToggleCollision() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            ConeToggle = !ConeToggle;
            foreach (GameObject GBoid in GreenBoids) {
                GBoid.GetComponent<Flock>().addCone = ConeToggle;
            }
            foreach (GameObject RBoid in RedBoids) {
                RBoid.GetComponent<Flock>().addCone = ConeToggle;
            }
            ConeText.text = "Cone Check: " + ConeToggle;
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            PredictionToggle = !PredictionToggle;
            foreach (GameObject GBoid in GreenBoids) {
                GBoid.GetComponent<Flock>().addCone = PredictionToggle;
            }
            foreach (GameObject RBoid in RedBoids) {
                RBoid.GetComponent<Flock>().addCone = PredictionToggle;
            }
            PredictionText.text = "Collision Prediction: " + PredictionToggle;
        }
    }

    // Move to the next scene
    private void MovetoScene() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SceneManager.LoadScene(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SceneManager.LoadScene(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SceneManager.LoadScene(2);
        }
    }

    // Show the spawn rectangle
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(GreenBoidLead.transform.position, GreenBoidLead.transform.localScale);
        Gizmos.DrawCube(RedBoidLead.transform.position, RedBoidLead.transform.localScale);
    }
}
