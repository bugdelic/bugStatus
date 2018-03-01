using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class MisakiFotn : MonoBehaviour {
	public GameObject[] dots;
	public string initialData;
	public string editedData;
	public int stage=1;
	public GameObject face0;
	public GameObject face1;
	public GameObject face2;
	public GameObject face3;
	public FotnGameManager manager;

	Thread thread;

	Object sync = new Object();

	public int count = 0;
	public int life=100;
	public bool isDead=false;


	// Use this for initialization
	void Start () {
		
		thread = new Thread(ThreadWork);

		thread.Start();
        StartCoroutine("misakiAnimation");

}
	
	void OnApplicationQuit() {

		if( thread != null)

			thread.Abort(); 

  	}

	
	void ThreadWork(){

		while(true){

			Thread.Sleep(0);

			lock(sync){

				count += 1;

			}

		}

	}
    //「コルーチン」で呼び出すメソッド
    IEnumerator misakiAnimation(){
		if(initialData!=editedData){
			setTypeFace(editedData);
		}
		//setActiveが遅かったので超小さくするという対応にしています
		face0.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		face1.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		face2.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		face3.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		switch(stage){
			case 0:
				face0.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			break;
			case 1:
				face1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			break;
			case 2:
				face2.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			break;
			case 3:
				face3.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			break;
			default:
			break;
		}
		yield return new WaitForSeconds(0.01f);  //10秒待つ
		count++;
		if(count>10000000){
			stage++;
			count=0;
		}
		if(life==0){
			isDead=true;
			Destroy(this.gameObject);
		}


        StartCoroutine("misakiAnimation");
    }
	// Update is called once per frame
	void Update () {
	}
	public void setTypeFaceTest(){
		this.setTypeFace("0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,EOL");
	}
	public void setTypeFace(string param){

		manager.SoundPlayer(6);
		initialData=param;
		editedData=param;

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
		
		//Debug.Log(dots);
		
	}
}
