using UnityEngine;
using System.Collections;

public class Boid : MonoBehaviour
{
	// Boid毎のステータス
	public int status;

	// 自身が個体の時にターゲットとしているBoss。
	// ターゲットになるBossが存在しない場合は全群の中央に寄ろうとする
	public Boid targetBoss = null;

	// 自身が消滅するタイムアウト
	public float extinctionTimeOut = 120.0f;

	// 自身が生成された時間
	public float createTime;

	// 加速度(個々に加速度を保持)
	private Vector3 acceleration;

	// このBoidのコントローラ
	BoidsController boidController;

	public Boid () {
	}

	void Start () {
		createTime = Time.time;

		//親オブジェクトを取得
		this.boidController = gameObject.GetComponentInParent<BoidsController>();
	}

	void Update () {
	}

	// Boid同士が衝突した時
	void OnCollisionStay(Collision collision) {
		
		// 親のBoidsControllerを呼び出してGameObjectを削除
		// if (!boidController.isBoss(collision.gameObject))
		// 	boidController.destroyBoid(collision.gameObject);
	}
}
