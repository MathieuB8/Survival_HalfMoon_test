using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public GameObject[] floorTiles;
	public int level_size = 10;

	private int floorTilesLength = 0;
	private List <Vector3> gridPositions = new List<Vector3> ();
	private Transform levelHolder; 

	void InitialiseGrid(){
		gridPositions.Clear ();
		for (int x = 0; x < level_size; x++){
			for(int y = 0; y < level_size; y++){
				gridPositions.Add (new Vector3 (x, y, 0f));
			}
		}
	}

	void Awake () {
		floorTilesLength = floorTiles.Length;
		InitialiseGrid ();
		CreateLevel(floorTiles,level_size);
	}

	int RandomTileIndex(GameObject[] listTiles,int length){
		return (int)Random.Range (0, length);
	}

	void CreateLevel(GameObject[] listGameObjects, int size){
		levelHolder = new GameObject ("LevelHolder").transform;
		for (int x = 0; x < level_size; x++){
			for(int y = 0; y < level_size; y++){
				int TileIndex = RandomTileIndex (floorTiles, floorTilesLength);
				GameObject tile = floorTiles[TileIndex];
				GameObject instance = Instantiate (tile, gridPositions [0], Quaternion.identity);
				instance.transform.SetParent(levelHolder);
				gridPositions.RemoveAt (0);
			}
		}
	}
}
