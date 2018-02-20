using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Boid
	{
		// 漢字の辺、つくり、ひらがな、カタカナで個体の挙動を
		// 変えたりする時に利用する
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
	}
}

