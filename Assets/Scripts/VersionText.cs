using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionText : MonoBehaviour
{

    public TextMeshProUGUI versionText;

    void Awake () {
        if (versionText != null) {
            versionText.text = $"v{Application.version}";
        }
    }

}
