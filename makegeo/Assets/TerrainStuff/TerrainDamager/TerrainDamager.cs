﻿/*
	The following license supersedes all notices in the source code.

	Copyright (c) 2019 Kurt Dekker/PLBM Games All rights reserved.

	http://www.twitter.com/kurtdekker

	Redistribution and use in source and binary forms, with or without
	modification, are permitted provided that the following conditions are
	met:

	Redistributions of source code must retain the above copyright notice,
	this list of conditions and the following disclaimer.

	Redistributions in binary form must reproduce the above copyright
	notice, this list of conditions and the following disclaimer in the
	documentation and/or other materials provided with the distribution.

	Neither the name of the Kurt Dekker/PLBM Games nor the names of its
	contributors may be used to endorse or promote products derived from
	this software without specific prior written permission.

	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS
	IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED
	TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
	PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
	HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
	SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
	TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
	PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
	LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
	NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
	SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDamager : MonoBehaviour
{
	void Awake()
	{
		// Since a lot of terrains are zero-based and hence cannot go
		// lower, we offer the ability to lift all the heights up and
		// lower the main transform position.
	}

	IEnumerator TestDeformByKeystroke()
	{
		var terrain = GetComponent<Terrain>();

		var terrainData = Instantiate<TerrainData>( terrain.terrainData);

		terrain.terrainData = terrainData;

		Debug.Log( "size = " + terrainData.size);

		Debug.Log( "Tap any key to apply damage");
		while( !Input.anyKeyDown)
		{
			yield return null;
		}

		float[,] heightmap = terrainData.GetHeights( 0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);

		float adjust = 0.1f;

		for (int i = 0; i < heightmap.GetLength(0); i++)
		{
			for (int j = 0; j < heightmap.GetLength(1); j++)
			{
				heightmap[i,j] += adjust;
			}
		}

		terrainData.SetHeights( 0, 0, heightmap);

		transform.position += Vector3.up * -(adjust * terrainData.size.y);
	}

	void Start()
	{
		StartCoroutine( TestDeformByKeystroke());
	}
}
