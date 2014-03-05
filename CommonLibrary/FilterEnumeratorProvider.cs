using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CommonLibrary
{
    public class FilterEnumeratorProvider<T>:IEnumerable<T>,IDisposable,IEnumerable
    {
        #region Fields

        private IEnumerable<T> Source;

        private Predicate<T> Filter;

        #endregion

        #region Methods

        public FilterEnumeratorProvider(IEnumerable<T> source, Predicate<T> filter)
        {
            this.Source = source;
            this.Filter = filter;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new FilterEnumerator<T>(Source, Filter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FilterEnumerator<T>(Source, Filter); 
        }

        public void Dispose()
        {
            
        }

        #endregion
    }

    public class FilterEnumeratorProvider : IEnumerable, IDisposable
    {
        #region Fields

        private IEnumerable Source;

        private Predicate<object> Filter;

        #endregion

        #region Methods

        public FilterEnumeratorProvider(IEnumerable source, Predicate<object> filter)
        {
            this.Source = source;
            this.Filter = filter;
        }

        public IEnumerator GetEnumerator()
        {
            return new FilterEnumerator(Source, Filter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FilterEnumerator(Source, Filter);
        }

        public void Dispose()
        {

        }

        #endregion
    }
}
