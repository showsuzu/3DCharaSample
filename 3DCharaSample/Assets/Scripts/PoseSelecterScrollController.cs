using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleApp.UI
{
	public class PoseSelecterScrollController : MonoBehaviour {
		// 保有している3Dモデルのポーズをリスト表示＆ボタン化して、選択＆ポーズを変更できるようにする
		// 前提条件
		// ・各ポーズの名称および、説明データはあらかじめ、CharacterConstData.csに設定されているものとする
		// ・各ポーズは3DモデルでAnimatorとして定義されているものとする
		// ・Animatorで定義されているポーズ名称と、CharacterConstData.csで定義されている名称は一致しなければならない
		[SerializeField]
		RectTransform prefab = null;

		string[] _txt_tbl;
		string[] _name_tbl;
		public static int NowPoseNo;

		public void PoseRefresh(int chloth_no){
			// cloth_noは衣装（モデル）のオフセット値で、ポーズのテーブルを持ち替える時に使用するが現状は利用しない
			// すでにモデルの更新はClothesSelecterScrollControllerクラスで行われているので、ポーズ更新する
			ChangePose (NowPoseNo);
		}

		void Start ()
		{
			NowPoseNo = 0;
			// まずは、メニューを構築する（メニューを構築する親オブジェクトにこのスクリプトが登録されれいることが前提）
			this.selectTbl();
			this.setMenu ();
		}

		void ChangePose(int tblOffset){
			// ポーズを更新
			GameObject.Find ("CharacterController").GetComponent<CharacterController> ().ChangePose (_name_tbl [tblOffset]);

			Button[] _gobjs = GameObject.Find ("PoseContent").GetComponentsInChildren<Button> ();
			if(_gobjs == null){
				return;
			}
			int i = 0;
			foreach(Button _gobj in _gobjs){
				if(i == tblOffset){
					_gobj.GetComponentInChildren<Image>().color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 160.0f / 255.0f, 255.0f / 255.0f);
				} else {
					_gobj.GetComponentInChildren<Image>().color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
				}
				i++;
			}
			NowPoseNo = tblOffset;
		}

		void selectTbl(){
			//キャラクタ毎にポーズテーブルの持ちわけ
			//実質同じ
			int _chara = GameController.getSelecter ();
			if (_chara == 1) {
				// Yuko
				_txt_tbl = CharacterConstData.pose_txt;
				_name_tbl = CharacterConstData.pose_Tbl;
			} else if (_chara == 2) {
				// Misaki
				_txt_tbl = CharacterConstData.pose_txt;
				_name_tbl = CharacterConstData.pose_Tbl;
			} else {
				// Kohaku
				_txt_tbl = CharacterConstData.pose_txt;
				_name_tbl = CharacterConstData.pose_Tbl;
			}
		}

		void setMenu(){
			for(int i=0; i<_txt_tbl.Length; i++)
			{
				var _item = GameObject.Instantiate(prefab) as RectTransform;
				_item.SetParent(transform, false);

				var _text = _item.GetComponentInChildren<Text> ();
				_text.text = _txt_tbl[i];

				Button _button = _item.GetComponent<Button> ();
				this.AddButtonEvent (_button, i);

				// 最初のポーズを選択状態にする
				if (i == 0) {
					_item.GetComponentInChildren<Image> ().color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 160.0f / 255.0f, 255.0f / 255.0f);
				}
			}
		}

		void AddButtonEvent(Button btn, int i){
			// ボタンオブジェクトにクリック時のリスナーを追加する
			btn.onClick.AddListener(() => {
				this.ChangePose(i);
			});	
		}
	}
}
