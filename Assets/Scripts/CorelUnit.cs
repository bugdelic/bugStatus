using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorelUnit : MonoBehaviour {

	public FotnGameManager manager;
	public Vector3 scannedPosition;
	public Transform particle;
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
    void OnTriggerEnter(Collider other) {
        //Destroy(this.gameObject);
		//Debug.Log("HELLO");
		manager.CorelHit();

            Vector3 position=new Vector3(0,0,0);
            Transform effect = Instantiate(particle, position, this.transform.rotation);
			effect.parent=this.transform;

        	effect.localPosition = position;
			//effect.localPositon=position;
    }
}
