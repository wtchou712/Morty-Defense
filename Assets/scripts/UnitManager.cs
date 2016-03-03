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
	public GameObject flashPrefab;
	public GameObject goobPrefab;

	public bool unlockedFrozenMorty = false;
	public bool unlockedKarateMorty = false;
	public bool unlockedShadowMorty = false;

	public bool regMortyCooldown = false; 
	public bool frozenMortyCooldown = false; 
	public bool karateMortyCooldown = false; 
	public bool shadowMortyCooldown = false;

	public Button regMortyButton;
	public Button frozenMortyButton;
	public Button karateMortyButton;
	public Button shadowMortyButton;

	public mortyButton regMortyBtn;
	public mortyButton frozenMortyBtn;
	public mortyButton karateMortyBtn;
	public mortyButton shadowMortyBtn;

	public GameObject regMortyLabel;
	public GameObject frozenMortyLabel;
	public GameObject karateMortyLabel;
	public GameObject shadowMortyLabel;

	public string frozenMortyText; 
	public string karateMortyText;
	public string shadowMortyText;

	//made gold a static variable 
	public static int gold; 
	public Text goldText;
	public float SpawnDistance = 50; 

	public int regularMortyCost = 10;
	public int frozenMortyCost = 20;
	public int karateMortyCost = 30;
	public int shadowMortyCost = 50;


	double goldGenTime = 0;

	public int currentWave = 1; 
	public Text waveText; 
	public int enemySpawnCount = 0;
	public int enemyDeathCount = 0;


	// Use this for initialization
	void Start () {

		regMortyBtn = new mortyButton(regMortyButton);
		frozenMortyBtn = new mortyButton (frozenMortyButton);
		karateMortyBtn = new mortyButton (karateMortyButton);
		shadowMortyBtn = new mortyButton (shadowMortyButton);

		gold = 30;
		waveText.enabled = false;
		StartCoroutine(spawnWave());

		//set the unlock message
		frozenMortyText = frozenMortyLabel.GetComponent<Text> ().text;
		frozenMortyLabel.GetComponent<Text>().text = "Unlock with " + 2*frozenMortyCost + "G\nFrozen Morty (2)\nSlow units for 2 seconds \nStrong against Flargo";

		karateMortyText = karateMortyLabel.GetComponent<Text> ().text;
		karateMortyLabel.GetComponent<Text>().text = "Unlock with " + 2*karateMortyCost + "G\nKarate Morty (3)\nHigh damage\nSpeed ";

		shadowMortyText = shadowMortyLabel.GetComponent<Text> ().text;
		shadowMortyLabel.GetComponent<Text>().text = "Unlock with " + 2*shadowMortyCost + "G\nShadow Morty (4)\nAvoid enemy units\nStrong against all ";
	}


	// Update is called once per frame
	void Update () {
        goldGenTime += Time.deltaTime;
		generateGold();
		UpdateGoldAmount();
		//checkUnlockButtons ();
		checkKeyPressed();
		checkButtonEnabled ();

	}

	public class mortyButton{
		public Button btn;
		public bool cooldown = false;
		public bool unlocked = false; 

		public mortyButton(Button mortyBtn){
			btn = mortyBtn;
		}
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
		
	private void SpawnGoob() {
		GameObject goob = GameObject.Instantiate(goobPrefab);
		goob.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		goob.transform.position = new Vector3(7, Random.Range (-3.8f, -4.2f), 0);
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
		if (!unlockedFrozenMorty) { //unlock frozen morty
			if (gold >= 2 * frozenMortyCost) {
				unlockedFrozenMorty = true; 
				gold -= 2 * frozenMortyCost;
				frozenMortyBtn.btn.interactable = true;
				frozenMortyLabel.GetComponent<Text> ().text = frozenMortyText;
			}
		}
		else if (gold >= frozenMortyCost){
			SpawnFrozenMorty ();
			StartCoroutine (spawnDelay (frozenMortyBtn, 3));
		}
	}

	public void karateMortyClick(){
		if (!unlockedKarateMorty) { //unlock frozen morty
			if (gold >= 2 * karateMortyCost) {
				unlockedKarateMorty = true; 
				gold -= 2 * karateMortyCost;
				karateMortyBtn.btn.interactable = true;
				karateMortyLabel.GetComponent<Text> ().text = karateMortyText;
			}
		}
		else if (gold >= karateMortyCost){
			SpawnKarateMorty ();
			StartCoroutine (spawnDelay (karateMortyBtn, 5));
		}
	}

	public void shadowMortyClick(){
		if (!unlockedShadowMorty) { //unlock frozen morty
			if (gold >= 2 * shadowMortyCost) {
				unlockedShadowMorty = true; 
				gold -= 2 * shadowMortyCost;
				shadowMortyBtn.btn.interactable = true;
				shadowMortyLabel.GetComponent<Text> ().text = shadowMortyText;
			}
		}
		else if (gold >= shadowMortyCost){
			SpawnShadowMorty ();
			StartCoroutine (spawnDelay (shadowMortyBtn, 7));
		}
	}

	IEnumerator spawnDelay(mortyButton btn, int delayTime){
		setButton(btn, false);
		yield return new WaitForSeconds(delayTime);
		setButton (btn, true);
	}

	public void setButton(mortyButton button, bool state){
		button.btn.interactable = state;
		button.cooldown = !state; 
	}

	public void UpdateGoldAmount() {
		goldText.text = "Gold Amount: " + gold.ToString ();
	}

	public void rewardGold(int goldReward){
		gold += goldReward;
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
		int clusterCount = 0;
		int unitID = Random.Range(1, currentWave + 1);
		while (enemySpawnCount < currentWave * 9) {
			if (clusterCount == 3) {
				clusterCount = 0;
				unitID = Random.Range(1, currentWave + 1);
				yield return new WaitForSeconds(6);
			}
			if (unitID == 1) {
				SpawnFlargo ();
			} else if (unitID == 2) {
				SpawnPrax ();
			} else if (unitID == 3) {
				SpawnMermaid ();
			} else if (unitID == 4) {
				SpawnGoob ();
			}

			yield return new WaitForSeconds(2);
			clusterCount++;
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

	public void checkKeyPressed() {
		if(Input.GetButtonDown("SpawnRegularMorty") && regMortyBtn.btn.interactable == true){
			regularMortyClick();
		}
		if(Input.GetButtonDown("SpawnFrozenMorty") && (frozenMortyBtn.btn.interactable == true || !unlockedFrozenMorty)){
			frozenMortyClick();
		}
		if(Input.GetButtonDown("SpawnKarateMorty") && (karateMortyBtn.btn.interactable == true || !unlockedKarateMorty)){
			karateMortyClick();
		}
		if(Input.GetButtonDown("SpawnShadowMorty") && (shadowMortyBtn.btn.interactable == true || !unlockedShadowMorty)){
			shadowMortyClick();
		}
	}

	public void checkButtonEnabled(){
		if (gold < regularMortyCost || regMortyBtn.cooldown) {
			regMortyBtn.btn.interactable = false; 
		} 
		else {
			regMortyBtn.btn.interactable = true;
		}
		if (gold < frozenMortyCost || !unlockedFrozenMorty || frozenMortyBtn.cooldown) {
			frozenMortyBtn.btn.interactable = false; 
		} else {
			frozenMortyBtn.btn.interactable = true;
		}
		if (gold < karateMortyCost || !unlockedKarateMorty || karateMortyBtn.cooldown) {
			karateMortyBtn.btn.interactable = false; 
		} else {
			karateMortyBtn.btn.interactable = true;
		}
		if (gold < shadowMortyCost || !unlockedShadowMorty || shadowMortyBtn.cooldown) {
			shadowMortyBtn.btn.interactable = false; 
		} else {
			shadowMortyBtn.btn.interactable = true;
		}
	}

	public void displayFlash(Vector3 allyPosition, Vector3 enemyPosition) {
		Vector3 flashPosition = (allyPosition + enemyPosition) / 2;
		StartCoroutine (playFlash (flashPosition));

	}
	IEnumerator playFlash(Vector3 flashPosition){
		GameObject flash = GameObject.Instantiate(flashPrefab);
		flash.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		flash.transform.position = flashPosition;
		yield return new WaitForSeconds (0.333f);
		Destroy (flash);
	}
		


}
	
