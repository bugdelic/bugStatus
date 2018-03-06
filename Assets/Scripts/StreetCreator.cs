using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StreetCreator : MonoBehaviour
{
	//roadのプレハブ
	public GameObject road;
	public GameObject road_intersection_L;
	public GameObject road_intersection_T;
	//床
	public GameObject plane;
	//道路を管理するためのクラス
	public class ArrayController
	{
		public int type = 0;
		public bool sub = false;
		//0:未決定,1:結合あり,2結合なし
		public int left = 0;
		public int right = 0;
		public int up = 0;
		public int down = 0;
	}
	//上記のクラスを配列で用意	
	public ArrayController[,] array;
	//自動生成する幅と高さ
	public int width;
	public int height;
	// Use this for initialization
	void Start ()
	{
		CreateCity ();
	}
	void Update(){
		//Rが押されたらシーンを再読み込み(別の形になる)
		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (0);
		}
	}
	void CreateCity ()
	{
		//配列
		array = new ArrayController[width, height];
		//床の大きさを幅，高さに応じて変更
		plane.transform.localScale = new Vector3 (width / 2, 1, height / 2);
		plane.transform.position = new Vector3 (width, 0.02f, height);
		plane.GetComponent<Renderer> ().material.color = Color.white;
		//配列の中身初期化
		for (int i = 1; i < width - 1; i++) {
			for (int j = 1; j < height - 1; j++) {
				array [i, j] = new ArrayController (){ type = 0, sub = false };
			}
		}
		//円周
		//左上の角は右と下をつなぐ曲線
		array [0, 0] = new ArrayController (){ type = 1 };
		//左下の角は右と上をつなぐ曲線
		array [0, height - 1] = new ArrayController (){ type = 2 };
		//右上の角は左と下をつなぐ曲線
		array [width - 1, 0] = new ArrayController (){ type = 3 };
		//右下の角は左と上をつなぐ曲線
		array [width - 1, height - 1] = new ArrayController (){ type = 4 };
		//一時的な数値格納
		int temp;
		for (int j = 1; j < height - 1; j++) {
			//左端
			if (Random.Range (0, 2) == 0) {
				temp = 5;
				array [1, j].left = 2;
			} else {
				temp = 9;
				array [1, j].left = 1;
			}
			array [0, j] = new ArrayController (){ type = temp, sub = true };
			//右端
			if (Random.Range (0, 2) == 0) {
				temp = 5;
				array [width - 2, j].right = 2;
			} else {
				temp = 7;
				array [width - 2, j].right = 1;
			}
			array [width - 1, j] = new ArrayController (){ type = temp, sub = true };
		}
		for (int i = 1; i < width - 1; i++) {
			//上端
			if (Random.Range (0, 2) == 0) {
				temp = 6;
				array [i, 1].up = 2;
			} else {
				temp = 10;
				array [i, 1].up = 1;
			}
			array [i, 0] = new ArrayController (){ type = temp, sub = true };
			//下端
			if (Random.Range (0, 2) == 0) {
				temp = 6;
				array [i, height - 2].down = 2;
			} else {
				temp = 8;
				array [i, height - 2].down = 1;
			}
			array [i, height - 1] = new ArrayController (){ type = temp, sub = true };
		}

		//中身を生成していく
		for (int j = 1; j < height - 1; j++) {
			for (int i = 1; i < width - 1; i++) {
				if (!array [i, j].sub) {
					//上は結合しなくてはならないか
					if (array [i, j].up == 1) {
						//上と左を結合しなけらばならないか
						if (array [i, j].left == 1) {
							//さらに右も結合するか，するなら上左を結合する曲がり道かT(上左右)かT(上左下)確定
							if (array [i, j].right == 1) {
									//T(上左右)確定
									array [i, j].type = 8;
									array [i, j].sub = true;
								//
								array [i - 1, j].right = 1;
								array [i + 1, j].left = 1;
								array [i, j + 1].up = 2;
								array [i, j - 1].down = 1;
							} else if (array [i, j].down == 1) {
								//T(左下)確定
								array [i, j].type = 7;
								array [i, j].sub = true;
								array [i - 1, j].right = 1;
								array [i + 1, j].left = 2;
								array [i, j + 1].up= 1;
								array [i, j - 1].down = 1;
							} else if (array [i, j].down == 2) {
								//左曲がり確定
								array [i, j].type = 4;
								array [i, j].sub = true;
								array [i - 1, j].right = 1;
								array [i + 1, j].left = 2;
								array [i, j + 1].up = 2;
								array [i, j - 1].down = 1;
							} else {
								//左曲がりか左下確定
								if (Random.Range (0, 2) == 0) {
									array [i, j].type = 4;
									array [i, j].sub = true;
									array [i - 1, j].right = 1;
									array [i + 1, j].left = 2;
									array [i, j + 1].up = 2;
									array [i, j - 1].down = 1;
								} else {
									array [i, j].type = 7;
									array [i, j].sub = true;
									array [i - 1, j].right = 1;
									array [i + 1, j].left = 2;
									array [i, j + 1].up = 1;
									array [i, j - 1].down = 1;
								}
							}
						} else if (array [i, j].right == 1) {
							//左は結合しないが上は結合かつ右も結合
							//上と右をつなぐ曲がり路か上と右と下のT字路
							if (Random.Range (0, 2) == 0) {
								//上,右,下
								array [i, j].type = 9;
								array [i, j].sub = true;
								array [i - 1, j].right = 2;
								array [i + 1, j].left = 1;
								array [i, j + 1].up = 1;
								array [i, j - 1].down = 1;
							} else {
								//上右

								array [i, j].type = 2;
								array [i, j].sub = true;
								array [i - 1, j].right = 2;
								array [i + 1, j].left = 1;
								array [i, j + 1].up = 2;
								array [i, j - 1].down = 1;
							}
						}
						//左が繋げないが上を繋がなくてはならない
						if (!array [i, j].sub) {
							//もし右をつながなくてはならないなら
							if (array [i, j].right == 1) {
								//上右
								array [i, j].type = 2;
								array [i, j].sub = true;
								array [i - 1, j].right = 2;
								array [i + 1, j].left = 1;
								array [i, j + 1].up = 2;
								array [i, j - 1].down = 1;
							} else {
								if (array [i, j].right != 2) {
									//右を繋いでも良いならランダムで
									if (Random.Range (0, 2) == 0) {
										//上右
										array [i, j].type = 2;
										array [i, j].sub = true;
										array [i - 1, j].right = 2;
										array [i + 1, j].left = 1;
										array [i, j + 1].up = 2;
										array [i, j - 1].down = 1;
									} else {
										//上下
										array [i, j].type = 5;
										array [i, j].sub = true;
										array [i - 1, j].right = 2;
										array [i + 1, j].left = 2;
										array [i, j + 1].up = 1;
										array [i, j - 1].down = 1;
									}
								} else {
									//左右をつないではならないから
									//上下
									array [i, j].type = 5;
									array [i, j].sub = true;
									array [i - 1, j].right = 2;
									array [i + 1, j].left = 2;
									array [i, j + 1].up = 1;
									array [i, j - 1].down = 1;
								}
							}
						}

					}
					//上は空いてない
					else if (array [i, j].right == 1) {
						//右と下か右と左
						if (array [i, j].left == 1) {
							//右左を結合

							if (Random.Range (0, 2) == 0) {
								array [i, j].type = 6;
								array [i, j].sub = true;
								array [i - 1, j].right = 1;
								array [i + 1, j].left = 1;
								array [i, j + 1].up = 2;
								array [i, j - 1].down = 2;
							} else {
								//左右下
								array [i, j].type =10;
								array [i, j].sub = true;
								array [i - 1, j].right = 1;
								array [i + 1, j].left = 1;
								array [i, j + 1].up = 1;
								array [i, j - 1].down = 2;
							}
						} else if (array [i, j].down != 2) {
							//右下確定
							array [i, j].type = 1;
							array [i, j].sub = true;
							array [i - 1, j].right = 2;
							array [i + 1, j].left = 1;
							array [i, j + 1].up = 1;
							array [i, j - 1].down = 2;
						}

					}
					//上と右は結合しなくてよく，左は結合
					else if (array [i, j].left == 1) {
						if (array [i, j].right != 2) {
							if (Random.Range (0, 2) == 0) {
								//左，右
								array [i, j].type = 6;
								array [i, j].sub = true;
								array [i - 1, j].right = 1;
								array [i + 1, j].left = 1;
								array [i, j + 1].up = 2;
								array [i, j - 1].down = 2;
							} else {
								//左，下，右
								array [i, j].type = 10;
								array [i, j].sub = true;
								array [i - 1, j].right = 1;
								array [i + 1, j].left = 1;
								array [i, j + 1].up = 1;
								array [i, j - 1].down = 2;
							}
						} else {
							//左，下
							array [i, j].type = 3;
							array [i, j].sub = true;
							array [i - 1, j].right= 1;
							array [i + 1, j].left = 2;
							array [i, j + 1].up = 1;
							array [i, j - 1].down = 2;
						}
					}
				}
			}
		}
		//下の列を作る
		for (int i = 1; i < width - 1; i++) {
			if (array [i, height-1].up == 1 && array [i, height-2].sub) {
				temp = 8;
				array [i, height-2].down = 1;
			} else {
				temp = 6;
				array [i, height-2].down = 2;
			}
			array [i,height-1] = new ArrayController (){ type = temp, sub = true };
		}

		//作成
		GameObject tempobj;
		for (int j = 0; j < height; j++) {
			for (int i = 0; i < width; i++) {
				switch (array [i, j].type) {
				case 1://(下右)
					tempobj = Instantiate (road_intersection_L, new Vector3 (i * 2, 0, (height- j) * 2), Quaternion.Euler (new Vector3 (0, 180, 0)));
					tempobj.name = i + "," + j;
					break;
				case 2://(上右)
					tempobj = Instantiate (road_intersection_L, new Vector3 (i * 2, 0, (height- j) * 2), Quaternion.Euler (new Vector3 (0, 90, 0)));
					tempobj.name = i + "," + j;
					break;
				case 3://(下左)
					tempobj = Instantiate (road_intersection_L, new Vector3 (i * 2, 0, (height- j) * 2), Quaternion.Euler (new Vector3 (0, 270, 0)));
					tempobj.name = i + "," + j;
					break;
				case 4://(上左)
					tempobj = Instantiate (road_intersection_L, new Vector3 (i * 2, 0, (height- j) * 2), Quaternion.identity);
					tempobj.name = i + "," + j;
					break;
				case 5://(上下)
					tempobj = Instantiate (road, new Vector3 (i * 2, 0, (height- j) * 2), Quaternion.identity);
					tempobj.name = i + "," + j;
					break;
				case 6://(右左)
					tempobj = Instantiate (road, new Vector3 (i * 2, 0, (height- j) * 2), Quaternion.Euler (new Vector3 (0, 90, 0)));
					tempobj.name = i + "," + j;
					break;
				case 7://T(上左下)
					tempobj = Instantiate (road_intersection_T, new Vector3 (i * 2, 0, (height- j) * 2), Quaternion.identity);
					tempobj.name = i + "," + j;
					break;
				case 8://T(上左右)
					tempobj = Instantiate (road_intersection_T, new Vector3 (i * 2, 0, (height- j) * 2), Quaternion.Euler (new Vector3 (0, 90, 0)));
					tempobj.name = i + "," + j;
					break;
				case 9://T(右下上)
					tempobj = Instantiate (road_intersection_T, new Vector3 (i * 2, 0, (height- j) * 2), Quaternion.Euler (new Vector3 (0, 180, 0)));
					tempobj.name = i + "," + j;
					break;
				case 10://T(右左下)
					tempobj = Instantiate (road_intersection_T, new Vector3 (i * 2, 0, (height- j) * 2), Quaternion.Euler (new Vector3 (0, 270, 0)));
					tempobj.name = i + "," + j;
					break;
				case 11://ここから建物とか
					
					break;
				}
			}
		}

	}
}