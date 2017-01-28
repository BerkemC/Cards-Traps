using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameMechanics : MonoBehaviour {
	public GameObject Assasin,Shield,Swordsman,Dwarf,LuckOpen,Merchant;
	public Text InfoText,TurnText,DiceText,LogText;
	private bool isPlayed = false,log = false,isDone = false;
	private string cardName,tempCardName;
	private GameObject Players;
	private int TurnNumber = 0,randomNumber,remainingTurn = 0;
	// Use this for initialization
	void Start () {
		////////////////////Removing Unwanted Characters////////////////////
		Players = GameObject.Find ("Players");
		for(int i = 0;i!=4;i++){
			if (GameSystem.players [i] == "null") {
				foreach(Transform character in Players.transform){
					if (character.gameObject.tag == GameSystem.chars [i]) {
						Destroy (character.gameObject);
					}
				}
			}
		}/////////////////////////////////////////////////////////////////////
	}
	// Update is called once per frame
	void Update () {
		if(TurnNumber %40 == 0)LogText.text ="";
		if (GameSystem.players [TurnNumber % 4] == "null") {
			TurnNumber++;
		}
		TurnText.text = TurnNumber.ToString ();
		InfoText.text = (GameSystem.players [((TurnNumber) % 4) ]).ToString () + "'s Turn";
		if(!log) {
			LogText.text += "(Turn: " + TurnNumber.ToString () + ")" + (GameSystem.players [((TurnNumber) % 4)]).ToString () + "'s Turn" + "\n";
			log = true;
		}
			

		GameObject currentPlayer = GameObject.FindGameObjectWithTag (GameSystem.chars [TurnNumber % 4]);
		if(currentPlayer.GetComponent<Character>().movePos >= 90){
			LogText.text += "(Turn: " + TurnNumber.ToString () + ")" + (GameSystem.players [((TurnNumber) % 4)]).ToString () + "Has Won The Game !!!!" + "\n";
			InfoText.text =(GameSystem.players [((TurnNumber) % 4)]).ToString () + "Has Won The Game !!!!";
			TurnText.text = (GameSystem.players [((TurnNumber) % 4)]).ToString () + "Has Won The Game !!!!";
		}
		if(GameObject.Find ("Inventory"))GameObject.Find ("Inventory").GetComponent<Inventory> ().InventoryNumbers (currentPlayer);
	}
	public void RollDice(){
		if(isPlayed== false) {
			isPlayed = true;
			randomNumber = (int)Random.Range (1f, 7f);


			GameObject currentPlayer = GameObject.FindGameObjectWithTag (GameSystem.chars [TurnNumber % 4]);
			currentPlayer.GetComponent<Character> ().checkTraps ();
			if(currentPlayer.GetComponent<Character>().IsWeb()){
				LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" Has Been Caught By Spider Trap: His Dice Will Be Counted As Its Half Value."+"\n";
				currentPlayer.GetComponent<Character> ().movePos += (int)randomNumber/2;
				currentPlayer.GetComponent<Character> ().setIsWeb (false);
			}else if(currentPlayer.GetComponent<Character>().IsPoison()){
				LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" Has Been Poisoned : He Will Not Move This Turn."+"\n";
				currentPlayer.GetComponent<Character> ().setIsPoison (false);
			}else if(currentPlayer.GetComponent<Character>().IsSoldier()){
				LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" Has Been Captured By Soldiers : He Needs To Roll Even Dice To Escape."+"\n";
				if(randomNumber % 2 == 0){
					LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" Has Escaped : He Rolled :"+randomNumber.ToString()+"\n";
					currentPlayer.GetComponent<Character> ().movePos += (int)randomNumber/2;
					currentPlayer.GetComponent<Character> ().setIsLava (false);
				} 
			}else if(currentPlayer.GetComponent<Character>().IsLava()){
				if(currentPlayer.tag == "Swordsman"){
					LogText.text += "(Turn: " + TurnNumber.ToString () + ")" + (GameSystem.players [((TurnNumber) % 4)]).ToString () + " Has Been Trapped By Lava ,But Since He Is A Swordsman, He Will Avoid That Trap With Half Dice Value For This Turn" + "\n";
					currentPlayer.GetComponent<Character> ().movePos += (int)randomNumber/2;
					currentPlayer.GetComponent<Character> ().setIsLava (false);
				}
				else {
					remainingTurn++;
					LogText.text += "(Turn: " + TurnNumber.ToString () + ")" + (GameSystem.players [((TurnNumber) % 4)]).ToString () + " Has Been Trapped For " + (2 - remainingTurn).ToString () + "Turns: He Will Not Move This Turn." + "\n";
					if (remainingTurn == 2) {
						currentPlayer.GetComponent<Character> ().setIsLava (false);
						remainingTurn = 0;
					}
				}
			}
			else{
				currentPlayer.GetComponent<Character> ().movePos += (int)randomNumber;
			}
			LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" has rolled: "+ randomNumber.ToString()+"\n";
			DiceText.text = "You Rolled: " + randomNumber.ToString ();
			if (GameObject.Find (currentPlayer.GetComponent<Character> ().movePos.ToString ()))


				currentPlayer.transform.position =GameObject.Find (currentPlayer.GetComponent<Character> ().movePos.ToString ()).transform.position;


			if (GameObject.Find (currentPlayer.GetComponent<Character> ().movePos.ToString ()).transform.FindChild ("Panel")) {
				if (GameObject.Find (currentPlayer.GetComponent<Character> ().movePos.ToString ()).transform.FindChild ("Panel").transform.FindChild ("Image").GetComponent<Image> ().color == Color.black) {
					if(currentPlayer.GetComponent<Character>().IsLava() == false && currentPlayer.GetComponent<Character>().IsWeb() == false &&currentPlayer.GetComponent<Character>().IsPoison() == false && currentPlayer.GetComponent<Character>().IsSoldier() == false && currentPlayer.GetComponent<Character>().IsWait() == false ) {
						RollChanceStep (currentPlayer);
					}
				}
			}
			if(GameObject.Find ("Inventory").activeSelf)GameObject.Find ("Inventory").GetComponent<Inventory> ().InventoryNumbers (currentPlayer);

		}

	}
	public void ChoicesButton(GameObject panel){
		if(panel.activeSelf== false){
			panel.SetActive (true);
		}else{
			panel.SetActive (false);
		}
	}
	public void RollDiceWithButton(){
		
		if (GameSystem.players [TurnNumber % 4] != "null" && isPlayed== false) {
			isPlayed = true;
			randomNumber = (int)Random.Range (1f, 7f);
			GameObject currentPlayer = GameObject.FindGameObjectWithTag (GameSystem.chars [TurnNumber % 4]);
			currentPlayer.GetComponent<Character> ().checkTraps ();
			if(currentPlayer.GetComponent<Character>().IsWeb()){
				LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" Has Been Caught By Spider Trap: His Dice Will Be Counted As Its Half Value."+"\n";
				currentPlayer.GetComponent<Character> ().movePos += (int)randomNumber/2;
				currentPlayer.GetComponent<Character> ().setIsWeb (false);
			}else if(currentPlayer.GetComponent<Character>().IsPoison()){
				LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" Has Been Poisoned : He Will Not Move This Turn."+"\n";
				currentPlayer.GetComponent<Character> ().setIsPoison (false);
			}else if(currentPlayer.GetComponent<Character>().IsSoldier()){
				LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" Has Been Captured By Soldiers : He Needs To Roll Even Dice To Escape."+"\n";
				if(randomNumber % 2 == 0){
					LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" Has Escaped : He Rolled :"+randomNumber.ToString()+"\n";
					currentPlayer.GetComponent<Character> ().movePos += (int)randomNumber;
					currentPlayer.GetComponent<Character> ().setIsSoldiers (false);
				} 
			}else if(currentPlayer.GetComponent<Character>().IsLava()){
				if(currentPlayer.tag == "Swordsman"){
					LogText.text += "(Turn: " + TurnNumber.ToString () + ")" + (GameSystem.players [((TurnNumber) % 4)]).ToString () + " Has Been Trapped By Lava ,But Since He Is A Swordsman, He Will Avoid That Trap With Half Dice Value For This Turn" + "\n";
					currentPlayer.GetComponent<Character> ().movePos += (int)randomNumber/2;
					currentPlayer.GetComponent<Character> ().setIsLava (false);
				}
				else {
					remainingTurn++;
					LogText.text += "(Turn: " + TurnNumber.ToString () + ")" + (GameSystem.players [((TurnNumber) % 4)]).ToString () + " Has Been Trapped For " + (2 - remainingTurn).ToString () + "Turns: He Will Not Move This Turn." + "\n";
					if (remainingTurn == 2) {
						currentPlayer.GetComponent<Character> ().setIsLava (false);
						remainingTurn = 0;
					}
				}
			}else if (currentPlayer.GetComponent<Character>().IsWait()){
				LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" Has Chosen To Wait For Trade: He Will Not Move This Turn."+"\n";
				currentPlayer.GetComponent<Character> ().setIsWait (false);
			}
			else{
				currentPlayer.GetComponent<Character> ().movePos += (int)randomNumber;
			}
			LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" has rolled: "+ randomNumber.ToString()+"\n";
			DiceText.text = "You Rolled: " + randomNumber.ToString ();
			if(GameObject.Find (currentPlayer.GetComponent<Character> ().movePos.ToString ()))currentPlayer.transform.position = new Vector3 (GameObject.Find (currentPlayer.GetComponent<Character> ().movePos.ToString ()).transform.position.x, GameObject.Find (currentPlayer.GetComponent<Character> ().movePos.ToString ()).transform.position.y, 0f);
			if (GameObject.Find (currentPlayer.GetComponent<Character> ().movePos.ToString ()).transform.FindChild ("Panel")) {
				if (GameObject.Find (currentPlayer.GetComponent<Character> ().movePos.ToString ()).transform.FindChild ("Panel").transform.FindChild ("Image").GetComponent<Image> ().color == Color.black) {
					if(currentPlayer.GetComponent<Character>().IsLava() == false && currentPlayer.GetComponent<Character>().IsWeb() == false &&currentPlayer.GetComponent<Character>().IsPoison() == false && currentPlayer.GetComponent<Character>().IsSoldier() == false && currentPlayer.GetComponent<Character>().IsWait() == false ) {
						RollChanceStep (currentPlayer);
					}
				}
			}
			if(GameObject.Find ("Inventory"))GameObject.Find ("Inventory").GetComponent<Inventory> ().InventoryNumbers (currentPlayer);

		}
	}
	public void OpenInventory(GameObject inventory){
		if(inventory.activeSelf==false){
			inventory.SetActive (true);
		}
		else{
			inventory.SetActive (false);
		}
	}
	void RollChanceStep(GameObject currentPlayer){
		if(Random.value <0.6f){//Roll Trap
			isDone=false;
			LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString ()+" has opened a trap"+"\n";
			int tempTrap = (int)Random.Range (1f, 5f);
			if(tempTrap == 1){
				currentPlayer.GetComponent<Character> ().AddTrap ("Spider");
				tempCardName = "AntiWeb";
				LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString ()+" has opened a Spider Trap"+"\n";
			}else if(tempTrap == 2){
				if(currentPlayer.tag == "Dwarf") LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString ()+" has opened a Poison Trap"+"\n"+"But he avoided it by being a Dwarf \n";
				else{
					currentPlayer.GetComponent<Character> ().AddTrap ("Poison");
					tempCardName = "Antidote";
					LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString ()+" has opened a Poison Trap"+"\n";
				}
			}else if(tempTrap == 3){
				currentPlayer.GetComponent<Character> ().AddTrap ("Lava");
				tempCardName = "AntiLava";
				LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" has opened a Lava Trap"+"\n";
			}else if(tempTrap == 4){
				if(currentPlayer.tag == "Shield")LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString ()+" has opened a Balrog Trap"+"\n"+"But he avoided it by being Shield \n";
				else {
					currentPlayer.GetComponent<Character> ().AddTrap ("Balrog");
					tempCardName = "Eagles";
					LogText.text += "(Turn: " + TurnNumber.ToString () + ")" + (GameSystem.players [((TurnNumber) % 4)]).ToString () + " has opened a Balrog Trap" + "\n";
				}
			}else if(tempTrap == 5){
				if(currentPlayer.tag == "Assasin") LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString ()+" has opened a Soldier Trap"+"\n"+"But he avoided it by being a Assasin \n";
				else {
					currentPlayer.GetComponent<Character> ().AddTrap ("Soldiers");
					tempCardName = "Cape";
					LogText.text += "(Turn: " + TurnNumber.ToString () + ")" + (GameSystem.players [((TurnNumber) % 4)]).ToString () + " has opened a Soldier Trap" + "\n";
				}
			}
			OpenLuckOpen ("Trap", tempTrap,currentPlayer);
			LuckOpen.transform.FindChild ("UseCardButton").GetComponent<Button> ().onClick.AddListener(() => {if(isDone== false)UseCard (tempCardName);});

		}else{//Roll Card
			LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString ()+" has opened a card"+"\n";
			int tempCard = (int)Random.Range (1f, 5f);
			if(tempCard == 1){
				currentPlayer.GetComponent<Character> ().AddCard ("AntiWeb");
				LogText.text +="(Turn: "+TurnNumber.ToString().ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" has opened a AntiWeb Card"+"\n";
			}else if(tempCard == 2){
				currentPlayer.GetComponent<Character> ().AddCard ("Antidote");
				LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString ()+" has opened a Antidote Card"+"\n";
			}else if(tempCard == 3){
				currentPlayer.GetComponent<Character> ().AddCard ("AntiLava");
				LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString ()+" has opened a AntiLava Card"+"\n";
			}else if(tempCard == 4){
				currentPlayer.GetComponent<Character> ().AddCard ("Cape");
				LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () +" has opened a Cape Card"+"\n";
			}
			OpenLuckOpen ("Card", tempCard,currentPlayer);
		}
	}
	public void EndTurn(){
		TurnNumber++;
		LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber-1) % 4) ]).ToString () +" has ended his turn"+"\n";
		isPlayed = false;
		log = false;
	}
	public void openGameLog(GameObject Log){
		if(Log.activeSelf==false){
			Log.SetActive (true);
		}
		else{
			Log.SetActive (false);
		}
	}
	void OpenLuckOpen(string luckType,int givenType,GameObject currentPlayer){
		LuckOpen.gameObject.SetActive (true);
		LuckOpen.GetComponent<LuckOpen>().ChangePictureAccordingly (luckType, givenType,currentPlayer);
	}
	public void MoveOn(){
		LuckOpen.SetActive (false);
	}
	void UseCard(string cardType){
		if(isDone == false) {
			GameObject currentPlayer = GameObject.FindGameObjectWithTag (GameSystem.chars [TurnNumber % 4]);
			LogText.text += "(Turn: " + TurnNumber.ToString () + ")" + (GameSystem.players [((TurnNumber) % 4)]).ToString () + " has used his" + cardName + " card and removed effects" + "\n";
			currentPlayer.GetComponent<Character> ().RemoveTrapLast ();
			currentPlayer.GetComponent<Character> ().RemoveGivenCard (cardType);
			LuckOpen.SetActive (false);
			isDone = true;
		}


	}
	public void CloseMerchant(GameObject mer){
		mer.gameObject.SetActive (false);
	}

	public void UseReversePortal(){
		GameObject currentPlayer = GameObject.FindGameObjectWithTag (GameSystem.chars [TurnNumber % 4]);
		int movePos = currentPlayer.GetComponent<Character>().movePos;
		if(movePos==3){
			LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () + "Has Entered To Revese-Teleport\n";
			gameObject.transform.position = GameObject.Find ("9").transform.position;
			movePos = 9;
		}
		else if(movePos==8){
			LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () + "Has Entered To Revese-Teleport\n";
			gameObject.transform.position = GameObject.Find ("28").transform.position;
			movePos = 28;
		}
		else if(movePos==32){
			LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () + "Has Entered To Revese-Teleport\n";
			gameObject.transform.position = GameObject.Find ("40").transform.position;
			movePos = 40;
		}
		else if(movePos==47){
			LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () + "Has Entered To Revese-Teleport\n";
			gameObject.transform.position = GameObject.Find ("52").transform.position;
			movePos = 52;
		}
		else if(movePos==26){
			LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () + "Has Entered To Revese-Teleport\n";
			gameObject.transform.position = GameObject.Find ("64").transform.position;
			movePos = 64;
		}
		else if(movePos==38){
			LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () + "Has Entered To Revese-Teleport\n";
			gameObject.transform.position = GameObject.Find ("73").transform.position;
			movePos = 73;
		}
		else if(movePos==53){
			LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () + "Has Entered To Revese-Teleport\n";
			gameObject.transform.position = GameObject.Find ("79").transform.position;
			movePos = 79;
		}
		else if(movePos==49){
			LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () + "Has Entered To Revese-Teleport\n";
			gameObject.transform.position = GameObject.Find ("83").transform.position;
			movePos = 83;
		}
		GameObject.FindGameObjectWithTag (GameSystem.chars [TurnNumber % 4]).GetComponent<Character> ().RemoveGivenCard ("ReversePortal");
	}
	public void UseEagles(){
		int randomNumber = (int)Random.Range (1f, 7f);
		LogText.text +="(Turn: "+TurnNumber.ToString()+")"+(GameSystem.players [((TurnNumber) % 4) ]).ToString () + "Has Used Eagles Card And Moved "+randomNumber.ToString()+" Steps Forward\n";
		GameObject.FindGameObjectWithTag (GameSystem.chars [TurnNumber % 4]).GetComponent<Character> ().movePos += randomNumber;
	}

}
