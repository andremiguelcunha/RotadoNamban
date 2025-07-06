using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class MultiImageSpawnerWithButton : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;

    [System.Serializable]
    public class ImagePrefabPair
    {
        public string imageName;
        public GameObject prefab;
        public string sceneToLoad; // cena específica por imagem (opcional)
    }

    [Header("Mapeamento de imagens para prefabs")]
    public List<ImagePrefabPair> imagePrefabs;

    [Header("Referência ao botão da UI")]
    public GameObject buttonFromScene;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private string currentSceneToLoad = "";

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();

        if (buttonFromScene != null)
        {
            buttonFromScene.SetActive(false);
        }
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            HandleImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            HandleImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            string imageName = trackedImage.referenceImage.name;
            if (spawnedPrefabs.TryGetValue(imageName, out GameObject prefab))
            {
                prefab.SetActive(false);
            }
        }
    }

    void HandleImage(ARTrackedImage trackedImage)
{
    if (trackedImage.trackingState != TrackingState.Tracking)
        return;

    string imageName = trackedImage.referenceImage.name;
    var config = imagePrefabs.Find(p => p.imageName == imageName);
    if (config == null || config.prefab == null)
        return;

    // 🔥 Desativa os outros prefabs
    foreach (var pair in spawnedPrefabs)
    {
        if (pair.Key != imageName && pair.Value != null)
        {
            pair.Value.SetActive(false);
        }
    }

    if (!spawnedPrefabs.ContainsKey(imageName))
{
    GameObject spawned = Instantiate(config.prefab, trackedImage.transform.position, trackedImage.transform.rotation);
    spawned.name = "Prefab_" + imageName;

    // Define escala com base no nome da imagem
    if (imageName.ToLower() == "barco")
    {
        spawned.transform.localScale = new Vector3(0.004f, 0.004f, 0.004f);
    }
    else if (imageName.ToLower() == "balanca")
    {
        spawned.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    spawnedPrefabs[imageName] = spawned;
}



    // Atualiza posição/rotação
    spawnedPrefabs[imageName].transform.position = trackedImage.transform.position;
    spawnedPrefabs[imageName].transform.rotation = trackedImage.transform.rotation;
    spawnedPrefabs[imageName].SetActive(true); // reativa caso tenha sido desativado

    // Ativa o botão
    if (buttonFromScene != null && !buttonFromScene.activeSelf)
    {
        currentSceneToLoad = config.sceneToLoad;
        buttonFromScene.SetActive(true);

        var btn = buttonFromScene.GetComponentInChildren<UnityEngine.UI.Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => LoadScene());
    }
}


    void LoadScene()
    {
        if (!string.IsNullOrEmpty(currentSceneToLoad))
        {
            Debug.Log("Carregando cena: " + currentSceneToLoad);
            SceneManager.LoadScene(currentSceneToLoad);
        }
    }
}
