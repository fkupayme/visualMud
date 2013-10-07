using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	
	public Vector3 positionOffset = new Vector3(0,10,-10);
	
	// Use this for initialization
	void Start () {
		positionOffset = transform.position - player.transform.position;
	}
	
	public Transform player;
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + positionOffset;
		//transform.LookAt(player.transform.position);
	}
}
