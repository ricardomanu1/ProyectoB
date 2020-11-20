using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Range(1, 50)]
    public float maxForce = 10;
    [Range(1, 50)]
    public float minForce = 10;

    [Range(1, 3)]
    public float maxSize = 1;
    private float minSize = 1;

    public GameObject basicObjective = null;
    public GameObject spawner = null;
    //public GameObject anglePointer1 = null;
    //public GameObject anglePointer2 = null;

    private AudioSource aud;

    int difficulty;
    private void Start()
    {
        aud = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            spawnObject();
        }

        


    }

    public void StartSpawning(int difficulty)
    {
        this.difficulty = difficulty;
        InvokeRepeating("spawnObject", 3, 0.2f);
    }

    public void StopSpawning()
    {
        CancelInvoke("spawnObject");
    }

    private void spawnObject()
    {
        Vector3 spawnpoint = new Vector3(Random.Range(-13f, 13f), 1, Random.Range(-13f, 13f));

        while ((spawnpoint.x < 3 && spawnpoint.x > -3 && spawnpoint.z < 7 && spawnpoint.z > -3))
        {
            spawnpoint = new Vector3(Random.Range(-15f,15f), 1, Random.Range(-15f, 15f));
        }

        transform.position = spawnpoint;

        aud.Play();


        Objective objective = Instantiate(basicObjective, spawnpoint, Quaternion.identity).GetComponent<Objective>();

        Vector3 probs = new Vector3(Random.Range(-1f,1f), Random.value + 0.3f, Random.Range(-1f, 1f));

        //objective.SetParameters(Random.Range(minForce,maxForce) * (difficulty + 1),Random.Range(minSize,maxSize),probs,(anglePointer1.transform.position - spawner.transform.position).normalized,(anglePointer2.transform.position - spawner.transform.position).normalized);

        objective.SetParameters(Random.Range(minForce, maxForce) * (difficulty + 1), Random.Range(minSize, maxSize), probs);

        objective.StartLaunch();
    }
}
