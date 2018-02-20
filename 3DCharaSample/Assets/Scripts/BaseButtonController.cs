using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GUI上のボタンのクリック操作の元請け
// 各ボタン名に応じて、処理を分担する
// 処理は、button変数に割り当てられたソースコード（オブジェクト）で行う
// button変数への割り当てはInspector画面で行われる
// 一次受けを共通化することで、Inspector画面でボタン処理をいちいち設定しなくてもよくなるため、ボタンオブジェクト自体のコピペが可能となる
public class BaseButtonController : MonoBehaviour {

	public BaseButtonController button;

	public void OnClick(){
		if (button == null)
		{
			throw new System.Exception("Button instance is null!!");
		}
		// 自身のオブジェクト名を渡す
		button.OnClick(this.gameObject.name);
	}

	protected virtual void OnClick(string objectName)
	{
		// 呼ばれることはない
		Debug.Log("Base Button");
	}
}
