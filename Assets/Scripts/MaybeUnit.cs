using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaybeUnit : MonoBehaviour {

	public FotnGameManager manager;
	public int timer=100;
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
			manager.feverEnd();
			Destroy(this.gameObject);
		}

        StartCoroutine("maybeAnimation");
    }
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        //Destroy(this.gameObject);
		//Debug.Log("HELLO");
		manager.MaybeHit();
		Boid hinge = other.transform.GetComponent<Boid>();
		if(hinge){

		hinge.isMaybe=true;
		hinge.targetMaybe=this.transform;
		}

			//effect.localPositon=position;
    }
}
