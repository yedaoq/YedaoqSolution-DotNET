using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CommonLibrary
{
    /// <summary>
    /// ת������������Դ���ݼ�����ת�����;����ΪConvertEnumerator<TOutput>�ķ��Ͱ汾
    /// </summary>
    /// <typeparam name="TIntput">����Դ����������</typeparam>
    /// <typeparam name="TOutput">ת�������������</typeparam>
    public class ConvertEnumerator<TInput,TOutput> : IEnumerator<TOutput>,IEnumerator, IDisposable
    {
        #region Fields

        private IEnumerator<TInput> Source;

        private Converter<TInput,TOutput> Converter;

        #endregion

        #region Properties

        /// <summary>
        /// ��ǰֵ
        /// </summary>
        public TOutput Current
        {
            get
            {
                return Converter(Source.Current);
            }
        }

        /// <summary>
        /// ��ǰֵ
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
        /// �ͷ���Դ
        /// </summary>
        public void Dispose()
        {

        }

        #endregion
    }

    /// <summary>
    /// ת������������Դ���ݼ�����ת�����;����ΪConvertEnumerator<TIntput, TOutput>�ķǷ��Ͱ汾
    /// </summary>
    /// <typeparam name="TOutput">ת�������������</typeparam>
    public class ConvertEnumerator<TOutput> : IEnumerator<TOutput>, IEnumerator, IDisposable
    {
        #region Fields

        private IEnumerator Source;

        private Converter<object, TOutput> Converter;

        #endregion

        #region Properties

        /// <summary>
        /// ��ǰֵ
        /// </summary>
        public TOutput Current
        {
            get
            {
                return Converter(Source.Current);
            }
        }

        /// <summary>
        /// ��ǰֵ
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
        /// �ͷ���Դ
        /// </summary>
        public void Dispose()
        {

        }

        #endregion
    }
}
