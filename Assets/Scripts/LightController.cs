using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour {

	public GameObject light1;
	public GameObject light2;
	public GameObject stage;
	public bool isLight1;
	public bool isLight2;
	public bool isFever;
	public bool isReverse;

	public Renderer stageRenderer;
	public Slider sliderH;
	public Slider sliderS;
	public Slider sliderB;
	// Use this for initialization
	void Start () {
		
		
		//Debug.Log(dots);
		
	}

    //「コルーチン」で呼び出すメソッド
    IEnumerator initLayout(){

		int dir=1;

			if(isReverse){
			dir=-1;	
			}
		if(isFever){
			light1.transform.Rotate(new Vector3(0, 360*dir, 0) * Time.deltaTime, Space.World);
			light2.transform.Rotate(new Vector3(0, 15*dir, 0) * Time.deltaTime, Space.World);

		}else{

			light1.transform.Rotate(new Vector3(0, 1*dir, 0) * Time.deltaTime, Space.World);
			light2.transform.Rotate(new Vector3(0, 15/360*dir, 0) * Time.deltaTime, Space.World);
		}
    	yield return null;  
			//yield break;
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("initLayout");
		if(isLight1){
			light1.SetActive(true);
		}else{
			light1.SetActive(false);
		}
		if(isLight2){
			light2.SetActive(true);
		}else{
			light2.SetActive(false);
		}

		
		//Debug.Log ("H:"+sliderH.value);
		//Debug.Log ("S:"+sliderS.value);
		//Debug.Log ("B:"+sliderB.value);
		stageRenderer.material.SetColor("_Color", Color.HSVToRGB(sliderH.value, sliderS.value,sliderB.value));

	}
	public void LightActivateL(){
		//Debug.log("HELLO");
		if(isLight1){
			isLight1=false;
		}else{
			isLight1=true;
		}
	}
	public void LightActivateR(){
		//Debug.log("HELLO2");
		if(isLight2){
			isLight2=false;
		}else{
			isLight2=true;
		}
	}
	
	public void LightControlL(){
		
	}
	public void LightControlR(){
		
	}
	public void LightFever(){
		
		if(isFever){
			isFever=false;
		}else{
			isFever=true;
		}
	}
}
