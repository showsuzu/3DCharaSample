using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleApp.UI
{
	public class ScrollControlleronPoseScene : MonoBehaviour {
		[SerializeField]
		RectTransform prefab = null;
		[SerializeField]
		RectTransform non_prefab = null;

		public string[] pose_txt = null;
		public string[] pose_name = null;

		// Use this for initialization
		void Start () {
			if (pose_txt != null && pose_txt.Length > 0) {
				for (int i = 0; i < pose_txt.Length; i++) {
					var item = GameObject.Instantiate (prefab) as RectTransform;
					item.SetParent (transform, false);

					var text = item.GetComponentInChildren<Text> ();
					text.text = pose_txt [i];

					Button button = item.GetComponentInChildren<Button> ();
					this.AddButtonEvent (button, i);
				}
			} else {
				var item = GameObject.Instantiate(non_prefab) as RectTransform;
				item.SetParent(transform, false);
			}
		}

		void AddButtonEvent(Button btn, int counter){
			// ボタンオブジェクトにクリック時のリスナーを追加する
			btn.onClick.AddListener(() => {
				this.PoseChanger(counter);
			});	
		}

		void PoseChanger(int num) {
			Debug.Log ("PlayButton on " + pose_name[num]);
		}

		// Update is called once per frame
		void Update () {

		}
	}
}
