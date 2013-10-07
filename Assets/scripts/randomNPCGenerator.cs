using UnityEngine;
using System.Collections;

public class randomNPCGenerator : MonoBehaviour {
	
	public Transform player;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	private bool hasSpawnedGroups = false;
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(isPlayerCloseEnough())
		{
			spawnNPCs();
		}
	}
	
	
	public Vector3 topLeft = new Vector3(-100,0,100);
	public Vector3 topRight = new Vector3(100,0,100);
	public Vector3 botLeft = new Vector3(-100,0,-100);
	public Vector3 botRight = new Vector3(100,0,-100);
	
	private void spawnNPCs(){
		if(!hasSpawnedGroups)
		{
			Vector3[] spawnPoints = new Vector3[4];
			spawnPoints[0] = player.position + topLeft;
			spawnPoints[1] = player.position + topRight;
			spawnPoints[2] = player.position + botLeft;
			spawnPoints[3] = player.position + botRight;
			
			foreach(Vector3 point in spawnPoints)
			{
				generateRandomNPC(point);
			}		
			
			hasSpawnedGroups = true;
		}
	}
	
	public GameObject npc1,npc2;
	
	private void generateRandomNPC(Vector3 p)
	{
		Instantiate(npc1,p, Quaternion.LookRotation(player.position));
		p.x += 3;
		Instantiate(npc2,p, Quaternion.LookRotation(player.position));
	}
	
	public float inRangeDistance = 30;
	
	private bool isPlayerCloseEnough()
	{
		Vector3 distance = player.position - transform.position;
		distance.x = Mathf.Abs(distance.x);
		distance.z = Mathf.Abs(distance.z);
		
		return distance.x + distance.z < inRangeDistance;
	}
}
