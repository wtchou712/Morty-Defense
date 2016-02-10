using UnityEngine;
using System.Collections;

public class regularMortyScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(GetComponent<Collider>(), GetComponent<Collider>());
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (new Vector3 (1f * Time.deltaTime, 0f, 0f));
	}

    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.gameObject.name.Contains("flargo"))
        {
            Debug.Log("Morty attacked Flargo");
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }

		//When collided with enemy tower, enemy tower disappears for now 
		if (collision.collider.gameObject.name.Contains("Enemy Tower"))
		{
			Debug.Log("Destroyed enemy tower!");
			Destroy(collision.collider.gameObject);
			Destroy(gameObject);
		}
    }
    
}
