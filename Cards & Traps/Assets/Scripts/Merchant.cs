using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Merchant : MonoBehaviour {
	private GameObject currentPlayer;
	public GameObject Assasin,Dwarf,Shield,Swordsman;
	public InputField giveField, takeField,giveNumberField,takeNumberField;
	public Text MerchantInfoText;
	private int worthNumber =0,giveIndex=0,takeIndex=0;
	private int[] worthList = {1,1,1,3,4,5,1},tempListForGiveNumber,tempListForTakeNumber;
	private string[] tempListForGive,tempListForTake,GameCardList= {"antiweb","antidote","antilava","eagles","aragorn","reverseportal","cape"};
	void Start(){
		
		tempListForGive = new string[20];
		tempListForTake = new string[20];
		tempListForGiveNumber = new int[20];
		tempListForTakeNumber = new int[20];

	}
	void Update(){
		if(GameSystem.chars [(int.Parse (GameObject.Find ("GameMechanics").GetComponent<GameMechanics> ().TurnText.text)) % 4] == "Assasin"){
			currentPlayer = Assasin;
		}else if(GameSystem.chars [(int.Parse (GameObject.Find ("GameMechanics").GetComponent<GameMechanics> ().TurnText.text)) % 4] == "Dwarf"){
			currentPlayer = Dwarf;
		}else if(GameSystem.chars [(int.Parse (GameObject.Find ("GameMechanics").GetComponent<GameMechanics> ().TurnText.text)) % 4] == "Swordsman"){
			currentPlayer = Swordsman;
		}else if(GameSystem.chars [(int.Parse (GameObject.Find ("GameMechanics").GetComponent<GameMechanics> ().TurnText.text)) % 4] == "Shield"){
			currentPlayer = Shield;
		}


		Debug.Log (worthNumber);

	}
	public void AddToDeal(){
		if (takeField.text == "") {//Given will be added
			if (checkIfThereIs (giveField.text)) {//If there is such card
				if (checkIfThereIsThatMany (int.Parse (giveNumberField.text), giveField.text.ToLower ())) {//If Player has that much card
					tempListForGiveNumber [giveIndex] = int.Parse (giveNumberField.text);
					tempListForGive [giveIndex] = giveField.text.ToLower ();
					giveIndex++;
					giveField.text = "";
					giveNumberField.text="";
					MerchantInfoText.text = "Added To Give List";
				} else {
					MerchantInfoText.text = "You don't have that much card!";
				}
			} else {
				MerchantInfoText.text = "There Is No Such Card!";
			}
		} else if (giveField.text == "") {//Take will be added
			if (checkIfThereIs (takeField.text.ToLower ())) {//If There is such card
				tempListForTakeNumber [takeIndex] = int.Parse (takeNumberField.text);
				tempListForTake [takeIndex] = takeField.text.ToLower ();
				takeIndex++;
				takeField.text = "";
				takeNumberField.text="";
				MerchantInfoText.text = "Added To Take List";
			}
		}else{
			if (checkIfThereIs (giveField.text)) {//If there is such card
				if (checkIfThereIsThatMany (int.Parse (giveNumberField.text), giveField.text.ToLower ())) {//If Player has that much card
					tempListForGiveNumber [giveIndex] = int.Parse (giveNumberField.text);
					tempListForGive [giveIndex] = giveField.text.ToLower ();
					giveField.text = "";
					giveNumberField.text="";
					MerchantInfoText.text = "Added To Give List";
				} else {
					MerchantInfoText.text = "You don't have that much card!";
				}
			} else {
				MerchantInfoText.text = "There Is No Such Card!";
			}
			if (checkIfThereIs (takeField.text.ToLower ())) {//If There is such card
				tempListForTakeNumber [giveIndex] = int.Parse (takeNumberField.text);
				tempListForTake [giveIndex] = takeField.text.ToLower ();
				takeField.text = "";
				takeNumberField.text="";
				MerchantInfoText.text = "Added To Take List";
			}
		}
	}

	bool checkIfThereIs(string cardName){//Check if there is such card
		for(int i = 0;i!=GameCardList.Length;i++){
			Debug.Log ("Name: " + GameCardList [i].ToLower () + "Card: " + cardName);
			if (GameCardList [i].ToLower () == cardName.ToLower ())
				return true;
		}
		if(cardName.ToLower() =="wait"){
			worthNumber += int.Parse(giveNumberField.text);
			for(int i=0;i!=int.Parse(giveNumberField.text);i++)currentPlayer.GetComponent<Character> ().AddTrap ("Wait");
			return true;
		}
		return false;
	}
	bool checkIfThereIsThatMany(int cardNumber,string cardName){
		int numberCheck =0 ;
		for(int i=0;i!=currentPlayer.GetComponent<Character>().CardIndex;i++){
			Debug.Log (currentPlayer.GetComponent<Character> ().Cards [i].ToLower ());
			if(currentPlayer.GetComponent<Character> ().Cards[i].ToLower() == cardName.ToLower()){
				numberCheck++;
			}
		}
		if (cardName.ToLower () == "wait")
			return true;
		if (numberCheck >= cardNumber)
			return true;
		return false;
	}
	void checkWorthNumbers(){
		for(int i =0;i!=giveIndex;i++){
			for(int j =0;j !=GameCardList.Length;j++){
				if(tempListForGive[i].ToLower() == GameCardList[j].ToLower()){
					worthNumber += tempListForGiveNumber [i] * worthList [j];
				}
			}
		}
		for(int i =0;i!=takeIndex;i++){
			for(int j =0;j !=GameCardList.Length;j++){
				if(tempListForTake[i].ToLower() == GameCardList[j].ToLower()){
					worthNumber -= tempListForTakeNumber [i] * worthList [j];
				}
			}
		}
	}
	void removeCardsFromPlayer(){
		Debug.Log ("Remove");
		for(int i=0;i!=giveIndex;i++){
			for(int j=0;j!=currentPlayer.GetComponent<Character>().CardIndex;j++){
				if(tempListForGive[i].ToLower()==currentPlayer.GetComponent<Character>().Cards[j].ToLower()){
					currentPlayer.GetComponent<Character>().Cards[j] = "null";
					tempListForGiveNumber [i]--;
					if (tempListForGiveNumber [i] > 0)
						i--;
				}
			}
		}
	}
	void addCardsToPlayer(){
		Debug.Log ("Add");
		for(int i=0;i!=takeIndex;i++){
			for(int j=0;j != tempListForTakeNumber[i];j++){
				Debug.Log ("Added: " + tempListForTake [i] + "To : " + currentPlayer.tag);
				if(tempListForTake [i].ToLower()=="antiweb"){
					currentPlayer.GetComponent<Character> ().AddCard ("AntiWeb");
				}else if(tempListForTake [i].ToLower()=="antidote"){
					currentPlayer.GetComponent<Character> ().AddCard ("Antidote");
				}else if(tempListForTake [i].ToLower()=="antilava"){
					currentPlayer.GetComponent<Character> ().AddCard ("AntiLava");
				}else if(tempListForTake [i].ToLower()=="cape"){
					currentPlayer.GetComponent<Character> ().AddCard ("Cape");
				}else if(tempListForTake [i].ToLower()=="aragorn"){
					currentPlayer.GetComponent<Character> ().AddCard ("Aragorn");
				}else if(tempListForTake [i].ToLower()=="eagles"){
					currentPlayer.GetComponent<Character> ().AddCard ("Eagles");
				}else if(tempListForTake [i].ToLower()=="reverseportal"){
					currentPlayer.GetComponent<Character> ().AddCard ("ReversePortal");
				}

			}
		}
	}
	public void resetThings(){
		tempListForGive = new string[20];
		tempListForTake = new string[20];
		tempListForGiveNumber = new int[20];
		tempListForTakeNumber = new int[20];
		worthNumber = 0;
	}
	public void IsDeal(){
		if(worthNumber >= 0){//If it is a fair deal
			MerchantInfoText.text = "I guess it is a deal!";
			removeCardsFromPlayer ();
			addCardsToPlayer ();
			resetThings ();
		}else {
			MerchantInfoText.text = "Sorry pal, but it is not a fair deal";
		}
	}
}
