﻿/*
	Created by Carl Emil Carlsen.
	Copyright 2016 Sixth Sensor.
	All rights reserved.
	http://sixthsensor.dk
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace OscSimpl.Examples
{
	public class OSCBundles : MonoBehaviour
	{

        public int stageScale =900;
        public int localCount =0;
		public GameObject uiWrapper;
		public Transform fotnParent;
		public Transform corelParent;
		public Transform maybeParent;

		public Text demoText;


		public Text sendLabel1;
		public Text sendLabel2;
		public Text receiveLabel1;
		public Text receiveLabel2;
		public Text receiveLabel3;
		public Text receiveLabel4;
		public Text receiveLabel5;


		public Text receiveLabel11;
		public Text receiveLabel12;
		public Text receiveLabel13;
		public Text receiveLabel14;
		public Text receiveLabel15;

		public Text receiveLabel21;
		public Text receiveLabel22;
		public Text receiveLabel23;
		public Text receiveLabel24;
		public Text receiveLabel25;

		OscOut oscOut;
		OscIn oscIn;

		public MisakiFotn fotnBase;
		public MaybeUnit maybeBase;
		public CorelUnit corelBase;

		public int maybeCount;
		public int corelCount;


		public Transform cameraTarget;
		public TargetCameraController scopeTarget; 

		public string address1 = "/fotn/start";
		public string address2 = "/fotn/end";
		public string address3 = "/fotn/jis";
		public string address4 = "/fotn/utf";
		public string address5 = "/fotn/matrix";


		public string address11 = "/coral/start";
		public string address12 = "/coral/end";
		public string address13 = "/coral/x";
		public string address14 = "/coral/z";
		public string address15 = "/coral/charactor";


		public string address21 = "/maybe/start";
		public string address22 = "/maybe/end";
		public string address23 = "/maybe/x";
		public string address24 = "/maybe/y";
		public string address25 = "/maybe/charactor";

		public FotnGameManager manager;


		void Start()
		{
			// Create objects for sending and receiving
			oscOut = gameObject.AddComponent<OscOut>();
			oscIn = gameObject.AddComponent<OscIn>(); 

			// Prepare for sending messages to applications on this device on port 7000.
			oscOut.Open( 7000 );

			// Prepare for receiving messages on port 7000.
			oscIn.Open( 7000 );

			// Forward recived messages with addresses to methods.
			oscIn.Map( address1, OnMessage1Received );
			oscIn.Map( address2, OnMessage2Received );
			oscIn.Map( address3, OnMessage3Received );
			oscIn.Map( address4, OnMessage4Received );
			oscIn.Map( address5, OnMessage5Received );
			oscIn.Map( address11, OnMessage11Received );
			oscIn.Map( address12, OnMessage12Received );
			oscIn.Map( address13, OnMessage13Received );
			oscIn.Map( address14, OnMessage14Received );
			oscIn.Map( address15, OnMessage15Received );
			oscIn.Map( address21, OnMessage21Received );
			oscIn.Map( address22, OnMessage22Received );
			oscIn.Map( address23, OnMessage23Received );
			oscIn.Map( address24, OnMessage24Received );
			oscIn.Map( address25, OnMessage25Received );

			// Show UI.
			uiWrapper.SetActive( true );
		}


		void Update()
		{

		}
		public void createFotn(){
			
		//public string address1 = "/fotn/start";
		//public string address2 = "/fotn/end";
		//public string address3 = "/fotn/jis";
		//public string address4 = "/fotn/utf";
		//public string address5 = "/fotn/matrix";
			// Create a bundle, add two messages with seperate addresses and values, then send.
			OscBundle bundle = new OscBundle();
			OscMessage message1 = new OscMessage( address1, Random.value );
			OscMessage message2 = new OscMessage( address2, "0xSSS" );
			OscMessage message3 = new OscMessage( address3, "0xTTT" );
			OscMessage message4 = new OscMessage( address4, "0xUUU" );
			OscMessage message5 = new OscMessage( address5, "0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,EOL" );
			bundle.Add( message1 );
			bundle.Add( message2 );
			bundle.Add( message3 );
			bundle.Add( message4 );
			bundle.Add( message5 );
			oscOut.Send( bundle );
			Debug.Log("HELLO");
		} 

		public void createCorel(){
			
		//public string address11 = "/coral/start";
		//public string address12 = "/coral/end";
		//public string address13 = "/coral/x";
		//public string address14 = "/coral/y";
		//public string address15 = "/coral/charactor";
			// Create a bundle, add two messages with seperate addresses and values, then send.

        StartCoroutine("corelCreator");
		} 

    //「コルーチン」で呼び出すメソッド
    IEnumerator corelCreator(){
 
			GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("Corel");
			foreach (GameObject obj in tagobjs) {
				Destroy (obj);
			}
			for(int i=0;i<corelCount;i++){
				Debug.Log("開始");
				yield return new WaitForSeconds(0.1f);  //10秒待つ
				OscBundle bundle = new OscBundle();
				OscMessage message11 = new OscMessage( address11, Random.value );
				OscMessage message12 = new OscMessage( address12, Random.value );
				OscMessage message13 = new OscMessage( address13, Random.value*stageScale-stageScale/2 );
				OscMessage message14 = new OscMessage( address14, Random.value*stageScale-stageScale/2 );
				OscMessage message15 = new OscMessage( address15, 111 );
				bundle.Add( message11 );
				bundle.Add( message12 );
				bundle.Add( message13 );
				bundle.Add( message14 );
				bundle.Add( message15 );
				oscOut.Send( bundle );

				//Debug.Log("HELLO");
				Debug.Log("0.1秒経ちました");
			}
 
    }

		public void createMaybe(){
			
			//public string address21 = "/maybe/start";
			//public string address22 = "/maybe/end";
			//public string address23 = "/maybe/x";
			//public string address24 = "/maybe/z";
			//public string address25 = "/maybe/charactor";
			// Create a bundle, add two messages with seperate addresses and values, then send.



        StartCoroutine("maybeCreator");
		} 

    //「コルーチン」で呼び出すメソッド
    IEnumerator maybeCreator(){
 
			GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("Maybe");
			foreach (GameObject obj in tagobjs) {
				Destroy (obj);
			}
			for(int i=0;i<maybeCount;i++){
				Debug.Log("開始");
				yield return new WaitForSeconds(0.1f);  //10秒待つ
				
				OscBundle bundle = new OscBundle();
				OscMessage message21 = new OscMessage( address21, Random.value );
				OscMessage message22 = new OscMessage( address22, Random.value );
				OscMessage message23 = new OscMessage( address23, Random.value*stageScale-stageScale/2 );
				OscMessage message24 = new OscMessage( address24, Random.value*stageScale-stageScale/2 );
				OscMessage message25 = new OscMessage( address25, Random.value*stageScale+stageScale/2 );
				bundle.Add( message21 );
				bundle.Add( message22 );
				bundle.Add( message23 );
				bundle.Add( message24 );
				bundle.Add( message25 );
				oscOut.Send( bundle );
				Debug.Log("HELLO");

				//Debug.Log("HELLO");
				Debug.Log("0.1秒経ちました");
			}
 
    }


		void OnMessage1Received( OscMessage message )
		{// Update UI.
			receiveLabel1.text = message.ToString();
            //localCount=0;
		}


		void OnMessage2Received( OscMessage message )
		{// Update UI
			receiveLabel2.text = message.ToString();
			//単語の終わりなので空白を一つ作る。
            localCount++;
		}
		void OnMessage3Received( OscMessage message )
		{// Update UI
			receiveLabel3.text = message.ToString();
		}
		void OnMessage4Received( OscMessage message )
		{// Update UI
			receiveLabel4.text = message.ToString();
		}
		void OnMessage5Received( OscMessage message )
		{// Update UI
			receiveLabel5.text = message.ToString();

            localCount++;
            Vector3 position=new Vector3(localCount * 10.0f*(-1.0f), 0, 0);
            //Vector3 rotation=new Vector3(i * 2.0f, 0, 0);

            MisakiFotn fotn = (MisakiFotn)Instantiate(fotnBase, position, transform.rotation);
			fotn.manager=manager;
            fotn.setTypeFace(message.ToString());
            //fotn.setTypeFace("0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,EOL");
            fotn.transform.parent=fotnParent;

            position=new Vector3((localCount-2) * 10.0f*(-1.0f), 0, 0);
            cameraTarget.position=position;

			scopeTarget.target=fotn.transform;

		}


		void OnMessage11Received( OscMessage message )
		{// Update UI.
			receiveLabel11.text = message.ToString();
            //localCount=0;
		}

		void OnMessage12Received( OscMessage message )
		{// Update UI

			receiveLabel12.text = message.ToString();
			//単語の終わりなので空白を一つ作る。
            localCount++;
		}
		void OnMessage13Received( OscMessage message )
		{// Update UI
			string[] stArrayData = message.ToString().Split(' ');
			receiveLabel13.text = stArrayData[1];
		}
		void OnMessage14Received( OscMessage message )
		{// Update UI
			string[] stArrayData = message.ToString().Split(' ');
			receiveLabel14.text = stArrayData[1];
		}
		void OnMessage15Received( OscMessage message )
		{// Update UI
			
			Debug.Log("HELLO CorelUnit");
			localCount++;
			receiveLabel15.text = message.ToString();

            Vector3 position=new Vector3( float.Parse(receiveLabel13.text),0, float.Parse(receiveLabel14.text));
            //Vector3 rotation=new Vector3(i * 2.0f, 0, 0);

            CorelUnit corel = (CorelUnit)Instantiate(corelBase, position, transform.rotation);
			//fotn.manager=manager;
            //fotn.setTypeFace(message.ToString());
            //fotn.setTypeFace("0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,EOL");
            corel.transform.parent=corelParent;

            //position=new Vector3((localCount-2) * 10.0f*(-1.0f), 0, 0);
            //cameraTarget.position=position;

			//scopeTarget.target=fotn.transform;

		}
		void OnMessage21Received( OscMessage message )
		{// Update UI.
			receiveLabel21.text = message.ToString();
            //localCount=0;
		}


		void OnMessage22Received( OscMessage message )
		{// Update UI
			receiveLabel22.text = message.ToString();
			//単語の終わりなので空白を一つ作る。
            localCount++;
		}
		void OnMessage23Received( OscMessage message )
		{// Update UI
			string[] stArrayData = message.ToString().Split(' ');
			receiveLabel23.text = stArrayData[1];
		}
		void OnMessage24Received( OscMessage message )
		{// Update UI
			string[] stArrayData = message.ToString().Split(' ');
			receiveLabel24.text = stArrayData[1];
		}
		void OnMessage25Received( OscMessage message )
		{// Update UI
            localCount++;
			receiveLabel25.text = message.ToString();

            Vector3 position=new Vector3( float.Parse(receiveLabel23.text),0, float.Parse(receiveLabel24.text));
            //Vector3 rotation=new Vector3(i * 2.0f, 0, 0);

            MaybeUnit maybe = (MaybeUnit)Instantiate(maybeBase, position, transform.rotation);
			//fotn.manager=manager;
            //fotn.setTypeFace(message.ToString());
            //fotn.setTypeFace("0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,EOL");
            maybe.transform.parent=maybeParent;

            //position=new Vector3((localCount-2) * 10.0f*(-1.0f), 0, 0);
            //cameraTarget.position=position;

			//scopeTarget.target=fotn.transform;

		}
	}
}