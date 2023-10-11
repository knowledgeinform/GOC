using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.IO;

public class LoadManager:MonoBehaviour
{
    public static LoadManager Instance;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private GameObject neuronLoader;
    [SerializeField] private Slider _progressBar;

    [SerializeField] private string filePath;

    private static List<string> listA = new List<string>();

    private static int count = 1;

    // Start is called before the first frame update
    void Awake()
    {
        _loaderCanvas.SetActive(false);
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else {
            Destroy(gameObject);
        }
        using(var reader = new StreamReader(@filePath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                listA.Add(values[4]);
            }
        }
    }

    public async void LoadScene(string sceneName) {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        neuronLoader.GetComponent<MeshDownloader>().seg_id = (listA[count]);
        count+=1;
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do {
            await Task.Delay(100);
            _progressBar.value = scene.progress;
        } while ((scene.progress < 0.9f));

        scene.allowSceneActivation = true;

        _loaderCanvas.SetActive(false);
    }
}
