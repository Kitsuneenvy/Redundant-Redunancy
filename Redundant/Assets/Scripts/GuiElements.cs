using UnityEngine;
using System.Collections;

public class GuiElements : MonoBehaviour {

	Rect characterDataBox = new Rect (0,0,Screen.width/6,Screen.height);
	bool displayCharacterData = false;
	GameObject selectedCharacter;

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
				Vector3 touchWorldPosition = Camera.main.WorldToScreenPoint(startPos);
				RaycastHit2D rayHit = Physics2D.OverlapPoint(touchWorldPosition);
			}
		}
	
	}

	void OnGUI(){
		if(displayCharacterData == true){
			characterDataBox = GUILayout.Window(0,characterDataBox,CharacterDataFunction,"");
		}
	}

	void CharacterDataFunction(int id){

	}
}
