using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View {
    public class PointerClickListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
        public event Action<PointerEventData> OnDown = delegate { };
        public event Action<PointerEventData> OnUp = delegate { };

        public void OnPointerDown(PointerEventData eventData) =>
            OnDown(eventData);

        public void OnPointerUp(PointerEventData eventData) =>
            OnUp(eventData);
    }
}