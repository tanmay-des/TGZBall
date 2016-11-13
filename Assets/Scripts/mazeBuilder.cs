using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class mazeBuilder : MonoBehaviour {

	public int rows = 20;
	public int columns = 20;
	public float spacing = 0.50f;
	public GameObject block;

	public List<Vector3> gridPositions = new List<Vector3> ();
	private List<List<bool> > maze = new List<List<bool> > ();

	private bool valid(int i, int j){
		return i > 0 && j > 0 && i < maze.Count - 1 && j < maze[0].Count - 1;
	}

	void r_dfs(int i, int j){
		int[] dx = { 0, 2, 0, -2 };
		int[] dy = { 2, 0, -2, 0 };
		List<KeyValuePair<int, int> > c_list = new List<KeyValuePair<int, int> >();
		for (int k = 0; k < 4; k++) {
			int n_i = i + dx [k];
			int n_j = j + dy [k];
			if (valid (n_i, n_j) && maze [n_i] [n_j] == true)
				c_list.Add (new KeyValuePair<int, int> (n_i, n_j));
		}
		if (c_list.Count > 0) {
			int r = Random.Range (0, c_list.Count);
			int wi = (i + c_list [r].Key) >> 1;
			int wj = (j + c_list [r].Value) >> 1;
			maze [wi] [wj] = maze [c_list [r].Key] [c_list [r].Value] = false;
			r_dfs (c_list [r].Key, c_list [r].Value);
			r_dfs (i, j);
		}
	}

	void init_maze(){
		int r = (rows << 1) + 1;
		int c = (columns << 1) + 1;
		for (int i = 0; i < r; i++){
			List<bool> cc = new List<bool>();
			for (int j = 0; j < c; j++)
				cc.Add (true);
			maze.Add (cc);
		}
		
	}
	void MainBoardInitate(){
		GameObject mz = GameObject.FindGameObjectWithTag ("MazeBoard");
		gridPositions.Clear ();
		for (int x = 0; x < 2 * columns + 1; x++) {

			for (int z = 0; z < 2 * rows + 1 ; z++) {
				if (maze [x] [z]) {
					GameObject instance = Instantiate (block,new Vector3(0,0,0),Quaternion.identity) as GameObject;
					instance.transform.SetParent (mz.transform);
					instance.transform.Translate (new Vector3 (x+mz.transform.position.x, 1, z+mz.transform.position.z) );
				}
			}
		}


	}
		

	// Use this for initialization
	void Start () {
		init_maze ();
		r_dfs (1, 1);
		MainBoardInitate ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
