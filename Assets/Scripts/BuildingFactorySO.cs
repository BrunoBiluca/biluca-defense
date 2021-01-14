using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingFactory")]
public class BuildingFactorySO : ScriptableObject {

    public List<BuildingSO> possibleBuildings;

    public BuildingSO GetByShortcut(string shortcut) {
        return possibleBuildings.Find(b => b.Shortcut == shortcut);
    }

    internal BuildingSO GetByIndex(int idx) {
        return possibleBuildings[idx];
    }
}
