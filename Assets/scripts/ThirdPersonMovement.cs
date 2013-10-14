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
	
	private bool superMoveMode = false;
	
	void Update () {
		
		if(superMoveMode)
		{
			
		}
		else
		{
			if(player.isGrounded)
			{
				moveDirection.Set(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
	
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;
				
				if(Input.GetButton("Jump"))
				{
					moveDirection.y = jumpSpeed;
				}
				
				player.transform.Rotate(0,Input.GetAxis("Rotate"),0);
			}
			
			moveDirection.y -= gravity * Time.deltaTime;
			
			player.Move(moveDirection*Time.deltaTime);
		}
		
	}
}
