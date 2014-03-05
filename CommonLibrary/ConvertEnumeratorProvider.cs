using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CommonLibrary
{
    /// <summary>
    /// 转换迭代器提供者：用于获取ConvertEnumerator，该迭代器可对数据源中项进行转换后枚举；此类为ConvertEnumeratorProvider<TOutput>的泛型版本
    /// </summary>
    /// <typeparam name="TInput">源数据中数据类型</typeparam>
    /// <typeparam name="TOutput">转换后的数据类型</typeparam>
    public class ConvertEnumeratorProvider<TInput, TOutput> : IEnumerable<TOutput>, IDisposable, IEnumerable
    {
        #region Fields

        private IEnumerable<TInput> Source;

        private Converter<TInput, TOutput> Converter;

        #endregion

        #region Methods

        public ConvertEnumeratorProvider(IEnumerable<TInput> source, Converter<TInput, TOutput> converter)
        {
            this.Source = source;
            this.Converter = converter;
        }

        public IEnumerator<TOutput> GetEnumerator()
        {
            return new ConvertEnumerator<TInput, TOutput>(Source, Converter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ConvertEnumerator<TInput, TOutput>(Source, Converter);
        }

        public void Dispose()
        {

        }

        #endregion
    }

    /// <summary>
    /// 转换迭代器提供者：用于获取ConvertEnumerator，该迭代器可对数据源中项进行转换后枚举；此类为ConvertEnumeratorProvider<TInput, TOutput>的非泛型版本
    /// </summary>
    /// <typeparam name="TOutput">转换后的数据类型</typeparam>
    public class ConvertEnumeratorProvider<TOutput> : IEnumerable<TOutput>, IDisposable, IEnumerable
    {
        #region Fields

        private IEnumerable Source;

        private Converter<object, TOutput> Converter;

        #endregion

        #region Methods

        public ConvertEnumeratorProvider(IEnumerable source, Converter<object, TOutput> converter)
        {
            this.Source = source;
            this.Converter = converter;
        }

        public IEnumerator<TOutput> GetEnumerator()
        {
            return new ConvertEnumerator<TOutput>(Source, Converter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ConvertEnumerator<TOutput>(Source, Converter);
        }

        public void Dispose()
        {

        }

        #endregion
    }
}
