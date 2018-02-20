using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleApp.UI
{
	public class ScrollController : MonoBehaviour {

		[SerializeField]
		RectTransform prefab = null;

		// テーブル定義はCharacterConstData.csで行なっているが
		// 音声コンテンツはテーブルをpublic宣言して、UnityのInspector画面で設定できるようにしてみた
		// （編集が面倒）
		public string[] kohaku_txt;
		public string[] kohaku_voicename;
		public string[] misaki_txt;
		public string[] misaki_voicename;
		public string[] yuko_txt;
		public string[] yuko_voicename;

		string[] txt_tbl;
		string[] voicename_tbl;

		void Start () 
		{
			// TODO. 配列化したいところ
			int chara = GameController.getSelecter ();
			if (chara == 1) {
				txt_tbl = yuko_txt;
				voicename_tbl = yuko_voicename;
			} else if (chara == 2) {
				txt_tbl = misaki_txt;
				voicename_tbl = misaki_voicename;
			} else {
				txt_tbl = kohaku_txt;
				voicename_tbl = kohaku_voicename;
			}

			// テーブルサイズを確認する
			if (txt_tbl.Length == 0 || voicename_tbl.Length == 0) {
				// テーブルに何も設定されていなければ、専用の表示を行い、処理を終了する
				var _prefab = Resources.Load ("Prefabs/NothingNode", typeof(RectTransform)) as RectTransform;
				var item = GameObject.Instantiate (_prefab) as RectTransform;
				item.SetParent (transform, false);
				return;
			}

			// リストを生成
			for(int i=0; i < txt_tbl.Length; i++)
			{
				var item = GameObject.Instantiate(prefab) as RectTransform;
				item.SetParent(transform, false);

				var text = item.GetComponentInChildren<Text>();
				text.text = txt_tbl[i];

				Button button = item.GetComponentInChildren<Button>();
				this.AddButtonEvent(button, i);
			}
		}

		void AddButtonEvent(Button btn, int counter){
			// ボタンオブジェクトにクリック時のリスナーを追加する
			btn.onClick.AddListener(() => {
				this.VoicePlay(counter);
			});	
		}

		// 音声の再生
		// 各ボタンに、AddButtonEvent()で以下の処理をリスナー登録しているので、ボタンクリック時に呼ばれる
		void VoicePlay(int num) {
			Debug.Log ("VoicePlayButton on " + voicename_tbl[num]);
			AudioSource audio = (AudioSource)this.GetComponentInChildren<AudioSource> ();
			AudioClip clip = (AudioClip)Resources.Load ("Voices/" + voicename_tbl[num]);
			audio.PlayOneShot (clip);
		}
	}
}
