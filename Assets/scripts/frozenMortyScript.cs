using UnityEngine;
using System.Collections;

public class frozenMortyScript : MonoBehaviour {
	public EnemyTowerScript tempScript;
	public UnitManager unitManagerScript;
	public flargoScript flargoObj;
	public praxScript praxObj;
	public mermaidScript mermaidObj;

	public int current_health = 40;
	public int damage = 5;

	public Vector3 movementSpeed = new Vector3 (0.4f * Time.deltaTime, 0f, 0f);

	// Use this for initialization
	void Start () {
		unitManagerScript = Camera.main.GetComponent<UnitManager>();
		Physics.IgnoreLayerCollision (8, 8);
	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate (new Vector3 (0.4f * Time.deltaTime, 0f, 0f));
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			if (collision.collider.gameObject.name.Contains("flargo")) {
				flargoObj = collision.collider.gameObject.GetComponent<flargoScript>();
				flargoObj.current_health -= damage * 2; //frozen morty is better against flargo
				current_health -= flargoObj.damage;

				unitManagerScript.displayFlash (flargoObj.transform.position, this.transform.position);
				flargoObj.transform.position += new Vector3 (0.5f, 0f, 0f);
				this.transform.position += new Vector3 (-0.5f, 0f, 0f);

				collision.collider.gameObject.SendMessage ("slowMovementSpeed");

				Dead ();
				if (flargoObj.current_health <= 0) {
					Destroy(collision.collider.gameObject);
					unitManagerScript.enemyUnitKilled();
					unitManagerScript.rewardGold (10);
				}
			}
			if (collision.collider.gameObject.name.Contains("prax"))
			{
				praxObj = collision.collider.gameObject.GetComponent<praxScript>();
				praxObj.current_health -= damage;
				current_health -= praxObj.damage;

				unitManagerScript.displayFlash (praxObj.transform.position, this.transform.position);
				praxObj.transform.position += new Vector3 (0.5f, 0f, 0f);
				this.transform.position += new Vector3 (-0.5f, 0f, 0f);

				Dead ();
				if (praxObj.current_health <= 0) {
					Destroy(collision.collider.gameObject);
					unitManagerScript.enemyUnitKilled();
					unitManagerScript.rewardGold (15);
				}
			}
			if (collision.collider.gameObject.name.Contains("mermaid"))
			{
				mermaidObj = collision.collider.gameObject.GetComponent<mermaidScript>();
				mermaidObj.current_health -= damage * 2; //karate is better against prax enemy unity
				current_health -= mermaidObj.damage;

				unitManagerScript.displayFlash (mermaidObj.transform.position, this.transform.position);
				mermaidObj.transform.position += new Vector3 (0.5f, 0f, 0f);
				this.transform.position += new Vector3 (-0.5f, 0f, 0f);

				Dead();
				if (mermaidObj.current_health <= 0) {
					Destroy(collision.collider.gameObject);
					unitManagerScript.enemyUnitKilled();
					unitManagerScript.rewardGold (15);
				}

			}
		}
		//When collided with enemy tower, enemy tower disappears for now 
		if (collision.collider.gameObject.name.Contains("Enemy Tower"))
		{
			tempScript = collision.collider.gameObject.GetComponent<EnemyTowerScript>();
			tempScript.decreaseHealth(5f);
			////debug.Log("Attacked enemy tower!");
			this.transform.position += new Vector3 (-0.5f, 0f, 0f);
		}
	}
		

	void Dead() {
		if (current_health <= 0) {
			Destroy (gameObject);
		}
	}
		
}
