using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitManager : MonoBehaviour {

	public GameObject regularMortyPrefab;
    public GameObject flargoPrefab;
	public GameObject frozenMortyPrefab;
	public GameObject karateMortyPrefab;
	public GameObject praxPrefab;
	public Button regMortyBtn;
	public Button frozenMortyBtn;
	public Button karateMortyBtn;

	//made gold a static variable 
	public static int gold; 
	public Text goldText;
	public float SpawnDistance = 50; 

	public int regularMortyCost = 10;
	public int frozenMortyCost = 20;
	public int karateMortyCost = 30;

	double goldGenTime = 0;
    double spawnFlargoTime = 0; 
	double spawnPraxTime = 0; 

	public int currentWave = 1; 
	public Text waveText; 
	public int enemySpawnCount=0;


	// Use this for initialization
	void Start () {
		gold = 30;
		waveText.enabled = false;
	}

	
	// Update is called once per frame
	void Update () {
        goldGenTime += Time.deltaTime;
		generateGold();

		spawnFlargoTime += Time.deltaTime;
		spawnPraxTime += Time.deltaTime;
        spawnEnemy();

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

	private void SpawnKarateMorty(){
		GameObject karateMorty = GameObject.Instantiate (karateMortyPrefab);
		karateMorty.transform.localScale = new Vector3 (-0.5f, 0.5f, 0.5f);
		karateMorty.transform.position = new Vector3 (-7, Random.Range (-3.8f, -4.2f), 0);
		gold -= karateMortyCost;
	}
		
    
    private void spawnEnemy()
    {
	    if (spawnFlargoTime > 3.0f)
	    {
	        spawnFlargoTime -= 3.0f;
	        SpawnFlargo();
	    }
		if (spawnPraxTime > 5.0f) {
			spawnPraxTime -= 5.0f;
			SpawnPrax ();
		}
        else { }
		enemySpawnCount++;
    }

    private void SpawnFlargo(){ 
		GameObject flargo = GameObject.Instantiate(flargoPrefab);
		flargo.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		flargo.transform.position = new Vector3(7, Random.Range(-3.8f, -4.2f), 0);
		Debug.Log("Spawning Object: " + flargo.name);
    }

	private void SpawnPrax(){ 
		GameObject prax = GameObject.Instantiate(praxPrefab);
		prax.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		prax.transform.position = new Vector3(7, Random.Range(-3.8f, -4.2f), 0);
		Debug.Log("Spawning Object: " + prax.name);
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
			StartCoroutine(spawnDelay(regMortyBtn, 1));
		}
    }

	public void frozenMortyClick(){
		Debug.Log ("Spawn Frozen Morty");
		if (gold >= frozenMortyCost){
			SpawnFrozenMorty ();
			StartCoroutine (spawnDelay (frozenMortyBtn, 3));
		}
	}

	public void karateMortyClick(){
		Debug.Log ("Spawn Karate Morty");
		if (gold >= karateMortyCost){
			SpawnKarateMorty ();
			StartCoroutine (spawnDelay (karateMortyBtn, 5));
		}
	}

	IEnumerator spawnDelay(Button btn, int delayTime){
		btn.interactable = false; 
		yield return new WaitForSeconds(delayTime);
		btn.interactable = true;
	}

	public void UpdateGoldAmount() {
		goldText.text = "Gold Amount: " + gold.ToString ();
	}

	public void rewardGold(int goldReward){
		gold += goldReward;
		Debug.Log ("gold rewarded : " + goldReward);
	}

	public void waveComplete(){
		waveText.text = "Wave " + currentWave + " Completed";
		StartCoroutine (displayWaveComplete ());
		currentWave++; 
	}

	IEnumerator displayWaveComplete(){
		waveText.enabled = true;	
		yield return new WaitForSeconds (3);
		waveText.enabled = false; 
	}
		
}
	
