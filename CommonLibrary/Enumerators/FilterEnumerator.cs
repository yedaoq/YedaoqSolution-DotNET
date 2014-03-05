using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CommonLibrary.Enumerators
{
    public class FilterEnumerator<T>:IEnumerator<T>,IDisposable,IEnumerator
    {
        #region Fields

        private IEnumerator<T> Source;

        private Predicate<T> Filter;

        #endregion

        #region Properties

        /// <summary>
        /// ��ǰֵ
        /// </summary>
        public T Current
        {
            get { return Source.Current; }
        }

        /// <summary>
        /// ��ǰֵ
        /// </summary>
        object IEnumerator.Current
        {
            get { return Source.Current; }
        }

        #endregion

        #region Methods

        public FilterEnumerator(IEnumerable<T> source, Predicate<T> filter)
        {
            if (object.ReferenceEquals(null, source) || object.ReferenceEquals(null, filter))
            {
                throw new ArgumentNullException();
            }

            this.Source = source.GetEnumerator();
            this.Filter = filter;
        }

        public FilterEnumerator(IEnumerator<T> source, Predicate<T> filter)
        {
            if (object.ReferenceEquals(null, source) || object.ReferenceEquals(null, filter))
            {
                throw new ArgumentNullException();
            }

            this.Source = source;
            this.Filter = filter;
        }

        public bool MoveNext()
        {
            while (Source.MoveNext())
            {
                if (Filter(Source.Current)) return true;
            }
            return false;
        }

        public void Reset()
        {
            Source.Reset();
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose()
        {

        }

        #endregion
    }

    public class FilterEnumerator : IEnumerator, IDisposable
    {
        #region Fields

        private IEnumerator Source;

        private Predicate<object> Filter;

        #endregion

        #region Properties

        /// <summary>
        /// ��ǰֵ
        /// </summary>
        public object Current
        {
            get { return Source.Current; }
        }

        /// <summary>
        /// ��ǰֵ
        /// </summary>
        object IEnumerator.Current
        {
            get { return Source.Current; }
        }

        #endregion

        #region Methods

        public FilterEnumerator(IEnumerable source, Predicate<object> filter)
        {
            if (object.ReferenceEquals(null, source) || object.ReferenceEquals(null, filter))
            {
                throw new ArgumentNullException();
            }

            this.Source = source.GetEnumerator();
            this.Filter = filter;
        }

        public FilterEnumerator(IEnumerator source, Predicate<object> filter)
        {
            if (object.ReferenceEquals(null, source) || object.ReferenceEquals(null, filter))
            {
                throw new ArgumentNullException();
            }

            this.Source = source;
            this.Filter = filter;
        }

        public bool MoveNext()
        {
            while (Source.MoveNext())
            {
                if (Filter(Source.Current)) return true;
            }
            return false;
        }

        public void Reset()
        {
            Source.Reset();
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose()
        {

        }

        #endregion
    }
}
