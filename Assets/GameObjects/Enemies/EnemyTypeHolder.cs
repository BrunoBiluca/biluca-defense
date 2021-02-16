using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeHolder : MonoBehaviour {

    [SerializeField]
    private EnemySO enemy;
    public EnemySO Enemy {
        get { return enemy; }
        set { enemy = value; }
    }

}
