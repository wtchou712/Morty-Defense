using UnityEngine;
using System.Collections;

public class flargoScript : MonoBehaviour {

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
			Debug.Log("Flargo destroyed ally tower");
			Destroy(collision.collider.gameObject);
			Destroy(gameObject);
		}
	}
}
