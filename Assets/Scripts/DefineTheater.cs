using UnityEngine;
using System.IO;
 
public class DefineTheater : MonoBehaviour {
    public SpriteRenderer sr;
	public string url="_ExDir/stage0.png";
	public int count;
	public int textureLength;
	public Texture2D myTexture;
	public Texture2D[] textureArray;
	public IMG2Sprite myImg2Sprite;
	public bool inited;
	public int textureSize=32;
	private Sprite createdSprite;
	
    void Start () {
		//getUrlImage();

		myTexture=myImg2Sprite.LoadTexture(url);
		textureArray= myImg2Sprite.SplitTex( myTexture, textureSize,textureSize);
		textureLength=textureArray.Length;

		inited=true;
		this.sr = GetComponent<SpriteRenderer> ();

    }
 
 
    void Update () {
		if(inited){

		if(textureLength>count){
			setTexture();
			count++;
		}else{
			count=0;
		}
		}
    }
	public void  setTexture(){
        createdSprite = Sprite.Create (textureArray[count], new Rect (0, 0, textureSize, textureSize), new Vector2 (0, 1), 1);
		this.sr.sprite =createdSprite;
	}

}
