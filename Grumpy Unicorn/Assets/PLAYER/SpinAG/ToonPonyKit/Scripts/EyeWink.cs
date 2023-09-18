using UnityEngine;

public class EyeWink : MonoBehaviour {

    #pragma warning disable 0108
    [SerializeField]
    private Renderer renderer;

    [SerializeField]
    private float closeEyeTime;

    [SerializeField]
    private float openEyeTime;

    [SerializeField]
    private float openEyeTimeDelta = 2f;

    [SerializeField]
    private Texture closeEye;

    private Texture openEye;

    private Material material;

    private float remainingTime;

    private void Start() {
        material = renderer.material;
        remainingTime = CalculateOpenEyeTime();
        openEye = material.mainTexture;
    }
    private void Update() {
        remainingTime -= Time.deltaTime;

        if (remainingTime >= 0) {
            return;
        }

        if (material.mainTexture == openEye) {
            material.mainTexture = closeEye;
            remainingTime = closeEyeTime;

        } else {
            material.mainTexture = openEye;
            remainingTime = CalculateOpenEyeTime();
        }
    }

    private float CalculateOpenEyeTime() {
        return openEyeTime + Random.value * openEyeTimeDelta;
    }
}
