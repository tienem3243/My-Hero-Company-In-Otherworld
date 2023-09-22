using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TimelineController : MonoBehaviour
{
    [SerializeField] List<TimelineAsset> timeline;
    [SerializeField] List<PlayableDirector> playableDirector;

    public List<TimelineAsset> Timeline { get => timeline; set => timeline = value; }

    public void Play()
    {
        playableDirector.ForEach(x => x.Play());
    }
    public void PlayIndex(int index)
    {

        TimelineAsset selectedAsset;
        selectedAsset = Timeline[index];
        playableDirector[0].Play(selectedAsset);

    }

}
