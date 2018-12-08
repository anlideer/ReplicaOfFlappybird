using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public AudioSource deadAudio;
    public AudioSource flyAudio;

    public float upForce = 1000f;
    public static bool realiving = false;
    public Vector3 realiveP = new Vector3(-3.5f, 0.13f, 0);


    Rigidbody2D rb;
    Animator anim;
    bool flying;
    bool deadAnimPlayed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        flying = false;
        deadAnimPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GM.isAlive && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            flying = true;
            anim.Play("flying");
            flyAudio.Play();
        }
        if (GM.isAlive && (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)))
        {
            flying = false;
            anim.Play("static");
        }

        if (flying)
        {
            rb.AddForce(upForce * Vector2.up * Time.deltaTime);
        }

        if (GM.isAlive == false && deadAnimPlayed == false)
        {
            deadAnimPlayed = true;
            anim.Play("dead");
            deadAudio.Play();
        }

        if (realiving)
        {
            realiving = false;
            rb.velocity = new Vector2(0, 0);
            transform.position = realiveP;
            // TODO: 添加音效！
        }
	}
}
