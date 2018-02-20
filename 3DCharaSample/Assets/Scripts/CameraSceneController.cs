using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleApp.UI
{
	public class CameraSceneController : MonoBehaviour {

		// Use this for initialization
		void Start () {
			// 以下の処理で、プレハブからキャラクタを持ってきて表示する
			// 他の画面の状況は加味せずに、単純に表示するのみ
			GameObject.Find ("CharacterController").GetComponent<CharacterController> ().ChangeClothes (0);
		}
	}
}
