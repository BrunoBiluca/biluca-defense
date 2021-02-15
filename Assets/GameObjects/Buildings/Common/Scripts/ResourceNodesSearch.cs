using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodesSearch {

    public static int NearbyNodes(Vector3 position, ResourceGeneratorConfig config) {
        var colliders = Physics2D.OverlapCircleAll(
            position, config.detectionRadius);

        var resourceNodeCount = 0;
        foreach(var collider in colliders) {
            var resourceNode = collider.GetComponent<ResourceNode>();
            if(resourceNode == null) continue;

            if(resourceNode.resourceType == config.ResourceType
                && resourceNodeCount < config.maxResourceNodes) {
                resourceNodeCount++;
            }
        }

        return resourceNodeCount;
    }

}
