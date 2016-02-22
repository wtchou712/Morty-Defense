using UnityEngine;
using System.Collections;

public class regularMortyScript : MonoBehaviour {
	public EnemyTowerScript tempScript;
	public UnitManager unitManagerScript;
	//intiailize enemy scripts to get health and damage
	public flargoScript flargoObj;
	public praxScript praxObj;

	public int current_health = 20;
	public int damage = 10;
	// Use this for initialization
	void Start () {
		unitManagerScript = Camera.main.GetComponent<UnitManager>();
		Debug.Log ("Layer: " + gameObject.layer);
		Physics.IgnoreLayerCollision (8, 8);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (new Vector3 (1f * Time.deltaTime, 0f, 0f));
	}



    void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.tag == "Enemy")
		{
			if (collision.collider.gameObject.name.Contains("flargo")) {
				flargoObj = collision.collider.gameObject.GetComponent<flargoScript>();
				flargoObj.current_health -= damage;
				current_health -= flargoObj.damage;
				flargoObj.transform.position += new Vector3 (0.5f, 0f, 0f);
				this.transform.position += new Vector3 (-0.5f, 0f, 0f);
				Dead ();

				if (flargoObj.current_health <= 0) {
					Destroy(collision.collider.gameObject);
				}
				unitManagerScript.rewardGold (7);
			}
			if (collision.collider.gameObject.name.Contains("prax"))
			{
				praxObj = collision.collider.gameObject.GetComponent<praxScript>();
				praxObj.current_health -= damage;
				current_health -= praxObj.damage;
				praxObj.transform.position += new Vector3 (0.5f, 0f, 0f);
				this.transform.position += new Vector3 (-0.5f, 0f, 0f);
				unitManagerScript.rewardGold (12);
			}
		}
		//When collided with enemy tower, enemy tower disappears for now 
		if (collision.collider.gameObject.name.Contains("Enemy Tower"))
		{
			tempScript = collision.collider.gameObject.GetComponent<EnemyTowerScript>();
			tempScript.decreaseHealth(5f);
			Debug.Log("Attacked enemy tower!");
			Destroy(gameObject);
		}
			
    }
	void Dead() {
		if (current_health <= 0) {
			Destroy (gameObject);
		}
	}
    
}
