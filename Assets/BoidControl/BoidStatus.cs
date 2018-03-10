using UnityEngine;
using System.Collections;

public class BoidStatus
{
	// Boid毎のステータス(仮で何かに使うかも)
	public int status;

	// 自身が消滅するタイムアウト
	public float lifetime;

	// 自身が生成された時間
	public float createTime;

	// 速度
	public float velocity;

	public BoidStatus () {
		createTime = Time.time;
	}
}
