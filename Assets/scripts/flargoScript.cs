﻿using UnityEngine;
using System.Collections;

public class flargoScript : MonoBehaviour {
	public AllyTowerScript tempScript;
	public int current_health = 20;
	public int damage = 5;

	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision (9, 9);

	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(new Vector3(-0.2f * Time.deltaTime, 0f, 0f));
    }

	void OnCollisionEnter(Collision collision)
	{
		//When collided with ally tower, ally tower disappears for now 
		if (collision.collider.gameObject.name.Contains("Ally Tower"))
		{
			//Debug.Log ("collision here");
			tempScript = collision.collider.gameObject.GetComponent<AllyTowerScript>();
			tempScript.decreaseHealth(5f);
			this.transform.position += new Vector3 (0.5f, 0f, 0f);
		}
	}
}
