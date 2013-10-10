using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	
	public Vector3 positionOffset = new Vector3(0,10,-10);
	
	// Use this for initialization
	void Start () {
		positionOffset = player.transform.position - transform.position;
	}
	
	public Transform player;

	
	public float cameraEditModeSpeed = 10;
	
	private Vector3 cameraTargetPos = Vector3.zero;
	
	void LateUpdate()
	{
		
//		if(cameraEditMode)
//		{
//			
//			Vector3 cameraMove = new Vector3(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse ScrollWheel")*-1,Input.GetAxis("Mouse Y"));
//			
//			
//			cameraTargetPos = transform.position + cameraMove;
//			
//			if(Input.GetButton("Jump"))
//			{
//				positionOffset = player.transform.position - transform.position;
//				cameraEditMode = false;
//			}
//			
//			
//		}
//		
		transform.position = Vector3.Lerp(transform.position, player.transform.position + positionOffset, Time.deltaTime * cameraEditModeSpeed);
//		
		
	}
	
	private bool cameraEditMode = false;
	
	void OnGUI()
	{
		if (GUI.Button (new Rect (10,10,300,100), "Set Camera")) {
			cameraEditMode = true;
		}
	}
}
