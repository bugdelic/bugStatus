using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Opening : MonoBehaviour {
	public int triggerTime;
	public int frame;
	// Use this for initialization
	void Start () {
		frame=0;
	}
	
	// Update is called once per frame
	void Update () {
		frame++;
		if(frame>triggerTime){
			MoveScene();
		}
	}

    public void MoveScene()
    {
        // 引数にシーン名を指定する
        // Build Settings で確認できる sceneBuildIndex を指定しても良い
        //SceneManager.LoadScene("MainScene"); 
        SceneManager.LoadScene("misaki"); 
    }
}
