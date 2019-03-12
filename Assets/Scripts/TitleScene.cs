using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public GameObject blackCube;
    private List<List<GameObject>> animationCubes = new List<List<GameObject>>();

    void Start() {
        for (float r = 1f; r < 10f; r += 0.8f) {
            animationCubes.Add(new List<GameObject>());
            for (float theta = 0f; theta < Mathf.PI * 2; theta += Mathf.PI / 20f) {
                animationCubes[animationCubes.Count - 1].Add(Instantiate(
                    blackCube, new Vector3(r * Mathf.Cos(theta), r * Mathf.Sin(theta), 10), Quaternion.identity));
            }
        }
    }

    void Update() {
        for (int i = 0; i < animationCubes.Count; i++) {
            animationCubes[i].ForEach((cube) => {
                cube.transform.RotateAround(Vector3.zero, new Vector3(0, 0, 1), -0.1f * i + 0.15f);
            });
        }
        if (Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene("SimulationScene");
        }
    }
}
