using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject pipe;

    public float colddown = 2f;
    float nextTime = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Range: -3f -- 2.5f
        if (GM.isAlive && (Time.time >= nextTime))
        {
            nextTime = Time.time + colddown;
            Vector3 spawnPos = transform.position;
            spawnPos.y = Random.Range(-3f, 2.5f);
            Instantiate(pipe, spawnPos, transform.rotation);
        }
	}
}
