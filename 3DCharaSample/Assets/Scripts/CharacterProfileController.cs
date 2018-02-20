using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleApp.UI
{
	public class CharacterProfileController : MonoBehaviour {

		// Use this for initialization
		void Start () {
			GameObject _gameObj;
			string _objName;
			int _chara = GameController.getSelecter ();	// 選択されているキャラクタ番号

			// プロフィールの表示
			for (int i = 0; i < CharacterConstData.textObjElement.Length; i++) {
				_objName = CharacterConstData.textObjElement [i];
				_gameObj = GameObject.Find (_objName);
				var textObj = _gameObj.GetComponentInChildren<Text> ();
				//TODO かっこ悪い。各配列を配列に代入できれば良い
				switch (i) {
					case 0:
						textObj.text = CharacterConstData.charaNameTbl [_chara];
						break;
					case 1:
						textObj.text = CharacterConstData.birthdayTbl [_chara];
						break;
					case 2:
						textObj.text = CharacterConstData.bloodTypeTbl [_chara];
						break;
					case 3:
						textObj.text = CharacterConstData.descriptionTbl [_chara];
						break;
				}
			}

			// キャラクタの表示
			GameObject.Find ("CharacterController").GetComponent<CharacterController> ().ChangeClothes (0);
		}

	}
}
