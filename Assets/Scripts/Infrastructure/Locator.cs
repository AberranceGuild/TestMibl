namespace Infrastructure {
    public class Locator<T> where T : new() {
        public static readonly T Instance = new();
    }
}