using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {
	static bool pslide_stats, cslide_stats;

	// Use this for initialization
	void Start () {
		pslide_stats = false;
		cslide_stats = false;
	}
	
	public static void poseClick(){
		if (cslide_stats) {
			cslide_stats = false;
			GameObject.Find ("cSlidePanel").GetComponent<SampleApp.UI.PanelSlider> ().SlideOut ();
		}
		if (pslide_stats) {
			pslide_stats = false;
			GameObject.Find ("pSlidePanel").GetComponent<SampleApp.UI.PanelSlider> ().SlideOut ();
		} else {
			pslide_stats = true;
			GameObject.Find ("pSlidePanel").GetComponent<SampleApp.UI.PanelSlider> ().SlideIn ();
		}
	}

	public static void clothesClick(){
		if (pslide_stats) {
			pslide_stats = false;
			GameObject.Find ("pSlidePanel").GetComponent<SampleApp.UI.PanelSlider> ().SlideOut ();
		}
		if (cslide_stats) {
			cslide_stats = false;
			GameObject.Find ("cSlidePanel").GetComponent<SampleApp.UI.PanelSlider> ().SlideOut ();
		} else {
			cslide_stats = true;
			GameObject.Find ("cSlidePanel").GetComponent<SampleApp.UI.PanelSlider> ().SlideIn ();
		}
	}
}
