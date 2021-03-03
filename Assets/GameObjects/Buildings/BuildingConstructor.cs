using Assets.GameManagers;
using Assets.UnityFoundation.TimeUtils;
using System;
using UnityEngine;

namespace Assets.GameObjects.Buildings {
    public class BuildingConstructor : MonoBehaviour {

        public static void Create(BuildingSO buildingType, Vector3 position) {
            var constructionPlaceholder = Instantiate(
                GameAssets.Instance.constructionPlaceHolderPrefab, position, Quaternion.identity
            );
            constructionPlaceholder.GetComponent<BuildingConstructor>().Setup(buildingType);
        }

        private BoxCollider2D boxCollider2D;
        private BuildingTypeHolder typeHolder;
        private SpriteRenderer sprite;

        void Awake() {
            boxCollider2D = GetComponent<BoxCollider2D>();
            sprite = transform.Find("sprite").GetComponent<SpriteRenderer>();
            typeHolder = GetComponent<BuildingTypeHolder>();
        }

        private void Setup(BuildingSO buildingType) {

            BoxCollider2D prefabCollider = buildingType.Prefab.GetComponent<BoxCollider2D>();
            boxCollider2D.size = prefabCollider.size;
            boxCollider2D.offset = prefabCollider.offset;

            sprite.sprite = buildingType.Prefab
                .Find("image").GetComponent<SpriteRenderer>().sprite;

            typeHolder.BuildingType = buildingType;
        }
    }
}