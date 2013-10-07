using UnityEngine;
using System.Collections;

public class terrainGenerator : MonoBehaviour {
	
	public Transform target;
	
	public Vector3 terrainTileSize = new Vector3(1,1,1);
	public int numberOfTilesToLoad = 5;
	
	// Use this for initialization
	void Start () {
		
		for(int i = 0;i<numberOfTilesToLoad;i++)
		{
			for(int j=0;j<numberOfTilesToLoad;j++)
			{
				float x = i * terrainTileSize.x;
				float z = j * terrainTileSize.z;
				var terrain = generateTerrain();
				terrain.transform.position = target.position + new Vector3(x,-1,z);
				terrain = generateTerrain();
				terrain.transform.position = target.position + new Vector3(x*-1,-1,z*-1);
				terrain = generateTerrain();
				terrain.transform.position = target.position + new Vector3(x*-1,-1,z);
				terrain = generateTerrain();
				terrain.transform.position = target.position + new Vector3(x,-1,z*-1);
			}
		}
		
		
	}
	
	private GameObject generateTerrain()
	{
		TerrainData td = new TerrainData();
		td.size = terrainTileSize;
		SplatPrototype[] tdTexture = new SplatPrototype[1];
		tdTexture[0] = new SplatPrototype();
		tdTexture[0].texture = getRandomTexture();
		
		td.splatPrototypes = tdTexture;
		
		
		
		return Terrain.CreateTerrainGameObject(td);
	}
	
	public Texture2D[] terrainTextures;
	
	private Texture2D getRandomTexture()
	{
		int index = UnityEngine.Random.Range(0,terrainTextures.Length -1);
		return terrainTextures[index];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
