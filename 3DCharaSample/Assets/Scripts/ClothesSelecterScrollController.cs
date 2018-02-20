using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleApp.UI
{
	public class ClothesSelecterScrollController : MonoBehaviour {
		// 保有している3Dモデルを衣装と称してリスト表示＆ボタン化して、選択＆モデル変更できるようにする
		// 前提条件
		// ・各名称および、説明データはあらかじめ、CharacterConstData.csに設定されているものとする
		// ・各ポーズは3DモデルでAnimatorとして定義されているものとする
		// ・Animatorで定義されているポーズ名称と、CharacterConstData.csで定義されている名称は一致しなければならない
		[SerializeField]
		RectTransform prefab = null;

		string[] _txt_tbl;
		private int NowClothesNo;

		// Use this for initialization
		void Start () {
			// 最初は０番から
			NowClothesNo = 0;
			//キャラクタ毎にポーズテーブルの持ちわけ
			int _chara = GameController.getSelecter ();
			if (_chara == 1) {
				// Yuko
				_txt_tbl = CharacterConstData.yuko_clothes_txt;
			} else if (_chara == 2) {
				// Misaki
				_txt_tbl = CharacterConstData.misaki_clothes_txt;
			} else {
				// Kohaku
				_txt_tbl = CharacterConstData.kohaku_clothes_txt;
			}

			for(int i=0; i<_txt_tbl.Length; i++)
			{
				var _item = GameObject.Instantiate(prefab) as RectTransform;
				_item.SetParent(transform, false);

				var _text = _item.GetComponentInChildren<Text> ();
				_text.text = _txt_tbl[i];

				Button _button = _item.GetComponent<Button> ();
				this.AddButtonEvent (_button, i);

				// 最初のモデルを選択状態を明示するために、塗りつぶし色を変更する
				if (i == 0) {
					_item.GetComponentInChildren<Image> ().color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 160.0f / 255.0f, 255.0f / 255.0f);
				}
			}
		}

		void AddButtonEvent(Button btn, int i){
			// ボタンオブジェクトにクリック時のリスナーを追加する
			btn.onClick.AddListener(() => {
				this.ChangeClothes(i);
			});	
		}

		void ChangeClothes(int tblOffset){
			// キャラクタモデルを更新
			if (NowClothesNo == tblOffset) {
				return;
			}
			GameObject.Find ("CharacterController").GetComponent<CharacterController> ().ChangeClothes (tblOffset);

			// 番号を更新
			NowClothesNo = tblOffset;

			// ポーズを更新（衣装を変更する前のポーズに）
			GameObject.Find ("PoseContent").GetComponent<PoseSelecterScrollController>().PoseRefresh(NowClothesNo);

			Button[] _gobjs = GameObject.Find ("ClothesContent").GetComponentsInChildren<Button> ();
			if(_gobjs == null){
				return;
			}

			// リストを一通りシークして塗りつぶし色を設定し直す
			int i = 0;
			foreach(Button _gobj in _gobjs){
				if(i == tblOffset){
					_gobj.GetComponentInChildren<Image>().color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 160.0f / 255.0f, 255.0f / 255.0f);
				} else {
					_gobj.GetComponentInChildren<Image>().color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
				}
				i++;
			}
		}
	}
}
