using UnityEngine;
using System.Collections;

public class Room {
	
	private GameObject floor;
	private GameObject[] walls;
	private GameObject[] exits;
	private GameObject wallPrefab;
	private GameObject exitPrefab;
	private Vector3 position;
	private bool leaveNorthOpen = false;
	private bool leaveSouthOpen = false;
	private bool leaveEastOpen = false;
	private bool leaveWestOpen = false;

	public bool LeaveEastOpen {
		get {
			return this.leaveEastOpen;
		}
		set {
			leaveEastOpen = value;
		}
	}

	public bool LeaveNorthOpen {
		get {
			return this.leaveNorthOpen;
		}
		set {
			leaveNorthOpen = value;
		}
	}

	public bool LeaveSouthOpen {
		get {
			return this.leaveSouthOpen;
		}
		set {
			leaveSouthOpen = value;
		}
	}

	public bool LeaveWestOpen {
		get {
			return this.leaveWestOpen;
		}
		set {
			leaveWestOpen = value;
		}
	}
	public Vector3 Position {
		get {
			return this.position;
		}
		set {
			position = value;
		}
	}	
	private Vector3 roomSize;

	public Vector3 RoomSize {
		get {
			return this.roomSize;
		}
		set {
			roomSize = value;
		}
	}
	
	public void randomizeRoom()
	{
		generateTerrain();
		generateWalls();
		generateExits();
	}
	
	private static int NORTH = 0;
	private static int SOUTH = 1;
	private static int EAST = 2;
	private static int WEST = 3;
	
	private void generateExits(){
		if(exits == null)
			exits = new GameObject[4];
		
		if(walls[NORTH] == null && !LeaveNorthOpen)
			exits[NORTH] = addExit(NorthMidPoint, NORTH);
		if(walls[SOUTH] == null && !LeaveSouthOpen)
			exits[SOUTH] = addExit(SouthMidPoint, SOUTH);
		if(walls[EAST] == null && !LeaveEastOpen)
			exits[EAST] = addExit(EastMidPoint, EAST);
		if(walls[WEST] == null && !LeaveWestOpen)
			exits[WEST] = addExit(WestMidPoint, WEST);
	}
	
	public Vector3 NorthMidPoint {
		get {
			return floor.transform.position + new Vector3(roomSize.x /2 ,0,roomSize.z);
		}
	}
	
	public Vector3 SouthMidPoint {
		get {
			return floor.transform.position + new Vector3(roomSize.x / 2,0,0);
		}
	}
	
	public Vector3 EastMidPoint {
		get {
			return floor.transform.position + new Vector3(roomSize.x ,0,roomSize.z / 2);
		}
	}
	
	public Vector3 WestMidPoint {
		get {
			return floor.transform.position + new Vector3(0,0,roomSize.z /2);
		}
	}
	
	public Vector3 RoomToNorthPosition {
		get {
			return floor.transform.position + new Vector3(0 ,0,roomSize.z);
		}
	}
	
	public Vector3 RoomToSouthPosition {
		get {
			return floor.transform.position - new Vector3(0,0,roomSize.z);
		}
	}
	
	public Vector3 RoomToEastPosition {
		get {
			return floor.transform.position + new Vector3(roomSize.x ,0,0);
		}
	}
	
	public Vector3 RoomToWestPosition {
		get {
			return floor.transform.position - new Vector3(roomSize.x,0,0);
		}
	}
	
	private GameObject addExit(Vector3 position, int side){
		GameObject exit = Object.Instantiate(exitPrefab) as GameObject;
		exit.transform.position = position;
		RoomExit roomexit = exit.GetComponent<RoomExit>();
		roomexit.Side = side;
		roomexit.onEnteredExit += onEnteredExit;
		return exit;
	}
	
	private void onEnteredExit(int side)
	{
		
		
		Room room = new Room();
		room.TerrainTextures = terrainTextures;
		room.RoomSize = roomSize;
		room.WallPrefab = WallPrefab;
		room.ExitPrefab = ExitPrefab;
		if(side == NORTH)
		{
			room.Position = RoomToNorthPosition;
			room.LeaveSouthOpen = true;
		}
		else if(side == SOUTH)		
		{
			room.Position = RoomToSouthPosition;
			room.LeaveNorthOpen = true;
		}
		else if(side == EAST)		
		{
			room.Position = RoomToEastPosition;
			room.LeaveWestOpen = true;
		}
		else if(side == WEST)		
		{
			room.Position = RoomToWestPosition;
			room.LeaveEastOpen = true;
		}

		
		room.randomizeRoom();
		
	
		
	}
	
	private void generateWalls()
	{
		if(walls == null)
		{
			walls = new GameObject[4];
		}
		int numOfWalls = UnityEngine.Random.Range(0,3);
		
		for(int i = 0;i<numOfWalls;i++)
		{
			int side = UnityEngine.Random.Range (1,4);
			
			switch (side)
			{
			case 1:
				if(walls[NORTH] == null && !LeaveNorthOpen)
				{
					walls[NORTH] = generateWall( new Vector3(roomSize.x,10,1), NorthMidPoint);
				}else{
					if(!LeaveNorthOpen)
						i--;
				}
				break;
			case 2:
				if(walls[SOUTH] == null && !LeaveSouthOpen)
				{
					walls[SOUTH] = generateWall( new Vector3(roomSize.x,10,1),  SouthMidPoint);
				}else{
					if(!LeaveSouthOpen)
						i--;
				}
				break;
			case 3:
				if(walls[EAST] == null && !LeaveEastOpen)
				{
					walls[EAST] = generateWall( new Vector3(1,10,roomSize.z),  EastMidPoint);
				}else{
					if(!LeaveEastOpen)
						i--;
				}
				break;
			case 4:
				if(walls[WEST] == null && !LeaveWestOpen)
				{
					walls[WEST] = generateWall( new Vector3(1,10,roomSize.z),  WestMidPoint);
				}else{
					if(!LeaveWestOpen)
						i--;
				}
				break;
			}
		}
	}
	
	

	private GameObject generateWall(Vector3 scale, Vector3 position)
	{
		GameObject w = Object.Instantiate(wallPrefab) as GameObject;
		w.SetActive(false);
		w.transform.localScale = scale;
		w.transform.position = position;
		w.SetActive(true);
		return w;
	}
	
	private void generateTerrain()
	{
		TerrainData td = new TerrainData();
		td.size = roomSize;
		SplatPrototype[] tdTexture = new SplatPrototype[1];
		tdTexture[0] = new SplatPrototype();
		tdTexture[0].texture = getRandomTexture();
		
		td.splatPrototypes = tdTexture;
		
		floor =  Terrain.CreateTerrainGameObject(td);
		floor.transform.position = position;
	}
	
	
	private Texture2D getRandomTexture()
	{
		int index = UnityEngine.Random.Range(0,terrainTextures.Length -1);
		return terrainTextures[index];
	}
	
	
	
	public GameObject WallPrefab{
		get { 
			return wallPrefab;
		}
		set { 
			wallPrefab = value;
		}
	}
	
	public GameObject ExitPrefab{
		get { 
			return exitPrefab;
		}
		set { 
			exitPrefab = value;
		}
	}
	
	private Texture2D[] terrainTextures;
	public Texture2D[] TerrainTextures
	{
		get { return terrainTextures;
		}
		set { terrainTextures = value;
		}
	}
	
}
