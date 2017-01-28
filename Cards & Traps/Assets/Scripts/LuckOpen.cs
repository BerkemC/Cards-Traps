using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LuckOpen : MonoBehaviour {
	public Sprite[] trapImages, cardImages;
	public Text LogText;
	private bool isPushed = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}
	public void ChangePictureAccordingly(string luckType,int givenType,GameObject currentPlayer){
		string[] cardList = currentPlayer.GetComponent<Character> ().Cards;
		if(luckType == "Trap"){
			if(givenType == 1){//Spider
				transform.FindChild("Panel").transform.FindChild("Image").GetComponent<Image>().sprite= trapImages [1];
				for(int i = 0;i!=cardList.Length;i++){
					if(cardList[i]=="AntiWeb"){
						transform.FindChild ("UseCardButton").gameObject.SetActive (true);
						break;
					}else transform.FindChild ("UseCardButton").gameObject.SetActive (false);
				}
			}else if(givenType == 2){//Posion
				transform.FindChild("Panel").transform.FindChild("Image").GetComponent<Image>().sprite = trapImages [0];
				for(int i = 0;i!=cardList.Length;i++){
					if(cardList[i]=="Antidote"){
						transform.FindChild ("UseCardButton").gameObject.SetActive (true);
						break;
					}else transform.FindChild ("UseCardButton").gameObject.SetActive (false);
				}
			}else if(givenType == 3){//Lava
				transform.FindChild("Panel").transform.FindChild("Image").GetComponent<Image>().sprite = trapImages [3];
				for(int i = 0;i!=cardList.Length;i++){
					if(cardList[i]=="AntiLava"){
						transform.FindChild ("UseCardButton").gameObject.SetActive (true);
						break;
					}else transform.FindChild ("UseCardButton").gameObject.SetActive (false);
				}
			}else if(givenType == 4){//Balrog
				transform.FindChild("Panel").transform.FindChild("Image").GetComponent<Image>().sprite = trapImages [4];
				for(int i = 0;i!=cardList.Length;i++){
					if(cardList[i]=="Eagles"){
						transform.FindChild ("UseCardButton").gameObject.SetActive (true);
						break;
					}else {
						if(currentPlayer.tag != "Shield") {
							isPushed = false;
							transform.FindChild ("UseCardButton").gameObject.SetActive (false);
							transform.FindChild ("SufferButton").GetComponent<Button> ().onClick.AddListener (delegate {
								if (isPushed == false) {
									int randomNumber = (int)Random.Range (1f, 7f);
									if (currentPlayer.GetComponent<Character> ().movePos - randomNumber >= 0)
										currentPlayer.GetComponent<Character> ().movePos -= randomNumber;
									else
										currentPlayer.GetComponent<Character> ().movePos = 0;
									currentPlayer.transform.position = GameObject.Find (currentPlayer.GetComponent<Character> ().movePos.ToString ()).transform.position;
									LogText.text += "(" + currentPlayer.tag.ToString () + " has been pushed " + randomNumber.ToString () + " steps by Balrog\n";
									isPushed = true;
								}
							});
						}
					}
				}
			}
			else if(givenType == 5){//Soldiers
				transform.FindChild("Panel").transform.FindChild("Image").GetComponent<Image>().sprite = trapImages [2];
				for(int i = 0;i!=cardList.Length;i++){
					if(cardList[i]=="Cape"){
						transform.FindChild ("UseCardButton").gameObject.SetActive (true);
						break;
					}else transform.FindChild ("UseCardButton").gameObject.SetActive (false);
				}
			}
			transform.FindChild("SufferButton").GetComponentInChildren<Text>().text = "Suffer!";
		}else{
			if(givenType ==2){//Antidote
				transform.FindChild("Panel").transform.FindChild("Image").GetComponent<Image>().sprite = cardImages [2];
			}else if(givenType == 1){//Antiweb
				transform.FindChild("Panel").transform.FindChild("Image").GetComponent<Image>().sprite = cardImages [1];
			}else if(givenType == 3){//Antilava
				transform.FindChild("Panel").transform.FindChild("Image").GetComponent<Image>().sprite = cardImages [5];
			}else if(givenType == 4){//Cape
				transform.FindChild("Panel").transform.FindChild("Image").GetComponent<Image>().sprite = cardImages [3];
			}
			transform.FindChild("SufferButton").GetComponentInChildren<Text>().text = "Take Card";
			transform.FindChild ("UseCardButton").gameObject.SetActive (false);
		}
	}
}
