using Infrastructure;
using UnityEngine;
using View.Interfaces;

namespace View.Proxies {
    public class MainCanvasProxy : Proxy<MainCanvas>, IMainCanvas {
        public Canvas Canvas =>
            Subject.Canvas;
    }
}