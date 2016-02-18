using UnityEngine;
using System.Collections;

public class flargoScript : MonoBehaviour {
	//public AllyTowerScript tempScript = gameObject.AddComponent<AllyTowerScript>();
	public AllyTowerScript tempScript;

	public int current_health = 30;
	public int damage = 10;
	// Use this for initialization
	void Start () {

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
