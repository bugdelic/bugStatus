using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorelEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
       Destroy(gameObject,3);    //Instantiateしてから５秒後に必ず消滅する
	}
	
	// Update is called once per frame
	void Update () {
	}
}
