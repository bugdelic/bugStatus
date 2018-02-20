using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Boid
	{
		// 漢字の辺、つくり、ひらがな、カタカナで個体の挙動を変えたりする時に利用する
		// TODO:特定のenum定義じゃなく、利便性のためにナンバリングしたstateにした方が良いかも。
		//      public int status = 0; // 0:default
		//                             // 1以降の数字はBoidを制御する側で任意に定義して
		//                             // state machineとして利用できるように...みたいな。
		//                             // このBoidはstate(性格)を持たない個としてのみ定義
		//                             // した方が利便性が上がりそう。
		public enum Status {
			HEN,
			TUKURI,
			HIRAGANA,
			KATAKANA
		};			
		public Status status;


		// とりあえずGameObject
		public GameObject body;

		// 自身が個体の時にターゲットとしているBoss
		public Boid targetBoss;

		// 自身が消滅するタイムアウト
		public float extinctionTimeOut;

		// 自身が生成された時間
		private float createTime;

		// 加速度(個々に加速度を保持)
		private Vector3 acceleration;
	}
}

