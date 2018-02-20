﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

	// 変数
	public GameObject CharaObj;			// Characterを表示するためのオブジェクトを指定

	public GameObject SelectCharacter;	// 何も指定しなくて良い

	Animator _animator;

	public GameObject getSelectCharacter(){
		return SelectCharacter;
	}

	public void setSelectCharacter(GameObject  obj){
		SelectCharacter = obj;
	}

	public void ChangeClothes(int num){
		// 3Dモデルを変更する処理
		// 3Dモデルを変更するため、ポーズも同時に表示し直す必要がある
		string _prefabName;
		// 選択されているキャラクタ番号を取得する
		int _chara = GameController.getSelecter ();

		switch (_chara) {
		case 1:
			//Yuko
			if (num >= SampleApp.UI.CharacterConstData.YukoClothesTbl.Length) {
				num = 0;
			}
			_prefabName = SampleApp.UI.CharacterConstData.YukoClothesTbl[num];
			break;
		case 2:
			//Misaki
			if (num >= SampleApp.UI.CharacterConstData.MisakiClothesTbl.Length) {
				num = 0;
			}
			_prefabName = SampleApp.UI.CharacterConstData.MisakiClothesTbl[num];
			break;
		default:
			//Kohaku
			if (num >= SampleApp.UI.CharacterConstData.KohakuClothesTbl.Length) {
				num = 0;
			}
			_prefabName = SampleApp.UI.CharacterConstData.KohakuClothesTbl[num];
			break;
		}
		CharaDisp (_prefabName);
	}

	// Update is called once per frame
	void CharaDisp (string prN) {
		// キャラクタのプレハブをインスタンス化して表示する
		string _pn = "Prefabs/" + prN;
		var _prefab = Resources.Load(_pn);
		GameObject _item = Instantiate(_prefab, Vector3.zero, Quaternion.Euler(0, 180, 0)) as GameObject;

		//			GameObject _gameObj = GameObject.Find ("CharacterObject");
		// 表示中のモデルを削除し
		foreach (Transform _delobj in CharaObj.transform) {
			GameObject.Destroy (_delobj.gameObject);
		}
		// CharacterObjectが親になるようにtransformを代入する
		_item.transform.parent = CharaObj.transform;
		_item.transform.localPosition = Vector3.zero;
		_item.transform.localScale = new Vector3 (1, 1, 1);	// サイズを等倍に

		_animator = (Animator)_item.GetComponentInChildren<Animator> ();
		SelectCharacter = _item;
	}

	public void ChangePose(string name){
		// 念のための判定
		if (_animator == null) {
			_animator = (Animator)SelectCharacter.GetComponentInChildren<Animator> ();
			if (_animator == null)
				return;
		}
		// ポーズを変更
		_animator.CrossFadeInFixedTime (name, 0.4f, 0);

	}
}
