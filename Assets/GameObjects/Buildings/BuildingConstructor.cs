using Assets.GameManagers;
using Assets.UnityFoundation.TimeUtils;
using Assets.UnityFoundation.UI.ProgressElements.ProgressCircle;
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
        private ProgressCircle progressCircle;

        void Awake() {
            boxCollider2D = GetComponent<BoxCollider2D>();
            sprite = transform.Find("sprite").GetComponent<SpriteRenderer>();
            typeHolder = GetComponent<BuildingTypeHolder>();
            progressCircle = transform.Find("progressCircle").GetComponent<ProgressCircle>();
        }

        private void Setup(BuildingSO buildingType) {
            BoxCollider2D prefabCollider = buildingType.Prefab.GetComponent<BoxCollider2D>();
            boxCollider2D.size = prefabCollider.size;
            boxCollider2D.offset = prefabCollider.offset;

            sprite.sprite = buildingType.Sprite;

            typeHolder.BuildingType = buildingType;
            progressCircle.Setup(buildingType.constructionTime);
        }

        private float constructionTimer = 0f;
        private void Update() {
            if(typeHolder.BuildingType == null) return;

            constructionTimer += Time.deltaTime;
            sprite.material.SetFloat(
                "_Progress", constructionTimer / typeHolder.BuildingType.constructionTime
            );
            if(constructionTimer >= typeHolder.BuildingType.constructionTime) {
                ConstructBuilding();
            }
        }

        private void ConstructBuilding() {
            Instantiate(typeHolder.BuildingType.Prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}