using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
//using System.Threading;

public class MisakiFotn : MonoBehaviour {
	public GameObject[] dots;
	public string initialData;
	public string editedData;
	public string character;
	public int stage=1;
	public GameObject face0;
	public GameObject face1;
	public GameObject face2;
	public GameObject face3;
	public GameObject face5;
	public FotnGameManager manager;
	public TextMeshPro Text0;
	public TextMeshPro Text1;
	public TextMeshPro Text2;
	public TextMeshPro Text3;

//	Thread thread;

	Object sync = new Object();

	public int count = 0;
	public int life=100;
	public bool isDead=false;


	// Use this for initialization
	void Start () {
		
		//thread = new Thread(ThreadWork);

		//thread.Start();
        //StartCoroutine("misakiAnimation");

}
	/*
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
	 */
    //「コルーチン」で呼び出すメソッド
    IEnumerator misakiAnimation(){

       // StartCoroutine("misakiAnimation");
    	yield return null;  
		//yield return new WaitForSeconds(0.01f);  //10秒待つ


        //StartCoroutine("misakiAnimation");
    }
	// Update is called once per frame
	void Update () {

        float goNextFrameTime = Time.realtimeSinceStartup + 0.01f;

		count++;
		if(count>10000000){
			manager.SoundPlayer(12);
			stage++;
			count=0;
		}
		if(life==0){
			manager.SoundPlayer(14);
			isDead=true;
			// ここで除去を行わないことにします。
			// Destroy(this.gameObject);
		}

		if(initialData!=editedData){
			setTypeFace(editedData);
		}
		//setActiveが遅かったので超小さくするという対応にしています
		face0.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		face1.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		face2.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		face3.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		face5.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
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
			case 5:
				face5.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			break;
			default:
			break;
		}
	}

	public void setTypeFaceTest(){

		this.setTypeFace("0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,EOL");
	}
	public void setTypeFace(string param){

		manager.SoundPlayer(6);
		initialData=param;
		editedData=param;

        StartCoroutine("initLayout");
		
		manager.SoundPlayer(11);
		//Debug.Log(dots);
		
	}

    //「コルーチン」で呼び出すメソッド
    IEnumerator initLayout(){

        float goNextFrameTime = Time.realtimeSinceStartup + 0.01f;
		string[] stArrayData = editedData.Split(',');
		// データを確認する
		int i=0;
		foreach (string stData in stArrayData) {

            // 10msec以上経過したら次フレームへ
            if (Time.realtimeSinceStartup >= goNextFrameTime)
            {
                yield return null;
                goNextFrameTime = Time.realtimeSinceStartup + 0.01f;
            }
			if(i==64){
			yield break;
			}
			if(stData=="1"){
				dots[i].SetActive (true);
			}else{
				dots[i].SetActive(false);
			}
			i++;
		}
    	yield return null;  
			//yield break;
	}
}
