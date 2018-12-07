using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour {

    public float speed = 5f;
    public float destroyTime = 20f;
    float timeCnt;

	// Use this for initialization
	void Start () {
        timeCnt = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
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
