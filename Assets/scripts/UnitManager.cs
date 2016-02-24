using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitManager : MonoBehaviour {

	public GameObject regularMortyPrefab;
    public GameObject flargoPrefab;
	public GameObject frozenMortyPrefab;
	public GameObject karateMortyPrefab;
	public GameObject praxPrefab;
	public GameObject mermaidPrefab;
	public GameObject shadowPrefab;

	public Button regMortyBtn;
	public Button frozenMortyBtn;
	public Button karateMortyBtn;
	public Button shadowMortyBtn;

	//made gold a static variable 
	public static int gold; 
	public Text goldText;
	public float SpawnDistance = 50; 

	public int regularMortyCost = 10;
	public int frozenMortyCost = 20;
	public int karateMortyCost = 30;
	public int shadowMortyCost = 50;

	double goldGenTime = 0;
    double spawnFlargoTime = 0; 
	double spawnPraxTime = 0; 
	double spawnMermaidTime = 0;
	double spawnShadowTime = 0;

	public int currentWave = 1; 
	public Text waveText; 
	public int enemySpawnCount = 0;
	public int enemyDeathCount = 0;


	// Use this for initialization
	void Start () {
		gold = 30;
		waveText.enabled = false;
		StartCoroutine(spawnWave());
	}

	
	// Update is called once per frame
	void Update () {
        goldGenTime += Time.deltaTime;
		generateGold();

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
	private void SpawnShadowMorty(){
		GameObject shadowMorty = GameObject.Instantiate (shadowPrefab);
		shadowMorty.transform.localScale = new Vector3 (-0.5f, 0.5f, 0.5f);
		shadowMorty.transform.position = new Vector3 (-7, Random.Range (-3.8f, -4.2f), 0);
		gold -= shadowMortyCost;
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
		if (spawnMermaidTime > 7.0f) {
			spawnMermaidTime -= 7.0f;
			SpawnMermaid();
		}
        else { }
		enemySpawnCount++;
    }

    private void SpawnFlargo(){ 
		GameObject flargo = GameObject.Instantiate(flargoPrefab);
		flargo.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		flargo.transform.position = new Vector3(7, Random.Range(-3.8f, -4.2f), 0);
    }

	private void SpawnPrax(){ 
		GameObject prax = GameObject.Instantiate(praxPrefab);
		prax.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		prax.transform.position = new Vector3(7, Random.Range(-3.8f, -4.2f), 0);
	}
	private void SpawnMermaid() {
		GameObject mermaid = GameObject.Instantiate(mermaidPrefab);
		mermaid.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		mermaid.transform.position = new Vector3(7, Random.Range (-3.8f, -4.2f), 0);
	}
		

	private void generateGold() { 
		while (goldGenTime > 1.0f) {
            goldGenTime -= 1.0f; 
			gold += 5; 
		}
	}

    public void regularMortyClick()//button for spawning regular morty
    {
		//if button pressed and gold greater > unit cost, spawn unit 
		if (gold >= regularMortyCost) {
			SpawnRegularMorty ();
			StartCoroutine(spawnDelay(regMortyBtn, 1));
		}
    }

	public void frozenMortyClick(){
		if (gold >= frozenMortyCost){
			SpawnFrozenMorty ();
			StartCoroutine (spawnDelay (frozenMortyBtn, 3));
		}
	}

	public void karateMortyClick(){
		if (gold >= karateMortyCost){
			SpawnKarateMorty ();
			StartCoroutine (spawnDelay (karateMortyBtn, 5));
		}
	}

	public void shadowMortyClick(){
		if (gold >= shadowMortyCost){
			SpawnShadowMorty ();
			StartCoroutine (spawnDelay (shadowMortyBtn, 7));
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

	public void enemyUnitKilled(){
		enemyDeathCount++;
		if (enemyDeathCount == enemySpawnCount) {
			waveComplete();
		}
	}

	public void waveComplete(){
		enemyDeathCount = 0; 
		enemySpawnCount = 0; 
		waveText.text = "Wave " + currentWave + " Completed";
		StartCoroutine (displayWaveComplete ());
		currentWave++;
		StartCoroutine(spawnWave());
	}

	IEnumerator displayWaveComplete(){
		waveText.enabled = true;	
		yield return new WaitForSeconds (3);
		waveText.enabled = false; 
	}

	IEnumerator spawnWave(){
		//enemyUnits = currentWave * 10
		//random(1, currentWave) for which enemy to spawn
		for (int i = 0; i < currentWave * 10; i++) {
			int unitID = Random.Range (1, currentWave + 1); 
			if (unitID == 1) {
				SpawnFlargo ();
			} else if (unitID == 2) {
				SpawnPrax ();
			} else if (unitID == 3) {
				SpawnMermaid ();
			}
			//add more enemy unit IDs here
			yield return new WaitForSeconds(2);
			enemySpawnCount++;
		}
	}

	public void gameOver(bool win){
		if (win) {
			waveText.text = "Player wins!";
		} 
		else {
			waveText.text = "Game over!"; 
		}
		waveText.enabled = true;
	}
		
}
	
