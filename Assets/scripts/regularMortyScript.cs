using UnityEngine;
using System.Collections;

public class regularMortyScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
//		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (new Vector3 (5f * Time.deltaTime, 0f, 0f));
	}
}
