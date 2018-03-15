using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotnGameManager : MonoBehaviour {

    public LightController light;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    public AudioClip audioClip4;
    public AudioClip audioClip5;
    public AudioClip audioClip6;
    public AudioClip audioClip7;
    public AudioClip audioClip8;
    public AudioClip audioClip9;
    public AudioClip audioClip10;

    public AudioClip audioClip11;
    public AudioClip audioClip12;
    public AudioClip audioClip13;
    public AudioClip audioClip14;
    public AudioClip audioClip15;
    public AudioClip BGM;
    AudioSource audioSource;
    AudioSource BGMSource;

    public bool isReverse;
    public float rotationSpeed;
    public Transform stage;

	// Use this for initialization
	void Start () {
		
    audioSource = gameObject.GetComponent<AudioSource>();
    audioSource.clip = audioClip1;


        StartCoroutine("stageRotator");

	}
	
	// Update is called once per frame
    void Update () {
        
        if ( Input.GetKeyDown(KeyCode.A) == true ) {
            Debug.Log( "A!" );// Torigger
            SoundPlayer(1);
        }else  if ( Input.GetKeyDown(KeyCode.Alpha1) == true ) {
            SoundPlayer(1);
        }else  if ( Input.GetKeyDown(KeyCode.Alpha2) == true ) {
            SoundPlayer(2);
        }else  if ( Input.GetKeyDown(KeyCode.Alpha3) == true ) {
            SoundPlayer(3);
        }else  if ( Input.GetKeyDown(KeyCode.Alpha4) == true ) {
            SoundPlayer(4);
        }else  if ( Input.GetKeyDown(KeyCode.Alpha5) == true ) {
            SoundPlayer(5);
        }else  if ( Input.GetKeyDown(KeyCode.Alpha6) == true ) {
            SoundPlayer(6);
        }else  if ( Input.GetKeyDown(KeyCode.Alpha7) == true ) {
            SoundPlayer(7);
        }else  if ( Input.GetKeyDown(KeyCode.Alpha8) == true ) {
            SoundPlayer(8);
        }else  if ( Input.GetKeyDown(KeyCode.Alpha9) == true ) {
            SoundPlayer(9);
        }

    }

    //「コルーチン」で呼び出すメソッド
    IEnumerator stageRotator(){
        int dir=1;
        if(isReverse){
            dir=-1;
        } 
			stage.Rotate(new Vector3(0, rotationSpeed*dir, 0) * Time.deltaTime, Space.World);

				yield return new WaitForSeconds(0.01f);  //10秒待つ
        //Debug.Log("ROTATION"+rotationSpeed);
                //this.stageRotator();
		//Debug.Log(dots);

        StartCoroutine("stageRotator");
    }
    public void CorelHit(){
        this.SoundPlayer(7);
    }
    public void MaybeHit(){
        this.SoundPlayer(5);

    }


    public void MaybeStart(){
        this.SoundPlayer(2);
        light.LightFever();
    }
    public void feverEnd(){
        light.isFever=false;
    }
    public void SoundPlayer(int SoundID){
        switch (SoundID)
        {
            case 1:
                audioSource.PlayOneShot( audioClip1 );
            break;
            case 2:
                audioSource.PlayOneShot( audioClip2 );
            break;
            case 3:
                audioSource.PlayOneShot( audioClip3 );
            break;
            case 4:
                audioSource.PlayOneShot( audioClip4 );
            break;
            case 5:
                audioSource.PlayOneShot( audioClip5 );
            break;
            case 6:
                audioSource.PlayOneShot( audioClip6 );
            break;
            case 7:
                audioSource.PlayOneShot( audioClip7 );
            break;
            case 8:
                audioSource.PlayOneShot( audioClip8 );
            break;
            case 9:
                audioSource.PlayOneShot( audioClip9 );
            break;
            case 10:
               // audioSource.PlayOneShot( audioClip9 );
            break;
            case 11:
                audioSource.PlayOneShot( audioClip11 );
            break;
            case 12:
                audioSource.PlayOneShot( audioClip12 );
            break;
            case 13:
                audioSource.PlayOneShot( audioClip13 );
            break;
            case 14:
                audioSource.PlayOneShot( audioClip14 );
            break;
            case 15:
                audioSource.PlayOneShot( audioClip15 );
            break;
            default:
            break;
        }
    }
}
