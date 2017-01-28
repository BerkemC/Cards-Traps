using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameSystem : MonoBehaviour {
	public static string[] players;
	public static string[] chars;
	private string[] names;
	private string[] characters;
	private int turn = 0,randomNumber;
	public Text infoText,turnText;
	private int nameInputCount = 0;
	public GameObject[] buttons;
	public Transform characterOptions;
	public GameObject random,startButton;
	// Use this for initialization
	void Start () {
		players = new string[4];
		chars = new string[4];
		names = new string[4];
		characters = new string[4];
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevel==1 && nameInputCount == 4){
			characterOptions.gameObject.SetActive (false);
			random.SetActive (true);
		}
	}
	public void NameEntry(InputField field){
		names [nameInputCount]= field.text;
		foreach(Transform avatar in characterOptions){
			if(avatar.gameObject.GetComponent<Toggle>() && avatar.gameObject.activeSelf == true){
				if(avatar.gameObject.GetComponent<Toggle>().isOn){
					characters [nameInputCount] = avatar.gameObject.tag.ToString();
					avatar.gameObject.SetActive (false);
					break;
				}
			}
		}
		foreach(Transform avatar in characterOptions){
			if(avatar.gameObject.GetComponent<Toggle>() && avatar.gameObject.activeSelf == true){
				avatar.gameObject.GetComponent<Toggle> ().isOn = true;
				break;
			}
		}
		buttons [nameInputCount].SetActive (false);
		nameInputCount++;
		if(nameInputCount <4)buttons [nameInputCount].SetActive (true);

	}
	public void randomizeButton(){
		int[] temp = new int[4];
		for(int i=0;i!= 4;i++){
			randomNumber = (int)Random.Range (0f, (4f-i));
			GameSystem.players [i] = names [randomNumber];
			GameSystem.chars [i] = characters [randomNumber];
			names [randomNumber] = names [3 - i];
			characters [randomNumber] = characters [3 - i];
			random.transform.FindChild ("RandomText").GetComponent<Text> ().text += (i + 1).ToString() +"-"+ players [i]+"\n";
			random.transform.FindChild ("Button").gameObject.SetActive (false);
			startButton.gameObject.SetActive (true);
		}
			
	}
		
}
