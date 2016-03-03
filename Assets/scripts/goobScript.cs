using UnityEngine;
using System.Collections;

public class goobScript : MonoBehaviour {
	public AllyTowerScript tempScript;
	public int current_health = 80;
	public int damage = 40;

	public float speedFactor = -0.7f;

	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision (9, 9);
	}

	// Update is called once per frame
	void Update () {
		moveUnit (Time.deltaTime);
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

	public void moveUnit(float time){
		this.transform.Translate(new Vector3 (speedFactor * time, 0f, 0f));
	}

	public void slowMovementSpeed(){
		StartCoroutine (halfSpeed ());
	}

	IEnumerator halfSpeed(){
		float prevFactor = speedFactor;
		//speedFactor = prevFactor / 2;
		speedFactor = 0;
		yield return new WaitForSeconds (2f);
		speedFactor = prevFactor;

	}
}
