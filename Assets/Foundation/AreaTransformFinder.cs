using LanguageExt;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AreaTransformFinder: MonoBehaviour {
    [SerializeField]
    private float lookForTargetsTimerMax = .2f;

    [SerializeField]
    private Transform defaultTarget;

    private Type lookingForType;
    private float lookRangeRadius;

    private Transform target;
    public Option<Transform> Target { 
        get {
            return target != null ? Option<Transform>.Some(target) : Option<Transform>.None;
        } 
    }

    private float lookForTargetsTimer;

    void Start() {
        lookForTargetsTimer = Random.Range(0f, lookForTargetsTimerMax);
    }

    public void Setup(Transform defaultTarget, Type lookingForType, float lookRangeRadius = 6f) {
        this.defaultTarget = defaultTarget;
        this.lookingForType = lookingForType;
        this.lookRangeRadius = lookRangeRadius;
    }

    void Update() {
        lookForTargetsTimer += Time.deltaTime;
        if(lookForTargetsTimer >= lookForTargetsTimerMax) {
            lookForTargetsTimer = 0f;
            Find();
        }
    }

    private void Find() {
        var nearObjects = Physics2D.OverlapCircleAll(transform.position, lookRangeRadius);

        foreach(var obj in nearObjects) {
            var searchedComponent = obj.gameObject.GetComponent(lookingForType);
            if(searchedComponent != null) {
                target = obj.transform;
                return;
            }
        }

        target = defaultTarget;
    }
}
