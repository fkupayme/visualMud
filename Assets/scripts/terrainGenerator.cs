using UnityEngine;
using System.Collections;

public class terrainGenerator : MonoBehaviour {
	
	public GameObject player;
	
	public Texture2D[] terrainTextures;
	
	public Vector3 terrainTileSize = new Vector3(1,1,1);
	public int numberOfTilesToLoad = 5;
	
	public GameObject wall;
	public  GameObject exit;
	public GameObject edge;
	
	// Use this for initialization
	void Start () {
			player.SetActive(false);
		
	}

	private Room makeDefaultRoom (Vector3 pos)
	{
		Room home = new Room();
		home.TerrainTextures = terrainTextures;
		home.RoomSize = terrainTileSize;
		home.WallPrefab = wall;
		home.ExitPrefab = exit;
		home.EdgePrefab = edge;
		home.Position = pos;
		home.ExitHandler = (side) => {Debug.Log (side);};
		return home;
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
		
		
		if(GUI.Button (new Rect(10,10,300,100), "Generate Player Home"))
		{
			Area newCharArea = new Area();
			
			Room home = makeDefaultRoom(player.transform.position - new Vector3(0,1,0));			
			Room homeN1 = makeDefaultRoom(home.Position + new Vector3(home.RoomSize.x,0,0));
			homeN1.LeaveSouthOpen = true;
			homeN1.randomizeRoom();
			Room homeS1 = makeDefaultRoom(home.Position - new Vector3(home.RoomSize.x,0,0));
			homeS1.LeaveNorthOpen = true;
			homeS1.randomizeRoom();
			Room homeW1 = makeDefaultRoom(home.Position - new Vector3(0,0,home.RoomSize.x));		
			homeW1.LeaveEastOpen = true;
			homeW1.randomizeRoom();
			Room homeE1 = makeDefaultRoom(home.Position + new Vector3(0,0,home.RoomSize.x));
			homeE1.LeaveWestOpen = true;
			homeE1.randomizeRoom();
			
			home.generateTerrain();
			
			home.addNorthExit(homeN1);
			home.addSouthExit(homeS1);
			home.addEastExit(homeE1);
			home.addWestExit(homeW1);
			
			home.generateEdges();
			
			player.transform.position = home.WestMidPoint + new Vector3((home.RoomSize.x /2),1,0);
			player.SetActive(true);
			
		}
		
		if (GUI.Button (new Rect (10,110,300,100), "Generate Random Room")) {
			Room room =  makeDefaultRoom(player.transform.position - new Vector3(0,1,0));	
			
			room.randomizeRoom();
			
			player.transform.position = room.WestMidPoint + new Vector3((room.RoomSize.x /2),1,0);
			player.SetActive(true);
		}
		
		
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
