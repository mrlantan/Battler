﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    [SerializeField] private Animator animatorObject;
    // CODE BELOW RELIES ON AnimationClip names being the same as actual animation names for 
    // .Play function in the animator
    public AnimationClip Run;
    public AnimationClip Idle;
    public AnimationClip Attack;
    public AnimationClip Death;
    public AnimationClip IsHit;
    public Camera gameCamera;

    [SerializeField] private Character character;
    void Start()
    {
        Dictionary<Character.AnimationType, string> animationTypeToString = new Dictionary<Character.AnimationType, string>();

        // REMOVE " (UnityEngine.AnimationClip)" from end of string(28 characters). EXTREMELY UNRELIABLE
        string idle = Idle.ToString();
        idle = idle.Remove(idle.Length - 28);
        string run = Run.ToString();
        run = run.Remove(run.Length - 28);
        string isHit = IsHit.ToString();
        isHit = isHit.Remove(isHit.Length - 28);
        string attack = Attack.ToString();
        attack = attack.Remove(attack.Length - 28);
        string death = Death.ToString();
        death = death.Remove(death.Length - 28);

        animationTypeToString.Add(Character.AnimationType.Idle, idle);
        animationTypeToString.Add(Character.AnimationType.Run, run);
        animationTypeToString.Add(Character.AnimationType.IsHit, isHit);
        animationTypeToString.Add(Character.AnimationType.Death, death);
        animationTypeToString.Add(Character.AnimationType.Attack, attack);

        animatorObject = this.gameObject.GetComponent<Animator>();
        Character.CharacterStats stats = new Character.CharacterStats(100);
        stats.movSpeed = 0.22f;
        Character.SceneInfo sceneInfo = new Character.SceneInfo();
        sceneInfo.mainCamera = gameCamera;
        sceneInfo.animator = animatorObject;
        sceneInfo.gameObject = this.gameObject;

        character = this.gameObject.AddComponent<Character>();
        character.init(stats, sceneInfo, animationTypeToString);
    }

    private int frameC = 0;
    void Update()
    {
        if (frameC == 100) {
            character.addActionSequence(Character.ActionSequenceType.MoveToNextScene);
        }

        frameC++;
    }
}
