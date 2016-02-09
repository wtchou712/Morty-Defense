using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitManager : MonoBehaviour {

	private Vector3 _AllySpawnPoint = new Vector3 (-5, -0.95f, 0);
    private Vector3 _EnemySpawnPoint = new Vector3(5, -0.915f, 0);
	public GameObject regularMortyPrefab;
    public GameObject flargoPrefab;

	//made gold a static variable 
	public static int gold; 
	public Text goldText;
	public float SpawnDistance = 50; 

	public int regularMortyCost = 10;

	double goldGenTime = 0;
    double spawnFlargoTime = 0; 


	// Use this for initialization
	void Start () {
		gold = 30;
		goldText = GetComponent<Text> ();
		UpdateGoldAmount ();
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
		var regularMorty = MakeRegularMorty ();
		regularMorty.transform.position = _AllySpawnPoint;
		gold -= regularMortyCost;
	}

	private GameObject MakeRegularMorty() {
		GameObject obj = GameObject.Instantiate (regularMortyPrefab);
		obj.transform.localScale = new Vector3(-1, 1, 1);
		return obj;
	}
    
    private void spawnEnemy(string enemyName)
    {
        if (enemyName == "flargo")
        {
            while (spawnFlargoTime > 2.0f)
            {
                spawnFlargoTime -= 2.0f;
                SpawnFlargo();
            }
        }
        else { }
    }

    private void SpawnFlargo(){ 
        var flargo = MakeFlargo();
        flargo.transform.position = _EnemySpawnPoint;

    }

    private GameObject MakeFlargo()
    {
        GameObject obj = GameObject.Instantiate(flargoPrefab);
        obj.transform.localScale = new Vector3(1, 1, 1);
        Debug.Log("Spawning Object: " + obj.name);
        return obj;
    }

	private void generateGold() { 
		while (goldGenTime > 1.0f) {
            goldGenTime -= 1.0f; 
			gold += 5; 
			Debug.Log ("gold amount: " + gold);
		}
	}

    public void onClick()
    {
		//if button pressed and gold greater > unit cost, spawn unit 
		if (gold >= regularMortyCost) {
			SpawnRegularMorty ();
		}
    }

	public void UpdateGoldAmount() {
		goldText.text = "Gold Amount: " + gold.ToString ();
	}
		
}
	
