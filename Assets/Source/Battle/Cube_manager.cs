using UnityEngine;
using System;
using System.Collections;

public class Cube_manager : MonoBehaviour {
	//Mode change
	public static float easy_mode = 1.0f;
	public static float normal_mode = 0.5f;
	public static float hard_mode = 0.3f;
	public float mode = easy_mode;

	private GameObject main_manager;
	private Main main;
	private GameObject cube;
	private GameObject player;

	//map情報
	static int N = 6 * 8;
	static int MAXNUM = 20;
	public int[] map;
	int[,] edge = new int[N,N];
	int[] flag = new int[N];

	/*連想配列テーブル*/
	public Hashtable table;


/*CubeObject*/
	public GameObject Cube0;
	public GameObject Cube1;
	public GameObject Cube2;
	public GameObject Cube3;
	public GameObject Cube4;
	public GameObject Cube5;
	
	public GameObject Cube6;
	public GameObject Cube7;
	public GameObject Cube8;
	public GameObject Cube9;
	public GameObject Cube10;
	public GameObject Cube11;
	
	public GameObject Cube12;
	public GameObject Cube13;
	public GameObject Cube14;
	public GameObject Cube15;
	public GameObject Cube16;
	public GameObject Cube17;
	
	public GameObject Cube18;
	public GameObject Cube19;
	public GameObject Cube20;
	public GameObject Cube21;
	public GameObject Cube22;
	public GameObject Cube23;
	
	public GameObject Cube24;
	public GameObject Cube25;
	public GameObject Cube26;
	public GameObject Cube27;
	public GameObject Cube28;
	public GameObject Cube29;
	
	public GameObject Cube30;
	public GameObject Cube31;
	public GameObject Cube32;
	public GameObject Cube33;
	public GameObject Cube34;
	public GameObject Cube35;
	
	public GameObject Cube36;
	public GameObject Cube37;
	public GameObject Cube38;
	public GameObject Cube39;
	public GameObject Cube40;
	public GameObject Cube41;
	
	public GameObject Cube42;
	public GameObject Cube43;
	public GameObject Cube44;
	public GameObject Cube45;
	public GameObject Cube46;
	public GameObject Cube47;

	void BirthCube() {
		table = new Hashtable ();
		table.Add ("Cube0", Cube0);
		table.Add ("Cube1", Cube1);
		table.Add ("Cube2", Cube2);
		table.Add ("Cube3", Cube3);
		table.Add ("Cube4", Cube4);
		table.Add ("Cube5", Cube5);
		
		table.Add ("Cube6", Cube6);
		table.Add ("Cube7", Cube7);
		table.Add ("Cube8", Cube8);
		table.Add ("Cube9", Cube9);
		table.Add ("Cube10", Cube10);
		table.Add ("Cube11", Cube11);
		
		table.Add ("Cube12", Cube12);
		table.Add ("Cube13", Cube13);
		table.Add ("Cube14", Cube14);
		table.Add ("Cube15", Cube15);
		table.Add ("Cube16", Cube16);
		table.Add ("Cube17", Cube17);
		
		table.Add ("Cube18", Cube18);
		table.Add ("Cube19", Cube19);
		table.Add ("Cube20", Cube20);
		table.Add ("Cube21", Cube21);
		table.Add ("Cube22", Cube22);
		table.Add ("Cube23", Cube23);
		
		table.Add ("Cube24", Cube24);
		table.Add ("Cube25", Cube25);
		table.Add ("Cube26", Cube26);
		table.Add ("Cube27", Cube27);
		table.Add ("Cube28", Cube28);
		table.Add ("Cube29", Cube29);
		
		table.Add ("Cube30", Cube30);
		table.Add ("Cube31", Cube31);
		table.Add ("Cube32", Cube32);
		table.Add ("Cube33", Cube33);
		table.Add ("Cube34", Cube34);
		table.Add ("Cube35", Cube35);

		table.Add ("Cube36", Cube36);
		table.Add ("Cube37", Cube37);
		table.Add ("Cube38", Cube38);
		table.Add ("Cube39", Cube39);
		table.Add ("Cube40", Cube40);
		table.Add ("Cube41", Cube41);
		
		table.Add ("Cube42", Cube42);
		table.Add ("Cube43", Cube43);
		table.Add ("Cube44", Cube44);
		table.Add ("Cube45", Cube45);
		table.Add ("Cube46", Cube46);
		table.Add ("Cube47", Cube47);
	}
	
