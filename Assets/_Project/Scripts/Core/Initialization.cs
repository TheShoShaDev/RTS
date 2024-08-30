using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class Initialization : MonoInstaller
{
    [SerializeField] private Image _coreBooterSceneTransitioner;

    public override void InstallBindings()
    {
        EventManager eventManager = new EventManager();
        Container.Bind<EventManager>().AsSingle();

        CoreDataHandler coreDataHandler = new CoreDataHandler();
        Container.Bind<CoreDataHandler>().AsSingle();

        CoreBooter coreBooter = new CoreBooter(_coreBooterSceneTransitioner);
        Container.Bind<CoreBooter>().AsSingle();
    }
}
