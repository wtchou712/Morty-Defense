using UnityEngine;
using System.Collections;

public class praxScript : MonoBehaviour {
	//public AllyTowerScript tempScript = gameObject.AddComponent<AllyTowerScript>();
	public AllyTowerScript tempScript;
	public int current_health = 100;
	public int damage = 10;
	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision (9, 9);
	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate(new Vector3(-0.3f * Time.deltaTime, 0f, 0f));
	}

	void OnCollisionEnter(Collision collision)
	{
		//When collided with ally tower, ally tower disappears for now 
		if (collision.collider.gameObject.name.Contains("Ally Tower"))
		{
			tempScript = collision.collider.gameObject.GetComponent<AllyTowerScript>();
			tempScript.decreaseHealth(5f);
			this.transform.position += new Vector3 (0.5f, 0f, 0f);
		}
	}
}
