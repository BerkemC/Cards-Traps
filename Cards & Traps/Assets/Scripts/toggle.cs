using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class toggle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.tag =="Swordsman"){
			if(gameObject.GetComponent<Toggle>().isOn) {
				GameObject.Find ("CharacterInfoText").GetComponent<Text> ().text = "Swordsman: Thanks to his military and survival knowledge, this character can avoid areas that is blocked by Lava. He can use half of the dice when he gets to area with lava and moves on normally after that turn.";
			}
		}else if(gameObject.tag =="Shield"){
			if (gameObject.GetComponent<Toggle> ().isOn) {
				GameObject.Find ("CharacterInfoText").GetComponent<Text> ().text = "Shield: With the help of his magical shield, this character can block Balrog's push.Therefore, he cannot be pushed backwards.";
			}
		}else if(gameObject.tag =="Assasin"){
			if (gameObject.GetComponent<Toggle> ().isOn) {
				GameObject.Find ("CharacterInfoText").GetComponent<Text> ().text = "Assasin: He can avoid soldiers with his talent to move in stealth.Therefore, he cannot be captured by the soldiers.";
			}
		}else if(gameObject.tag =="Dwarf"){
			if (gameObject.GetComponent<Toggle> ().isOn) {		
				GameObject.Find ("CharacterInfoText").GetComponent<Text> ().text = "Dwarf: He is immune to poisons thanks to a special potion that can be created by dwarfs.Therefore, he cannot be poisoned by posion blades.";
			}
		}
	}
}
