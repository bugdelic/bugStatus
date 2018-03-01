using UnityEngine;
using System.Collections;
using System.IO;
 
//PNGからTexture2DやSpriteを取得する
public class IMG2Sprite : MonoBehaviour {
 
   // This script loads a PNG or JPEG image from disk and returns it as a Sprite
   // Drop it on any GameObject/Camera in your scene (singleton implementation)
   //
   // Usage from any other script:
   // MySprite = IMG2Sprite.instance.LoadNewSprite(FilePath, [PixelsPerUnit (optional)])
 /* 
   private static IMG2Sprite _instance;
 
   public static IMG2Sprite instance
   {
     get    
     {
       //If _instance hasn't been set yet, we grab it from the scene!
       //This will only happen the first time this reference is used.
 
       if(_instance == null)
         _instance = GameObject.FindObjectOfType<IMG2Sprite>();
       return _instance;
     }
   }*/
 
   public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 32.0f) {
   
     // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference
     
     Sprite NewSprite = new Sprite();
     Texture2D SpriteTexture = LoadTexture(FilePath);
     NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height),new Vector2(0,0), PixelsPerUnit);
 
     return NewSprite;
   }
 
   public  Texture2D LoadTexture(string FilePath) {
 
     // Load a PNG or JPG file from disk to a Texture2D
     // Returns null if load fails
 
     Texture2D Tex2D;
     byte[] FileData;
 
     if (File.Exists(FilePath)){
       FileData = File.ReadAllBytes(FilePath);
       Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
       if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
         return Tex2D;                 // If data = readable -> return texture
     }  
     return null;                     // Return null if load failed
   }
 
	/// <returns>分割されたテクスチャを配列で返す</returns>
  　　　　　　　　　　　　// UnityでTexture分割ウインドウ作ってみた
  	public   Texture2D[] SplitTex(Texture2D texture, int w, int h)
	{
		if (texture == null)
			throw new System.ArgumentNullException ("texture");
		if (texture == null || w <= 0 || h <= 0) { return null; }
 
		//string srcName = texture.name;
		//Debug.LogFormat(" *****srcName= {0}" ,srcName);  //<---- 名前未設定データ
 
		int num_w = Mathf.FloorToInt((float)texture.width / w);
		int num_h = Mathf.FloorToInt((float)texture.height / h);
		System.Collections.Generic.List<Texture2D> texs = new System.Collections.Generic.List<Texture2D>();
		Debug.LogFormat(" *****　num_w= {0},num_h={1}" ,num_w,num_h);
		
		for (int ih = 0; ih < num_h; ih++) {
		  for (int iw = 0; iw < num_w; iw++) {
				// ピクセルコピー
				Texture2D tmp = new Texture2D(w, h, TextureFormat.RGBA32, false);
				tmp.SetPixels( texture.GetPixels(w *  (num_w-iw-1), h * (num_h-ih-1), w, h) );
				tmp.name = "tmp_" + (num_h-ih-1) + "_" + (num_w-iw-1);// ex) image_0_0.png
				tmp.Apply();
				texs.Add (tmp);
			}
		}
 
		return texs.ToArray();
	}
 
}