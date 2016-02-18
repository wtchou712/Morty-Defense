using UnityEngine;
using System.Collections;

public class frozenMortyScript : MonoBehaviour {
	public EnemyTowerScript tempScript;
	public UnitManager unitManagerScript;
	public flargoScript flargoObj;

	public int current_health = 40;
	public int damage = 10;

	// Use this for initialization
	void Start () {
		unitManagerScript = Camera.main.GetComponent<UnitManager>();
	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate (new Vector3 (1f * Time.deltaTime, 0f, 0f));
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Ally") {
			Physics.IgnoreCollision (GetComponent<Collider>(), collision.collider);
		}
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
					Debug.Log ("flargoObj dead");
					Destroy(collision.collider.gameObject);
				}
				unitManagerScript.rewardGold (7);
			}
			if (collision.collider.gameObject.name.Contains("prax"))
			{
				Destroy(collision.collider.gameObject);
				Destroy(gameObject);
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
