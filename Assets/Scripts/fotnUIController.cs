using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fotnUIController : MonoBehaviour {

	public int controlID=0;
	public int unit=300;
	public Transform[] menu;
	public int dt=10;
	public int count;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pos=transform.position;
		float tx=controlID*unit;

		Vector3 targetPos=new Vector3(tx,pos.y,pos.z);

		Vector3 currentPos=new Vector3(pos.x+(tx-pos.x)/10,pos.y,pos.z);
		transform.position=currentPos;
		
	}
	public void changeSceneNext(){
			if(controlID==count){
				controlID=0;
			}else{
				controlID++;
			}
	}
	public void changeSceneBack(){
			if(controlID==0){
				controlID=count;
			}else{
				controlID--;
			}
	}

}
