﻿using System;
using System.IO;
using UnityEngine;

// iOSおよびAndroidの各OS用の処理
// キャプチャした画像をそれぞれのOSのデフォルトで持っている画像ビューワーで閲覧できるようにする
// あわせて、Plugins配下の処理も必要
//（そもそもこの処理は、それぞれのPluginを呼び出すための処理）
public static class NativeGallery
{
#if !UNITY_EDITOR && UNITY_ANDROID
    private static AndroidJavaClass m_ajc = null;
    private static AndroidJavaClass AJC
    {
        get
        {
			if( m_ajc == null ){
				m_ajc = new AndroidJavaClass( "com.cassio.nativegallerymodule.NativeGallery" );
			}
            return m_ajc;
        }
    }
#elif !UNITY_EDITOR && UNITY_IOS
    [System.Runtime.InteropServices.DllImport( "__Internal" )]
    private static extern void _ScreenshotWriteToAlbum(string path);

    [System.Runtime.InteropServices.DllImport( "__Internal" )]
    private static extern void _VideoWriteToAlbum(string path);
#endif

	public static void SaveToGallery( byte[] mediaBytes, string directoryName, string filenameFormatted, bool isImage )
	{
		if( mediaBytes == null || mediaBytes.Length == 0 )
			throw new ArgumentException( "Parameter 'mediaBytes' is null or empty!" );

		if( directoryName == null || directoryName.Length == 0 )
			throw new ArgumentException( "Parameter 'directoryName' is null or empty!" );

		if( filenameFormatted == null || filenameFormatted.Length == 0 )
			throw new ArgumentException( "Parameter 'filenameFormatted' is null or empty!" );

		string path = GetSavePath( directoryName, filenameFormatted );

		File.WriteAllBytes( path, mediaBytes );

		SaveToGallery( path, isImage );
	}

	public static void SaveToGallery( string existingMediaPath, string directoryName, string filenameFormatted, bool isImage )
	{
		if( !File.Exists( existingMediaPath ) )
			throw new FileNotFoundException( "File not found at " + existingMediaPath );

		if( directoryName == null || directoryName.Length == 0 )
			throw new ArgumentException( "Parameter 'directoryName' is null or empty!" );

		if( filenameFormatted == null || filenameFormatted.Length == 0 )
			throw new ArgumentException( "Parameter 'filenameFormatted' is null or empty!" );

		string path = GetSavePath( directoryName, filenameFormatted );

		File.Copy( existingMediaPath, path, true );

		SaveToGallery( path, isImage );
	}

	public static void SaveToGallery( Texture2D image, string directoryName, string filenameFormatted )
	{
		if( image == null )
			throw new ArgumentException( "Parameter 'image' is null!" );

//#if !UNITY_EDITOR && UNITY_ANDROID
//		string path = "";
//		string fileName = "";
//		using (AndroidJavaClass jcEnvironment = new AndroidJavaClass ("android.os.Environment"))
//		using (AndroidJavaObject joExDir = jcEnvironment.CallStatic<AndroidJavaObject> ("getExternalStorageDirectory")) {
//			path = joExDir.Call<string>("toString")+"/FanappSample/";
//		}
//		//フォルダがなければ作成
//		if (!Directory.Exists(path)) Directory.CreateDirectory(path);
//		//ファイル名入力
//		fileName = System.DateTime.Now.Ticks.ToString ()+".png";
//		path += fileName;
//
//		Debug.Log("Save dir : " + path);
//
//		using (FileStream BinaryFile = new FileStream(path, FileMode.Create, FileAccess.Write)) {
//		using (BinaryWriter Writer = new BinaryWriter(BinaryFile)) {
//				Writer.Write (image.EncodeToPNG());
//			}
//		}
//
//		using (AndroidJavaClass jcUnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer"))
//		using (AndroidJavaObject joActivity = jcUnityPlayer.GetStatic<AndroidJavaObject> ("currentActivity"))
//		using (AndroidJavaObject joContext = joActivity.Call<AndroidJavaObject> ("getApplicationContext")) {
//			using (AndroidJavaClass jcMediaScannerConnection = new AndroidJavaClass ("android.media.MediaScannerConnection"))
//			jcMediaScannerConnection.CallStatic ("scanFile", joContext, new string[] { fileName }, new string[] { "image/png" }, null);
//		}
//		Handheld.StopActivityIndicator();
//#else

		if( filenameFormatted.EndsWith( ".jpeg" ) || filenameFormatted.EndsWith( ".jpg" ) )
			SaveToGallery( image.EncodeToJPG( 100 ), directoryName, filenameFormatted, true );
		else if( filenameFormatted.EndsWith( ".png" ) )
			SaveToGallery( image.EncodeToPNG(), directoryName, filenameFormatted, true );
		else
			SaveToGallery( image.EncodeToPNG(), directoryName, filenameFormatted + ".png", true );
//#endif
	}

	public static void DeleteFromGallery( string path, bool isImage )
	{
		if( !File.Exists( path ) )
			throw new FileNotFoundException( "File not found at " + path );

		File.Delete( path );

#if !UNITY_EDITOR && UNITY_ANDROID
        using( AndroidJavaClass unityClass = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
		using( AndroidJavaObject context = unityClass.GetStatic<AndroidJavaObject>( "currentActivity" ) )
		{
			AJC.CallStatic( "MediaDeleteFile", context, path, isImage );
		}
#endif
		Debug.Log( "Deleted from gallery: " + path );
	}

	private static void SaveToGallery( string path, bool isImage )
	{
#if !UNITY_EDITOR && UNITY_ANDROID
        using( AndroidJavaClass unityClass = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
		using( AndroidJavaObject context = unityClass.GetStatic<AndroidJavaObject>( "currentActivity" ) )
		{
			AJC.CallStatic( "MediaScanFile", context, path );
		}

		Debug.Log( "Saved to gallery: " + path );
#elif !UNITY_EDITOR && UNITY_IOS
		if( isImage )
	        _ScreenshotWriteToAlbum( path );
		else
			_VideoWriteToAlbum( path );

		Debug.Log( "Saving to Pictures: " + Path.GetFileName( path ) );
#endif
	}

	private static string GetSavePath( string directoryName, string filenameFormatted )
	{
		string saveDir;
#if !UNITY_EDITOR && UNITY_ANDROID
		saveDir = AJC.CallStatic<string>( "GetMediaPath", directoryName );
#else
		saveDir = Application.persistentDataPath;
#endif
		
		if( filenameFormatted.Contains( "{0}" ) )
		{
			int fileIndex = 0;
			string path;
			do
			{
				path = Path.Combine( saveDir, string.Format( filenameFormatted, ++fileIndex ) );
			} while( File.Exists( path ) );

			return path;
		}

		saveDir = Path.Combine( saveDir, filenameFormatted );
		Debug.Log ("Save Dir : " + saveDir);

#if !UNITY_EDITOR && UNITY_IOS
		// iOS internally copies images/videos to Photos directory of the system,
		// but the process is async. The redundant file is deleted by objective-c code
		// automatically after the media is saved but while it is being saved, the file
		// should NOT be overwritten. Therefore, always ensure a unique filename on iOS
		if( File.Exists( saveDir ) )
		{
			return GetSavePath( directoryName,
				Path.GetFileNameWithoutExtension( filenameFormatted ) + " {0}" + Path.GetExtension( filenameFormatted ) );
		}
#endif

		return saveDir;
	}
}