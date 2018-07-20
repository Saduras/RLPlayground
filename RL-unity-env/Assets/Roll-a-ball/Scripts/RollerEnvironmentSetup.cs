using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RollerEnvironmentSetup : MonoBehaviour {

    public Transform environmentPrototype;
    public Vector3 offset = new Vector3(10, 0, 10);
    public int count = 1;

    [SerializeField, HideInInspector]
    Transform[] environments = new Transform[0];

    private void Start()
    {
        this.gameObject.SetActive(!Application.isPlaying);
    }

    void Update () {
		if(count != environments.Length) {
            for (int i = 0; i < environments.Length; i++) {
                DestroyImmediate(environments[i].gameObject);
            }

            environments = new Transform[count];
            for (int i = 0; i < count; i++) {
                environments[i] = Instantiate(environmentPrototype, offset * (i + 1), Quaternion.identity);
                environments[i].name = "Environment " + i;
            }
        }
	}
}
