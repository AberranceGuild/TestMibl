using Cysharp.Threading.Tasks;
using Services.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Services {
    public class QuitService : IQuiteService {
        private readonly ISaveLoadService _saveLoadService;

        public QuitService(ISaveLoadService saveLoadService) =>
            _saveLoadService = saveLoadService;

        public void Quite() =>
            QuiteAsync()
                .Forget();

        public async UniTask QuiteAsync() {
            await _saveLoadService.Save();
            if (Application.isEditor) {
#if UNITY_EDITOR
                if (EditorApplication.isPlaying)
                    EditorApplication.ExitPlaymode();
#endif
            }
            else
                Application.Quit();
        }
    }
}