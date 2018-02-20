using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

namespace Cassio.FanappSample.TouchInput
{
	// 画面上のキャラクタに対するタッチ操作を処理する
	[RequireComponent(typeof(Image))]
	public class CharacterTouchController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		public GameObject charaObject;

		//zoomの許容範囲.
		public float ScaleMin = 0.2f;
		public float ScaleMax = 5.0f;
		public float DeltaX4RotTh = 20;	// Pixel

		private Vector2 lastPos;
		private Vector3 screenPosition;
		private Vector3 offset;
		private Vector3 startPosition;
		private Quaternion startRotation;

		bool m_Dragging, m_Pinch, m_Rotation;
		int[] TouchIdTbl = new int[3];
		int TouchIdOffset = 0;

		private float backDist = 0.0f;


#if !UNITY_EDITOR
		private Vector3 m_Center;
		private Image m_Image;
#else
		Vector3 m_PreviousMouse;
		float m_PreviousMousePosX;
#endif

		// Use this for initialization
		void Start () {
			m_Dragging = false;
			m_Pinch = false;
			m_Rotation = false;
			TouchIdOffset = 0;
#if !UNITY_EDITOR
			m_Image = GetComponent<Image>();
			m_Center = m_Image.transform.position;            
#endif
		}

		void OnDragging(Vector3 touchPos)
		{
			if (!m_Dragging) {
				m_Dragging = true;
				m_Pinch = false;
				m_Rotation = false;
				Debug.Log ("Now Dragging!!");
			}

			//track mouse position.
			Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

			//convert screen position to world position with offset changes.
			Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;

			//It will update target gameobject's current postion.
			charaObject.transform.position = currentPosition;
		}

