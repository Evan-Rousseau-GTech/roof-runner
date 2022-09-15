using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCS : MonoBehaviour {

	public Transform Player ;
	public  float CloudsSpeed;




	
	// Update is called once per frame
	void Update () {
		if (!Player)
			return;



		gameObject.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);

		transform.Rotate(0,Time.deltaTime*CloudsSpeed ,0); 
	}




}
