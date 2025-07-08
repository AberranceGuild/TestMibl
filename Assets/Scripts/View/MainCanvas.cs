using UnityEngine;
using View.Interfaces;

namespace View {
    public class MainCanvas : ViewBase, IMainCanvas {
        [field: SerializeField]
        public Canvas Canvas { get; private set; }
    }
}