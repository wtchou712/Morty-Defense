using UnityEngine;
using System.Collections;

public class frozenMortyScript : MonoBehaviour {
	public EnemyTowerScript tempScript;
	public UnitManager unitManagerScript;

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
			Debug.Log ("Ally collision ignored");
		}
		if (collision.gameObject.tag == "Enemy")
		{
			if (collision.collider.gameObject.name.Contains("flargo")) {
				Debug.Log("Morty attacked Flargo");
				Destroy(collision.collider.gameObject);
				Destroy(gameObject);
				unitManagerScript.rewardGold (7);
			}
			if (collision.collider.gameObject.name.Contains("prax"))
			{
				Debug.Log("Morty attacked Prax");
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
}
