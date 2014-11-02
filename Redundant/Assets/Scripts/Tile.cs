using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Tile : MonoBehaviour {

	public Tile connection1;
	public Tile connection2;
	public Tile connection3;
	public Tile connection4;

	bool carryingUnit = false;
	GameObject carriedUnit = null;

	//public int tileType;
	public enum TileType{Water,Structure,Cobble,Field,Forest};

	public TileType tileType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	List<GameObject> DetermineMovementRange(int mov){
		List<GameObject> tilesInRange = new List<GameObject>();
		tilesInRange.Add(this.gameObject);
		for(int i = mov; i >0; i--){
			foreach(GameObject tile in tilesInRange){
				if(tile.GetComponent<Tile>().connection1!=null){
					if(!tilesInRange.Contains(tile.GetComponent<Tile>().connection1.gameObject)){
						tilesInRange.Add(connection1.gameObject);
					}
				}
				if(tile.GetComponent<Tile>().connection2!=null){
					if(!tilesInRange.Contains(tile.GetComponent<Tile>().connection2.gameObject)){
						tilesInRange.Add(connection2.gameObject);
					}
				}
				if(tile.GetComponent<Tile>().connection3!=null){
					if(!tilesInRange.Contains(tile.GetComponent<Tile>().connection3.gameObject)){
						tilesInRange.Add(connection3.gameObject);
					}
				}
				if(tile.GetComponent<Tile>().connection4!=null){
					if(!tilesInRange.Contains(tile.GetComponent<Tile>().connection4.gameObject)){
						tilesInRange.Add(connection4.gameObject);
					}
				}
			}
		}
		return(tilesInRange);
	}
}
