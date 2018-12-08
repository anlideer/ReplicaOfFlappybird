using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject pipe;

    public float colddown = 2f;
    float nextTime = 0f;
    bool adjusted = false;

	// Use this for initialization
	void Start () {
        adjusted = false;
        colddown = 2f;
    }
	
	// Update is called once per frame
	void Update () {
        if (!adjusted && GM.skillSetted)
        {
            adjusted = true;
            if (GM.skills[1] > GM.skills[2])
            {
                colddown = 2f / ( pipe.GetComponent <Pipes>().factor * (GM.skills[1] - GM.skills[2]));
            }
            else if (GM.skills[1] < GM.skills[2])
            {
                colddown = 2f * (pipe.GetComponent<Pipes>().factor * (GM.skills[2] - GM.skills[1]));
            }

        }
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
