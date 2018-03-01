using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

namespace animSprite{
    [System.Serializable]
    public class ReplaceInfo{
        public string fromName;
        public string toName;
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimSpriteChange : MonoBehaviour {

    private SpriteRenderer sr2;
	
	public string url="/Users/satorupan/Desktop/sprite1.png";
        [SerializeField] string spriteResourcePath="";
        [SerializeField] ReplaceInfo[] replaceInfo;
        SpriteRenderer sr;
        Sprite[] resSprArr;
		public Sprite fotnSprite;
        Dictionary<string,string> repDic;

        void Start () {
            prepare ();  
			getUrlImage();
			
    }
        void LateUpdate () {
            sprChange ();
        }

        void prepare(){
            Resources.LoadAll<Sprite>(spriteResourcePath);
            sr = GetComponent<SpriteRenderer> ();
            resSprArr = Resources.FindObjectsOfTypeAll<Sprite> ();
            repDic = new Dictionary<string, string> ();
            foreach (ReplaceInfo info in replaceInfo) {
                repDic.Add (info.fromName, info.toName);
            }
        }

        void sprChange(){
			/* 

            string sprName = sr.sprite.name;
			*/
            string sprName = fotnSprite.name;

            int pos = sprName.LastIndexOf ('_');
            if (pos >= 0) {
                string pre = sprName.Substring (0, pos);
                if (repDic.ContainsKey (pre)) {
                    string post = sprName.Substring (pos);
                    Sprite retSpr = resSprArr.FirstOrDefault(e=>e.name.Equals(repDic [pre] + post));
                    if (retSpr != null) {
                        sr.sprite = retSpr;
                    }
                }
            }
        }

	public void  getUrlImage(){

        this.sr2 = this.gameObject.GetComponent<SpriteRenderer>();
        Texture2D texture = ReadTexture (url, 90, 110);
        Sprite createdSprite = Sprite.Create (texture, new Rect (0, 0, 800, 500), new Vector2 (0, 1), 1);
        this.sr2.sprite = createdSprite;
		fotnSprite=createdSprite;

	}
 
    private Texture2D ReadTexture(string path, int width, int height){
        byte[] readBinary = ReadPngFile(path);
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(readBinary);
        texture.filterMode = FilterMode.Point;
        return texture;
    }
	byte[] ReadPngFile(string path){
    	FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
    	BinaryReader bin = new BinaryReader(fileStream);
    	byte[] values = bin.ReadBytes((int)bin.BaseStream.Length);

    	bin.Close();

    	return values;
	}
    }
} // namespace animSprite