using System;
using System.Collections;
using System.Collections.Generic;

namespace ShortCord.MonoGame.Collections {

    public class HashListItem {
        public HashListItem(object obj) {
            Type = obj.GetType();
            Item = obj;
        }
        public Type Type;
        public object Item;

        public override bool Equals(object obj) {
            HashListItem item = obj as HashListItem;
            if (item != null && item.Type == Type) {
                return true;
            } else {
                return false;
            }
        }

        public override int GetHashCode() {
            return Type.GetHashCode() * 17 + Item.GetHashCode();
        }
    }

    public class HashList : IList<HashListItem> {

        List<HashListItem> backingList;

        public HashList() {
            backingList = new List<HashListItem>();
        }

        #region IList<>

        public HashListItem this[int index] {
            get { return backingList[index]; }
            set { backingList[index] = value; }
        }

        public int Count { get { return backingList.Count; } }

        public bool IsReadOnly { get { return false; } }

        public void Add(HashListItem item) {
            for (int i = 0; i < Count; i++) {
                if (item.Type == this[i].Type) {
                    throw new InvalidOperationException($"Can not add more than one of {item.Type}");
                }
            }

            backingList.Add(item);
        }

        public void Clear() => backingList.Clear();

        public bool Contains(HashListItem item) => backingList.Contains(item);

        public void CopyTo(HashListItem[] array, int arrayIndex) => backingList.CopyTo(array, arrayIndex);

        public IEnumerator<HashListItem> GetEnumerator() => backingList.GetEnumerator();

        public int IndexOf(HashListItem item) => backingList.IndexOf(item);

        public void Insert(int index, HashListItem item) => backingList.Insert(index, item);

        public bool Remove(HashListItem item) => backingList.Remove(item);

        public void RemoveAt(int index) => backingList.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        #endregion
    }
}
