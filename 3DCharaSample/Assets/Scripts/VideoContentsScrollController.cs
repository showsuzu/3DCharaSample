using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace SampleApp.UI
{
	public class VideoContentsScrollController : MonoBehaviour {
		[SerializeField]
		RectTransform prefab = null;

		// 動画は２種類のパターンがある想定でテーブルを持つようにする
		string[,] SP_tbl;
		string[,] PB_tbl;

		// Use this for initialization
		void Start () {
			// 動画はキャラクタ毎に２種類のパターンがある想定
			// 定義は、CharacterConstData.cs
			// TODO. 配列化したいところ
			int chara = GameController.getSelecter ();
			if (chara == 1) {
				Debug.Log ("for Yuko Proc");
				SP_tbl = CharacterConstData.yuko_SpecialMoveVideo;
				PB_tbl = CharacterConstData.yuko_PurchaseBonusVideo;
			} else if (chara == 2) {
				Debug.Log ("for Misaki Proc");
				SP_tbl = CharacterConstData.misaki_SpecialMoveVideo;
				PB_tbl = CharacterConstData.misaki_PurchaseBonusVideo;
			} else {
				Debug.Log ("for Kohaku Proc");
				SP_tbl = CharacterConstData.kohaku_SpecialMoveVideo;
				PB_tbl = CharacterConstData.kohaku_PurchaseBonusVideo;
			}

			// テーブルサイズを確認する
			if (SP_tbl.Length == 0 && PB_tbl.Length == 0) {
				// テーブルに何も設定されていなければ、専用の表示を行い、処理を終了する
				var _prefab = Resources.Load ("Prefabs/NothingNode", typeof(RectTransform)) as RectTransform;
				var item = GameObject.Instantiate (_prefab) as RectTransform;
				item.SetParent (transform, false);
				return;
			}

			// リストを生成
			// 使用するサムネイル、動画ファイルのフォルダは暫定的に固定にしてあるので注意
			Image image;
			Sprite pic;
			// Special Move Tableのメニュー作成
			for(int i=0; i < SP_tbl.GetLength(0); i++)
			{
				var item = GameObject.Instantiate(prefab) as RectTransform;
				item.SetParent(transform, false);

				image = item.GetChild (0).GetComponentInChildren<Image> ();
				pic = Resources.Load<Sprite> ("Movie/thumbnail/" + SP_tbl[i,0]);
				image.sprite = pic;

				var text = item.GetComponentInChildren<Text>();
				text.text = SP_tbl[i,1];

				Button button = item.GetComponentInChildren<Button>();
				this.AddButtonEvent(button, i, "SP");
			}
			// Video Tableのメニュー作成
			for(int i=0; i < PB_tbl.GetLength(0); i++)
			{
				var item = GameObject.Instantiate(prefab) as RectTransform;
				item.SetParent(transform, false);

				image = item.GetChild (0).GetComponentInChildren<Image> ();
				pic = Resources.Load<Sprite> ("Movie/thumbnail/" + PB_tbl[i,0]);
				image.sprite = pic;

				var text = item.GetComponentInChildren<Text>();
				text.text = PB_tbl[i,1];

				Button button = item.GetComponentInChildren<Button>();
				this.AddButtonEvent(button, i, "PB");
			}
		}

		void AddButtonEvent(Button btn, int counter, string kind){
			// ボタンオブジェクトにクリック時のリスナーを追加する
			if(kind == "SP"){
				btn.onClick.AddListener(() => {
					this.SPVideoPlaying(counter);
				});	
			} else {
				btn.onClick.AddListener(() => {
					this.PBVideoPlaying(counter);
				});	
			}
		}

		// 動画の再生
		// 各ボタンに、AddButtonEvent()で以下の処理をリスナー登録しているので、ボタンクリック時にいずれかが呼ばれる
		void SPVideoPlaying(int num) {
			GameObject.Find ("VideoPlayerController").GetComponent<SampleApp.UI.VideoPlayerController> ().VideoStart ("Movie/" + SP_tbl [num, 2]);
		}

		void PBVideoPlaying(int num) {
			GameObject.Find ("VideoPlayerController").GetComponent<SampleApp.UI.VideoPlayerController> ().VideoStart ("Movie/" + PB_tbl [num, 2]);
		}
	}
}
