using UnityEngine.Assertions;

namespace Infrastructure {
    public class Proxy<T> where T : class {
        private T _subject;

        protected T Subject {
            get {
                Assert.IsNotNull(_subject, nameof(_subject));
                return _subject;
            }
        }

        public bool IsSubjectExists() =>
            _subject != null;

        public void SetSubject(T subject) {
            Assert.IsNotNull(subject, nameof(subject));
            _subject = subject;
            OnSetSubject();
        }

        protected virtual void OnSetSubject() { }
    }
}