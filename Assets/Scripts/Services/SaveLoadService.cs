using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Services.Interfaces;
using UnityEngine;

namespace Services {
    public class SaveLoadService : ISaveLoadService {
        private static readonly string Folder = Path.Combine(Application.persistentDataPath, "Saves");
        private const string FILE_NAME = "data.json";

        public async UniTask Save() {
            if (Directory.Exists(Folder) is false)
                Directory.CreateDirectory(Folder);

            string json = JsonUtility.ToJson(Locator<Saves>.Instance);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await File.WriteAllBytesAsync(Path.Combine(Folder, FILE_NAME), bytes)
                      .AsUniTask();
        }

        public async UniTask Load() {
            string targetFile = Path.Combine(Folder, FILE_NAME);
            if (File.Exists(targetFile) is false)
                return;

            byte[] data = await File.ReadAllBytesAsync(targetFile)
                                    .AsUniTask();

            string json = Encoding.UTF8.GetString(data);
            JsonUtility.FromJsonOverwrite(json, Locator<Saves>.Instance);
        }
    }
}