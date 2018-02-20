using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
	public static int selecter = 0;		// Character selecter 0:kohaku, 1:yuko, 2:misaki

	public static int getSelecter(){
		return selecter;
	}

	static string[] charascene = {
		"CharacterProfile",
		"CharacterProfile",
		"CharacterProfile"
	};

	public static string getProfileScene(){
		return charascene [selecter];
	}

	public static int setCharaSelecter(int chara_num){
		if (chara_num >= 0 && chara_num <= 2) {
			selecter = chara_num;
		}
		return selecter;
	}
}
