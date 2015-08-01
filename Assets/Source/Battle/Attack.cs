using UnityEngine;
using System.Collections;

/*情報をまとめるクラス*/
public class attack_information {
	public float g_time;
	public Vector3 position;
	public string abilityname;
	public string P_or_E = "";
	public GameObject name;
}

/*攻撃のエフェクト、および当たり判定などの判定を生成するプログラム*/
public class Attack : MonoBehaviour {
	
	public GameObject fire1Prehab;

	public GameObject p_attackCube;
	public GameObject e_attackCube;
	public GameObject p_cureCube;
	public GameObject e_cureCube;
	public GameObject damage_area;

	/*
	fire boom
	thunder lightning



	 */

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	/*ファイア周囲1*/
	public void fire_attack(attack_information atk) {
		int i, j;
		
		/*次の攻撃までの間隔*/
		float intervalTime = 1.0f;
		/*プレイヤーが前の攻撃を行ったときからの時間*/
		float gametime = atk.g_time;
		/*Playerの初期位置を保存する*/
		Vector3 Player_position = atk.position;
		/*effect time*/
		float attacktime;
		
		//.Log (intervalTime);
		
		if (gametime >= intervalTime) {
			//if(true) {
			/*初期値は左斜め下*/
			i = -50;
			j = -50;
			
			/*positionを左斜め下から開始するようにする*/
			atk.position.x -= 50f;
			atk.position.y -= 50f;
			
			while(i <= 50) {
				while(j <= 50) {
					if(atk.position != Player_position) {
						GameObject effect = (GameObject)Instantiate (fire1Prehab, atk.position, transform.rotation);
						attacktime = effect.GetComponent<ParticleSystem>().startLifetime;
						GameObject attack_block;

						attack_block = (GameObject)Instantiate (p_attackCube, atk.position, transform.rotation);

						Destroy(attack_block,attacktime);
						
					}
					atk.position.y += 50f;
					j = j + 50;
				}
				/*yを初期値に戻す*/
				j = -50;
				atk.position.y -= 150f;
				
				atk.position.x += 50f;
				i = i + 50;
			}
			//技が終了したらプレイヤーの時間を0に戻す
			atk.name.GetComponent<Player>().change_p_time(0f);

		}
		return;
	}
}
