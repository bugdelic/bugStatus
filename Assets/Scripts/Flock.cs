﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {
    public LightController light;
    public int activeFotn=0;
    public bool isMaybe;
    public Transform parent;
    public bool debugOn;
    //toggles and weightings to determine which forces are more prevalent. TODO: make weighting dynamic based on different rules
    public bool useCohesion;
    [Range(0,1)]
    public float cohesionWeight = 1;
    public bool useAlignment;
    [Range(0,1)]
    public float alignmentWeight = 1;
    public bool useSeparation;
    [Range(0,1)]
    public float separationWeight = 1;
    public bool useRandom;
    [Range(0,1)]
    public float randomWeight = 1;
    public bool useBoundary;
    [Range(0,1)]
    public float boundaryWeight;

    public float flockSpawnRadius;
    public int flockSize;
    public float neighbourSearchRadius;
    public float separationRadius;
    public float maximumBoundRadius;

    public float maxSpeed, minSpeed;
    public float maxSteeringForce;

    public GameObject boidPrefab;
   
    [Range(0,2)]
    public float slowTimePercent;

    [SerializeField]
    public List<Boid> boids;

    public FlockController flockController;

    public Flock(int flockSize) {
        this.flockSize = flockSize;
    }

    void Start() {
        boids.Clear();
        
    }

    void Update() {
        isMaybe=light.isFever;
        Vector3 flockCentroid = Vector3.zero;
        foreach (Boid b in boids) {
            flockCentroid += b.transform.position;
            b.UpdateBehaviour();
			b.ExecuteMovement();
		}
        
/*
        flockCentroid = flockCentroid / boids.Count;
        transform.position = flockCentroid;

        Time.timeScale = slowTimePercent;
        for (int i = activeFotn; i < flockSize; i++ ){
            Vector3 randomPos = transform.position + Random.insideUnitSphere * flockSpawnRadius;
            Boid b = Instantiate(boidPrefab,randomPos,Quaternion.Euler(randomPos - transform.position)).GetComponent<Boid>();
            b.name = "Boid_" + i;
            b.Velocity = Random.insideUnitSphere.normalized;
            b.ParentFlock = this;
            boids.Add(b);
            b.transform.parent=parent;
            activeFotn++;
            
        }
*/
    }
    public GameObject createFotn(string TTT){
        flockSize++;

        Vector3 flockCentroid = Vector3.zero;
        flockCentroid = flockCentroid / boids.Count;
        transform.position = flockCentroid;

        Time.timeScale = slowTimePercent;

        Vector3 randomPos = transform.position + Random.insideUnitSphere * flockSpawnRadius;
        Boid b = Instantiate(boidPrefab,randomPos,Quaternion.Euler(randomPos - transform.position)).GetComponent<Boid>();
        b.name = "Boid_" + activeFotn;
        b.fotnCode=TTT;
        b.Velocity = Random.insideUnitSphere.normalized;
        b.ParentFlock = this;
        boids.Add(b);
        b.transform.parent=parent;

        activeFotn++;
        return b.gameObject;
    }
    
}
