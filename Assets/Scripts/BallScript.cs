using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	public float maxSpeed = 10f;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(rb.velocity.magnitude > maxSpeed){
			rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
		}
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Goal"))
		{
			Destroy(gameObject);
			Debug.Log("GOAL!!!!");
		}
	}
}
