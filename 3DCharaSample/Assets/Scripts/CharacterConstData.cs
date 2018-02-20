using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleApp.UI
{
	public static class Constants
	{
		public const float speed = 120f;
		public const float angle = 30.0f;
	}

	public class CharacterConstData : MonoBehaviour {
		// Profile画面のオブジェクト
		public static string[] textObjElement = {
			"NameText",
			"BirthText",
			"BloodtypeText",
			"DescriptionText"
		};

		// Profile画面の表示内容。とりあえずUnityの公開データを転記する
		// なお、このデータはユニティちゃんライセンスに準拠する。
		// http://unity-chan.com/contents/license_jp/ 
		public static string[] charaNameTbl = {
			"大鳥こはく\nKohaku Otori",
			"神林ゆうこ\nYuko Kanbayashi",
			"藤原みさき\nMisaki Fijiwara"
		};

		public static string[] birthdayTbl = {
			"８月１３日生まれ\n１７歳",
			"６月９日生まれ\n１７歳",
			"４月２１日生まれ\n１７歳"
		};

		public static string[] bloodTypeTbl = {
			"血液型：AO",
			"血液型：AB",
			"血液型：O"
		};

		public static string[] descriptionTbl = {
			"ご存知我らが「ユニティちゃん」。大鳥財団CEOの一人娘で大変溺愛して育てられたが、持ち前の活発さが優って夢は映画やゲームのアクション俳優。割と流されやすい性格だが腹をくくるのも早く、やると決めたら全力で挑む。好物のカレーコロッケを与えるということを聞くようになる（らしい）。\n\nCV：角元\u3000明日香\n\n\ntest\n最下段テスト\ntest",
			"いつもマイペースなこはくの同級生。マイクロビーズのバランスボールを愛用していて、少し退屈したりすると直ぐ寝てしまう。バランスボール上で眠ってはよく落ちるので「らっこ」と呼ばれる。実は結構なゲーマーでインディー系の不思議なタイトルを発掘するのが得意。\nCV：大森\u3000日雅",
			"こはく達の学年の生徒会長。こはく、ゆうこ、みさきの三人組中彼女だけクラスが違う。学園では優等生だがプライベートは非常に大雑把でいい加減。ミサキチの愛称で呼ばれることが多く、こはくにユニティちゃん、ゆうこにラッコというあだ名をつけたのは彼女である。\nCV：大島\u3000美咲"
		};

		// for Clothes Changing
		// 衣装の変更は、キャラクタのモデル全部を変更する（パーツの差し替えではない）
		// UnityのAsset Storeからダウンロードできるデータが一つのなので、全てのキャラクタでそのデータを使い回す
		// （公式サイトには他のデータもあるが、容量削減のため）
		public static string[] KohakuClothesTbl = {
			"unitychan",
			"unitychan"
		};

		public static string[] YukoClothesTbl = {
			"unitychan",
			"unitychan"
		};

		public static string[] MisakiClothesTbl = {
			"unitychan",
			"unitychan"
		};

		public static string[] kohaku_clothes_txt = {
			"普通のやつ",
			"念のため普通のやつ"
		};

		public static string[] yuko_clothes_txt = {
			"普通のやつ",
			"念のため普通のやつ"
		};

		public static string[] misaki_clothes_txt = {
			"普通のやつ",
			"念のため普通のやつ"
		};


		// for Pose Changing
		// モーションのテーブル。本当はAnimatorの中をシークして生成したいがとりあえずベタがき
		public static string[] pose_Tbl = {
			"POSE01",
			"POSE02",
			"POSE03",
			"POSE04",
			"POSE05",
			"POSE06",
			"POSE07",
			"POSE08",
			"POSE09",
			"POSE10",
			"POSE11",
			"POSE12",
			"POSE13",
			"POSE14",
			"POSE15",
			"POSE16",
			"POSE17",
			"POSE18",
			"POSE19",
			"POSE20",
			"POSE21",
			"POSE22",
			"POSE23",
			"POSE24",
			"POSE25",
			"POSE26",
			"POSE27",
			"POSE28",
			"POSE29",
			"POSE30",
			"POSE31"
		};
		public static string[] pose_txt = {
			"POSE01：顔の前で両手を合わせて立っているヤツ",
			"POSE02：腕を伸ばして腰のところで手を重ねて立っているポーズ",
			"POSE03：POSE02の横に構えたヤツ",
			"POSE04：片足あげて敬礼的な",
			"POSE05：手を頭の後ろに持って行っていって照れているようなポーズ",
			"POSE06：空気椅子",
			"POSE07：ジャンプっっっっ",
			"POSE08：空気椅子。。。はうっ",
			"POSE09：右足を上げて",
			"POSE10：とりゃー",
			"POSE11：背面跳びか？　背面跳びなのか？",
			"POSE12：とぉーーーーくを眺める",
			"POSE13：しゃがみ１",
			"POSE14：中腰",
			"POSE15：グラビア撮影か？",
			"POSE16：めひょう",
			"POSE17：しゃがみ２",
			"POSE18：しなだれ",
			"POSE19：体育すわり的",
			"POSE20：敬礼みたい",
			"POSE21：よくわからないポーズ",
			"POSE22：うぉっとっと。。。みたいなヤツ",
			"POSE23：ピース！！",
			"POSE24：うっほー",
			"POSE25：どぉぞぉこちらへ　みたいなヤツ",
			"POSE26：蹴り！！",
			"POSE27：ん？",
			"POSE28：構え１",
			"POSE29：構え２",
			"POSE30：突き！！",
			"POSE31：シリ"
		};



		// 動画は２種類のパターンがある想定でテーブルを持つようにする
		// ここでの想定は、キャラクタごとに、無料配布の特別な動画と、購入した動画の２種類
		// サムネイル画像と動画ファイルは以下の場所に置いてある想定で、このテーブルではファイル名のみを記入するソフト処理にしている
		//     ソフト処理：VideoContentsScrollController.cs
		//     サムネイルデータ保管場所：Resources/Movie/thumbnail/
		//     動画データ保管場所：Resources/Movie/
		// for special Movie
		public static string[,] kohaku_SpecialMoveVideo = {
			// 以下の構成です
			// サムネイル画像のファイル名, 説明文, 動画ファイル名
//			{"thumbnail1 file name", "Movie1 description", "Movie1 file name"},
//			{"thumbnail2 file name", "Movie2 description", "Movie2 file name"}
		};
		public static string[,] kohaku_PurchaseBonusVideo = {
			// 以下の構成です
			// サムネイル画像のファイル名, 説明文, 動画ファイル名
		};

		public static string[,] yuko_SpecialMoveVideo = {
			// 以下の構成です
			// サムネイル画像のファイル名, 説明文, 動画ファイル名
		};
		public static string[,] yuko_PurchaseBonusVideo = {
			// 以下の構成です
			// サムネイル画像のファイル名, 説明文, 動画ファイル名
		};

		public static string[,] misaki_SpecialMoveVideo = {
			// 以下の構成です
			// サムネイル画像のファイル名, 説明文, 動画ファイル名
		};
		public static string[,] misaki_PurchaseBonusVideo = {
			// 以下の構成です
			// サムネイル画像のファイル名, 説明文, 動画ファイル名
		};
	}
}
