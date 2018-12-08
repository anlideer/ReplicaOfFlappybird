using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour {

    public float speed = 5f;
    public float destroyTime = 20f;
    public float factor = 1.2f;
    float timeCnt;
    public bool speedSetted;

	// Use this for initialization
	void Start () {
        timeCnt = Time.time;
        speedSetted = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!speedSetted && GM.skillSetted)
        {
            speedSetted = true;
            if (GM.skills[1] > GM.skills[2])
            {
                speed = 5f *  factor* (GM.skills[1] - GM.skills[2]);
            }
            else if (GM.skills[1] < GM.skills[2])
            {
                speed = 5f / (factor * (GM.skills[2] - GM.skills[1]));
            }
        }
		if (GM.isAlive)
        {
            transform.Translate(speed * Vector3.left * Time.deltaTime);
        }

        if (timeCnt + destroyTime < Time.time && GM.isAlive)
            Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GM.score += 1;
            GM.scoreUp = true;
        }
      
    }
}
