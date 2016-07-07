using System.Collections;
using System.Collections.Generic;
using Entities;
using MonoGameTest.Entities;

namespace Collections {
    public class EntityCollection : IList<IEntity> {

        List<IEntity> backingList;

        public EntityCollection() {
            backingList = new List<IEntity>();
        }

        #region IList implementations

        public IEntity this[int index] {
            get { return backingList[index]; }
            set { backingList[index] = value; }
        }

        public int Count {
            get { return backingList.Count; }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public void Add(IEntity item) => backingList.Add(item);
        public void Clear() => backingList.Clear();
        public bool Contains(IEntity item) => backingList.Contains(item);
        public void CopyTo(IEntity[] array, int arrayIndex) => backingList.CopyTo(array, arrayIndex);
        public IEnumerator<IEntity> GetEnumerator() => backingList.GetEnumerator();
        public int IndexOf(IEntity item) => backingList.IndexOf(item);
        public void Insert(int index, IEntity item) => backingList.Insert(index, item);
        public bool Remove(IEntity item) => backingList.Remove(item);
        public void RemoveAt(int index) => backingList.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        /// <summary>
        /// Get a entity from the list
        /// </summary>
        public T Get<T>() where T : BaseEntity {
            T toReturn = null;
            for (int i = 0; i < Count; i++) {
                var tmp = this[i] as T;
                if (tmp != null) {
                    toReturn = tmp;
                    break;
                }
            }
            return toReturn;
        }

        /// <summary>
        /// Get and remove a entity from the list
        /// </summary>
        public T GetAndRemove<T>() where T : BaseEntity {
            T entity = Get<T>();
            Remove(entity);
            return entity;
        }

    }
}
