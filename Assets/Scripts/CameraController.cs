﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour {


	

	public Transform playerTransform;
	public int depth = -20;

	void Start(){
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerTransform != null)
		{
			transform.position = playerTransform.position + new Vector3(0,0,depth);
		}
	}
	public void setTarget(Transform target)
	{
		playerTransform = target;
	}
}
