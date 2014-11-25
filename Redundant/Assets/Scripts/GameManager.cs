using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject activeUnit  = null;
	bool initialised = false;
	List<GameObject> allTiles = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(initialised == false){
			foreach(GameObject tile in GameObject.FindGameObjectsWithTag("Tile")){
				allTiles.Add(tile);
			}
		}
	}

	public void resetTiles(Color newColor){
		foreach(GameObject tile in allTiles){
			tile.GetComponent<SpriteRenderer>().color = newColor;
		}
	}
}
