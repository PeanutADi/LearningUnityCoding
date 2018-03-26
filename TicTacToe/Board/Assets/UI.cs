using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Button b1, b2, b3, b4, b5, b6, b7, b8, b9;

	public Image img;  
	public Image img1;  
	public Image img2; 

	private int result = 0; //1代表玩家一，2代表玩家二

	private int turn = 1; //1代表玩家一的回合,0玩家二

	private int[,] board = new int[3,3];//board数据

	// Use this for initialization
	void Reset(){
		b1.image = img1;
		turn = 1;
		result = 0;
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				board [i, j] = 0;
			}
		}
	}

	void Start () {
		Reset ();
	}

	void ClickByP1(Button b){
		b.image = img1;
	}

	void ClickByP2(Button b){
		b.image = img2;
	} 

	void OnGUI(){
		if (b1)
			b1.image = img1;
		//GUI.Label (new Rect (230, 120, 100, 100), "Welcomt to TicTacToe!",img);
		//GUI.Button ();
	}
}
