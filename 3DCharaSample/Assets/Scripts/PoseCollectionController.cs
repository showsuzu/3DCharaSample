using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleApp.UI
{
	public class PoseCollectionController : MonoBehaviour {
		public static GameObject SelectCharacter;

		public static GameObject getSelectCharacter(){
			return SelectCharacter;
		}

		public static void setSelectCharacter(GameObject  obj){
			SelectCharacter = obj;
		}

		// Use this for initialization
		void Start () {
			GameObject.Find ("CharacterController").GetComponent<CharacterController> ().ChangeClothes (0);
		}
	}
}
