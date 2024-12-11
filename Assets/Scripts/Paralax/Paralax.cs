using UnityEngine;

public class Paralax : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform backgroundTransform;
        public float speed = 0.2f;
    }

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private ParallaxLayer[] layers;

    void Start()
    {
        cameraTransform = Camera.main != null ? Camera.main.transform : null;

        if (cameraTransform == null)
        {
            Debug.LogError("Câmera principal não encontrada! Certifique-se de que a câmera tem a tag 'MainCamera'.");
            return;
        }

        lastCameraPosition = cameraTransform.position;

        GameObject parallaxBackground = GameObject.Find("ParallaxBackGround");
        if (parallaxBackground != null)
        {
            Transform[] childTransforms = parallaxBackground.GetComponentsInChildren<Transform>();

            layers = new ParallaxLayer[childTransforms.Length - 1];

            float baseSpeed = 0.2f;

            for (int i = 1; i < childTransforms.Length; i++)
            {
                layers[i - 1] = new ParallaxLayer
                {
                    backgroundTransform = childTransforms[i],
                    speed = baseSpeed * Mathf.Pow(2, i-1)
                };
            }
        }
        else
        {
            Debug.LogWarning("Objeto 'ParallaxBackGround' não encontrado na cena. Parallax não será aplicado.");
            layers = new ParallaxLayer[0];
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null)
        {
            Debug.LogWarning("Câmera principal não encontrada no LateUpdate. Parallax não será aplicado.");
            return;
        }

        if (layers == null || layers.Length == 0)
        {
            return;
        }

        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        foreach (var layer in layers)
        {
            if (layer.backgroundTransform != null)
            {
                Vector3 newPosition = layer.backgroundTransform.position;
                newPosition.x += deltaMovement.x * layer.speed;
                newPosition.y += deltaMovement.y * layer.speed;
                layer.backgroundTransform.position = newPosition;
            }
        }

        lastCameraPosition = cameraTransform.position;
    }
}