using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Character : MonoBehaviour {
	public int movePos=0,CardIndex = 0,TrapIndex = 0;
	public string[] Cards;
	public string[] Traps;
	private int TurnNumber;
	private bool isWeb = false,isPoison = false,isSoldiers = false,isLava = false,isWait = false;
	public GameObject merchant;
	public Text LogText;
	// Use this for initialization
	void Start(){
		Cards = new string[100];
		Traps = new string[100];
		//TurnNumber = int.Parse (GameObject.Find ("GameMechanics").GetComponent<GameMechanics> ().TurnText.text);
	}
	void Update(){
		if(movePos==9){
			LogText.text +="Player Has Entered To Teleport\n";
			gameObject.transform.position = GameObject.Find ("3").transform.position;
			movePos = 3;
		}
		if(movePos==28){
			LogText.text +="Player Has Entered To Teleport\n";
			gameObject.transform.position = GameObject.Find ("8").transform.position;
			movePos = 8;
		}
		if(movePos==40){
			LogText.text +="Player Has Entered To Teleport\n";
			gameObject.transform.position = GameObject.Find ("32").transform.position;
			movePos = 32;
		}
		if(movePos==52){
			LogText.text +="Player Has Entered To Teleport\n";
			gameObject.transform.position = GameObject.Find ("47").transform.position;
			movePos = 47;
		}
		if(movePos==64){
			LogText.text +="Player Has Entered To Teleport\n";
			gameObject.transform.position = GameObject.Find ("26").transform.position;
			movePos = 26;
		}
		if(movePos==73){
			LogText.text +="Player Has Entered To Teleport\n";
			gameObject.transform.position = GameObject.Find ("38").transform.position;
			movePos = 38;
		}
		if(movePos==79){
			LogText.text +="Player Has Entered To Teleport\n";
			gameObject.transform.position = GameObject.Find ("53").transform.position;
			movePos = 53;
		}
		if(movePos==83){
			LogText.text +="Player Has Entered To Teleport\n";
			gameObject.transform.position = GameObject.Find ("49").transform.position;
			movePos = 49;
		}
		if(movePos == 16 ||movePos == 31 ||movePos == 44 ||movePos == 54 ||movePos == 66 ||movePos == 81 ){
			merchant.gameObject.SetActive(true);
		}
	}

	public void AddCard(string cardType){
		Cards [CardIndex] = cardType;
		if(CardIndex < 100)CardIndex++;
	}
	public void AddTrap(string trapType){
		Traps [TrapIndex] = trapType;
		if(TrapIndex<100)TrapIndex++;
	}
	public void RemoveTrapLast(){
		if(TrapIndex > 0)TrapIndex--;
		Traps[TrapIndex]=""; 
	}
	public void RemoveGivenTrap(string Trap){
		for(int i= 0;i!=TrapIndex;i++){
			if(Traps[i] ==Trap ){
				Traps [i] = "null";
				break;
			}
		}
	}
	public void RemoveGivenCard(string card){
		for(int i= 0;i!=CardIndex;i++){
			if(Cards[i] ==card ){
				Cards [i] = "null";
				break;
			}
		}
	}
	public void checkTraps(){
		for(int i=0;i!= TrapIndex;i++){
			if (Traps [i] == "Spider") {
				isWeb = true;
				Traps[i] = "null";
				break;
			}else if(Traps[i]== "Poison"){
				isPoison = true;
				Traps[i] = "null";
				break;
			}else if(Traps[i]== "Soldiers"){
				isSoldiers = true;
				Traps[i] = "null";
				break;
			}else if(Traps[i] == "Lava"){
				isLava = true;
				Traps[i] ="null";
				break;
			}else if(Traps[i]=="Wait"){
				isWait = true;
				Traps [i] = "null";
				break;
			}
			
		}
	}
	public bool IsWait(){
		return isWait;
	}
	public bool IsWeb(){
		return isWeb;
	}
	public bool IsPoison(){
		return isPoison;
	}
	public bool IsSoldier(){
		return isSoldiers;
	}
	public void setIsWeb(bool state){
		isWeb = state;
	}
	public void setIsPoison(bool state){
		isPoison = state;
	}
	public void setIsSoldiers(bool state){
		isSoldiers = state;
	}
	public bool IsLava(){
		return isLava;
	}
	public void setIsLava(bool state){
		isLava= state;
	}
	public void setIsWait(bool state){
		isWait = state;
	}

}
