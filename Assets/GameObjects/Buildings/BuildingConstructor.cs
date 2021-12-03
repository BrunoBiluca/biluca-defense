using Assets.GameManagers;
using Assets.UnityFoundation.UI.ProgressElements.ProgressCircle;
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
        private BuildingSO buildingType;
        private SpriteRenderer sprite;
        private ProgressCircle progressCircle;

        void Awake() {
            boxCollider2D = GetComponent<BoxCollider2D>();
            sprite = transform.Find("sprite").GetComponent<SpriteRenderer>();
            progressCircle = transform.Find("progress_circle").GetComponent<ProgressCircle>();
        }

        private void Setup(BuildingSO buildingType) {
            BoxCollider2D prefabCollider = buildingType.Prefab.GetComponent<BoxCollider2D>();
            boxCollider2D.size = prefabCollider.size;
            boxCollider2D.offset = prefabCollider.offset;

            this.buildingType = buildingType;
            sprite.sprite = buildingType.Sprite;
            progressCircle.Setup(buildingType.constructionTime);
        }

        private float constructionTimer = 0f;
        private void Update() {
            if(buildingType == null) return;

            constructionTimer += Time.deltaTime;
            sprite.material.SetFloat(
                "_Progress", constructionTimer / buildingType.constructionTime
            );
            if(constructionTimer >= buildingType.constructionTime) {
                ConstructBuilding();
            }
        }

        private void ConstructBuilding() {
            Instantiate(GameAssets.Instance.buildingPlacedParticles, 
                transform.position, Quaternion.identity);
            Instantiate(buildingType.Prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}