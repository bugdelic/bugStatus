using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class BoidsController : MonoBehaviour
	{
		/// <summary>
		/// 群れの個体数を指定します。
		/// </summary>
		public int MaxChild = 30;

		/// <summary>
		/// 群れの乱れ具合を指定します。
		/// 大きいほど各個体の個性が反映されまとまりません。
		/// 最小値は 0, 最大値は 1 です。
		/// </summary>
		public float Turbulence = 1f;

		/// <summary>
		/// 各個体の距離を指定します。大きいほど各個体が距離を取ります。
		/// </summary>
		public float Distance = 4f;

		/// <summary>
		/// 群れにする個体を指定します。
		/// この個体を複製して群れにします。
		/// </summary>
		public GameObject BoidsChild;

		/// <summary>
		/// 群れの中央部に表示するモデルを指定します。
		/// 群れの中央部を目視することができます。
		/// </summary>
		public GameObject BoidsCenter;

		/// <summary>
		/// 群れのコントローラです。仮にボスと名付けます。
		/// 群れはボスを基準に動きます。
		/// </summary>
		// TODO: 複数Bossの定義
		public GameObject BoidsBoss;

		/// <summary>
		/// 群れとして扱う各個体を配列として扱います。
		/// </summary>
		public GameObject[] BoidsChildren;

		public BoidsController ()
		{
		}

		// Use this for initialization
		void Start ()
		{
			//子供を生成して適当な位置に置きます。
			this.BoidsChildren = new GameObject[MaxChild];

			for (int i = 0; i < this.MaxChild; i++) {
				this.BoidsChildren[i] = GameObject.Instantiate(BoidsChild) as GameObject;
				this.BoidsChildren[i].transform.position
				= new Vector3(UnityEngine.Random.Range(-50f, 50f),
					this.BoidsChild.transform.position.y,
					UnityEngine.Random.Range(-50f, 50f));
			}
		}

		// Update is called once per frame
		void Update ()
		{
			//各個体の座標から、群れの中央の座標を求めます。
			Vector3 center = Vector3.zero;
			foreach (GameObject child in this.BoidsChildren)
			{
				center += child.transform.position;
			}
			center /= (BoidsChildren.Length - 1);

			//ここでは群れをコントロールするために、
			//仮に、ボスの方へ向かう習性があるとします。
			//算出した中央の座標を、ボスとの中間地点へ移動します。
			center += this.BoidsBoss.transform.position;
			center /= 2;
			this.BoidsCenter.transform.position = center;

			// TODO: 各個体は近くのボスに寄ろうとする


			//[特性-1 : 各個体は群れの中央に寄ろうとする]
			foreach (GameObject child in this.BoidsChildren)
			{
				//中央方向への単位ベクトルを取得して、その方向へ向かおうとさせます。
				//係数が大きいほどその個体の個性が反映され、自分の進行方向へ移動します。
				//係数が 1 のとき、完全に中央へ向かわず、0 とき、完全に中央へ向かいます。
				Vector3 dirToCenter = (center - child.transform.position).normalized;
				Vector3 direction = (child.GetComponent<Rigidbody>().velocity.normalized * this.Turbulence
					+ dirToCenter * (1 - this.Turbulence)).normalized;

				//個体によって速さを変えます(必要なら方向も)。
				//別途制御用パラメータを実装して個性を表現しても良いでしょう。
				//ここでは取りあえず毎回ランダムにします。
				//各個体の個性の差が大きいほど、群れ全体がバラつきます。
				//各個体が固有の速度の差が大きいほど縦長の群れを形成しやすくなります。
				direction *= UnityEngine.Random.Range(20f, 30f);

				child.GetComponent<Rigidbody>().velocity = direction;
			}

			//[特性-2 : 各個体は互いに距離を取ろうとする]
			//各個体間の距離を Collider のみに頼ると、
			//密集時に、常に等間隔を取る様な群れが簡単にできます。
			//例えば、兵隊のようなものを表現するときは、
			//Collider による判定のみで十分でしょう。(処理負荷は大きいですが)
			//一方で、昆虫や哺乳類の群れを表現するには規則的過ぎます。
			//Collider を利用しない場合は、
			//適度な距離を保てるような仕組みを用意する必要があります。

			foreach (GameObject child_a in this.BoidsChildren)
			{
				foreach (GameObject child_b in this.BoidsChildren)
				{
					if (child_a == child_b)
					{
						continue;
					}

					//ここでは Colider に頼りません。
					//個体 a と個体 b の距離が設定された値 (Distance) より小さいとき、
					//個体 a の進行方向を、個体 b と反対の方向へ設定します。
					//いわゆるパーソナルスペースとなるので、
					//各個体に固有のパラメータとして設定すれば、個性を表現できます。
					//固定値でも良いですが、バラけさせた方が、より生物に近い表現になります。
					Vector3 diff = child_a.transform.position - child_b.transform.position;
					if (diff.magnitude < UnityEngine.Random.Range(2, this.Distance))
					{
						child_a.GetComponent<Rigidbody>().velocity =
							diff.normalized * child_a.GetComponent<Rigidbody>().velocity.magnitude;
					}
				}
			}

			//各個体のベクトルから、群れの平均ベクトルを求めます。
			Vector3 averageVelocity = Vector3.zero;
			foreach (GameObject child in this.BoidsChildren)
			{
				averageVelocity += child.GetComponent<Rigidbody>().velocity;
			}
			averageVelocity /= this.BoidsChildren.Length;

			//[特性-3 : 各個体は群れの平均移動ベクトルに合わせようとする]
			//この特性-3はなくてもそれなりに動きます。
			//Boids の群れモデルを基本的な実装で再現すると、
			//都合上、個体数が増えるほどにオーダーが膨れ上がる上、
			//平均化すると各個体の変化が小さくなります。
			//したがって、不要なら特性3は無視しても良いかもしれません。
			//例えば遠方の群れであったり、個体数があまりにも多い場合には、
			//見た目にほとんど変化が現れない可能性があります。
			foreach (GameObject child in this.BoidsChildren)
			{
				//Turbulence は群れ乱れの係数です。
				//最小値は 0 で、最大値は 1 です。
				//値が大きいほど群れがまとまりにくくなります。
				//0 のとき、群れの各個体の移動は、完全に群れ全体の移動へ合わせます。
				//0.5 のとき、各個体の移動は群れ全体の移動に向かって 50% 補正されます。
				//1 のとき、各個体は群れ全体の移動を考慮しません。
				child.GetComponent<Rigidbody>().velocity = child.GetComponent<Rigidbody>().velocity * this.Turbulence
					+ averageVelocity * (1f - this.Turbulence);

				//[Option] 各個体を回転させるとき。
				//回転の方法は表現する対象によって切り替える必要があります。
				//まったく回転させないのが適切な場合もあるでしょう。

				//Type1 : 最も簡単なものは、個体の進行方向に合わせて回転させる方法です。
				//群れ全体が移動しているときは一定の方向を向きますが、移動が低速になると、
				//各個体が衝突してバラバラの方向を向くようになり、移動時よりも統一感が損なわれます。
				child.transform.rotation =
					Quaternion.Slerp(child.transform.rotation,
						Quaternion.LookRotation(child.GetComponent<Rigidbody>().velocity.normalized),
						Time.deltaTime * 10f);

				//Type2 : 餌に群がるような群れを表現するは、目標を見るようにすれば良いです。
				//ここでは群れの中央に設定しています。
				child.transform.LookAt(center);

				//Type3 : 魚群のような、方向がそろうような群れは、
				//群れの平均移動ベクトルを利用すれば良いです。
				//Type1 の方が生物の特性に近い挙動はしますが、魚群のようなものを表現する場合には、
				//Type3 のような極端な表現の方がらしく見えます。
				//全体の移動量が少ないときも各個体が同じ方向を向くので、
				//静止時用の特別な動きの実装がなくてもそれらしく見えます。
				child.transform.rotation =
				    Quaternion.Slerp(child.transform.rotation,
				                     Quaternion.LookRotation(averageVelocity.normalized),
				                     Time.deltaTime * 3f);
			}
		}
	}
}

