﻿using UnityEngine;
using System.Collections;

public class PosAsObjectC : MonoBehaviour {
	
	public GameObject theObject;
	
	public bool x;
	public bool y;
	public bool z;
	public Vector3 addPos;

	void Update () {
		
		/*if (!theObject) {
			GameObject[] tanks = GameObject.FindGameObjectsWithTag ("Tank");
			foreach (GameObject tank in tanks) {
				if (tank.networkView.isMine) {
					theObject = tank;
				}
			}
			if (!theObject) return;
		}*/
		
		if (theObject) {
		
			Vector3 temporary;
		
			if (x)
				temporary.x = theObject.transform.position.x + addPos.x;
			else 
				temporary.x = addPos.x;
		
			if (y)
				temporary.y = theObject.transform.position.y + addPos.y;
			else 
				temporary.y = addPos.y;
		
			if (z)
				temporary.z = theObject.transform.position.z + addPos.z;
			else 
				temporary.z = addPos.z;
		
			transform.position = temporary;
	
		}
	}
}