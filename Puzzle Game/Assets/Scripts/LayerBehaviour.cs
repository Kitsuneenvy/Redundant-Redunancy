using UnityEngine;
using System.Collections;


[System.Serializable]
public class Layer {
	public int ID;
	public float currentAngle;
	public Color currentColor;
	public Color defaultColor;

}

public class LayerBehaviour : MonoBehaviour {

	public Layer LayerInfo;
	
	Vector2 touchDeltaPosition;
	public bool ActiveLayer = false;
	Color originalColor;
	public bool correctAngle = false;
	bool changedLayer = false;
	bool movingLayer = false;
	bool aboveCorrect = false;
	bool belowCorrect = false;
	// Use this for initialization
	void Start () {
		originalColor = this.GetComponent<SpriteRenderer>().color;
		LayerInfo.defaultColor = originalColor;
		LayerInfo.currentColor = originalColor;
		LayerInfo.currentAngle = this.transform.rotation.z;
		this.GetComponent<SpriteRenderer>().color = LayerInfo.currentColor;
		this.transform.eulerAngles= new Vector3(0,0,Random.Range(0.0f,360.0f));
	}
	
	// Update is called once per frame
	void Update () {
		if(ActiveLayer == true){
			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
				
				touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				float touchDeltaX = Input.GetTouch(0).deltaPosition.x;
				float touchDeltaY = Input.GetTouch(0).deltaPosition.y;
				if(Mathf.Abs(touchDeltaX) > Mathf.Abs(touchDeltaY)){
					if(Mathf.Abs(touchDeltaX) > 0){
						transform.Rotate(Vector3.forward,touchDeltaX*-2);
						LayerInfo.currentAngle = this.transform.rotation.z;
					}
					LayerInfo.currentAngle = clampRotation(this.transform.rotation.z);
					checkAboveAndBelow();
					if(aboveCorrect==true && belowCorrect ==true){
						correctAngle = true;
					}
//					if(LayerInfo.currentAngle <= 4 || LayerInfo.currentAngle >= 356){
//						correctAngle = true;
//						this.GetComponent<SpriteRenderer>().color = Color.blue;
//					} else {
//						this.GetComponent<SpriteRenderer>().color = LayerInfo.defaultColor;
//					}
					movingLayer = true;
				} else if ((Mathf.Abs(touchDeltaX) < Mathf.Abs(touchDeltaY))&&changedLayer == false&&movingLayer == false){
					if(touchDeltaY > 0){
						GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().switchLayerUp();
						changedLayer = true;
					} else {
						GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().switchLayerDown();
						changedLayer = true;
					}
				}
			} else if(Input.touchCount == 0){
				changedLayer = false;
				movingLayer = false;
			}
		}
	}

	void OnGUI (){
	}


	public void setCurrentAlpha(float alpha){
		LayerInfo.currentColor = new Color(LayerInfo.currentColor.r,LayerInfo.currentColor.g,LayerInfo.currentColor.b,alpha);
		this.GetComponent<SpriteRenderer>().color = LayerInfo.currentColor;
	}

	public void setActiveLayer(bool newValue){
		ActiveLayer = newValue;
		if(ActiveLayer == false){
			//LayerInfo.currentColor = new Color(LayerInfo.currentColor.r,LayerInfo.currentColor.g,LayerInfo.currentColor.b,alpha);
		} else {
			LayerInfo.currentColor = LayerInfo.defaultColor;
			this.GetComponent<SpriteRenderer>().color = LayerInfo.currentColor;
		}
	}

	float clampRotation(float rotation){
		float newRotation = 0;
		if(rotation<0){
			newRotation = rotation+360;
		} else if( rotation >360){
			newRotation = rotation -360;
		}
		if(newRotation<0||newRotation>360){
			clampRotation(newRotation);
			return 0;
		} else {
			return newRotation;
		}
	}
	void checkAboveAndBelow(){
		if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().returnAboveAndBelowCorrect(LayerInfo.ID).x == 1){
			aboveCorrect = true;
		} else {
			aboveCorrect = false;
		}
		if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().returnAboveAndBelowCorrect(LayerInfo.ID).y == 1){
			belowCorrect = true;
		} else {
			belowCorrect = false;
		}
	}
}
