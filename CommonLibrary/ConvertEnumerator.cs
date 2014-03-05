using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CommonLibrary
{
    /// <summary>
    /// 转换迭代器：对源数据集进行转换输出;此类为ConvertEnumerator<TOutput>的泛型版本
    /// </summary>
    /// <typeparam name="TIntput">数据源中数据类型</typeparam>
    /// <typeparam name="TOutput">转换后的数据类型</typeparam>
    public class ConvertEnumerator<TInput,TOutput> : IEnumerator<TOutput>,IEnumerator, IDisposable
    {
        #region Fields

        private IEnumerator<TInput> Source;

        private Converter<TInput,TOutput> Converter;

        #endregion

        #region Properties

        /// <summary>
        /// 当前值
        /// </summary>
        public TOutput Current
        {
            get
            {
                return Converter(Source.Current);
            }
        }

        /// <summary>
        /// 当前值
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                return Converter(Source.Current);
            }
        }

        #endregion

        #region Methods

        public ConvertEnumerator(IEnumerable<TInput> source, Converter<TInput,TOutput> converter)
        {
            this.Source = source.GetEnumerator();
            this.Converter = converter;
        }

        public ConvertEnumerator(IEnumerator<TInput> source, Converter<TInput, TOutput> converter)
        {
            this.Source = source;
            this.Converter = converter;
        }

        public bool MoveNext()
        {
            return Source.MoveNext();
        }

        public void Reset()
        {
            Source.Reset();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {

        }

        #endregion
    }

    /// <summary>
    /// 转换迭代器：对源数据集进行转换输出;此类为ConvertEnumerator<TIntput, TOutput>的非泛型版本
    /// </summary>
    /// <typeparam name="TOutput">转换后的数据类型</typeparam>
    public class ConvertEnumerator<TOutput> : IEnumerator<TOutput>, IEnumerator, IDisposable
    {
        #region Fields

        private IEnumerator Source;

        private Converter<object, TOutput> Converter;

        #endregion

        #region Properties

        /// <summary>
        /// 当前值
        /// </summary>
        public TOutput Current
        {
            get
            {
                return Converter(Source.Current);
            }
        }

        /// <summary>
        /// 当前值
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                return Converter(Source.Current);
            }
        }

        #endregion

        #region Methods

        public ConvertEnumerator(IEnumerable source, Converter<object, TOutput> converter)
        {
            this.Source = source.GetEnumerator();
            this.Converter = converter;
        }

        public ConvertEnumerator(IEnumerator source, Converter<object, TOutput> converter)
        {
            this.Source = source;
            this.Converter = converter;
        }

        public bool MoveNext()
        {
            return Source.MoveNext();
        }

        public void Reset()
        {
            Source.Reset();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {

        }

        #endregion
    }
}
