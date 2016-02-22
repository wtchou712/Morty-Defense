using UnityEngine;
using System.Collections;

public class praxScript : MonoBehaviour {
	//public AllyTowerScript tempScript = gameObject.AddComponent<AllyTowerScript>();
	public AllyTowerScript tempScript;
	public int current_health = 80;
	public int damage = 20;
	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision (9, 9);
	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate(new Vector3(-0.5f * Time.deltaTime, 0f, 0f));
	}

	void OnCollisionEnter(Collision collision)
	{
		//When collided with ally tower, ally tower disappears for now 
		if (collision.collider.gameObject.name.Contains("Ally Tower"))
		{
			//Debug.Log ("collision here");
			tempScript = collision.collider.gameObject.GetComponent<AllyTowerScript>();
			tempScript.decreaseHealth(5f);
			//Debug.Log("Flargo destroyed ally tower");
			Destroy(gameObject);
		}
	}
}
