using System;
using System.Collections;
using System.Collections.Generic;
using ShortCord.MonoGame.Components;

namespace ShortCord.MonoGame.Collections {
    public sealed class ComponentCollection : List<IComponent> {}
    public sealed class GameObjectCollection : IList<GameObject> {
        List<GameObject> backingList;

        public GameObjectCollection() {
            backingList = new List<GameObject>();
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public GameObject this[int index] {
            get { return backingList[index]; }
            set { backingList[index] = value; }
        }

        public int Count => backingList.Count;
        
        public void Add(GameObject item) => backingList.Add(item);

        public bool Contains(GameObject item) => backingList.Contains(item);

        public void Clear() {
            for(int i = 0; i < Count; i++) {
                this[i].UnloadContent();
                this[i].Dispose();
            }
            backingList.Clear();
        }

        public void CopyTo(GameObject[] array, int arrayIndex) => backingList.CopyTo(array, arrayIndex);

        public IEnumerator<GameObject> GetEnumerator() => backingList.GetEnumerator();

        public int IndexOf(GameObject item) => backingList.IndexOf(item);

        public void Insert(int index, GameObject item) => backingList.Insert(index, item);

        public bool Remove(GameObject item) => backingList.Remove(item);

        public void RemoveAt(int index) => backingList.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
