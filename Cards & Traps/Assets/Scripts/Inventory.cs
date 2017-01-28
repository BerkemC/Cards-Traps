using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {
	private GameObject turns;
	private int turnNumber;
	public Text[] cardTexts;
	public Button[] useButtons;
	private int[] AcardNumbers,ShcardNumbers,DcardNumbers,SwcardNumbers;
	// Use this for initialization
	void Start () {
		turns = GameObject.Find ("GameMechanics");
		turnNumber = int.Parse (turns.GetComponent<GameMechanics> ().TurnText.text.ToString());
		AcardNumbers = new int[7];
		ShcardNumbers = new int[7];
		DcardNumbers = new int[7];
		SwcardNumbers = new int[7];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void InventoryNumbers(GameObject currentCharacter){
		AcardNumbers = new int[7];
		ShcardNumbers = new int[7];
		DcardNumbers = new int[7];
		SwcardNumbers = new int[7];
		for(int i =0;i!= currentCharacter.GetComponent<Character> ().Cards.Length;i++){
			if(currentCharacter.GetComponent<Character>().Cards[i]=="AntiWeb"){
				if(currentCharacter.tag == "Assasin")
					AcardNumbers [0]++;
				else if(currentCharacter.tag == "Shield")
					ShcardNumbers [0]++;
				else if(currentCharacter.tag == "Dwarf")
					DcardNumbers [0]++;
				else if(currentCharacter.tag == "Swordsman")
					SwcardNumbers [0]++;
			}else if(currentCharacter.GetComponent<Character>().Cards[i]=="Antidote"){
				if(currentCharacter.tag == "Assasin")
					AcardNumbers [1]++;
				else if(currentCharacter.tag == "Shield")
					ShcardNumbers [1]++;
				else if(currentCharacter.tag == "Dwarf")
					DcardNumbers [1]++;
				else if(currentCharacter.tag == "Swordsman")
					SwcardNumbers [1]++;
			}else if(currentCharacter.GetComponent<Character>().Cards[i]=="Eagles"){
				if(currentCharacter.tag == "Assasin")
					AcardNumbers [2]++;
				else if(currentCharacter.tag == "Shield")
					ShcardNumbers [2]++;
				else if(currentCharacter.tag == "Dwarf")
					DcardNumbers [2]++;
				else if(currentCharacter.tag == "Swordsman")
					SwcardNumbers [2]++;
			}else if(currentCharacter.GetComponent<Character>().Cards[i]=="Aragorn"){
				if(currentCharacter.tag == "Assasin")
					AcardNumbers [3]++;
				else if(currentCharacter.tag == "Shield")
					ShcardNumbers [3]++;
				else if(currentCharacter.tag == "Dwarf")
					DcardNumbers [3]++;
				else if(currentCharacter.tag == "Swordsman")
					SwcardNumbers [3]++;
			}else if(currentCharacter.GetComponent<Character>().Cards[i]=="ReversePortal"){
				if(currentCharacter.tag == "Assasin")
					AcardNumbers [4]++;
				else if(currentCharacter.tag == "Shield")
					ShcardNumbers [4]++;
				else if(currentCharacter.tag == "Dwarf")
					DcardNumbers [4]++;
				else if(currentCharacter.tag == "Swordsman")
					SwcardNumbers [4]++;
			}else if(currentCharacter.GetComponent<Character>().Cards[i]=="AntiLava"){
				if(currentCharacter.tag == "Assasin")
					AcardNumbers [5]++;
				else if(currentCharacter.tag == "Shield")
					ShcardNumbers [5]++;
				else if(currentCharacter.tag == "Dwarf")
					DcardNumbers [5]++;
				else if(currentCharacter.tag == "Swordsman")
					SwcardNumbers [5]++;
			}else if(currentCharacter.GetComponent<Character>().Cards[i]=="Cape"){
				if(currentCharacter.tag == "Assasin")
					AcardNumbers [6]++;
				else if(currentCharacter.tag == "Shield")
					ShcardNumbers [6]++;
				else if(currentCharacter.tag == "Dwarf")
					DcardNumbers [6]++;
				else if(currentCharacter.tag == "Swordsman")
					SwcardNumbers [6]++;
			}
		}
		if(currentCharacter.tag == "Assasin")
			ShowCardNumbers (AcardNumbers);
		else if(currentCharacter.tag == "Shield")
			ShowCardNumbers (ShcardNumbers);
		else if(currentCharacter.tag == "Dwarf")
			ShowCardNumbers (DcardNumbers);
		else if(currentCharacter.tag == "Swordsman")
			ShowCardNumbers (SwcardNumbers);

	}
	public void ShowCardNumbers(int[] cardNumbers){
		for(int i = 0;i!= cardNumbers.Length;i++){
			if (cardNumbers [i] == 0)
				useButtons [i].gameObject.SetActive (false);
			else useButtons [i].gameObject.SetActive (true);
		}
		cardTexts [0].text = "AntiWeb: " + cardNumbers [0].ToString ();
		cardTexts [1].text = "Antidote: " + cardNumbers [1].ToString ();
		cardTexts [2].text = "Eagles: " + cardNumbers [2].ToString ();
		cardTexts [3].text = "Aragorn: " + cardNumbers [3].ToString ();
		cardTexts [4].text = "ReversePortal: " + cardNumbers [4].ToString ();
		cardTexts [5].text = "AntiLava: " + cardNumbers [5].ToString ();
		cardTexts [6].text = "Cape: " + cardNumbers [6].ToString ();
	}
}
