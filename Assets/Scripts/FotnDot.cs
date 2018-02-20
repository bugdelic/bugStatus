using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotnDot : MonoBehaviour {

	Vector3 position0;
	int multipy=10;
	// Use this for initialization
	void Start () {
		position0=this.transform.localPosition;
		Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f)*multipy, 0, Random.Range(-10.0f, 10.0f)*multipy);
		this.transform.localPosition=position;
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 positionDelta=position0-this.transform.localPosition;

		Vector3 positionNext=new Vector3(
			(this.transform.localPosition.x+(position0.x- this.transform.localPosition.x)/10),
			(this.transform.localPosition.y+(position0.y- this.transform.localPosition.y)/10),
			(this.transform.localPosition.z+(position0.z- this.transform.localPosition.z)/10)
			);

		//Vector3 positionNext=new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
		this.gameObject.transform.localPosition=positionNext;

	}
}
