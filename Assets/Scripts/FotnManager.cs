﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotnManager : MonoBehaviour {

	public MisakiFotn[] charactor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setTypeFaceTest(){
		charactor[0].setTypeFace("0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,EOL");
		charactor[1].setTypeFace("0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,0,1,0,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,EOL");
		charactor[2].setTypeFace("1,0,0,0,0,0,0,0,1,0,0,1,1,1,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,1,0,1,0,0,0,0,0,1,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,EOL");
		charactor[3].setTypeFace("0,0,0,0,1,0,0,0,0,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,1,1,1,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,EOL");
		charactor[4].setTypeFace("1,0,0,0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,1,1,1,1,1,0,1,0,0,0,0,1,0,0,1,0,0,1,1,1,0,0,1,0,1,0,0,1,0,0,1,0,0,1,1,0,1,0,0,0,0,0,0,0,0,0,EOL");
	}
}
