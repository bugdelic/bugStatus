using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisakiFotn : MonoBehaviour {
	public GameObject[] dots;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setTypeFaceTest(){
		this.setTypeFace("0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,EOL");
	}
	public void setTypeFace(string param){
		string[] stArrayData = param.Split(',');
		// データを確認する
		int i=0;
		foreach (string stData in stArrayData) {
			if(i==64){
				return;
			}
			if(stData=="1"){
				dots[i].SetActive (true);
			}else{
				dots[i].SetActive(false);
			}
			i++;
		}
		
		Debug.Log(dots);
		
	}
}
