using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CommonLibrary
{
    /// <summary>
    /// ת���������ṩ�ߣ����ڻ�ȡConvertEnumerator���õ������ɶ�����Դ�������ת����ö�٣�����ΪConvertEnumeratorProvider<TOutput>�ķ��Ͱ汾
    /// </summary>
    /// <typeparam name="TInput">Դ��������������</typeparam>
    /// <typeparam name="TOutput">ת�������������</typeparam>
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
    /// ת���������ṩ�ߣ����ڻ�ȡConvertEnumerator���õ������ɶ�����Դ�������ת����ö�٣�����ΪConvertEnumeratorProvider<TInput, TOutput>�ķǷ��Ͱ汾
    /// </summary>
    /// <typeparam name="TOutput">ת�������������</typeparam>
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