	// Use this for initialization
	void Start () {
		main_manager = GameObject.Find("Main_manager");
		player = GameObject.Find("Player");
		main = main_manager.gameObject.GetComponent<Main>();
		BirthCube();
	}
	
	// Update is called once per frame
	void Update () {
		if (main.phase == 0) {
			make_map();
			Produce_damagefloor ();
			main.phase=1;
		}
	}

	/*ダメージ床生成関数*/
	private void Produce_damagefloor () {
		int i;
		
		for (i=0; i<N; i++) {
			if(map[i]==1){
				/*地雷設置場所(map[i]=1)をダメージ床にかえる*/
				string cube_name= ("Cube" + i.ToString ());
				cube = (GameObject)table[cube_name];
				cube.GetComponent<Cube> ().change_danger ();
			}
		}
	}

	public void change_mode(string mode) {
		if (mode == "easy") {
			this.mode = easy_mode;
		} else if (mode == "normal") {
			this.mode = normal_mode;
		} else if (mode == "hard") {
			this.mode = hard_mode;
		}

	}

	//mapをグラフとして作成
	private void makeGraph(){
		int i, j, r , playerPos= 0;
		map = new int[N];
		edge = new int[N,N];
		flag = new int[N];
		Vector3 p_position =  player.transform.position;
		
		Array.Clear(map,0,map.Length);
		Array.Clear(edge,0,edge.Length);
		Array.Clear(flag,0,flag.Length);

		playerPos = main.get_posNum(p_position);
		
		/*MAXNUM個地雷を置く（グラフにおける点を消去する）*/
		for (i=0; i<MAXNUM; i++) {
			r = UnityEngine.Random.Range(0,48);
			//点が重複する、またはプレイヤーの位置と重複する場合
			if(map[r]==1 || r==playerPos){
				i--;
				continue;
			}
			map[r] = 1;
		}


		/*edgeの設定*/
		for (i = 0; i < N; i++){
			/*点iが孤立点であれば次の点へ*/
			if (map[i] == 1) continue;
			for (j = 0; j < N; j++){
				/*点jが孤立点であれば次の点へ*/
				if (map[j] == 1) continue;
				/*点jが点iと隣接していれば辺を設定*/
				if(j == i-6 || j == i+6
				   || (j == i-1 && (i+1)%6 != 1)
				   || (j == i+1 && (i+1)%6 != 0) ){
					edge[i,j] = 1;
				}
			}
		}
	}

	//深さ優先探索により連結グラフかを見分ける
	private void dfs(int num){
		int i;
		/*点numが既に到達済みの場合*/
		if(flag[num]==1) return;
		
		/*点numを到達済みとして保存*/
		flag[num] = 1;
		
		/*点numから辺が伸びている点へ移動*/
		for(i = 0; i < N; i++){
			if(edge[num,i]==1){ /*点num から 点i へ辺がある*/
				dfs(i); /*点iへ移動*/
			}
		}
	}

	//連結グラフを出力する
	private void make_map(){
		int i;

		while(true){
			makeGraph();
			dfs (0);

			for(i=0;i<N;i++){
				if(flag[i]==0 && map[i]==0){
					break;
				}
			}
			if(i == N) break;
		}
		//connected
	}
	
	public int return_N(){ return N;}
	
	public int return_MAXNUM(){ return MAXNUM;}
	
	
	public void change_cubeColor(int cube_num){
		String cube_name = ("Cube"+cube_num.ToString());
		cube = (GameObject)table[cube_name];
		cube.GetComponent<Cube>().StartCoroutine("changeColor");
	}
	
	public Vector3 get_CubePosition(String cube_name){
		cube = (GameObject)table[cube_name];
		return cube.GetComponent<Cube> ().cubePosition ();
	}
}