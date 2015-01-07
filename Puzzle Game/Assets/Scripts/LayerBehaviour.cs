using UnityEngine;
using System.Collections;


[System.Serializable]
public class Layer {
	public int ID;
	public float correctAngle;
	public float currentAngle;
	public Color currentColor;
	public Color defaultColor;

}

public class LayerBehaviour : MonoBehaviour {

	public Layer LayerInfo;
	
	Vector2 touchDeltaPosition;
	public bool ActiveLayer = false;
	Color originalColor;
	bool correctAngle = false;
	// Use this for initialization
	void Start () {
		originalColor = this.GetComponent<SpriteRenderer>().color;
		LayerInfo.defaultColor = originalColor;
		LayerInfo.currentColor = originalColor;
		LayerInfo.currentAngle = this.transform.rotation.z;

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
						transform.Rotate(Vector3.forward,touchDeltaX*-4);
						LayerInfo.currentAngle = this.transform.rotation.z;
//					} else {
//						transform.Rotate(Vector3.forward,touchDeltaX*4);
//						LayerInfo.currentAngle = this.transform.rotation.z;
//					}
					}
					if(this.transform.rotation.z <= LayerInfo.correctAngle +4 && this.transform.rotation.z >= LayerInfo.correctAngle -4){
						correctAngle = true;
						//this.GetComponent<SpriteRenderer>().color = Color.blue;
					}
				} else if (Mathf.Abs(touchDeltaX) < Mathf.Abs(touchDeltaY)){
					this.GetComponent<SpriteRenderer>().color = Color.blue;
					if(touchDeltaY > 0){
						GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().switchLayerUp();
					} else {
						GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().switchLayerDown();
					}
				}
			}
		}
	}

	void OnGUI (){
		if(ActiveLayer){
			GUI.Label(new Rect(Screen.width/5, 10, 100, 20), this.name);
		}
	}




	public void setActiveLayer(bool newValue){
		ActiveLayer = newValue;
		if(ActiveLayer == false){
			LayerInfo.currentColor = new Color(LayerInfo.currentColor.r,LayerInfo.currentColor.g,LayerInfo.currentColor.b,100);
		} else {
			LayerInfo.currentColor = LayerInfo.defaultColor;
		}
		this.GetComponent<SpriteRenderer>().color = LayerInfo.currentColor;
	}
}
