using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {


	public enum CharacterClass : int {Soldier=0,Wizard=1,Cleric=2,Augur=3,Archer=4};
	public CharacterClass characterClass;

	int HP;
	int ATK;
	int DEF;
	int MOV;
	int EVD;
	int ACC = 1;
	int PointCost;

	GameObject carryingTile = null;

	// Use this for initialization
	void Start () {
		switch(characterClass){
		case((CharacterClass)0):
			HP = 22;
			ATK = 4;
			DEF = 5;
			MOV = 5;
			EVD = 2;
			PointCost = 10;
			break;
		case((CharacterClass)1):
			HP = 20;
			ATK = 3;
			DEF = 2;
			MOV = 5;
			EVD = 3;
			PointCost = 15;
			break;
		case((CharacterClass)2):
			HP = 14;
			ATK = 1;
			DEF = 1;
			MOV = 4;
			EVD = 5;
			PointCost = 20;
			break;
		case((CharacterClass)3):
			HP = 14;
			ATK = 2;
			DEF = 2;
			MOV = 4;
			EVD = 3;
			PointCost = 25;
			break;
		case((CharacterClass)4):
			HP = 14;
			ATK = 4;
			DEF = 2;
			MOV = 5;
			EVD = 4;
			PointCost = 20;
			break;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public int returnStats(int statIndex){
		switch(statIndex){
		case(0):
			return HP;
			break;
		case(1):
			return ATK;
			break;
		case(2):
			return DEF;
			break;
		case(3):
			return MOV;
			break;
		case(4):
			return EVD;
			break;
		case(5):
			return ACC;
			break;
		case(6):
			return PointCost;
			break;
		default:
			return 0;
			break;
		}

	}

}
