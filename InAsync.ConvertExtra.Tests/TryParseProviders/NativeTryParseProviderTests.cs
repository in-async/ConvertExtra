﻿using System;
using InAsync.Tests.TestHelpers;
using InAsync.Tests.TestHelpers.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InAsync.ConvertExtras.TryParseProviders.Tests {

    [TestClass]
    public class NativeTryParseProviderTests {

        private static NativeTryParseProvider TargetProvider() => NativeTryParseProvider.Default;

        [TestMethod] public void GetDelegate_Byte() => InternalGetDelegate_Supported<Byte>();

        [TestMethod] public void GetDelegate_ByteN() => InternalGetDelegate_Supported<Byte?>();

        [TestMethod] public void GetDelegate_SByte() => InternalGetDelegate_Supported<SByte>();

        [TestMethod] public void GetDelegate_SByteN() => InternalGetDelegate_Supported<SByte?>();

        [TestMethod] public void GetDelegate_Int16() => InternalGetDelegate_Supported<Int16>();

        [TestMethod] public void GetDelegate_Int16N() => InternalGetDelegate_Supported<Int16?>();

        [TestMethod] public void GetDelegate_UInt16() => InternalGetDelegate_Supported<UInt16>();

        [TestMethod] public void GetDelegate_UInt16N() => InternalGetDelegate_Supported<UInt16?>();

        [TestMethod] public void GetDelegate_Int32() => InternalGetDelegate_Supported<Int32>();

        [TestMethod] public void GetDelegate_Int32N() => InternalGetDelegate_Supported<Int32?>();

        [TestMethod] public void GetDelegate_UInt32() => InternalGetDelegate_Supported<UInt32>();

        [TestMethod] public void GetDelegate_UInt32N() => InternalGetDelegate_Supported<UInt32?>();

        [TestMethod] public void GetDelegate_Int64() => InternalGetDelegate_Supported<Int64>();

        [TestMethod] public void GetDelegate_Int64N() => InternalGetDelegate_Supported<Int64?>();

        [TestMethod] public void GetDelegate_UInt64() => InternalGetDelegate_Supported<UInt64>();

        [TestMethod] public void GetDelegate_UInt64N() => InternalGetDelegate_Supported<UInt64?>();

        [TestMethod] public void GetDelegate_Single() => InternalGetDelegate_Supported<Single>();

        [TestMethod] public void GetDelegate_SingleN() => InternalGetDelegate_Supported<Single?>();

        [TestMethod] public void GetDelegate_Double() => InternalGetDelegate_Supported<Double>();

        [TestMethod] public void GetDelegate_DoubleN() => InternalGetDelegate_Supported<Double?>();

        [TestMethod] public void GetDelegate_Decimal() => InternalGetDelegate_Supported<Decimal>();

        [TestMethod] public void GetDelegate_DecimalN() => InternalGetDelegate_Supported<Decimal?>();

        [TestMethod] public void GetDelegate_Boolean() => InternalGetDelegate_Supported<Boolean>();

        [TestMethod] public void GetDelegate_BooleanN() => InternalGetDelegate_Supported<Boolean?>();

        [TestMethod] public void GetDelegate_Char() => InternalGetDelegate_Supported<Char>();

        [TestMethod] public void GetDelegate_CharN() => InternalGetDelegate_Supported<Char?>();

        [TestMethod] public void GetDelegate_DateTime() => InternalGetDelegate_Supported<DateTime>();

        [TestMethod] public void GetDelegate_DateTimeN() => InternalGetDelegate_Supported<DateTime?>();

        [TestMethod] public void GetDelegate_TimeSpan() => InternalGetDelegate_Supported<TimeSpan>();

        [TestMethod] public void GetDelegate_TimeSpanN() => InternalGetDelegate_Supported<TimeSpan?>();

        [TestMethod] public void GetDelegate_Guid() => InternalGetDelegate_Supported<Guid>();

        [TestMethod] public void GetDelegate_GuidN() => InternalGetDelegate_Supported<Guid?>();

        [TestMethod] public void GetDelegate_String() => InternalGetDelegate_Supported<String>();

        [TestMethod] public void GetDelegate_Version() => InternalGetDelegate_Supported<Version>();

        [TestMethod] public void GetDelegate_Uri() => InternalGetDelegate_Supported<Uri>();

        private void InternalGetDelegate_Supported<TConversionType>() {
            foreach (var item in TryParseTestCaseStore.Query<TConversionType>()) {
                (TargetProvider().GetDelegate<TConversionType>(item.provider)(item.input, item.provider, out var actualResult), actualResult).Is((item.expected, item.expectedResult), $"No.{item.testNumber}");
            }

            foreach (var item in TryParseTestCaseStore.Query(typeof(TConversionType))) {
                (TargetProvider().GetDelegate(item.conversionType, item.provider)(item.input, item.provider, out var actualResult), actualResult).Is((item.expected, item.expectedResult), $"No.{item.testNumber}");
            }
        }

        [TestMethod] public void GetDelegate_ByteEnum() => InternalGetDelegate_NotSupported<ByteEnum>();

        [TestMethod] public void GetDelegate_ByteEnumN() => InternalGetDelegate_NotSupported<ByteEnum?>();

        [TestMethod] public void GetDelegate_IntEnum() => InternalGetDelegate_NotSupported<IntEnum>();

        [TestMethod] public void GetDelegate_IntEnumN() => InternalGetDelegate_NotSupported<IntEnum?>();

        [TestMethod] public void GetDelegate_TypeConvertableClass() => InternalGetDelegate_NotSupported<TypeConvertableClass>();

        [TestMethod] public void GetDelegate_VanillaClass() => InternalGetDelegate_NotSupported<VanillaClass>();

        private void InternalGetDelegate_NotSupported<TConversionType>() {
            foreach (var item in TryParseTestCaseStore.Query<TConversionType>()) {
                TargetProvider().GetDelegate<TConversionType>(item.provider).Is(null);
            }

            foreach (var item in TryParseTestCaseStore.Query(typeof(TConversionType))) {
                TargetProvider().GetDelegate(typeof(TConversionType), item.provider).Is(null);
            }
        }
    }
}