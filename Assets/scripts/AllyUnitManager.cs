using UnityEngine;
using System.Collections;

public class AllyUnitManager : MonoBehaviour {

	private Vector3 _NextSpawnPoint = new Vector3 (-5, 0, 0);
	public GameObject Prefab;

	public int gold; 
	public float SpawnDistance = 50; 

	public int regularMortyCost = 10;

	double time = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		generateGold(); 

		//if button pressed and gold greater > unit cost, spawn unit 
		if (gold >= regularMortyCost) {
			SpawnRegularMorty ();
		}
	}

	private void SpawnRegularMorty(){
		var regularMorty = MakeRegularMorty ();
		regularMorty.transform.position = _NextSpawnPoint;
		gold -= regularMortyCost;
	}

	private GameObject MakeRegularMorty() {
		GameObject obj = GameObject.Instantiate (Prefab);
		obj.transform.localScale = new Vector3 (-2, 2, 2);
		return obj;
	}

	private void generateGold() { 
		while (time > 1.0f) {
			time -= 1.0f; 
			gold += 5; 
			Debug.Log ("gold amount: " + gold);
			Debug.Log ("regular morty cos:" + regularMortyCost);
		}

	}


}
	
