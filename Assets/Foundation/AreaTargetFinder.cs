using LanguageExt;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AreaTargetFinder : MonoBehaviour {

    [SerializeField]
    private float lookForTargetsTimerMax = .2f;

    [SerializeField]
    private Transform defaultTarget;

    [SerializeField]
    private Type lookingForType;

    public Option<Transform> Target { get; private set; }

    private float lookForTargetsTimer;

    void Start() {
        lookForTargetsTimer = Random.Range(0f, lookForTargetsTimerMax);
    }

    public void Setup(Transform defaultTarget, Type lookingForType) {
        this.defaultTarget = defaultTarget;
        this.lookingForType = lookingForType;
    }

    void Update() {
        lookForTargetsTimer += Time.deltaTime;
        if(lookForTargetsTimer >= lookForTargetsTimerMax) {
            lookForTargetsTimer = 0f;
            Find();
        }
    }

    private void Find() {
        var nearObjects = Physics2D.OverlapCircleAll(transform.position, 6f);

        foreach(var obj in nearObjects) {
            if(obj.gameObject.GetComponent(lookingForType) != null) {
                Target = obj.transform;
                return;
            }
        }

        if(defaultTarget == null) {
            Target = Option<Transform>.None;
        }
        else {
            Target = defaultTarget;
        }
    }
}
