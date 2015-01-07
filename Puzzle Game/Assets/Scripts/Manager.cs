using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {

	Vector2 touchDeltaPosition;
	public GameObject currentLayer;
	int currentLayerID;
	public GameObject currentWorld;
	List<SpriteRenderer> layerRenderers = new List<SpriteRenderer>();
	List<GameObject> layers = new List<GameObject>();
	int level = 0;
	int timesCalled = 0;
	string nameOfFoundLayer;


	// Use this for initialization
	void Start () {

		foreach(SpriteRenderer layerRenderer in currentWorld.transform.GetComponentsInChildren<SpriteRenderer>()){
			layerRenderers.Add(layerRenderer);
		}
		for(int i = 0; i<currentWorld.transform.childCount; i++){
			layers.Add(currentWorld.transform.GetChild(i).gameObject);
		}
		currentLayerID = currentLayer.GetComponent<LayerBehaviour>().LayerInfo.ID;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount>0){
			touchDeltaPosition = Input.GetTouch(0).deltaPosition;
		}

	}

	void OnGUI(){
		GUI.Label(new Rect(10, 10, 100, 20), currentLayerID.ToString());
		GUI.Label(new Rect(Screen.width/2, 10, 100, 20), touchDeltaPosition.ToString());
		GUI.Label(new Rect(Screen.width/4, 10, 100, 20), nameOfFoundLayer);
	}

	void switchLayer(GameObject newLayer){
		currentLayer.GetComponent<LayerBehaviour>().setActiveLayer(false);
		currentLayer = newLayer;
		newLayer.GetComponent<LayerBehaviour>().setActiveLayer(true);
		currentLayerID = currentLayer.GetComponent<LayerBehaviour>().LayerInfo.ID;

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
		if(currentLayerID!=layers.Count){
			switchLayer(FindLayerByID(currentLayerID+1));
			nameOfFoundLayer = FindLayerByID(currentLayerID+1).name;
		}

	}
	public void switchLayerDown(){
		if(currentLayerID!=0){
			switchLayer(FindLayerByID(currentLayerID-1));
			nameOfFoundLayer = FindLayerByID(currentLayerID+1).name;
		}
	}
}
