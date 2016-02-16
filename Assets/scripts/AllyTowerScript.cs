using UnityEngine;
using System.Collections;

public class AllyTowerScript : MonoBehaviour {

	public float maxHealth = 100f;
	public float currentHealth = 0f;
	public GameObject healthBar;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
//		InvokeRepeating ("decreaseHealth", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void decreaseHealth(float healthLost){
		Debug.Log ("decreased HP");
		currentHealth -= healthLost;
		if (currentHealth <= 0) {
			Destroy (gameObject);
		}
		float calculatedHealth = currentHealth / maxHealth;
		setHealthBar (calculatedHealth);
	}

	public void setHealthBar(float myHealth){
		healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}
