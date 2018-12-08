using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GM.skills[0] > 0)
            {
                GM.skills[0] -= 1;
                PlayerController.realiving = true;
            }
            else
            {
                GM.isAlive = false;
            }
        }
    }
}
