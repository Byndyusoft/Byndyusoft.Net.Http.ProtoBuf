﻿using System;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.ProtoBuf;
using System.Threading.Tasks;
using Byndyusoft.Net.Http.ProtoBuf.Models;
using ProtoBuf.Meta;
using Xunit;

namespace Byndyusoft.Net.Http.ProtoBuf.Unit
{
    public class ProtoBufContentTests
    {
        private readonly MediaTypeHeaderValue _mediaType = MediaTypeHeaderValue.Parse("application/media-type");

        private readonly TypeModel _typeModel = RuntimeTypeModel.Default;

        [Fact]
        public void Create_NullInputType_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                ProtoBufContent.Create(new object(), null,
                    RuntimeTypeModel.Default, ProtoBufDefaults.MediaTypeHeader));

            Assert.Equal("inputType", exception.ParamName);
        }

        [Fact]
        public void Create_InputValueInvalidType_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                ProtoBufContent.Create(new SimpleType(), typeof(int),
                    RuntimeTypeModel.Default, ProtoBufDefaults.MediaTypeHeader));

            Assert.Equal(
                $"The specified type {typeof(int)} must derive from the specific value's type {typeof(SimpleType)}.",
                exception.Message);
        }

        [Fact]
        public void Create_Test()
        {
            var inputValue = SimpleType.Create();

            var content = ProtoBufContent.Create(inputValue, typeof(SimpleType), _typeModel, _mediaType);

            Assert.Same(inputValue, content.Value);
            Assert.Same(typeof(SimpleType), content.ObjectType);
            Assert.Same(_mediaType, content.Headers.ContentType);
            Assert.Same(_typeModel, content.TypeModel);
        }

        [Fact]
        public void Create_DefaultPropertyValues_Test()
        {
            var inputValue = SimpleType.Create();

            var content = ProtoBufContent.Create(inputValue, typeof(SimpleType));

            Assert.Same(inputValue, content.Value);
            Assert.Same(typeof(SimpleType), content.ObjectType);
            Assert.Same(ProtoBufDefaults.MediaTypeHeader, content.Headers.ContentType);
            Assert.Same(ProtoBufDefaults.TypeModel, content.TypeModel);
        }

        [Fact]
        public void Create_Generic_Test()
        {
            var inputValue = SimpleType.Create();

            var content = ProtoBufContent.Create(inputValue, _typeModel, _mediaType);

            Assert.Same(inputValue, content.Value);
            Assert.Same(typeof(SimpleType), content.ObjectType);
            Assert.Same(_mediaType, content.Headers.ContentType);
            Assert.Same(_typeModel, content.TypeModel);
        }

        [Fact]
        public void Create_Generic_DefaultPropertyValues_Test()
        {
            var inputValue = SimpleType.Create();

            var content = ProtoBufContent.Create(inputValue);

            Assert.Same(inputValue, content.Value);
            Assert.Same(typeof(SimpleType), content.ObjectType);
            Assert.Same(ProtoBufDefaults.MediaTypeHeader, content.Headers.ContentType);
            Assert.Same(ProtoBufDefaults.TypeModel, content.TypeModel);
        }

        [Fact]
        public async Task ReadAsByteArrayAsync_Test()
        {
            var inputValue = SimpleType.Create();
            var content = ProtoBufContent.Create(inputValue, _typeModel, _mediaType);

            var bytes = await content.ReadAsByteArrayAsync();
            await using var stream = new MemoryStream(bytes);

            var model = _typeModel.Deserialize<SimpleType>(stream);
            model.Verify();
        }

        [Fact]
        public async Task ReadAsByteArrayAsync_NullObject_Test()
        {
            var content = ProtoBufContent.Create<SimpleType>(null, _typeModel, _mediaType);

            var bytes = await content.ReadAsByteArrayAsync();
            await using var stream = new MemoryStream(bytes);

            Assert.Equal(0, stream.Length);
        }

        [Fact]
        public async Task ReadAsStreamArrayAsync_Test()
        {
            var inputValue = SimpleType.Create();
            var content = ProtoBufContent.Create(inputValue, _typeModel, _mediaType);

            await using var stream = await content.ReadAsStreamAsync();

            var model = _typeModel.Deserialize<SimpleType>(stream);
            model.Verify();
        }

        [Fact]
        public async Task ReadAsStreamArrayAsync_NullObject_Test()
        {
            var content = ProtoBufContent.Create<SimpleType>(null, _typeModel, _mediaType);

            await using var stream = await content.ReadAsStreamAsync();

            Assert.Equal(0, stream.Length);
        }

        [Fact]
        public async Task CopyToAsync_Test()
        {
            var inputValue = SimpleType.Create();
            var content = ProtoBufContent.Create(inputValue, _typeModel, _mediaType);
            await using var stream = new MemoryStream();

            await content.CopyToAsync(stream);
            stream.Position = 0;

            var model = _typeModel.Deserialize<SimpleType>(stream);
            model.Verify();
        }

        [Fact]
        public async Task CopyToAsync_NullObject_Test()
        {
            var content = ProtoBufContent.Create<SimpleType>(null, _typeModel, _mediaType);
            await using var stream = new MemoryStream();

            await content.CopyToAsync(stream);

            Assert.Equal(0, stream.Length);
        }
    }
}