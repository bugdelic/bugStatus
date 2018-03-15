using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscSimpl.Examples;

public class CorelUnit : MonoBehaviour {
	public OSCBundles osc;
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

//		Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Fotn")
        {
			
				int x=UnityEngine.Random.Range(0, 10);
				if(x==0){
					osc.fotnHybrid("0");
				}else{
					//	Debug.Log("HELLO");
     		 	 	 Boid b = other.transform.GetComponent<Boid>();
					osc.fotnTalker(b.fotnCode);
				}



        }
    }
}
