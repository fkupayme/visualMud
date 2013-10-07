using UnityEngine;
using System.Collections;

public class ThirdPersonMovement : MonoBehaviour {
	
	private CharacterController player;
	
	// Use this for initialization
	void Start () {
		player = GetComponent<CharacterController>();
	}
	
	public float speed = 6;
	public float jumpSpeed = 8;
	public float gravity = 20;
	
	private Vector3 moveDirection = Vector3.zero;
	
	void Update () {
		
		if(player.isGrounded)
		{
			moveDirection.Set(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			
			if(Input.GetButton("Jump"))
			{
				moveDirection.y = jumpSpeed;
			}
			
			if(moveDirection.x > 0 && moveDirection.z > 0)
			{
				Debug.DrawLine(transform.position,10*moveDirection);
			}
		}
		
		moveDirection.y -= gravity * Time.deltaTime;
		
		player.Move(moveDirection*Time.deltaTime);
		
	}
}
