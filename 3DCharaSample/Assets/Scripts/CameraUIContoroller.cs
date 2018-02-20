using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine.Events;

namespace SampleApp.UI
{
	public class CameraUIContoroller : MonoBehaviour {

		public UnityEvent OnCompleteCapture;
		public UnityEvent OnFailCapture;

		public Camera Cam;

#if UNITY_IOS
		[DllImport("__Internal")]
		private static extern void _PlaySystemShutterSound();
#endif

		public void OnClickCapture(){
			Debug.Log ("Press Capture Button!!");
			StartCoroutine(_CaptureScreen());
		}


		private IEnumerator _CaptureScreen(){
			yield return new WaitForEndOfFrame();

			// 各デバイスでシャッター音をならす
#if !UNITY_EDITOR && UNITY_IOS
			_PlaySystemShutterSound();
#elif !UNITY_EDITOR && UNITY_ANDROID
			var mediaActionSound = new AndroidJavaObject("android.media.MediaActionSound");
			mediaActionSound.Call("play", mediaActionSound.GetStatic<int>("SHUTTER_CLICK"));
#endif

			// 画面キャプチャする
			Texture2D screenShot = new Texture2D (Screen.width, Screen.height, TextureFormat.RGB24, false);
			RenderTexture rt = new RenderTexture (screenShot.width, screenShot.height, 24);
			RenderTexture prev = Cam.targetTexture;
			Cam.targetTexture = rt;
			Cam.Render ();
			Cam.targetTexture = prev;
			RenderTexture.active = rt;
			screenShot.ReadPixels (new Rect (0, 0, screenShot.width, screenShot.height), 0, 0);
			screenShot.Apply ();

			// 保存する
			// iOSとAndroidは、以下の記事からのコピー
			// http://baba-s.hatenablog.com/entry/2017/12/26/210500
			//
#if UNITY_EDITOR
			byte[] shotImage = screenShot.EncodeToPNG ();
			UnityEngine.Object.Destroy (screenShot);
			string fileName = "cap_" + DateTime.Now.ToString ("yyyyMMddHHmmssfff") + ".png";
			Debug.Log ("Path : " + Application.persistentDataPath);
			File.WriteAllBytes (Application.persistentDataPath + "/" + fileName, shotImage);
//#elif UNITY_IOS
//			byte[] shotImage = screenShot.EncodeToPNG ();
//			UnityEngine.Object.Destroy (screenShot);
//			_SendTexture(shotImage, shotImage.Length);
#else
			NativeGallery.SaveToGallery( screenShot, "FanappSample", "Fanapp img {0}.png" );
			UnityEngine.Object.Destroy (screenShot);
#endif
		}

		void ScanMedia(string fileName){
			if (Application.platform != RuntimePlatform.Android)
				return;
			//メディアスキャン
#if UNITY_ANDROID
			using (AndroidJavaClass jcUnityPlayer = new AndroidJavaClass ("com.Cassio.FanappSample"))
			using (AndroidJavaObject joActivity = jcUnityPlayer.GetStatic<AndroidJavaObject> ("currentActivity"))
			using (AndroidJavaObject joContext = joActivity.Call<AndroidJavaObject> ("getApplicationContext"))
			using (AndroidJavaObject jcMediaScannerConnection = new AndroidJavaClass ("android.media.MediaScannerConnection"))
			using (AndroidJavaClass jcEnvironment = new AndroidJavaClass ("android.os.Environment"))
			using (AndroidJavaObject joExDir = jcEnvironment.CallStatic<AndroidJavaObject> ("getExternalStorageDirectory")) {
				jcMediaScannerConnection.CallStatic("scanFile", joContext, new string[] {fileName}, new string[] {"image/png"}, null);
			}
			Handheld.StopActivityIndicator();
#endif
		}


#if UNITY_IOS
		//撮影後コールバックされる関数
		public void DidImageWriteToAlbum(string errorDescription) {
			Handheld.StopActivityIndicator();
			if (string.IsNullOrEmpty(errorDescription)) {
				OnCompleteCapture.Invoke();
			}else{
				OnFailCapture.Invoke();
			}
		}
#endif
	
	}
}
