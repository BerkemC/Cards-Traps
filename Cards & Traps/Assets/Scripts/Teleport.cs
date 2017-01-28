using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D col){
		print ("Trigger");
		if(col.gameObject.tag == "Assasin" ||col.gameObject.tag == "Shield"||col.gameObject.tag == "Swordsman"||col.gameObject.tag == "Dwarf"){
			col.gameObject.transform.position = gameObject.transform.FindChild ("Arrive").transform.position;
		}
	}
	void OnCollisionEnter2D(Collision2D col){
		print ("Collision");
		if(col.gameObject.tag == "Assasin" ||col.gameObject.tag == "Shield"||col.gameObject.tag == "Swordsman"||col.gameObject.tag == "Dwarf"){
			col.gameObject.transform.position = gameObject.transform.FindChild ("Arrive").transform.position;
		}
	}
}
