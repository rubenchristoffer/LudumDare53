using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{

    public static Transform FindAnyChildWithName (this Transform transform, string name) {
        if (transform.name.Equals(name)) {
            return transform;
        }

        foreach (Transform child in transform) {
            var result = FindAnyChildWithName(child, name);

            if (result != null) {
                return result;
            }
        }

        return null;
    }

}
