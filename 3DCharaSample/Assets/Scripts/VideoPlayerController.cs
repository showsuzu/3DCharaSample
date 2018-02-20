using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

namespace SampleApp.UI
{
	public class VideoPlayerController : MonoBehaviour {
		VideoPlayer videoPlayer;

		// Use this for initialization
		void Start () {
			// シーン内にあらかじめVideoプレイヤーを定義しておく必要がある
			// 定義しない場合は、ここでVideo Playerを追加する方法でも良い
			// ここでVideo Playerを追加する場合はAudio sourceも追加する必要がある
			var obj = GameObject.Find ("Video Player");
			videoPlayer = obj.GetComponent<VideoPlayer> ();
		}

		public void VideoStart(string videoclipfile){
			Debug.Log ("VdeoStart!");
			StartCoroutine (VideoPlayStart (videoclipfile));
		}

		private IEnumerator VideoPlayStart(string videoclipfile)
		{
			Application.runInBackground = true;

			var audioSource = videoPlayer.GetComponent<AudioSource>();
			VideoClip vclip = (VideoClip)Resources.Load (videoclipfile);
			// 再生する動画のサイズに合わせてRender Textureを準備する
			RenderTexture _renderTexture = new RenderTexture ((int)vclip.width, (int)vclip.height, 24);

			videoPlayer.playOnAwake = false;
			audioSource.playOnAwake = false;
			// Video Playerの設定
			videoPlayer.source = VideoSource.VideoClip;
			videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
			videoPlayer.renderMode = VideoRenderMode.RenderTexture;
			videoPlayer.EnableAudioTrack(0, true);
			videoPlayer.SetTargetAudioSource(0, audioSource);
			videoPlayer.clip = vclip;
			videoPlayer.targetTexture = _renderTexture;

			// Video Playerの準備（完了まで待つ）
			videoPlayer.Prepare();
			while (!videoPlayer.isPrepared)
				yield return null;

			// Video Playerの準備が完了した後
			// プレハブから動画再生用のRawImageをロードする
			var _prefab = Resources.Load("Prefabs/MovieRawImage");
			GameObject _rawImg = Instantiate(_prefab, Vector3.zero, Quaternion.Euler(0, 0, 0)) as GameObject;
			// Canvasが親になるようにtransformを代入する
			GameObject cvs = GameObject.Find("Canvas");
			_rawImg.transform.parent = cvs.transform;
			_rawImg.transform.localPosition = Vector3.zero;
			_rawImg.transform.localScale = new Vector3 (1, 1, 1);

			// RawImageに動画再生用のRenderTextureを設定する
			RawImage screen = _rawImg.GetComponent<RawImage>();
			screen.texture = _renderTexture;

			// 動画の再生開始（再生完了まで待つ）
			videoPlayer.Play();
			audioSource.Play();
			while (videoPlayer.isPlaying)
				yield return null;

			// 動画の再生完了
			videoPlayer.clip = null;
			videoPlayer.targetTexture = null;
			GameObject.Destroy (_rawImg.gameObject);
		}
	}
}
