using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorelUnit : MonoBehaviour {

	public Vector3 scannedPosition;
	// Use this for initialization
	void Start () {
	
        StartCoroutine("corelAnimation");	
	}
	
    //「コルーチン」で呼び出すメソッド
    IEnumerator corelAnimation(){
		yield return new WaitForSeconds(0.01f);  //10秒待つ

        StartCoroutine("corelAnimation");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
