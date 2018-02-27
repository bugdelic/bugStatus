using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotnGameManager : MonoBehaviour {

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
    public AudioClip BGM;
    AudioSource audioSource;
    AudioSource BGMSource;

	// Use this for initialization
	void Start () {
		
    audioSource = gameObject.GetComponent<AudioSource>();
    audioSource.clip = audioClip1;


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
            default:
            break;
        }
    }
}
