using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {

	Vector2 touchDeltaPosition;
	public GameObject currentLayer;
	public int currentLayerID;
	public GameObject currentWorld;
	List<SpriteRenderer> layerRenderers = new List<SpriteRenderer>();
	List<GameObject> layers = new List<GameObject>();
	int level = 0;
	int timesCalled = 0;
	string nameOfFoundLayer;
	GameObject mainCamera = null;
	public float cameraStartPosition = 3;
	public float cameraMovementMultiplier = 0.5f;
	bool initialised = false;


	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		foreach(SpriteRenderer layerRenderer in currentWorld.transform.GetComponentsInChildren<SpriteRenderer>()){
			layerRenderers.Add(layerRenderer);
		}
		for(int i = 0; i<currentWorld.transform.childCount; i++){
			layers.Add(currentWorld.transform.GetChild(i).gameObject);
		}
		currentLayerID = currentLayer.GetComponent<LayerBehaviour>().LayerInfo.ID;
		mainCamera.transform.position = new Vector3(mainCamera.transform.position.x,cameraStartPosition+cameraMovementMultiplier*currentLayerID,mainCamera.transform.position.z);

	}
	
	// Update is called once per frame
	void Update () {
		if(!initialised){
			switchLayer(currentLayer);
			initialised = true;
		}
		if(Input.GetKeyDown(KeyCode.A)){
			//switchLayer(FindLayerByID(currentLayerID+1));
			switchLayerUp();
		}
		if(Input.GetKeyDown(KeyCode.S)){
			switchLayerDown();
		}
		if(Input.touchCount>0){
			touchDeltaPosition = Input.GetTouch(0).deltaPosition;
		}

	}

	void OnGUI(){
		GUI.Label(new Rect(10, 10, 100, 20), currentLayerID.ToString());
		GUI.Label(new Rect(Screen.width/2, 10, 100, 20), layers.Count.ToString());
	}

	void switchLayer(GameObject newLayer){
		currentLayer.GetComponent<LayerBehaviour>().setActiveLayer(false);
		currentLayer = newLayer;
		newLayer.GetComponent<LayerBehaviour>().setActiveLayer(true);
		foreach(GameObject layer in layers){
			if(layer.GetComponent<LayerBehaviour>().LayerInfo.ID!=newLayer.GetComponent<LayerBehaviour>().LayerInfo.ID){
				layer.GetComponent<LayerBehaviour>().setCurrentAlpha(Mathf.Abs(((1.0f/(float)(((float)layer.GetComponent<LayerBehaviour>().LayerInfo.ID-(float)newLayer.GetComponent<LayerBehaviour>().LayerInfo.ID)/-5.0f))/10.0f)));
			}
		}
		currentLayerID = currentLayer.GetComponent<LayerBehaviour>().LayerInfo.ID;
		mainCamera.transform.position = new Vector3(mainCamera.transform.position.x,cameraStartPosition+cameraMovementMultiplier*currentLayerID,mainCamera.transform.position.z);

	}

	GameObject FindLayerByID(int IDToFind){
		foreach(GameObject layer in layers){
			if(layer.GetComponent<LayerBehaviour>().LayerInfo.ID == IDToFind){
				return layer;
			}
		}
		return null;
	}
	
	public void switchLayerUp(){
		if(currentLayerID!=layers.Count-1){
			switchLayer(FindLayerByID(currentLayerID+1));
			nameOfFoundLayer = FindLayerByID(currentLayerID).name;
		}

	}
	public void switchLayerDown(){
		if(currentLayerID!=0){
			switchLayer(FindLayerByID(currentLayerID-1));
			nameOfFoundLayer = FindLayerByID(currentLayerID).name;
		}
	}
	public Vector2 returnAboveAndBelowCorrect(int passedID){
		Vector2 aboveBelow = Vector2.zero;
		if(Mathf.Abs(FindLayerByID(passedID).GetComponent<LayerBehaviour>().LayerInfo.currentAngle- FindLayerByID(passedID+1).GetComponent<LayerBehaviour>().LayerInfo.currentAngle)<=4){
			aboveBelow.x = 1;
		}
		if(Mathf.Abs(FindLayerByID(passedID).GetComponent<LayerBehaviour>().LayerInfo.currentAngle- FindLayerByID(passedID-1).GetComponent<LayerBehaviour>().LayerInfo.currentAngle)<=4){
			aboveBelow.y = 1;
		}
		return aboveBelow;
	}
}
