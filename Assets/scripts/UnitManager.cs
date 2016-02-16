using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitManager : MonoBehaviour {

	public GameObject regularMortyPrefab;
    public GameObject flargoPrefab;
	public GameObject frozenMortyPrefab;

	//made gold a static variable 
	public static int gold; 
	public Text goldText;
	public float SpawnDistance = 50; 

	public int regularMortyCost = 10;
	public int frozenMortyCost = 20;

	double goldGenTime = 0;
    double spawnFlargoTime = 0; 


	// Use this for initialization
	void Start () {
		gold = 30;
	}

	
	// Update is called once per frame
	void Update () {
        goldGenTime += Time.deltaTime;
		generateGold();

		spawnFlargoTime += Time.deltaTime;
        spawnEnemy("flargo");

		UpdateGoldAmount ();

	}

	private void SpawnRegularMorty(){
		GameObject regularMorty = GameObject.Instantiate (regularMortyPrefab);
		regularMorty.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
		regularMorty.transform.position = new Vector3(-7, Random.Range(-3.8f, -4.2f), 0);
		gold -= regularMortyCost;
	}

	private void SpawnFrozenMorty(){
		GameObject frozenMorty = GameObject.Instantiate (frozenMortyPrefab);
		frozenMorty.transform.localScale = new Vector3 (-0.5f, 0.5f, 0.5f);
		frozenMorty.transform.position = new Vector3 (-7, Random.Range (-3.8f, -4.2f), 0);
		gold -= frozenMortyCost;
	}
		
    
    private void spawnEnemy(string enemyName)
    {
        if (enemyName == "flargo")
        {
            while (spawnFlargoTime > 4.0f)
            {
                spawnFlargoTime -= 4.0f;
                SpawnFlargo();
            }
        }
        else { }
    }

    private void SpawnFlargo(){ 
		GameObject flargo = GameObject.Instantiate(flargoPrefab);
		flargo.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		flargo.transform.position = new Vector3(7, Random.Range(-3.8f, -4.2f), 0);
		Debug.Log("Spawning Object: " + flargo.name);
    }
		

	private void generateGold() { 
		while (goldGenTime > 1.0f) {
            goldGenTime -= 1.0f; 
			gold += 5; 
//			Debug.Log ("gold amount: " + gold);
		}
	}

    public void regularMortyClick()//button for spawning regular morty
    {
		Debug.Log ("Spawn Regular Morty");
		//if button pressed and gold greater > unit cost, spawn unit 
		if (gold >= regularMortyCost) {
			SpawnRegularMorty ();
		}
    }

	public void frozenMortyClick(){
		Debug.Log ("Spawn Frozen Morty");
		if (gold >= frozenMortyCost){
			SpawnFrozenMorty ();
		}
	}

	public void UpdateGoldAmount() {
		goldText.text = "Gold Amount: " + gold.ToString ();
	}
		
}
	
