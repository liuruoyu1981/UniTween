﻿#if UNITY_POST_PROCESSING_STACK_V2
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Bloom")]
public class PPBloomTween : TweenData
{
    [Space(15)]
    [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
    public bool automaticOverride = true;
    [Space]
    public BloomCommand command;

    [HideIf("ShowColor")]
    public float to;
    [ShowIf("ShowColor")]
    public Color color;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        PostProcessVolume volume = (PostProcessVolume)GetComponent(uniTweenTarget);
        var setting = volume.profile.GetSetting<Bloom>();

        if (setting != null)
        {
            setting.active = automaticOverride;
            switch (command)
            {
                case BloomCommand.Intensity:
                    setting.intensity.overrideState = automaticOverride;
                    return DOTween.To(() => setting.intensity.value, x => setting.intensity.value = x, to, duration);
                case BloomCommand.Threshold:
                    setting.threshold.overrideState = automaticOverride;
                    return DOTween.To(() => setting.threshold.value, x => setting.threshold.value = x, to, duration);
                case BloomCommand.SoftKnee:
                    setting.softKnee.overrideState = automaticOverride;
                    return DOTween.To(() => setting.softKnee.value, x => setting.softKnee.value = x, to, duration);
                case BloomCommand.Diffusion:
                    setting.diffusion.overrideState = automaticOverride;
                    return DOTween.To(() => setting.diffusion.value, x => setting.diffusion.value = x, to, duration);
                case BloomCommand.AnamorphicRatio:
                    setting.anamorphicRatio.overrideState = automaticOverride;
                    return DOTween.To(() => setting.anamorphicRatio.value, x => setting.anamorphicRatio.value = x, to, duration);
                case BloomCommand.Color:
                    setting.color.overrideState = automaticOverride;
                    return DOTween.To(() => setting.color.value, x => setting.color.value = x, color, duration);
                case BloomCommand.DirtinessIntensity:
                    setting.dirtIntensity.overrideState = automaticOverride;
                    return DOTween.To(() => setting.dirtIntensity.value, x => setting.dirtIntensity.value = x, to, duration);
            }
        }
        else
        {
            Debug.Log("UniTween could not find a Bloom to tween. Be sure to add it on your Post Process Volume component");
        }

        return null;
    }

    private bool ShowColor()
    {
        return command == BloomCommand.Color;
    }

    public enum BloomCommand
    {
        Intensity,
        Threshold,
        SoftKnee,
        Diffusion,
        AnamorphicRatio,
        Color,
        DirtinessIntensity
    }
}
#endif