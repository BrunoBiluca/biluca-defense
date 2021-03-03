using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameManagers {
    public class GameAssets : MonoBehaviour {

        private static GameAssets instance;
        public static GameAssets Instance {
            get {
                if(instance == null) instance = FindObjectOfType<GameAssets>();
                return instance;
            }
        }

        public GameObject constructionPlaceHolderPrefab;

    }
}