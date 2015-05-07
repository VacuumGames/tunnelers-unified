﻿//
//  SFxMan.cs is part of Tunnelers: Unified
//  <https://github.com/VacuumGames/tunnelers-unified/>
//
//  Copyright (c) 2015 Juraj Fiala <doctorjellyface@riseup.net>
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
//  along with Tunnelers: Unified. If not, see <http://www.gnu.org/licenses/>.
//

using UnityEngine;

[RequireComponent (typeof (NetworkView))]
[RequireComponent (typeof (CFxMan))]

[AddComponentMenu ("Network/Fx")]

public class SFxMan: MonoBehaviour, IFxMan {

	public GameObject tankExplosion;

	// The network view instance on our GO
	NetworkView netView;

	void Awake () {

		netView = GetComponent <NetworkView> ();

	}

	public void CreateExplosion (Vector3 pos, Quaternion rot) {

		if (!Network.isServer || Network.isClient) {
			enabled = false;
			return;
		}

		Instantiate (tankExplosion, pos, rot);

		netView.RPC ("CCreateExplosion", RPCMode.All, pos, rot);
	
	}
	
}
