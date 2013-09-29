using UnityEngine;
using System.Collections;

public class ThirdPersonMovement : MonoBehaviour {
	
	private CharacterController player;
	
	// Use this for initialization
	void Start () {
		player = GetComponent<CharacterController>();
	}
	
	public float speed = 50;
	public float gravity = 20;
	
	// Update is called once per frame
	void FixedUpdate () {
		
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		Vector3 move = new Vector3(x,0,z);
		move *= Time.deltaTime;
		move *= speed;
		
		if(!player.isGrounded)
		{
			move.y -= gravity;
		}
		
		player.Move(move);
		
		
	}
}
