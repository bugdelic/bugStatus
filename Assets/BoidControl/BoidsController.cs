using System;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class BoidsController : MonoBehaviour
{
	/// <summary>
	/// 群れの乱れ具合を指定します。
	/// 大きいほど各個体の個性が反映されまとまりません。
	/// 最小値は 0, 最大値は 1 です。
	/// </summary>
	public float Turbulence = 0.5f;

	/// <summary>
	/// 各個体の距離を指定します。大きいほど各個体が距離を取ります。
	/// </summary>
	public float Distance = 20.0f;

	/// <summary>
	/// 各個体の生存期間の上限/下限を設定します。
	/// 個体生成時にRandom(min, max)で当てちゃいます。
	/// </summary>
	public float LifetimeMax = 120.0f;
	public float LifetimeMin = 30.0f;

	// 子の速度のレンジ
	public float MaxChildVelocity = 40.0f;
	public float MinChildVelocity = 20.0f;

	// 個性を発動するかどうか？フラグ
	public bool isPersonality = true;

	/// <summary>
	/// 群れのコントローラです。ボスと名付けます。
	/// 群れはボスを基準に動きます。複数ボスもOKです。
	/// </summary>
	public List<GameObject> boidsBosses = new List<GameObject> ();

	/// <summary>
	/// 群れとして扱う各個体を配列として扱います。
	/// </summary>
	public List<GameObject> boidsChildren = new List<GameObject>();

	/// <summary>
	/// 個体ごとのステータス。保持用。
	/// </summary>
	Dictionary<GameObject, BoidStatus> boidStatusList = new Dictionary<GameObject, BoidStatus>(); 

	/// <summary>
	///  子を追加する時のロック用
	/// </summary>
	UnityEngine.Object sync = new UnityEngine.Object();

	public BoidsController ()
	{
	}

	// 群へ子の追加
	public void addChild(GameObject child) {
		lock (sync) {
			BoidStatus boidStatus = new BoidStatus ();
			boidStatus.lifetime = UnityEngine.Random.Range (LifetimeMin, LifetimeMax);
			boidStatus.velocity = UnityEngine.Random.Range (MinChildVelocity, MaxChildVelocity);
			this.boidsChildren.Add (child);
			this.boidStatusList.Add(child, boidStatus);
		}
	}

	// 群へボスの追加
	public void addBoss(GameObject boss) {
		lock (sync) {
			BoidStatus boidStatus = new BoidStatus ();
			boidStatus.lifetime = UnityEngine.Random.Range (LifetimeMin, LifetimeMax);
			this.boidsBosses.Add (boss);
			this.boidStatusList.Add(boss, boidStatus);
		}
	}

	// 群からGameObject(子/ボス)を削除
	public void destroyBoid(GameObject boid) {
		lock (sync) {
			this.boidStatusList.Remove (boid);

			if (boidsChildren.Contains (boid)) boidsChildren.Remove (boid);
			if (boidsBosses.Contains (boid)) boidsBosses.Remove (boid);
			Destroy (boid);
		}
	}

	// 特定のGameObjectが子 or ボスか判定
	public bool isBoss(GameObject boid) {
		lock (sync) {
			if (boidsBosses.Contains (boid))
				return true;;
		}
		return false;
	}

	// 生存時間を過ぎたものは除去(ここでFotnManagerから渡されたGameObjectの消込をするかどうかは後々で)
	private void checkBoidsLifetime() {
		lock (sync) {
			foreach(GameObject boid in boidsChildren.ToArray()) {
				if (this.boidStatusList [boid].lifetime + this.boidStatusList [boid].createTime < Time.time) {
					boidsChildren.Remove (boid);
					boidStatusList.Remove (boid);
					Destroy (boid);
				}
			}

			foreach(GameObject boid in boidsBosses.ToArray()) {
				if (this.boidStatusList [boid].lifetime + this.boidStatusList [boid].createTime < Time.time) {
					boidsBosses.Remove (boid);
					boidStatusList.Remove (boid);
					Destroy (boid);
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
		// 生存期間チェック
		checkBoidsLifetime ();

		lock (sync) {
			// この最初のセクションが動きを付けるときのメインイベント部分
			// 個体とボスの関連性か、重さ(引力)か、一定速度か...悩ましい...orz
			// ボスが居る時は出来るだけボスに寄ろうとする
			Dictionary<GameObject, GameObject> childBossMapper = new Dictionary<GameObject, GameObject>(); 

			// 各個体は一番近くのボスに寄ろうとする 
			if (this.boidsBosses.Count > 0) {
				foreach (GameObject child in this.boidsChildren) {
					float dist = 10000000000.0f;
					GameObject targetBoss = null;
					foreach (GameObject boss in this.boidsBosses) {
						float tmpDis = (boss.transform.position - child.transform.position).magnitude;
						if (dist > tmpDis) {
							targetBoss = boss;
							dist = tmpDis;
							childBossMapper [child] = boss;
						}
					}

					// ボス方向への単位ベクトルを取得して、その方向へ向かおうとさせます。
					// 係数が大きいほどその個体の個性が反映され、自分の進行方向へ移動します。
					// 係数が 1 のとき、完全に中央へ向かわず、0 とき、完全に中央へ向かいます。
					Vector3 dirToCenter = (targetBoss.transform.position - child.transform.position).normalized;
					Vector3 direction = (child.GetComponent<Rigidbody> ().velocity.normalized *
						this.Turbulence + dirToCenter * (1 - this.Turbulence)).normalized;

					// 個体によって速さを変えます(必要なら方向も)。
					// 別途制御用パラメータを実装して個性を表現しても良いでしょう。
					// ここでは取りあえず毎回ランダムにします。
					// 各個体の個性の差が大きいほど、群れ全体がバラつきます。
					// 各個体が固有の速度の差が大きいほど縦長の群れを形成しやすくなります。
					if (isPersonality) {
						direction *= this.boidStatusList[child].velocity;
					} else {
						direction *= UnityEngine.Random.Range (20f, 30f);
					}
					child.GetComponent<Rigidbody> ().velocity = direction;
				}

			// ボスがだれも居ない時、[特性-1 : 各個体は群れの中央に寄ろうとする]
			} else {
				Vector3 center = Vector3.zero;
				foreach (GameObject child in this.boidsChildren) {
					center += child.transform.position;
				}
				center /= (boidsChildren.Count - 1);

				foreach (GameObject child in this.boidsChildren) {
					Vector3 dirToCenter = (center - child.transform.position).normalized;
					Vector3 direction = (child.GetComponent<Rigidbody> ().velocity.normalized * this.Turbulence
						+ dirToCenter * (1 - this.Turbulence)).normalized;

					direction *= UnityEngine.Random.Range (20f, 30f);
					child.GetComponent<Rigidbody> ().velocity = direction;
				}
			}

			// [特性-2 : 各個体は互いに距離を取ろうとする]
			// 各個体間の距離を Collider のみに頼ると、
			// 密集時に、常に等間隔を取る様な群れが簡単にできます。
			// 例えば、兵隊のようなものを表現するときは、
			// Collider による判定のみで十分でしょう。(処理負荷は大きいですが)
			// 一方で、昆虫や哺乳類の群れを表現するには規則的過ぎます。
			// Collider を利用しない場合は、
			// 適度な距離を保てるような仕組みを用意する必要があります。
			foreach (GameObject child_a in this.boidsChildren) {

				foreach (GameObject child_b in this.boidsChildren) {
					if (child_a == child_b) {
						continue;
					}

					// ここでは Colider に頼りません。
					// 個体 a と個体 b の距離が設定された値 (Distance) より小さいとき、
					// 個体 a の進行方向を、個体 b と反対の方向へ設定します。
					// いわゆるパーソナルスペースとなるので、
					// 各個体に固有のパラメータとして設定すれば、個性を表現できます。
					// 固定値でも良いですが、バラけさせた方が、より生物に近い表現になります。
					Vector3 diff = child_a.transform.position - child_b.transform.position;
					if (diff.magnitude < UnityEngine.Random.Range(2, this.Distance)) {
						child_a.GetComponent<Rigidbody>().velocity =
							diff.normalized * child_a.GetComponent<Rigidbody>().velocity.magnitude;
					}
				}
			}

			// 各ボスに群がる個体のベクトルの平均ベクトルを求めます。
			Dictionary<GameObject, Vector3> boidsAverageVector = new Dictionary<GameObject, Vector3>(); 
			Dictionary<GameObject, int> boidsCountByBoss = new Dictionary<GameObject, int>(); 
			// 全体の平均ベクトルを取得しておく
			Vector3 averageVelocity = Vector3.zero;
			foreach (GameObject child in this.boidsChildren) {
				averageVelocity += child.GetComponent<Rigidbody> ().velocity;
			}
			averageVelocity /= this.boidsChildren.Count;
			if (this.boidsBosses.Count > 0) {
				// ボス毎の平均ベクトルを求めるために初期化
				foreach (GameObject boss in this.boidsBosses) {
					boidsAverageVector [boss] = Vector3.zero;
					boidsCountByBoss [boss] = 0;
				}
				// 個体の平均ベクトルをボス毎に合算
				foreach (GameObject child in this.boidsChildren) {
					boidsAverageVector [childBossMapper[child]] += child.GetComponent<Rigidbody> ().velocity;
					boidsCountByBoss [childBossMapper[child]] += 1;
				}
				// ボス毎に平均ベクトルを取得(ボスが個体を持っていない場合はゼロ)
				foreach (GameObject boss in this.boidsBosses) {
					if (boidsCountByBoss [boss] != 0)
						boidsAverageVector [boss] /= boidsCountByBoss [boss];
				}
			}
			// [特性-3 : 各個体は群れの平均移動ベクトルに合わせようとする]
			// この特性-3はなくてもそれなりに動きます。
			// Boids の群れモデルを基本的な実装で再現すると、
			// 都合上、個体数が増えるほどにオーダーが膨れ上がる上、
			// 平均化すると各個体の変化が小さくなります。
			// したがって、不要なら特性3は無視しても良いかもしれません。
			// 例えば遠方の群れであったり、個体数があまりにも多い場合には、
			// 見た目にほとんど変化が現れない可能性があります。
			foreach (GameObject child in this.boidsChildren) {
				// Turbulence は群れ乱れの係数です。
				// 最小値は 0 で、最大値は 1 です。
				// 値が大きいほど群れがまとまりにくくなります。
				// 0 のとき、群れの各個体の移動は、完全に群れ全体の移動へ合わせます。
				// 0.5 のとき、各個体の移動は群れ全体の移動に向かって 50% 補正されます。
				// 1 のとき、各個体は群れ全体の移動を考慮しません。
				child.GetComponent<Rigidbody>().velocity = child.GetComponent<Rigidbody>().velocity * this.Turbulence
					+ (childBossMapper.ContainsKey(child) ? boidsAverageVector[childBossMapper[child]] : averageVelocity) * (1f - this.Turbulence);

				// [Option] 各個体を回転させるとき。
				// 回転の方法は表現する対象によって切り替える必要があります。
				// まったく回転させないのが適切な場合もあるでしょう。
				// Type1 : 最も簡単なものは、個体の進行方向に合わせて回転させる方法です。
				// 群れ全体が移動しているときは一定の方向を向きますが、移動が低速になると、
				// 各個体が衝突してバラバラの方向を向くようになり、移動時よりも統一感が損なわれます。
				//child.transform.rotation =
				//	Quaternion.Slerp(child.transform.rotation,
				//		Quaternion.LookRotation(child.GetComponent<Rigidbody>().velocity.normalized),
				//		Time.deltaTime * 10f);
				// Type2 : 餌に群がるような群れを表現するは、目標を見るようにすれば良いです。
				// ここでは自分が今、属してるボス中央に設定しています。
				//child.transform.LookAt((childBossMapper.ContainsKey(child) ? childBossMapper[child].transform.position : averageVelocity));
				// Type3 : 魚群のような、方向がそろうような群れは、
				// 群れの平均移動ベクトルを利用すれば良いです。
				// Type1 の方が生物の特性に近い挙動はしますが、魚群のようなものを表現する場合には、
				// Type3 のような極端な表現の方がらしく見えます。
				// 全体の移動量が少ないときも各個体が同じ方向を向くので、
				// 静止時用の特別な動きの実装がなくてもそれらしく見えます。
				//child.transform.rotation =
				//	Quaternion.Slerp(child.transform.rotation,
				//		Quaternion.LookRotation(
				//			(childBossMapper.ContainsKey(child) ? boidsAverageVector[childBossMapper[child]].normalized : averageVelocity.normalized)),
				//		Time.deltaTime * 3f);
			}
		}
	}
}
