using Code.Global.Animations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TryFade : MonoBehaviour
{
    public Image image;
    public Transform tra;
    public ScaleAnimationPreset presetScale;
    public PunchAnimationPreset presetPunch;
    public JumpAnimationPreset presetJump;
    void Start()
    {
        StartCoroutine(FadeOut());
    }

	private IEnumerator FadeOut()
	{
        yield return new WaitForSeconds(2);
      //  AnimationShortCuts.FadeOut(image);
        //AnimationShortCuts.PopEffect(tra);
        //AnimationShortCuts.ScaleAnimationLoop(transform, presetScale);
        //AnimationShortCuts.ScaleAnimation(transform, presetScale);
        //AnimationShortCuts.PunchPositionAnimation(transform, presetPunch);
         AnimationShortCuts.JumpAnimation(transform, Vector3.up, presetJump);
    }

	void Update()
    {
        
    }
}
