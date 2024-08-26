using Zenject;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreBooter : MonoBehaviour
{
    [Inject]
    private CoreDataHandler _coreDataHandler;

    public Image SceneTransitioner { get; private set; }

    private bool _sceneIsLoaded;

    public CoreBooter(Image sceneTransitioner) 
    {
        SceneTransitioner = sceneTransitioner;
    }

    private void OnEnable()
    {
        //EventManager.AddListener("LoadedScene", _OnLoadedScene);
    }

    private void OnDisable()
    {
        //EventManager.AddListener("LoadedScene", _OnLoadedScene);
    }

    private void Start()
    {
        LoadMenu();
    }

    private void _OnLoadedScene()
    {
        _sceneIsLoaded = true;
    }

    public void LoadMenu() => StartCoroutine(_SwitchingScene("menu"));
    public void LoadMap(string mapReference)
    {
        MapData data = Resources.Load<MapData>($"ScriptableObjects/Maps/{mapReference}");
        _coreDataHandler.SetMapData(data);
        string s = data.sceneName;
        StartCoroutine(_SwitchingScene("game", s));
    }

    private IEnumerator _SwitchingScene(string to, string map = "")
    {
        _sceneIsLoaded = false;
        SceneTransitioner.color = Color.clear;

        float t = 0;
        while (t < 1f)
        {
            SceneTransitioner.color = Color.Lerp(Color.clear, Color.black, t);
            t += Time.deltaTime;
            yield return null;
        }

        AsyncOperation op;
        if (to == "menu")
            op = _LoadMenu();
        else
            op = _LoadMap(map);

        yield return new WaitUntil(() => _sceneIsLoaded);

        t = 0;
        while (t < 1f)
        {
            SceneTransitioner.color = Color.Lerp(Color.black, Color.clear, t);
            t += Time.deltaTime;
            yield return null;
        }

        SceneTransitioner.color = Color.clear;
    }

    private AsyncOperation _LoadMap(string map)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(map, LoadSceneMode.Additive);
        AudioListener prevListener = Object.FindObjectOfType<AudioListener>();
        op.completed += (_) =>
        {
            if (prevListener != null) prevListener.enabled = false;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(map));
            Scene s = SceneManager.GetSceneByName("MainMenu");
            if (s != null && s.IsValid())
                SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive).completed += (_) =>
                {
                    SceneManager.UnloadSceneAsync(s);
                };
            else
                SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        };
        return op;
    }

    private AsyncOperation _LoadMenu()
    {
        AudioListener prevListener = Object.FindObjectOfType<AudioListener>();
        if (prevListener != null) prevListener.enabled = false;
        AsyncOperation op = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        op.completed += (_) =>
        {
            Scene s = SceneManager.GetSceneByName("GameScene");
            if (s != null && s.IsValid())
                SceneManager.UnloadSceneAsync(s);
            if (_coreDataHandler.Scene != null)
            {
                s = SceneManager.GetSceneByName(_coreDataHandler.Scene);
                if (s != null && s.IsValid())
                    SceneManager.UnloadSceneAsync(s);
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
        };
        return op;
    }
}
