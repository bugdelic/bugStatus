using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaybeUnit : MonoBehaviour {
	public int timer=1000;
	public Vector3 scannedPosition;
	// Use this for initialization
	void Start () {
		
        StartCoroutine("maybeAnimation");
	}
	
    //「コルーチン」で呼び出すメソッド
    IEnumerator maybeAnimation(){
		yield return new WaitForSeconds(0.01f);  //10秒待つ
		timer--;
		if(timer==0){
			
			Destroy(this.gameObject);
		}

        StartCoroutine("maybeAnimation");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
