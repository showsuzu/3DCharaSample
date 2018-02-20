using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSceneController : MonoBehaviour {
	static bool soundSelect;
//	static Text btnText;
//	static GameObject vObj, sObj;
	// Use this for initialization
	void Start () {
		// 音と動画の再生シーンだが、シーン開始時は必ず音声再生から始める
		GameObject.Find ("listChangeButton").GetComponentInChildren<Text> ().text = "動画再生画面へ";
		GameObject.Find ("VideosScrollView").SetActive (false);

		// 以下オブジェクトを保持しても、ボタンを処理する親オブジェクトが異なるため、処理時はかならずNullとなる
//		vObj = GameObject.Find ("VideosScrollView");
//		sObj = GameObject.Find ("SoundsScrollView");
//		vObj.SetActive (false);
		soundSelect = true;
	}

	public void changeDisp(){
		if(soundSelect){
			// true時は音再生時なので、動画に切り替える
			GameObject.Find ("listChangeButton").GetComponentInChildren<Text> ().text = "音声再生画面へ";
			GameObject.Find ("SoundsScrollView").SetActive (false);
			GameObject.Find ("Canvas").transform.Find ("VideosScrollView").gameObject.SetActive (true);
			soundSelect = false;
		} else {
			// false時は動画再生時なので、音に切り替える
			GameObject.Find ("listChangeButton").GetComponentInChildren<Text> ().text = "動画再生画面へ";
			GameObject.Find ("Canvas").transform.Find ("SoundsScrollView").gameObject.SetActive (true);
			GameObject.Find ("VideosScrollView").SetActive (false);
			soundSelect = true;
		}
	}
}
