using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : BaseButtonController {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnClick(string objectName)
	{
		// 渡されたオブジェクト名で処理を分岐
		//（オブジェクト名はどこかで一括管理した方がいいかも）
		// ヘッダーのボタン判定
		if ("BackButton".Equals (objectName)) {
			this.gotoProfileScene ();
		} else if ("SettingsButton".Equals (objectName)) {
			this.GoToSettings ();
		}
		// フッターのボタン判定
		else if ("Button1".Equals (objectName)) {
			this.OnClickButton1 ();
		} else if ("Button2".Equals (objectName)) {
			this.OnClickButton2 ();
		} else if ("Button3".Equals (objectName)) {
			this.OnClickButton3 ();
		}
		// 音声、動画再生画面用
		else if ("listChangeButton".Equals (objectName)) {
			// for PhraseAndScene 画面用のボタン処理
			this.gotoChangePlayer ();
		}
		// ポーズ、衣装変更用スライダーボタンの処理
		else if ("ClothesCHGButton".Equals(objectName)) {
			this.clothesClick();
		} else if ("PoseCHGButton".Equals(objectName)) {
			this.poseClick();
		}

		else {
			throw new System.Exception("Not implemented!!");
		}
	}

	// キャラクタセレクターボタン処理
	public void OnClickKohaku() {
		Debug.Log ("Go to the Kohaku status!!");
		GameController.setCharaSelecter (0);
		this.gotoProfileScene ();
	}
	public void OnClickYuko() {
		Debug.Log ("Go to the Yuko status!!");
		GameController.setCharaSelecter (1);
		this.gotoProfileScene ();
	}
	public void OnClickMisaki() {
		Debug.Log ("Go to the Misaki status!!");
		GameController.setCharaSelecter (2);
		this.gotoProfileScene ();
	}

	// ヘッダーボタンの処理
	public void GoToSettings() {
		SceneManager.LoadScene("SettingScene");	// カメラ画面に遷移
	}

	// フッターボタンの処理
	public void OnClickButton1() {
		Debug.Log ("Back Button1 Click");
		SceneManager.LoadScene("CameraScene");	// カメラ画面に遷移
	}
	public void OnClickButton2() {
		Debug.Log ("Back Button2 Click");
		SceneManager.LoadScene("SoundAndVideoPlayer");	// 
	}
	public void OnClickButton3() {
		Debug.Log ("Back Button3 Click");
		SceneManager.LoadScene("PoseCollection");	// ポーズ画面に遷移
	}
		

	// ViewerSelecter 画面用のボタン処理
	public void gotoPoseScene(){
		SceneManager.LoadScene("PoseCollection");	// キャラクタのポーズビューワー画面に遷移
	}

	// for 再生画面切り替え用のボタン処理
	public void gotoChangePlayer(){
		GameObject.Find ("SceneController").GetComponent<PlayerSceneController> ().changeDisp ();
	}

	// キャラクタプロフィール画面への遷移
	public void gotoProfileScene() {
		string sc = GameController.getProfileScene ();
		SceneManager.LoadScene(sc);	// キャラクタ画面に遷移
	}

	// スライダーの制御
	public void poseClick(){
		SliderController.poseClick ();
	}
	public void clothesClick(){
		SliderController.clothesClick ();
	}

}
