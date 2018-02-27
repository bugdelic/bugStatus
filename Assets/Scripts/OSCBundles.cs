/*
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

        public int localCount =0;
		public GameObject uiWrapper;
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
		public Transform cameraTarget;

		public string address1 = "/fotn/start";
		public string address2 = "/fotn/end";
		public string address3 = "/fotn/jis";
		public string address4 = "/fotn/utf";
		public string address5 = "/fotn/matrix";


		public string address11 = "/coral/start";
		public string address12 = "/coral/end";
		public string address13 = "/coral/x";
		public string address14 = "/coral/y";
		public string address15 = "/coral/z";


		public string address21 = "/maybe/start";
		public string address22 = "/maybe/end";
		public string address23 = "/maybe/x";
		public string address24 = "/maybe/y";
		public string address25 = "/maybe/charactor";



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

			// Show UI.
			uiWrapper.SetActive( true );
		}


		void Update()
		{
			// Create a bundle, add two messages with seperate addresses and values, then send.
			OscBundle bundle = new OscBundle();
			OscMessage message1 = new OscMessage( address1, Random.value );
			OscMessage message2 = new OscMessage( address2, Random.value );
			bundle.Add( message1 );
			bundle.Add( message2 );
			//oscOut.Send( bundle );

			// Update UI.
			sendLabel1.text = message1.ToString();
			sendLabel2.text = message2.ToString();
		}



		void OnMessage1Received( OscMessage message )
		{
			// Update UI.
			receiveLabel1.text = message.ToString();
            //localCount=0;
		}


		void OnMessage2Received( OscMessage message )
		{
			// Update UI
			receiveLabel2.text = message.ToString();

            localCount++;
		}
		void OnMessage3Received( OscMessage message )
		{
			// Update UI
			receiveLabel3.text = message.ToString();
		}
		void OnMessage4Received( OscMessage message )
		{
			// Update UI
			receiveLabel4.text = message.ToString();
		}
		void OnMessage5Received( OscMessage message )
		{
            localCount++;
			// Update UI
			receiveLabel5.text = message.ToString();

            Vector3 position=new Vector3(localCount * 10.0f*(-1.0f), 0, 0);
            //Vector3 rotation=new Vector3(i * 2.0f, 0, 0);

            MisakiFotn fotn = (MisakiFotn)Instantiate(fotnBase, position, transform.rotation);
            fotn.setTypeFace(message.ToString());
            //fotn.setTypeFace("0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,EOL");
            fotn.transform.parent=this.transform;

            position=new Vector3((localCount-2) * 10.0f*(-1.0f), 0, 0);
            cameraTarget.position=position;
            //Instantiate(fotnBase, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);

		}
	}
}