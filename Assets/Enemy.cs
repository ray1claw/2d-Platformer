using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[System.Serializable]
	public class EnemyStats {
		public int health = 20;
	}
	
	public EnemyStats enemyStats = new EnemyStats();
	private Animator anim;

	void Awake() {
		anim = GetComponent<Animator>();
	}
	
	void Update() {

	}
	
	public void DamageEnemy(int damage) {
		enemyStats.health -= damage;
		if(enemyStats.health <= 0) {
			anim.SetBool("Alive", false);
			GameMaster.KillEnemy(this);
		}
	}
}
