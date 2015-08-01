using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	/*Enemy情報*/
	private int HP = 5;

	//command
	private const int UP = 0;
	private const int DOWN = 1;
	private const int RIGHT = 2;
	private const int LEFT = 3;

	Vector3 position;

	//Object & Script
	private GameObject player;
	private GameObject main_manager;
	private Main main;
	private Cube_manager cube_manager;
	private GameObject cube;

	public GameObject bombPrefab;
	
	//flag	
	bool returnflag = false;
	bool moveEnd = false;

	//timer
	private const float interval = 0.5f;
	private float timer = interval;
	private int count = 0;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		main_manager = GameObject.Find("Main_manager");
		main = main_manager.GetComponent<Main> ();
		cube_manager = (GameObject.Find("Cube_manager")).GetComponent<Cube_manager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (main.phase==1){
			timer -= Time.deltaTime;
			if(timer <= 0){
				position = transform.position;
				if(count==0)move();
				else moveInitial();
				timer = interval;
			}
			if(moveEnd) main.phase = 2;
		}
	}
	
	//move from Cube0 to Cube47
	void move(){
		int posNum;
		
		if(!returnflag){
			if (position.x < 250){
					position.x += 50f;
			}else if(position.y > -350){
				position.y -= 50f;
				returnflag = !returnflag;
			}
		}else{
			if (position.x > 0){
				position.x -= 50f;
			}else if(position.y > -350){
				position.y -= 50f;
				returnflag = !returnflag;
			}
		}
		
		gameObject.transform.position = position;
		
		posNum = main.get_posNum(position);
		
		if(posNum-1 == main.get_posNum(player.transform.position)) count=8;
		if(cube_manager.map[posNum] == 1) cube_manager.change_cubeColor(posNum);
	}
	
	void moveInitial(){
		if (count > 1) position.y += 50f;
		else if(count == 1) position.x -= 50f;
		gameObject.transform.position = position;
		count--;
		if(count==0) moveEnd = true;
	}
	
		/*被ダメ判定　被ダメエフェクト*/
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag("p_attackCube")) {
			print("Enemy Damaged");
			HP -= 1;
			if(HP == 0) {
				//Instantiate(explosionPrehab,transform.position,transform.rotation);
				Destroy(gameObject);
				Application.LoadLevel ("ClearResult");
			}
		}
	}
	
}