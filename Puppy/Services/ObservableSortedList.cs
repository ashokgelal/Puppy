#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

#endregion

namespace PuppyFramework.Services
{
    /// <summary>
    ///     Implements an observable collection which maintains its items in sorted order. In particular, items remain sorted
    ///     when changes are made to their properties: they are reordered automatically when necessary to keep them sorted.</summary>
    /// <remarks>
    ///     <para>Source: https://bitbucket.org/rstarkov/wpfcrutches/src/tip/ObservableSortedList.cs </para>
    ///     <para>This class currently requires <typeparamref name="T" /> to be a reference type. This is because a couple of
    ///     methods operate on the basis of reference equality instead of the comparison used for sorting. As implemented,
    ///     their behaviour for value types would be somewhat unexpected.</para>
    ///     <para>The INotifyCollectionChange interface is fairly complicated and relatively poorly documented (see
    ///     http://stackoverflow.com/a/5883947/33080 for example), increasing the likelihood of bugs. And there are currently
    ///     no unit tests. There could well be bugs in this code.</para></remarks>
    public class ObservableSortedList<T> : IList<T>, INotifyPropertyChanged, INotifyCollectionChanged where T : class, INotifyPropertyChanged
    {
        #region Events

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields

        private IComparer<T> _comparer;
        private readonly List<T> _list;

        #endregion

        #region Properties

        /// <summary>Gets the number of items stored in this collection.</summary>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>Returns false.</summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>Gets the item at the specified index. Does not support setting.</summary>
        public T this[int index]
        {
            get { return _list[index]; }
            set { throw new InvalidOperationException("Cannot set an item at an arbitrary index in a ObservableSortedList."); }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.</summary>
        /// <remarks>
        ///     Certain serialization libraries require a parameterless constructor.</remarks>
        public ObservableSortedList()
            : this(5)
        {
        }

        /// <summary>Constructor.</summary>
        public ObservableSortedList(int capacity = 4, IComparer<T> comparer = null)
        {
            _list = new List<T>(capacity);
            _comparer = comparer ?? Comparer<T>.Default;
        }

        /// <summary>Constructor.</summary>
        public ObservableSortedList(IEnumerable<T> items, IComparer<T> comparer = null)
        {
            _list = new List<T>(items);
            _comparer = comparer ?? Comparer<T>.Default;
            _list.Sort(_comparer);
            foreach (var item in _list)
                item.PropertyChanged += ItemPropertyChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds an item to this collection, ensuring that it ends up at the correct place according to the sort order.</summary>
        public void Add(T item)
        {
            var i = _list.BinarySearch(item, _comparer);
            if (i < 0)
                i = ~i;
            else
                do i++; while (i < _list.Count && _comparer.Compare(_list[i], item) == 0);

            _list.Insert(i, item);
            item.PropertyChanged += ItemPropertyChanged;
            collectionChanged_Added(item, i);
            RaisePropertyChanged("Count");
        }

        /// <summary>
        /// Re-sorts this collection using the given <see cref="newComparer"/>. If the <see cref="newComparer"/> is null
        /// the default comparer is used otherwise the old comparer is replaced by this<see cref="newComparer"/> and is used
        /// to sort this collection whenever required.
        /// </summary>
        /// <param name="newComparer">Comparer to use to sort this collection. This also replaces the old comparer.</param>
        public void Sort(IComparer<T> newComparer = null)
        {
            _comparer = newComparer ?? _comparer;
            _list.Sort(_comparer);
        }

        /// <summary>Removes all items from this collection.</summary>
        public void Clear()
        {
            foreach (var item in _list)
                item.PropertyChanged -= ItemPropertyChanged;
            _list.Clear();
            RaiseCollectionChangedReset();
            RaisePropertyChanged("Count");
        }

        private void collectionChanged_Added(T item, int index)
        {
            var handler = CollectionChanged;
            if (handler != null)
                handler(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        private void RaiseCollectionChangedMoved(T item, int oldIndex, int newIndex)
        {
            var handler = CollectionChanged;
            if (handler != null)
                handler(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, item, newIndex, oldIndex));
        }

        private void RaiseCollectionChangedRemoved(T item, int index)
        {
            var handler = CollectionChanged;
            if (handler != null)
                handler(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }

        private void RaiseCollectionChangedReset()
        {
            var handler = CollectionChanged;
            if (handler != null)
                handler(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        ///     Returns a value indicating whether the specified item is contained in this collection.</summary>
        /// <remarks>
        ///     Uses binary search to make the operation more efficient.</remarks>
        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        /// <summary>Copies all items to the specified array.</summary>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        /// <summary>Enumerates all items in sorted order.</summary>
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <summary>
        ///     Gets the index of the specified item, or -1 if not found. Only reference equality matches are considered.</summary>
        /// <remarks>
        ///     Binary search is used to make the operation more efficient.</remarks>
        public int IndexOf(T item)
        {
            var i = _list.BinarySearch(item, _comparer);
            if (i < 0) return -1;
            if (ReferenceEquals(_list[i], item)) return i;
            // Search downwards
            for (var s = i - 1; s >= 0 && _comparer.Compare(_list[s], item) == 0; s--)
                if (ReferenceEquals(_list[s], item))
                    return s;
            // Search upwards
            for (var s = i + 1; s < _list.Count && _comparer.Compare(_list[s], item) == 0; s++)
                if (ReferenceEquals(_list[s], item))
                    return s;
            // Not found
            return -1;
        }

        /// <summary>Not supported on a sorted collection.</summary>
        public void Insert(int index, T item)
        {
            throw new InvalidOperationException("Cannot insert an item at an arbitrary index into a ObservableSortedList.");
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = (T)sender;
            var oldIndex = _list.IndexOf(item);

            // See if item should now be sorted to a different position
            if (Count <= 1 || (oldIndex == 0 || _comparer.Compare(_list[oldIndex - 1], item) <= 0)
                && (oldIndex == Count - 1 || _comparer.Compare(item, _list[oldIndex + 1]) <= 0))
                return;

            // Find where it should be inserted 
            _list.RemoveAt(oldIndex);
            var newIndex = _list.BinarySearch(item, _comparer);
            if (newIndex < 0)
                newIndex = ~newIndex;
            else
                do newIndex++; while (newIndex < _list.Count && _comparer.Compare(_list[newIndex], item) == 0);

            _list.Insert(newIndex, item);
            RaiseCollectionChangedMoved(item, oldIndex, newIndex);
        }

        private void RaisePropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>Removes the specified item, returning true if found or false otherwise.</summary>
        public bool Remove(T item)
        {
            var i = IndexOf(item);
            if (i < 0) return false;
            _list.RemoveAt(i);
            RaiseCollectionChangedRemoved(item, i);
            RaisePropertyChanged("Count");
            return true;
        }

        /// <summary>Removes the specified item.</summary>
        public void RemoveAt(int index)
        {
            var item = _list[index];
            _list.RemoveAt(index);
            RaiseCollectionChangedRemoved(item, index);
            RaisePropertyChanged("Count");
        }

        #endregion
    }
}