		void onPinch(){
			if (!m_Pinch) {
				m_Dragging = false;
				m_Pinch = true;
				Debug.Log ("PinchIn or PinchOut operating");
			}

			// タッチしている２点を取得
			Touch touch1 = Input.touches [TouchIdTbl [0]];
			Touch touch2 = Input.touches [TouchIdTbl [1]];

			// 2点タッチ開始時の距離を記憶
			if (touch2.phase == TouchPhase.Began) {
				backDist = Vector2.Distance (touch1.position, touch2.position);
			} else if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved) {
				// タッチ位置の移動後、長さを再測し、前回の距離からの相対値を取る。
				float newDist = Vector2.Distance (touch1.position, touch2.position);
				float transScale = (backDist - newDist) / 1000.0f;
				if (transScale != 0) {
					transScale = 1.0f - transScale;

					Vector3 newScale = charaObject.transform.localScale * transScale;
					// 画面に応じた大きさの制限を行う
					// Mathf.Clamp を使用する方が良いかもしれないが、x,y,zともに同じ倍率なので、泥臭くそれぞれ入れ直すことにします
					if (SceneManager.GetActiveScene ().name == "CameraScene") {
						// カメラ画面の時のサイズリミット
						if (newScale.z > 41f) {
							Debug.Log ("Now Upper Limit Scale " + newScale);
							newScale.x = 41f;
							newScale.y = 41f;
							newScale.z = 41f;
						}
					}
					if (SceneManager.GetActiveScene ().name == "PoseCollection") {
						// ポーズ画面の時のサイズリミット
						if (newScale.z > 1.9f) {
							Debug.Log ("Now Upper Limit Scale " + newScale);
							newScale.x = 1.9f;
							newScale.y = 1.9f;
							newScale.z = 1.9f;
						}
					}
					charaObject.transform.localScale = newScale;
					backDist = newDist;
				} else {
					Debug.Log ("transScale == 0... No Operation on PinchInOut Proc");
				}
			}
		}

		void onRotation(){
			// 新しい回転のためのコード（2点タッチのY軸ひねり）
			// タッチしている２点を取得
			Touch touch0 = Input.touches [TouchIdTbl [0]];
			Touch touch1 = Input.touches [TouchIdTbl [1]];

			// X軸のポジションがあまりに近いようなら、回転させない
			float deltaX = System.Math.Abs (touch0.position.x - touch1.position.x);
			if (deltaX <= DeltaX4RotTh) {
				Debug.Log ("横に差がないので回転させない");
				return;
			}

			if (!m_Rotation) {
				m_Rotation = true;
				m_Dragging = false;
				Debug.Log("Character Trun");
			}

			if (touch1.phase == TouchPhase.Began) {
				// 開始直後なら、リターン
				return;
			} else if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved) {
				// 2点ともに移動中
				float deltaY0 = touch0.deltaPosition.y;
				float deltaY1 = touch1.deltaPosition.y;
				if((deltaY0 < 0 && deltaY1 < 0) || (deltaY0 > 0 && deltaY1 > 0)) {
					Debug.Log ("Delta Y0 = " + deltaY0 + "  , Delta Y1 = " + deltaY1);
					Debug.Log("Same Direction");
				} else {
					// 回転開始
					// Y軸の動きの量の各タッチの平均をとって、その値分を回す
					float deltaAve = (System.Math.Abs(deltaY0) + System.Math.Abs(deltaY1))/2;
					float signY = System.Math.Sign(deltaY1);
					if(touch0.position.x <= touch1.position.x){
						// X軸の値が少ない方の動きに合わせる
						signY = System.Math.Sign(deltaY0);
					}
					if(signY == 0){
						if (touch0.position.x <= touch1.position.x) {
							// X軸の値が少ない方の動きがない場合は、もう一方の動きの符号反転にする
							signY = System.Math.Sign (deltaY1) * -1;
						} else {
							signY = System.Math.Sign (deltaY0) * -1;
						}
					}
					float yAngle = deltaAve * signY;	//回転させる角度を計算
					float roty = charaObject.transform.rotation.eulerAngles.y;	//現在の回転角を取得
					roty += yAngle;
					//指定した方向に回転する
					charaObject.transform.rotation = Quaternion.RotateTowards(charaObject.transform.rotation, Quaternion.Euler(0, roty, 0), 360.0f);
				}
			}
		}

		//starting point to dragging an object
		public void OnPointerDown(PointerEventData data)
		{
			Debug.Log ("OnPointerDown touchcount : " + Input.touchCount + "  ,   PointerID : " + data.pointerId);
			if (TouchIdOffset != 0) {
				// 2つ目のタッチはこちらのルート
				if (TouchIdOffset >= TouchIdTbl.Length)
					return;
				TouchIdTbl [TouchIdOffset] = data.pointerId;
				TouchIdOffset++;
				Debug.Log ("Now Multi tapping");
				return;
			}
			//TouchIdOffsetがゼロの時（初めて何らかのタップがされた時）
			Ray ray = Camera.main.ScreenPointToRay(data.position);
			RaycastHit hit = new RaycastHit();

			if (Physics.Raycast (ray.origin, ray.direction, out hit, Mathf.Infinity)) {
				//check if target object was hit
				if (hit.transform == charaObject.transform) {
					//yes, start dragging the object
					TouchIdTbl [TouchIdOffset] = data.pointerId;
					TouchIdOffset++;

					screenPosition = Camera.main.WorldToScreenPoint (charaObject.transform.position);
					offset = charaObject.transform.position -
						Camera.main.ScreenToWorldPoint (new Vector3 (data.position.x, data.position.y, screenPosition.z));
				} else {
					Debug.Log ("not pointed Character target");
				}
			} else {
				Debug.Log ("No target by Raycast");
			}
		}

		// Update is called once per frame
		public void Update () {
			if (TouchIdOffset == 1) {
				if ( SceneManager.GetActiveScene ().name == "CharacterProfile") {
					// プロファイル画面では処理しない
					return;
				}
				// キャラクタオブジェクトのドラッグ
#if !UNITY_EDITOR
				lastPos = new Vector2(Input.touches[TouchIdTbl[0]].position.x , Input.touches[TouchIdTbl[0]].position.y );
#else
				lastPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
#endif
				OnDragging(lastPos);
			} else if (TouchIdOffset == 2) {
				// キャラクタの回転
				onRotation();

				if ( SceneManager.GetActiveScene ().name == "CharacterProfile") {
					// プロファイル画面では処理しない
					return;
				}
				// キャラクタのピンチインアウト
				onPinch();
			}
		}

		public void OnPointerUp(PointerEventData data)
		{
			Debug.Log ("OnPointerUp touchcount : " + Input.touchCount);
			foreach (int i in TouchIdTbl) {
				// 一つでも離れたら処理終了
				if (i == data.pointerId) {
					m_Dragging = false;
					m_Pinch = false;
					m_Rotation = false;
					TouchIdOffset = 0;
				}
			}
		}
	}
}
