using Cysharp.Threading.Tasks;

namespace Services.Interfaces {
    public interface ISaveLoadService {
        UniTask Save();
        UniTask Load();
    }
}