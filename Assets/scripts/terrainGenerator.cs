using UnityEngine;
using System.Collections;

public class terrainGenerator : MonoBehaviour {
	
	public GameObject player;
	
	public Texture2D[] terrainTextures;
	
	public Vector3 terrainTileSize = new Vector3(1,1,1);
	public int numberOfTilesToLoad = 5;
	
	public GameObject wall;
	public  GameObject exit;
	
	// Use this for initialization
	void Start () {
			player.SetActive(false);
		
	}
	
	void OnGUI () {
//		if (GUI.Button (new Rect (10,10,300,100), "Generate random Square Terrain")) {
//			Transform target = player.transform;
//			for(int i = 0;i<numberOfTilesToLoad/2;i++)
//			{
//				for(int j=0;j<numberOfTilesToLoad/2;j++)
//				{
//					float x = i * terrainTileSize.x;
//					float z = j * terrainTileSize.z;
//					var terrain = generateTerrain();
//					terrain.transform.position = target.position + new Vector3(x,-1,z);
//					terrain = generateTerrain();
//					terrain.transform.position = target.position + new Vector3(x*-1,-1,z*-1);
//					terrain = generateTerrain();
//					terrain.transform.position = target.position + new Vector3(x*-1,-1,z);
//					terrain = generateTerrain();
//					terrain.transform.position = target.position + new Vector3(x,-1,z*-1);
//				}
//			}
//			
//			player.SetActive(true);
//		}
//		
		
		if (GUI.Button (new Rect (10,110,300,100), "Generate Random Room")) {
			Room room = new Room();
			room.TerrainTextures = terrainTextures;
			room.RoomSize = terrainTileSize;
			room.WallPrefab = wall;
			room.ExitPrefab = exit;
			room.Position = player.transform.position - new Vector3(0,1,0);
			
			room.randomizeRoom();
			
			player.transform.position += new Vector3(10,0,10);
			player.SetActive(true);
		}
		
		
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
