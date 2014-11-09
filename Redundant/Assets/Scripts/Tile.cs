using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Tile : MonoBehaviour {

	public Tile connection1;
	public Tile connection2;
	public Tile connection3;
	public Tile connection4;

	bool carryingUnit = false;
	bool characterActive = false;
	public GameObject carriedUnit = null;

	//public int tileType;
	public enum TileType{Water,Structure,Cobble,Field,Forest};

	public TileType tileType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount == 1){
			Touch touchZero = Input.GetTouch(0);
			Vector2 startPos = Vector2.zero;
			Vector2 direction = Vector2.zero;
			bool touchMoved = false;
			
			switch(touchZero.phase){
			case(TouchPhase.Began):
				startPos = touchZero.position;
				break;
			case(TouchPhase.Moved):
				direction = touchZero.position - startPos;
				break;
			case(TouchPhase.Ended):
				if(direction != Vector2.zero){
					touchMoved = true;
				}
				break;
			}
			if(touchMoved == false){
				if(characterActive ==false){
					RaycastHit2D rayHit = Physics2D.Raycast(startPos,-Vector2.up);
					
					if(rayHit.collider!=null){
						GameObject hitObject = rayHit.collider.gameObject;
						if(hitObject.gameObject == this.gameObject){
							if(carriedUnit!=null){
								characterActive = true;
								GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().activeUnit = carriedUnit;
								foreach(GameObject reachableTile in DetermineMovementRange(carriedUnit.GetComponent<Character>().returnStats(3))){
									if(reachableTile.GetComponent<Tile>().carryingUnit == false){
										//change sprite to moveable
									} else {
										//change sprite to attack
									}
								}
							}
						}
					}
					Vector3 touchWorldPosition = Camera.main.WorldToScreenPoint(startPos);
					//RaycastHit2D rayHit = Physics2D.OverlapPoint(touchWorldPosition);
				} else {
					RaycastHit2D rayHit = Physics2D.Raycast(startPos,-Vector2.up);
					
					if(rayHit.collider!=null){
						GameObject hitObject = rayHit.collider.gameObject;
						if(hitObject.gameObject == this.gameObject){
							List<GameObject> tiles = new List<GameObject>();
							tiles.Add(this.gameObject);
							if(carriedUnit==null){
								GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().activeUnit.GetComponent<Character>().move(tiles);

							}
						}
					}
				}
			}
		}
	
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

	public GameObject returnCarriedUnit(){
		return carriedUnit;
	}
}
