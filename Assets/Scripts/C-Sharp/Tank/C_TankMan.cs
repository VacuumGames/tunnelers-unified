﻿//
//  C_TankMan.cs is part of Tunnelers: Unified
//  <https://github.com/VacuumGames/Tunnelers-Unified/>.
//
//  Copyright (c) 2014 Juraj Fiala<doctorjellyface@riseup.net>
//
//  Tunnelers: Unified is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Tunnelers: Unified is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Tunnelers: Unified.  If not, see <http://www.gnu.org/licenses/>.
//

using UnityEngine;

[AddComponentMenu ("Client/Tank Man")]

public class C_TankMan : MonoBehaviour {

    public PlayerMan parent;
    
    public int lastMotionH;
    public int lastMotionV;
    
    public float speed = 10f;
    
    public M_TankController controller;
           
    public float positionErrorThreshold = 0.2f;
	public Vector3 serverPos;
	public Quaternion serverRot;
    
    void Awake () {
    	if (!Network.isClient || Network.isServer) {
    		enabled = false;
    		return;
    	}
    }
    
    void Update () {
    
		if (Network.isServer || (!Network.isServer && !Network.isClient)) {
			enabled = false;
	        return;
	    }

	    if (parent.GetOwner () != null && Network.player == parent.GetOwner ()) {
		//	Debug.Log ("I am the owner.");
			bool a = Input.GetKey (KeyCode.A);
			bool d = Input.GetKey (KeyCode.D);
			bool s = Input.GetKey (KeyCode.S);
			bool w = Input.GetKey (KeyCode.W);
			
			lastMotionH = M_TankController.GetHorizontalAxis (a,d);
			lastMotionV = M_TankController.GetVerticalAxis (s, w);
	        
	        networkView.RPC ("UpdateClientMotion", RPCMode.Server, lastMotionH, lastMotionV);
	        //Simulate how we think the motion should come out	        
			controller.Move (lastMotionH, lastMotionV);
	    }
	    
	    LerpToTarget ();
	}
	
	public void LerpToTarget () {
	
	//	Debug.Log ("Lerping.");
	
		LerpPos ();
		LerpRot ();
	
	}
	
	void LerpPos () {
	
		/*
		float distance = Vector3.Distance (transform.position, serverPos);
	
		float timing = distance / speed;
		float rate = 1f / timing;
		float t = 0f;
		t += Time.deltaTime * rate;
	    	
	//	float lerp = ((1f / distance) * speed * Time.deltaTime) / 100f;
	    transform.position = Vector3.Lerp (transform.position, serverPos, t);
	    */
	    
	    transform.position = Vector3.Lerp (transform.position, serverPos, Time.deltaTime * speed);
	}
	
	void LerpRot () {
		
		/*
		float distance = Quaternion.Angle (transform.rotation, serverRot);
	
		float timing = distance / (speed * 1000f);
		float rate = 1f / timing;
		float t = 0f;
		t += Time.deltaTime * rate;
		
		transform.rotation = Quaternion.Slerp (transform.rotation, serverRot, t);
		*/
		
		transform.rotation = Quaternion.Slerp (transform.rotation, serverRot, Time.deltaTime);
		
	}

}
