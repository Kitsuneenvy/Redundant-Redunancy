using UnityEngine;
using System.Collections;

public class GuiElements : MonoBehaviour {

	Rect characterDataBox = new Rect (0,0,Screen.width/6,Screen.height);
	bool displayCharacterData = false;
	public GameObject selectedCharacter;
	public GUISkin usedSkin;

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
				RaycastHit2D rayHit = Physics2D.Raycast(startPos,-Vector2.up);

				if(rayHit.collider!=null){
					GameObject hitObject = rayHit.collider.gameObject;
					if(hitObject.tag=="Tile"){
						if(hitObject.GetComponent<Tile>().returnCarriedUnit()!=null){
							selectedCharacter = hitObject.GetComponent<Tile>().returnCarriedUnit();
						}
					}
				}
				Vector3 touchWorldPosition = Camera.main.WorldToScreenPoint(startPos);
				//RaycastHit2D rayHit = Physics2D.OverlapPoint(touchWorldPosition);
			}
		}
		if(selectedCharacter==null){
			displayCharacterData = false;
		} else {
			displayCharacterData = true;
		}
	
	}

	void OnGUI(){
		GUI.skin = usedSkin;
		if(displayCharacterData == true){
			characterDataBox = GUILayout.Window(0,characterDataBox,CharacterDataFunction,"");
		}
	}

	void CharacterDataFunction(int id){
		GUILayout.BeginVertical();
		GUILayout.Label(selectedCharacter.name);
		//GUILayout.Label("Texture/image");
		GUILayout.Label("HP : " + selectedCharacter.GetComponent<Character>().returnStats(0).ToString());
		GUILayout.Label("ATK : " +selectedCharacter.GetComponent<Character>().returnStats(1).ToString());
		GUILayout.Label("DEF : " +selectedCharacter.GetComponent<Character>().returnStats(2).ToString());
		GUILayout.Label("MOV : " +selectedCharacter.GetComponent<Character>().returnStats(3).ToString());
		GUILayout.Label("EVD : " +selectedCharacter.GetComponent<Character>().returnStats(4).ToString());
		GUILayout.Label("ACC : " +selectedCharacter.GetComponent<Character>().returnStats(5).ToString());
		GUILayout.EndVertical();
	}
}
