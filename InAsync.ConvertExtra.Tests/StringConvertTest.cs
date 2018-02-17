using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace InAsync.Tests {

    [TestClass]
    public partial class StringConvertTest {
        private static CultureInfo InvariantCulture = CultureInfo.InvariantCulture;
        private static CultureInfo JapaneseCulture = CultureInfo.GetCultureInfo("ja-JP");
        private static CultureInfo CurrentCulture = CultureInfo.CurrentCulture;

        [TestMethod]
        public void ToOrDefault_Test() {
            "123.45".ToOrDefault<int>().Is(0);
            "123.45".ToOrDefault<int>(1).Is(1);
            "123.45".ToOrDefault<float>().Is(123.45f);
        }

        [TestMethod]
        public void TryParse_T_input_result_Test() {
            try {
                CultureInfo.CurrentCulture = InvariantCulture;
                { (StringConvert.TryParse<float>("+∞", out var result), result).Is((false, 0)); }

                CultureInfo.CurrentCulture = JapaneseCulture;
                { (StringConvert.TryParse<float>("+∞", out var result), result).Is((true, float.PositiveInfinity)); }
            }
            finally {
                CultureInfo.CurrentCulture = CurrentCulture;
            }
        }

        [TestMethod]
        public void TryParse_T_input_provider_result_Test() {
            { (StringConvert.TryParse<int>("123.45", null, out var result), result).Is((false, 0)); }
            { (StringConvert.TryParse<float>("123.45", null, out var result), result).Is((true, 123.45f)); }
            { (StringConvert.TryParse<int>("123.45", InvariantCulture, out var result), result).Is((false, 0)); }
            { (StringConvert.TryParse<float>("123.45", InvariantCulture, out var result), result).Is((true, 123.45f)); }
        }

        [TestMethod]
        public void TryParse_input_conversionType_result_Test() {
            try {
                CultureInfo.CurrentCulture = InvariantCulture;
                { (StringConvert.TryParse("+∞", typeof(float), out var result), result).Is((false, null)); }

                CultureInfo.CurrentCulture = JapaneseCulture;
                { (StringConvert.TryParse("+∞", typeof(float), out var result), result).Is((true, float.PositiveInfinity)); }
            }
            finally {
                CultureInfo.CurrentCulture = CurrentCulture;
            }
        }

        [TestMethod]
        public void TryParse_input_conversionType_provider_result_Test() {
            foreach (var item in TryParse_TestDataSource) {
                (StringConvert.TryParse(item.input, item.conversionType, item.provider, out var actual), actual).Is((item.expectedSuccess, item.expectedResult));
            }
        }

        private static IEnumerable<(string input, Type conversionType, IFormatProvider provider, bool expectedSuccess, object expectedResult)> TryParse_TestDataSource = new(string, Type, IFormatProvider, bool, object)[]{
            // byte
            (""                      , typeof(byte), InvariantCulture, false, null),
            ("0"                     , typeof(byte), InvariantCulture, true , (byte)0),
            ("0"                     , typeof(byte), null            , true , (byte)0),
            ("\t\n 1\t\n "           , typeof(byte), InvariantCulture, true , (byte)1),
            (byte.MinValue.ToString(), typeof(byte), InvariantCulture, true , byte.MinValue),
            (byte.MaxValue.ToString(), typeof(byte), InvariantCulture, true , byte.MaxValue),
            ("+1"                    , typeof(byte), InvariantCulture, true , (byte)1),
            ("-1"                    , typeof(byte), InvariantCulture, false, null),
            ("0x10"                  , typeof(byte), InvariantCulture, false, null),
            ("1,234"                 , typeof(byte), InvariantCulture, false, null),
            ("1,234.56"              , typeof(byte), InvariantCulture, false, null),
            (null                    , typeof(byte), InvariantCulture, false, null),

            // byte?
            (""                      , typeof(byte?), InvariantCulture, false, null),
            ("0"                     , typeof(byte?), InvariantCulture, true , (byte)0),
            ("0"                     , typeof(byte?), null            , true , (byte)0),
            ("\t\n 1\t\n "           , typeof(byte?), InvariantCulture, true , (byte)1),
            (byte.MinValue.ToString(), typeof(byte?), InvariantCulture, true , byte.MinValue),
            (byte.MaxValue.ToString(), typeof(byte?), InvariantCulture, true , byte.MaxValue),
            ("+1"                    , typeof(byte?), InvariantCulture, true , (byte)1),
            ("-1"                    , typeof(byte?), InvariantCulture, false, null),
            ("0x10"                  , typeof(byte?), InvariantCulture, false, null),
            ("1,234"                 , typeof(byte?), InvariantCulture, false, null),
            ("1,234.56"              , typeof(byte?), InvariantCulture, false, null),
            (null                    , typeof(byte?), InvariantCulture, true , null),

            // sbyte
            (""                       , typeof(sbyte), InvariantCulture, false, null),
            ("0"                      , typeof(sbyte), InvariantCulture, true , (sbyte)0),
            ("0"                      , typeof(sbyte), null            , true , (sbyte)0),
            ("\t\n 1\t\n "            , typeof(sbyte), InvariantCulture, true , (sbyte)1),
            (sbyte.MinValue.ToString(), typeof(sbyte), InvariantCulture, true , sbyte.MinValue),
            (sbyte.MaxValue.ToString(), typeof(sbyte), InvariantCulture, true , sbyte.MaxValue),
            ("+1"                     , typeof(sbyte), InvariantCulture, true , (sbyte)1),
            ("-1"                     , typeof(sbyte), InvariantCulture, true , (sbyte)-1),
            ("0x10"                   , typeof(sbyte), InvariantCulture, false, null),
            ("1,234"                  , typeof(sbyte), InvariantCulture, false, null),
            ("1,234.56"               , typeof(sbyte), InvariantCulture, false, null),
            (null                     , typeof(sbyte), InvariantCulture, false, null),

            // sbyte?
            (""                       , typeof(sbyte?), InvariantCulture, false, null),
            ("0"                      , typeof(sbyte?), InvariantCulture, true , (sbyte)0),
            ("0"                      , typeof(sbyte?), null            , true , (sbyte)0),
            ("\t\n 1\t\n "            , typeof(sbyte?), InvariantCulture, true , (sbyte)1),
            (sbyte.MinValue.ToString(), typeof(sbyte?), InvariantCulture, true , sbyte.MinValue),
            (sbyte.MaxValue.ToString(), typeof(sbyte?), InvariantCulture, true , sbyte.MaxValue),
            ("+1"                     , typeof(sbyte?), InvariantCulture, true , (sbyte)1),
            ("-1"                     , typeof(sbyte?), InvariantCulture, true , (sbyte)-1),
            ("0x10"                   , typeof(sbyte?), InvariantCulture, false, null),
            ("1,234"                  , typeof(sbyte?), InvariantCulture, false, null),
            ("1,234.56"               , typeof(sbyte?), InvariantCulture, false, null),
            (null                     , typeof(sbyte?), InvariantCulture, true , null),

            // short
            (""                       , typeof(short), InvariantCulture, false, null),
            ("0"                      , typeof(short), InvariantCulture, true , (short)0),
            ("0"                      , typeof(short), null            , true , (short)0),
            ("\t\n 1\t\n "            , typeof(short), InvariantCulture, true , (short)1),
            (short.MinValue.ToString(), typeof(short), InvariantCulture, true , short.MinValue),
            (short.MaxValue.ToString(), typeof(short), InvariantCulture, true , short.MaxValue),
            ("+1"                     , typeof(short), InvariantCulture, true , (short)1),
            ("-1"                     , typeof(short), InvariantCulture, true , (short)-1),
            ("0x10"                   , typeof(short), InvariantCulture, false, null),
            ("1,234"                  , typeof(short), InvariantCulture, true , (short)1234),
            ("1,234.56"               , typeof(short), InvariantCulture, false, null),
            (null                     , typeof(short), InvariantCulture, false, null),

            // short?
            (""                       , typeof(short?), InvariantCulture, false, null),
            ("0"                      , typeof(short?), InvariantCulture, true , (short)0),
            ("0"                      , typeof(short?), null            , true , (short)0),
            ("\t\n 1\t\n "            , typeof(short?), InvariantCulture, true , (short)1),
            (short.MinValue.ToString(), typeof(short?), InvariantCulture, true , short.MinValue),
            (short.MaxValue.ToString(), typeof(short?), InvariantCulture, true , short.MaxValue),
            ("+1"                     , typeof(short?), InvariantCulture, true , (short)1),
            ("-1"                     , typeof(short?), InvariantCulture, true , (short)-1),
            ("0x10"                   , typeof(short?), InvariantCulture, false, null),
            ("1,234"                  , typeof(short?), InvariantCulture, true , (short)1234),
            ("1,234.56"               , typeof(short?), InvariantCulture, false, null),
            (null                     , typeof(short?), InvariantCulture, true , null),

            // ushort
            (""                        , typeof(ushort), InvariantCulture, false, null),
            ("0"                       , typeof(ushort), InvariantCulture, true , (ushort)0),
            ("0"                       , typeof(ushort), null            , true , (ushort)0),
            ("\t\n 1\t\n "             , typeof(ushort), InvariantCulture, true , (ushort)1),
            (ushort.MinValue.ToString(), typeof(ushort), InvariantCulture, true , ushort.MinValue),
            (ushort.MaxValue.ToString(), typeof(ushort), InvariantCulture, true , ushort.MaxValue),
            ("+1"                      , typeof(ushort), InvariantCulture, true , (ushort)1),
            ("-1"                      , typeof(ushort), InvariantCulture, false, null),
            ("0x10"                    , typeof(ushort), InvariantCulture, false, null),
            ("1,234"                   , typeof(ushort), InvariantCulture, true , (ushort)1234),
            ("1,234.56"                , typeof(ushort), InvariantCulture, false, null),
            (null                      , typeof(ushort), InvariantCulture, false, null),

            // ushort?
            (""                        , typeof(ushort?), InvariantCulture, false, null),
            ("0"                       , typeof(ushort?), InvariantCulture, true , (ushort)0),
            ("0"                       , typeof(ushort?), null            , true , (ushort)0),
            ("\t\n 1\t\n "             , typeof(ushort?), InvariantCulture, true , (ushort)1),
            (ushort.MinValue.ToString(), typeof(ushort?), InvariantCulture, true , ushort.MinValue),
            (ushort.MaxValue.ToString(), typeof(ushort?), InvariantCulture, true , ushort.MaxValue),
            ("+1"                      , typeof(ushort?), InvariantCulture, true , (ushort)1),
            ("-1"                      , typeof(ushort?), InvariantCulture, false, null),
            ("0x10"                    , typeof(ushort?), InvariantCulture, false, null),
            ("1,234"                   , typeof(ushort?), InvariantCulture, true , (ushort)1234),
            ("1,234.56"                , typeof(ushort?), InvariantCulture, false, null),
            (null                      , typeof(ushort?), InvariantCulture, true , null),

            // int
            (""                     , typeof(int), InvariantCulture, false, null),
            ("0"                    , typeof(int), InvariantCulture, true , (int)0),
            ("0"                    , typeof(int), null            , true , (int)0),
            ("\t\n 1\t\n "          , typeof(int), InvariantCulture, true , (int)1),
            (int.MinValue.ToString(), typeof(int), InvariantCulture, true , int.MinValue),
            (int.MaxValue.ToString(), typeof(int), InvariantCulture, true , int.MaxValue),
            ("+1"                   , typeof(int), InvariantCulture, true , (int)1),
            ("-1"                   , typeof(int), InvariantCulture, true , (int)-1),
            ("0x10"                 , typeof(int), InvariantCulture, false, null),
            ("1,234"                , typeof(int), InvariantCulture, true , (int)1234),
            ("1,234.56"             , typeof(int), InvariantCulture, false, null),
            (null                   , typeof(int), InvariantCulture, false, null),

            // int?
            (""                     , typeof(int?), InvariantCulture, false, null),
            ("0"                    , typeof(int?), InvariantCulture, true , (int)0),
            ("0"                    , typeof(int?), null            , true , (int)0),
            ("\t\n 1\t\n "          , typeof(int?), InvariantCulture, true , (int)1),
            (int.MinValue.ToString(), typeof(int?), InvariantCulture, true , int.MinValue),
            (int.MaxValue.ToString(), typeof(int?), InvariantCulture, true , int.MaxValue),
            ("+1"                   , typeof(int?), InvariantCulture, true , (int)1),
            ("-1"                   , typeof(int?), InvariantCulture, true , (int)-1),
            ("0x10"                 , typeof(int?), InvariantCulture, false, null),
            ("1,234"                , typeof(int?), InvariantCulture, true , (int)1234),
            ("1,234.56"             , typeof(int?), InvariantCulture, false, null),
            (null                   , typeof(int?), InvariantCulture, true , null),

            // uint
            (""                      , typeof(uint), InvariantCulture, false, null),
            ("0"                     , typeof(uint), InvariantCulture, true , (uint)0),
            ("0"                     , typeof(uint), null            , true , (uint)0),
            ("\t\n 1\t\n "           , typeof(uint), InvariantCulture, true , (uint)1),
            (uint.MinValue.ToString(), typeof(uint), InvariantCulture, true , uint.MinValue),
            (uint.MaxValue.ToString(), typeof(uint), InvariantCulture, true , uint.MaxValue),
            ("+1"                    , typeof(uint), InvariantCulture, true , (uint)1),
            ("-1"                    , typeof(uint), InvariantCulture, false, null),
            ("0x10"                  , typeof(uint), InvariantCulture, false, null),
            ("1,234"                 , typeof(uint), InvariantCulture, true , (uint)1234),
            ("1,234.56"              , typeof(uint), InvariantCulture, false, null),
            (null                    , typeof(uint), InvariantCulture, false, null),

            // uint?
            (""                      , typeof(uint?), InvariantCulture, false, null),
            ("0"                     , typeof(uint?), InvariantCulture, true , (uint)0),
            ("0"                     , typeof(uint?), null            , true , (uint)0),
            ("\t\n 1\t\n "           , typeof(uint?), InvariantCulture, true , (uint)1),
            (uint.MinValue.ToString(), typeof(uint?), InvariantCulture, true , uint.MinValue),
            (uint.MaxValue.ToString(), typeof(uint?), InvariantCulture, true , uint.MaxValue),
            ("+1"                    , typeof(uint?), InvariantCulture, true , (uint)1),
            ("-1"                    , typeof(uint?), InvariantCulture, false, null),
            ("0x10"                  , typeof(uint?), InvariantCulture, false, null),
            ("1,234"                 , typeof(uint?), InvariantCulture, true , (uint)1234),
            ("1,234.56"              , typeof(uint?), InvariantCulture, false, null),
            (null                    , typeof(uint?), InvariantCulture, true , null),

            // long
            (""                      , typeof(long), InvariantCulture, false, null),
            ("0"                     , typeof(long), InvariantCulture, true , (long)0),
            ("0"                     , typeof(long), null            , true , (long)0),
            ("\t\n 1\t\n "           , typeof(long), InvariantCulture, true , (long)1),
            (long.MinValue.ToString(), typeof(long), InvariantCulture, true , long.MinValue),
            (long.MaxValue.ToString(), typeof(long), InvariantCulture, true , long.MaxValue),
            ("+1"                    , typeof(long), InvariantCulture, true , (long)1),
            ("-1"                    , typeof(long), InvariantCulture, true , (long)-1),
            ("0x10"                  , typeof(long), InvariantCulture, false, null),
            ("1,234"                 , typeof(long), InvariantCulture, true , (long)1234),
            ("1,234.56"              , typeof(long), InvariantCulture, false, null),
            (null                    , typeof(long), InvariantCulture, false, null),

            // long?
            (""                      , typeof(long?), InvariantCulture, false, null),
            ("0"                     , typeof(long?), InvariantCulture, true , (long)0),
            ("0"                     , typeof(long?), null            , true , (long)0),
            ("\t\n 1\t\n "           , typeof(long?), InvariantCulture, true , (long)1),
            (long.MinValue.ToString(), typeof(long?), InvariantCulture, true , long.MinValue),
            (long.MaxValue.ToString(), typeof(long?), InvariantCulture, true , long.MaxValue),
            ("+1"                    , typeof(long?), InvariantCulture, true , (long)1),
            ("-1"                    , typeof(long?), InvariantCulture, true , (long)-1),
            ("0x10"                  , typeof(long?), InvariantCulture, false, null),
            ("1,234"                 , typeof(long?), InvariantCulture, true , (long)1234),
            ("1,234.56"              , typeof(long?), InvariantCulture, false, null),
            (null                    , typeof(long?), InvariantCulture, true , null),

            // ulong
            (""                       , typeof(ulong), InvariantCulture, false, null),
            ("0"                      , typeof(ulong), InvariantCulture, true , (ulong)0),
            ("0"                      , typeof(ulong), null            , true , (ulong)0),
            ("\t\n 1\t\n "            , typeof(ulong), InvariantCulture, true , (ulong)1),
            (ulong.MinValue.ToString(), typeof(ulong), InvariantCulture, true , ulong.MinValue),
            (ulong.MaxValue.ToString(), typeof(ulong), InvariantCulture, true , ulong.MaxValue),
            ("+1"                     , typeof(ulong), InvariantCulture, true , (ulong)1),
            ("-1"                     , typeof(ulong), InvariantCulture, false, null),
            ("0x10"                   , typeof(ulong), InvariantCulture, false, null),
            ("1,234"                  , typeof(ulong), InvariantCulture, true , (ulong)1234),
            ("1,234.56"               , typeof(ulong), InvariantCulture, false, null),
            (null                     , typeof(ulong), InvariantCulture, false, null),

            // ulong?
            (""                       , typeof(ulong?), InvariantCulture, false, null),
            ("0"                      , typeof(ulong?), InvariantCulture, true , (ulong)0),
            ("0"                      , typeof(ulong?), null            , true , (ulong)0),
            ("\t\n 1\t\n "            , typeof(ulong?), InvariantCulture, true , (ulong)1),
            (ulong.MinValue.ToString(), typeof(ulong?), InvariantCulture, true , ulong.MinValue),
            (ulong.MaxValue.ToString(), typeof(ulong?), InvariantCulture, true , ulong.MaxValue),
            ("+1"                     , typeof(ulong?), InvariantCulture, true , (ulong)1),
            ("-1"                     , typeof(ulong?), InvariantCulture, false, null),
            ("0x10"                   , typeof(ulong?), InvariantCulture, false, null),
            ("1,234"                  , typeof(ulong?), InvariantCulture, true , (ulong)1234),
            ("1,234.56"               , typeof(ulong?), InvariantCulture, false, null),
            (null                     , typeof(ulong?), InvariantCulture, true , null),

            // float
            (""                       , typeof(float), InvariantCulture, false, null),
            ("0"                      , typeof(float), InvariantCulture, true , (float)0),
            ("0"                      , typeof(float), null            , true , (float)0),
            ("\t\n 1\t\n "            , typeof(float), InvariantCulture, true , (float)1),
            (float.MinValue.ToString(), typeof(float), InvariantCulture, true , float.Parse(float.MinValue.ToString())),
            (float.MaxValue.ToString(), typeof(float), InvariantCulture, true , float.Parse(float.MaxValue.ToString())),
            ("+1"                     , typeof(float), InvariantCulture, true , (float)1),
            ("-1"                     , typeof(float), InvariantCulture, true , (float)-1),
            ("0x10"                   , typeof(float), InvariantCulture, false, null),
            ("1,234"                  , typeof(float), InvariantCulture, true , (float)1234),
            ("1,234.56"               , typeof(float), InvariantCulture, true , (float)1234.56),
            (null                     , typeof(float), InvariantCulture, false, null),
            ("-∞"                    , typeof(float), InvariantCulture, false, null),
            ("+∞"                    , typeof(float), InvariantCulture, false, null),
            ("∞"                     , typeof(float), InvariantCulture, false, null),
            ("NaN (非数値)"           , typeof(float), InvariantCulture, false, null),
            ("-∞"                    , typeof(float), JapaneseCulture , true , float.NegativeInfinity),
            ("+∞"                    , typeof(float), JapaneseCulture , true , float.PositiveInfinity),
            ("∞"                     , typeof(float), JapaneseCulture , false, null),
            ("NaN (非数値)"           , typeof(float), JapaneseCulture , true , float.NaN),

            // float?
            (""                       , typeof(float?), InvariantCulture, false, null),
            ("0"                      , typeof(float?), InvariantCulture, true , (float)0),
            ("0"                      , typeof(float?), null            , true , (float)0),
            ("\t\n 1\t\n "            , typeof(float?), InvariantCulture, true , (float)1),
            (float.MinValue.ToString(), typeof(float?), InvariantCulture, true , float.Parse(float.MinValue.ToString())),
            (float.MaxValue.ToString(), typeof(float?), InvariantCulture, true , float.Parse(float.MaxValue.ToString())),
            ("+1"                     , typeof(float?), InvariantCulture, true , (float)1),
            ("-1"                     , typeof(float?), InvariantCulture, true , (float)-1),
            ("0x10"                   , typeof(float?), InvariantCulture, false, null),
            ("1,234"                  , typeof(float?), InvariantCulture, true , (float)1234),
            ("1,234.56"               , typeof(float?), InvariantCulture, true , (float)1234.56),
            (null                     , typeof(float?), InvariantCulture, true , null),
            ("-∞"                    , typeof(float?), InvariantCulture, false, null),
            ("+∞"                    , typeof(float?), InvariantCulture, false, null),
            ("∞"                     , typeof(float?), InvariantCulture, false, null),
            ("NaN (非数値)"           , typeof(float?), InvariantCulture, false, null),
            ("-∞"                    , typeof(float?), JapaneseCulture , true , float.NegativeInfinity),
            ("+∞"                    , typeof(float?), JapaneseCulture , true , float.PositiveInfinity),
            ("∞"                     , typeof(float?), JapaneseCulture , false, null),
            ("NaN (非数値)"           , typeof(float?), JapaneseCulture , true , float.NaN),

            // double
            (""                       , typeof(double), InvariantCulture, false, null),
            ("0"                      , typeof(double), InvariantCulture, true , (double)0),
            ("0"                      , typeof(double), null            , true , (double)0),
            ("\t\n 1\t\n "            , typeof(double), InvariantCulture, true , (double)1),
            (float.MinValue.ToString(), typeof(double), InvariantCulture, true , double.Parse(float.MinValue.ToString())),
            (float.MaxValue.ToString(), typeof(double), InvariantCulture, true , double.Parse(float.MaxValue.ToString())),
            ("+1"                     , typeof(double), InvariantCulture, true , (double)1),
            ("-1"                     , typeof(double), InvariantCulture, true , (double)-1),
            ("0x10"                   , typeof(double), InvariantCulture, false, null),
            ("1,234"                  , typeof(double), InvariantCulture, true , (double)1234),
            ("1,234.56"               , typeof(double), InvariantCulture, true , (double)1234.56),
            (null                     , typeof(double), InvariantCulture, false, null),
            ("-∞"                    , typeof(double), InvariantCulture, false, null),
            ("+∞"                    , typeof(double), InvariantCulture, false, null),
            ("∞"                     , typeof(double), InvariantCulture, false, null),
            ("NaN (非数値)"           , typeof(double), InvariantCulture, false, null),
            ("-∞"                    , typeof(double), JapaneseCulture , true , double.NegativeInfinity),
            ("+∞"                    , typeof(double), JapaneseCulture , true , double.PositiveInfinity),
            ("∞"                     , typeof(double), JapaneseCulture , false, null),
            ("NaN (非数値)"           , typeof(double), JapaneseCulture , true , double.NaN),

            // double?
            (""                       , typeof(double?), InvariantCulture, false, null),
            ("0"                      , typeof(double?), InvariantCulture, true , (double)0),
            ("0"                      , typeof(double?), null            , true , (double)0),
            ("\t\n 1\t\n "            , typeof(double?), InvariantCulture, true , (double)1),
            (float.MinValue.ToString(), typeof(double?), InvariantCulture, true , double.Parse(float.MinValue.ToString())),
            (float.MaxValue.ToString(), typeof(double?), InvariantCulture, true , double.Parse(float.MaxValue.ToString())),
            ("+1"                     , typeof(double?), InvariantCulture, true , (double)1),
            ("-1"                     , typeof(double?), InvariantCulture, true , (double)-1),
            ("0x10"                   , typeof(double?), InvariantCulture, false, null),
            ("1,234"                  , typeof(double?), InvariantCulture, true , (double)1234),
            ("1,234.56"               , typeof(double?), InvariantCulture, true , (double)1234.56),
            (null                     , typeof(double?), InvariantCulture, true , null),
            ("-∞"                    , typeof(double?), InvariantCulture, false, null),
            ("+∞"                    , typeof(double?), InvariantCulture, false, null),
            ("∞"                     , typeof(double?), InvariantCulture, false, null),
            ("NaN (非数値)"           , typeof(double?), InvariantCulture, false, null),
            ("-∞"                    , typeof(double?), JapaneseCulture , true , double.NegativeInfinity),
            ("+∞"                    , typeof(double?), JapaneseCulture , true , double.PositiveInfinity),
            ("∞"                     , typeof(double?), JapaneseCulture , false, null),
            ("NaN (非数値)"           , typeof(double?), JapaneseCulture , true , double.NaN),

            // decimal
            (""                         , typeof(decimal), InvariantCulture, false, null),
            ("0"                        , typeof(decimal), InvariantCulture, true , (decimal)0),
            ("0"                        , typeof(decimal), null            , true , (decimal)0),
            ("\t\n 1\t\n "              , typeof(decimal), InvariantCulture, true , (decimal)1),
            (decimal.MinValue.ToString(), typeof(decimal), InvariantCulture, true , decimal.Parse(decimal.MinValue.ToString())),
            (decimal.MaxValue.ToString(), typeof(decimal), InvariantCulture, true , decimal.Parse(decimal.MaxValue.ToString())),
            ("+1"                       , typeof(decimal), InvariantCulture, true , (decimal)1),
            ("-1"                       , typeof(decimal), InvariantCulture, true , (decimal)-1),
            ("0x10"                     , typeof(decimal), InvariantCulture, false, null),
            ("1,234"                    , typeof(decimal), InvariantCulture, true , (decimal)1234),
            ("1,234.56"                 , typeof(decimal), InvariantCulture, true , (decimal)1234.56),
            (null                       , typeof(decimal), InvariantCulture, false, null),
            ("-∞"                      , typeof(decimal), InvariantCulture, false, null),
            ("+∞"                      , typeof(decimal), InvariantCulture, false, null),
            ("∞"                       , typeof(decimal), InvariantCulture, false, null),
            ("NaN (非数値)"             , typeof(decimal), InvariantCulture, false, null),
            ("-∞"                      , typeof(decimal), JapaneseCulture , false, null),
            ("+∞"                      , typeof(decimal), JapaneseCulture , false, null),
            ("∞"                       , typeof(decimal), JapaneseCulture , false, null),
            ("NaN (非数値)"             , typeof(decimal), JapaneseCulture , false, null),
            (decimal.Zero.ToString()    , typeof(decimal), InvariantCulture, true , decimal.Zero),
            (decimal.One.ToString()     , typeof(decimal), InvariantCulture, true , decimal.One),
            (decimal.MinusOne.ToString(), typeof(decimal), InvariantCulture, true , decimal.MinusOne),

            // decimal?
            (""                         , typeof(decimal?), InvariantCulture, false, null),
            ("0"                        , typeof(decimal?), InvariantCulture, true , (decimal)0),
            ("0"                        , typeof(decimal?), null            , true , (decimal)0),
            ("\t\n 1\t\n "              , typeof(decimal?), InvariantCulture, true , (decimal)1),
            (decimal.MinValue.ToString(), typeof(decimal?), InvariantCulture, true , decimal.Parse(decimal.MinValue.ToString())),
            (decimal.MaxValue.ToString(), typeof(decimal?), InvariantCulture, true , decimal.Parse(decimal.MaxValue.ToString())),
            ("+1"                       , typeof(decimal?), InvariantCulture, true , (decimal)1),
            ("-1"                       , typeof(decimal?), InvariantCulture, true , (decimal)-1),
            ("0x10"                     , typeof(decimal?), InvariantCulture, false, null),
            ("1,234"                    , typeof(decimal?), InvariantCulture, true , (decimal)1234),
            ("1,234.56"                 , typeof(decimal?), InvariantCulture, true , (decimal)1234.56),
            (null                       , typeof(decimal?), InvariantCulture, true , null),
            ("-∞"                      , typeof(decimal?), InvariantCulture, false, null),
            ("+∞"                      , typeof(decimal?), InvariantCulture, false, null),
            ("∞"                       , typeof(decimal?), InvariantCulture, false, null),
            ("NaN (非数値)"             , typeof(decimal?), InvariantCulture, false, null),
            ("-∞"                      , typeof(decimal?), JapaneseCulture , false, null),
            ("+∞"                      , typeof(decimal?), JapaneseCulture , false, null),
            ("∞"                       , typeof(decimal?), JapaneseCulture , false, null),
            ("NaN (非数値)"             , typeof(decimal?), JapaneseCulture , false, null),

            // bool
            (""         , typeof(bool), InvariantCulture, false, null),
            ("true"     , typeof(bool), InvariantCulture, true , true),
            ("true"     , typeof(bool), null            , true , true),
            ("\ttrue\n ", typeof(bool), InvariantCulture, true , true),
            ("false"    , typeof(bool), InvariantCulture, true , false),
            ("True"     , typeof(bool), InvariantCulture, true , true),
            ("False"    , typeof(bool), InvariantCulture, true , false),
            ("TRUE"     , typeof(bool), InvariantCulture, true , true),
            ("FALSE"    , typeof(bool), InvariantCulture, true , false),
            ("tRue"     , typeof(bool), InvariantCulture, true , true),
            ("fAlse"    , typeof(bool), InvariantCulture, true , false),
            ("0"        , typeof(bool), InvariantCulture, false, null),
            ("+1"       , typeof(bool), InvariantCulture, false, null),
            ("-1"       , typeof(bool), InvariantCulture, false, null),
            ("0x10"     , typeof(bool), InvariantCulture, false, null),
            (null       , typeof(bool), InvariantCulture, false, null),

            // bool?
            (""         , typeof(bool?), InvariantCulture, false, null),
            ("true"     , typeof(bool?), InvariantCulture, true , true),
            ("true"     , typeof(bool?), null            , true , true),
            ("\ttrue\n ", typeof(bool?), InvariantCulture, true , true),
            ("false"    , typeof(bool?), InvariantCulture, true , false),
            ("True"     , typeof(bool?), InvariantCulture, true , true),
            ("False"    , typeof(bool?), InvariantCulture, true , false),
            ("TRUE"     , typeof(bool?), InvariantCulture, true , true),
            ("FALSE"    , typeof(bool?), InvariantCulture, true , false),
            ("tRue"     , typeof(bool?), InvariantCulture, true , true),
            ("fAlse"    , typeof(bool?), InvariantCulture, true , false),
            ("0"        , typeof(bool?), InvariantCulture, false, null),
            ("+1"       , typeof(bool?), InvariantCulture, false, null),
            ("-1"       , typeof(bool?), InvariantCulture, false, null),
            ("0x10"     , typeof(bool?), InvariantCulture, false, null),
            (null       , typeof(bool?), InvariantCulture, true , null),

            // char
            (""                      , typeof(char), InvariantCulture, false, null),
            ("0"                     , typeof(char), InvariantCulture, true , '0'),
            ("0"                     , typeof(char), null            , true , '0'),
            (" 1 "                   , typeof(char), InvariantCulture, false, null),
            (char.MinValue.ToString(), typeof(char), InvariantCulture, true , char.MinValue),
            (char.MaxValue.ToString(), typeof(char), InvariantCulture, true , char.MaxValue),
            ("+1"                    , typeof(char), InvariantCulture, false, null),
            ("-1"                    , typeof(char), InvariantCulture, false, null),
            ("0x10"                  , typeof(char), InvariantCulture, false, null),
            ("1,234"                 , typeof(char), InvariantCulture, false, null),
            ("1,234.56"              , typeof(char), InvariantCulture, false, null),
            (null                    , typeof(char), InvariantCulture, false, null),
            ("a"                     , typeof(char), InvariantCulture, true , 'a'),
            ("ab"                    , typeof(char), InvariantCulture, false, null),
            ("あ"                    , typeof(char), InvariantCulture, true , 'あ'),
            ("あ"                    , typeof(char), JapaneseCulture , true , 'あ'),

            // char?
            (""                      , typeof(char?), InvariantCulture, false, null),
            ("0"                     , typeof(char?), InvariantCulture, true , '0'),
            ("0"                     , typeof(char?), null            , true , '0'),
            (" 1 "                   , typeof(char?), InvariantCulture, false, null),
            (char.MinValue.ToString(), typeof(char?), InvariantCulture, true , char.MinValue),
            (char.MaxValue.ToString(), typeof(char?), InvariantCulture, true , char.MaxValue),
            ("+1"                    , typeof(char?), InvariantCulture, false, null),
            ("-1"                    , typeof(char?), InvariantCulture, false, null),
            ("0x10"                  , typeof(char?), InvariantCulture, false, null),
            ("1,234"                 , typeof(char?), InvariantCulture, false, null),
            ("1,234.56"              , typeof(char?), InvariantCulture, false, null),
            (null                    , typeof(char?), InvariantCulture, true , null),
            ("a"                     , typeof(char?), InvariantCulture, true , 'a'),
            ("ab"                    , typeof(char?), InvariantCulture, false, null),
            ("あ"                    , typeof(char?), InvariantCulture, true , 'あ'),
            ("あ"                    , typeof(char?), JapaneseCulture , true , 'あ'),

            // DateTime
            (""                          , typeof(DateTime), InvariantCulture, false, null),
            ("0"                         , typeof(DateTime), InvariantCulture, false, null),
            (DateTime.MinValue.ToString(), typeof(DateTime), InvariantCulture, true , DateTime.Parse(DateTime.MinValue.ToString())),
            (DateTime.MaxValue.ToString(), typeof(DateTime), InvariantCulture, true , DateTime.Parse(DateTime.MaxValue.ToString())),
            ("+1"                        , typeof(DateTime), InvariantCulture, false, null),
            ("-1"                        , typeof(DateTime), InvariantCulture, false, null),
            ("0x10"                      , typeof(DateTime), InvariantCulture, false, null),
            ("1,234"                     , typeof(DateTime), InvariantCulture, true , new DateTime(234, 1, 1)),
            ("1,234.56"                  , typeof(DateTime), InvariantCulture, false, null),
            (null                        , typeof(DateTime), InvariantCulture, false, null),
            ("2018-2-15 13:00"           , typeof(DateTime), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2018-2-15 13:00"           , typeof(DateTime), null            , true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2018/2/15 13:00"           , typeof(DateTime), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("\t\n 2018-2-15\t\n13:00\n ", typeof(DateTime), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2-15-2018 13:00"           , typeof(DateTime), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2/15/2018 13:00"           , typeof(DateTime), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2018/2/15 1:00 PM"         , typeof(DateTime), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2018年2月15日 13時0分"     , typeof(DateTime), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2018年 2月 15日 13時 0分"  , typeof(DateTime), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),

            // DateTime?
            (""                          , typeof(DateTime?), InvariantCulture, false, null),
            ("0"                         , typeof(DateTime?), InvariantCulture, false, null),
            (DateTime.MinValue.ToString(), typeof(DateTime?), InvariantCulture, true , DateTime.Parse(DateTime.MinValue.ToString())),
            (DateTime.MaxValue.ToString(), typeof(DateTime?), InvariantCulture, true , DateTime.Parse(DateTime.MaxValue.ToString())),
            ("+1"                        , typeof(DateTime?), InvariantCulture, false, null),
            ("-1"                        , typeof(DateTime?), InvariantCulture, false, null),
            ("0x10"                      , typeof(DateTime?), InvariantCulture, false, null),
            ("1,234"                     , typeof(DateTime?), InvariantCulture, true , new DateTime(234, 1, 1)),
            ("1,234.56"                  , typeof(DateTime?), InvariantCulture, false, null),
            (null                        , typeof(DateTime?), InvariantCulture, true , null),
            ("2018-2-15 13:00"           , typeof(DateTime?), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2018-2-15 13:00"           , typeof(DateTime?), null            , true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2018/2/15 13:00"           , typeof(DateTime?), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("\t\n 2018-2-15\t\n13:00\n ", typeof(DateTime?), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2-15-2018 13:00"           , typeof(DateTime?), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2/15/2018 13:00"           , typeof(DateTime?), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2018/2/15 1:00 PM"         , typeof(DateTime?), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2018年2月15日 13時0分"     , typeof(DateTime?), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),
            ("2018年 2月 15日 13時 0分"  , typeof(DateTime?), InvariantCulture, true , new DateTime(2018, 2, 15, 13, 0, 0)),

            // TimeSpan
            (""                          , typeof(TimeSpan), InvariantCulture, false, null),
            ("0"                         , typeof(TimeSpan), InvariantCulture, true , TimeSpan.Zero),
            (TimeSpan.MinValue.ToString(), typeof(TimeSpan), InvariantCulture, true , TimeSpan.Parse(TimeSpan.MinValue.ToString())),
            (TimeSpan.MaxValue.ToString(), typeof(TimeSpan), InvariantCulture, true , TimeSpan.Parse(TimeSpan.MaxValue.ToString())),
            ("+1"                        , typeof(TimeSpan), InvariantCulture, false, null),
            ("-1"                        , typeof(TimeSpan), InvariantCulture, true , new TimeSpan(-1, 0, 0, 0)),
            ("0x10"                      , typeof(TimeSpan), InvariantCulture, false, null),
            ("1,234"                     , typeof(TimeSpan), InvariantCulture, false, null),
            ("1,234.56"                  , typeof(TimeSpan), InvariantCulture, false, null),
            (null                        , typeof(TimeSpan), InvariantCulture, false, null),
            ("1.23:45:12.345"            , typeof(TimeSpan), InvariantCulture, true , new TimeSpan(1, 23, 45, 12, 345)),
            ("1.23:45:12.345"            , typeof(TimeSpan), null            , true , new TimeSpan(1, 23, 45, 12, 345)),
            ("\t\n 1.23:45:12.345\t\n "  , typeof(TimeSpan), InvariantCulture, true , new TimeSpan(1, 23, 45, 12, 345)),
            ("23 : 45 : 12"              , typeof(TimeSpan), InvariantCulture, false, null),
            ("11:45:12 PM"               , typeof(TimeSpan), InvariantCulture, false, null),
            ("1:23:45:12.345"            , typeof(TimeSpan), InvariantCulture, true , new TimeSpan(1, 23, 45, 12, 345)),
            ("+1.23:45:12.345"           , typeof(TimeSpan), InvariantCulture, false, null),
            ("-1.23:45:12.345"           , typeof(TimeSpan), InvariantCulture, true , -new TimeSpan(1, 23, 45, 12, 345)),
            ("1234.23:45:12.345"         , typeof(TimeSpan), InvariantCulture, true , new TimeSpan(1234, 23, 45, 12, 345)),
            ("1,234.23:45:12.345"        , typeof(TimeSpan), InvariantCulture, false, null),

            // TimeSpan?
            (""                          , typeof(TimeSpan?), InvariantCulture, false, null),
            ("0"                         , typeof(TimeSpan?), InvariantCulture, true , TimeSpan.Zero),
            (TimeSpan.MinValue.ToString(), typeof(TimeSpan?), InvariantCulture, true , TimeSpan.Parse(TimeSpan.MinValue.ToString())),
            (TimeSpan.MaxValue.ToString(), typeof(TimeSpan?), InvariantCulture, true , TimeSpan.Parse(TimeSpan.MaxValue.ToString())),
            ("+1"                        , typeof(TimeSpan?), InvariantCulture, false, null),
            ("-1"                        , typeof(TimeSpan?), InvariantCulture, true , new TimeSpan(-1, 0, 0, 0)),
            ("0x10"                      , typeof(TimeSpan?), InvariantCulture, false, null),
            ("1,234"                     , typeof(TimeSpan?), InvariantCulture, false, null),
            ("1,234.56"                  , typeof(TimeSpan?), InvariantCulture, false, null),
            (null                        , typeof(TimeSpan?), InvariantCulture, true , null),
            ("1.23:45:12.345"            , typeof(TimeSpan?), InvariantCulture, true , new TimeSpan(1, 23, 45, 12, 345)),
            ("1.23:45:12.345"            , typeof(TimeSpan?), null            , true , new TimeSpan(1, 23, 45, 12, 345)),
            ("\t\n 1.23:45:12.345\t\n "  , typeof(TimeSpan?), InvariantCulture, true , new TimeSpan(1, 23, 45, 12, 345)),
            ("23 : 45 : 12"              , typeof(TimeSpan?), InvariantCulture, false, null),
            ("11:45:12 PM"               , typeof(TimeSpan?), InvariantCulture, false, null),
            ("1:23:45:12.345"            , typeof(TimeSpan?), InvariantCulture, true , new TimeSpan(1, 23, 45, 12, 345)),
            ("+1.23:45:12.345"           , typeof(TimeSpan?), InvariantCulture, false, null),
            ("-1.23:45:12.345"           , typeof(TimeSpan?), InvariantCulture, true , -new TimeSpan(1, 23, 45, 12, 345)),
            ("1234.23:45:12.345"         , typeof(TimeSpan?), InvariantCulture, true , new TimeSpan(1234, 23, 45, 12, 345)),
            ("1,234.23:45:12.345"        , typeof(TimeSpan?), InvariantCulture, false, null),

            // Enum(byte)
            (""                        , typeof(TestByteEnum), InvariantCulture, false, null),
            ("0"                       , typeof(TestByteEnum), InvariantCulture, true , (TestByteEnum)0),
            ("0"                       , typeof(TestByteEnum), null            , true , (TestByteEnum)0),
            ("\t\n 1\t\n "             , typeof(TestByteEnum), InvariantCulture, true , (TestByteEnum)1),
            (nameof(TestByteEnum.None) , typeof(TestByteEnum), InvariantCulture, true , TestByteEnum.None),
            (nameof(TestByteEnum.Alpha), typeof(TestByteEnum), InvariantCulture, true , TestByteEnum.Alpha),
            (nameof(TestByteEnum.Beta) , typeof(TestByteEnum), InvariantCulture, true , TestByteEnum.Beta),
            ("\t\n bETa \t\n "         , typeof(TestByteEnum), InvariantCulture, true , TestByteEnum.Beta),
            ("+1"                      , typeof(TestByteEnum), InvariantCulture, true , (TestByteEnum)1),
            ("-1"                      , typeof(TestByteEnum), InvariantCulture, false, null),
            ("0x10"                    , typeof(TestByteEnum), InvariantCulture, false, null),
            ("1234"                    , typeof(TestByteEnum), InvariantCulture, false, null),
            ("1,234"                   , typeof(TestByteEnum), InvariantCulture, false, null),
            ("1,234.56"                , typeof(TestByteEnum), InvariantCulture, false, null),
            (null                      , typeof(TestByteEnum), InvariantCulture, false, null),

            // Enum(byte)?
            (""                        , typeof(TestByteEnum?), InvariantCulture, false, null),
            ("0"                       , typeof(TestByteEnum?), InvariantCulture, true , (TestByteEnum)0),
            ("0"                       , typeof(TestByteEnum?), null            , true , (TestByteEnum)0),
            ("\t\n 1\t\n "             , typeof(TestByteEnum?), InvariantCulture, true , (TestByteEnum)1),
            (nameof(TestByteEnum.None) , typeof(TestByteEnum?), InvariantCulture, true , TestByteEnum.None),
            (nameof(TestByteEnum.Alpha), typeof(TestByteEnum?), InvariantCulture, true , TestByteEnum.Alpha),
            (nameof(TestByteEnum.Beta) , typeof(TestByteEnum?), InvariantCulture, true , TestByteEnum.Beta),
            ("\t\n bETa \t\n "         , typeof(TestByteEnum?), InvariantCulture, true , TestByteEnum.Beta),
            ("+1"                      , typeof(TestByteEnum?), InvariantCulture, true , (TestByteEnum)1),
            ("-1"                      , typeof(TestByteEnum?), InvariantCulture, false, null),
            ("0x10"                    , typeof(TestByteEnum?), InvariantCulture, false, null),
            ("1234"                    , typeof(TestByteEnum?), InvariantCulture, false, null),
            ("1,234"                   , typeof(TestByteEnum?), InvariantCulture, false, null),
            ("1,234.56"                , typeof(TestByteEnum?), InvariantCulture, false, null),
            (null                      , typeof(TestByteEnum?), InvariantCulture, true , null),

            // Enum(int)
            (""                       , typeof(TestIntEnum), InvariantCulture, false, null),
            ("0"                      , typeof(TestIntEnum), InvariantCulture, true , (TestIntEnum)0),
            ("0"                      , typeof(TestIntEnum), null            , true , (TestIntEnum)0),
            ("\t\n 1\t\n "            , typeof(TestIntEnum), InvariantCulture, true , (TestIntEnum)1),
            (nameof(TestIntEnum.None) , typeof(TestIntEnum), InvariantCulture, true , TestIntEnum.None),
            (nameof(TestIntEnum.Gamma), typeof(TestIntEnum), InvariantCulture, true , TestIntEnum.Gamma),
            (nameof(TestIntEnum.Delta), typeof(TestIntEnum), InvariantCulture, true , TestIntEnum.Delta),
            ("\t\n dELta \t\n "       , typeof(TestIntEnum), InvariantCulture, true , TestIntEnum.Delta),
            ("+1"                     , typeof(TestIntEnum), InvariantCulture, true , (TestIntEnum)1),
            ("-1"                     , typeof(TestIntEnum), InvariantCulture, true , (TestIntEnum)(-1)),
            ("0x10"                   , typeof(TestIntEnum), InvariantCulture, false, null),
            ("1234"                   , typeof(TestIntEnum), InvariantCulture, true , (TestIntEnum)1234),
            ("1,234"                  , typeof(TestIntEnum), InvariantCulture, false, null),
            ("1,234.56"               , typeof(TestIntEnum), InvariantCulture, false, null),
            (null                     , typeof(TestIntEnum), InvariantCulture, false, null),

            // Enum(int)?
            (""                       , typeof(TestIntEnum?), InvariantCulture, false, null),
            ("0"                      , typeof(TestIntEnum?), InvariantCulture, true , (TestIntEnum)0),
            ("0"                      , typeof(TestIntEnum?), null            , true , (TestIntEnum)0),
            ("\t\n 1\t\n "            , typeof(TestIntEnum?), InvariantCulture, true , (TestIntEnum)1),
            (nameof(TestIntEnum.None) , typeof(TestIntEnum?), InvariantCulture, true , TestIntEnum.None),
            (nameof(TestIntEnum.Gamma), typeof(TestIntEnum?), InvariantCulture, true , TestIntEnum.Gamma),
            (nameof(TestIntEnum.Delta), typeof(TestIntEnum?), InvariantCulture, true , TestIntEnum.Delta),
            ("\t\n dELta \t\n "       , typeof(TestIntEnum?), InvariantCulture, true , TestIntEnum.Delta),
            ("+1"                     , typeof(TestIntEnum?), InvariantCulture, true , (TestIntEnum)1),
            ("-1"                     , typeof(TestIntEnum?), InvariantCulture, true , (TestIntEnum)(-1)),
            ("0x10"                   , typeof(TestIntEnum?), InvariantCulture, false, null),
            ("1234"                   , typeof(TestIntEnum?), InvariantCulture, true , (TestIntEnum)1234),
            ("1,234"                  , typeof(TestIntEnum?), InvariantCulture, false, null),
            ("1,234.56"               , typeof(TestIntEnum?), InvariantCulture, false, null),
            (null                     , typeof(TestIntEnum?), InvariantCulture, true , null),

            // Guid
            (""                                                , typeof(Guid), InvariantCulture, false, null),
            ("0"                                               , typeof(Guid), InvariantCulture, false, null),
            (Guid.Empty.ToString()                             , typeof(Guid), InvariantCulture, true , Guid.Empty),
            ("+1"                                              , typeof(Guid), InvariantCulture, false, null),
            ("-1"                                              , typeof(Guid), InvariantCulture, false, null),
            ("0x10"                                            , typeof(Guid), InvariantCulture, false, null),
            ("1,234"                                           , typeof(Guid), InvariantCulture, false, null),
            ("1,234.56"                                        , typeof(Guid), InvariantCulture, false, null),
            (null                                              , typeof(Guid), InvariantCulture, false, null),
            ("00000000-0000-0000-0000-000000000000"            , typeof(Guid), InvariantCulture, true , new Guid()),
            ("00000000-0000-0000-0000-000000000000"            , typeof(Guid), null            , true , new Guid()),
            ("00000000000000000000000000000000"                , typeof(Guid), InvariantCulture, true , new Guid()),
            ("{00000000-0000-0000-0000-000000000000}"          , typeof(Guid), InvariantCulture, true , new Guid()),
            ("{00000000000000000000000000000000}"              , typeof(Guid), InvariantCulture, false, null),
            ("{ 00000000-0000-0000-0000-000000000000 }"        , typeof(Guid), InvariantCulture, false, null),
            ("\t\n 00000000-0000-0000-0000-000000000000\t\n "  , typeof(Guid), InvariantCulture, true , new Guid()),
            ("\t\n 00000000000000000000000000000000\t\n "      , typeof(Guid), InvariantCulture, true , new Guid()),
            ("\t\n {00000000-0000-0000-0000-000000000000}\t\n ", typeof(Guid), InvariantCulture, true , new Guid()),
            ("\t\n {00000000000000000000000000000000}\t\n "    , typeof(Guid), InvariantCulture, false, null),
            ("0000 0000 0000 0000 0000 0000 0000 0000"         , typeof(Guid), InvariantCulture, false, null),
            ("0000-0000-0000-0000-0000-0000-0000-0000"         , typeof(Guid), InvariantCulture, false, null),
            ("01234567-89ab-cdef-0123-456789abcdef"            , typeof(Guid), InvariantCulture, true , new Guid("01234567-89ab-cdef-0123-456789abcdef")),

            // Guid?
            (""                                                , typeof(Guid?), InvariantCulture, false, null),
            ("0"                                               , typeof(Guid?), InvariantCulture, false, null),
            (Guid.Empty.ToString()                             , typeof(Guid?), InvariantCulture, true , Guid.Empty),
            ("+1"                                              , typeof(Guid?), InvariantCulture, false, null),
            ("-1"                                              , typeof(Guid?), InvariantCulture, false, null),
            ("0x10"                                            , typeof(Guid?), InvariantCulture, false, null),
            ("1,234"                                           , typeof(Guid?), InvariantCulture, false, null),
            ("1,234.56"                                        , typeof(Guid?), InvariantCulture, false, null),
            (null                                              , typeof(Guid?), InvariantCulture, true , null),
            ("00000000-0000-0000-0000-000000000000"            , typeof(Guid?), InvariantCulture, true , new Guid()),
            ("00000000-0000-0000-0000-000000000000"            , typeof(Guid?), null            , true , new Guid()),
            ("00000000000000000000000000000000"                , typeof(Guid?), InvariantCulture, true , new Guid()),
            ("{00000000-0000-0000-0000-000000000000}"          , typeof(Guid?), InvariantCulture, true , new Guid()),
            ("{00000000-0000-0000-0000-000000000000"           , typeof(Guid?), InvariantCulture, false, null),
            ("{00000000000000000000000000000000}"              , typeof(Guid?), InvariantCulture, false, null),
            ("{ 00000000-0000-0000-0000-000000000000 }"        , typeof(Guid?), InvariantCulture, false, null),
            ("\t\n 00000000-0000-0000-0000-000000000000\t\n "  , typeof(Guid?), InvariantCulture, true , new Guid()),
            ("\t\n 00000000000000000000000000000000\t\n "      , typeof(Guid?), InvariantCulture, true , new Guid()),
            ("\t\n {00000000-0000-0000-0000-000000000000}\t\n ", typeof(Guid?), InvariantCulture, true , new Guid()),
            ("\t\n {00000000000000000000000000000000}\t\n "    , typeof(Guid?), InvariantCulture, false, null),
            ("0000 0000 0000 0000 0000 0000 0000 0000"         , typeof(Guid?), InvariantCulture, false, null),
            ("0000-0000-0000-0000-0000-0000-0000-0000"         , typeof(Guid?), InvariantCulture, false, null),
            ("01234567-89ab-cdef-0123-456789abcdef"            , typeof(Guid?), InvariantCulture, true , new Guid("01234567-89ab-cdef-0123-456789abcdef")),

            // string
            (""           , typeof(string), InvariantCulture, true, ""),
            ("0"          , typeof(string), InvariantCulture, true, "0"),
            ("0"          , typeof(string), null            , true, "0"),
            ("\t\n 0\t\n ", typeof(string), InvariantCulture, true, "\t\n 0\t\n "),
            ("+1"         , typeof(string), InvariantCulture, true, "+1"),
            ("-1"         , typeof(string), InvariantCulture, true, "-1"),
            ("0x10"       , typeof(string), InvariantCulture, true, "0x10"),
            ("1,234"      , typeof(string), InvariantCulture, true, "1,234"),
            ("1,234.56"   , typeof(string), InvariantCulture, true, "1,234.56"),
            (null         , typeof(string), InvariantCulture, true, null),

            // Version
            (""                       , typeof(Version), InvariantCulture, false, null),
            ("0"                      , typeof(Version), InvariantCulture, false, null),
            ("+1"                     , typeof(Version), InvariantCulture, false, null),
            ("-1"                     , typeof(Version), InvariantCulture, false, null),
            ("0x10"                   , typeof(Version), InvariantCulture, false, null),
            ("1,234"                  , typeof(Version), InvariantCulture, false, null),
            ("1,234.56"               , typeof(Version), InvariantCulture, false, null),
            (null                     , typeof(Version), InvariantCulture, true , null),
            ("0.0"                    , typeof(Version), InvariantCulture, true , new Version(0, 0)),
            ("0.0"                    , typeof(Version), null            , true , new Version(0, 0)),
            ("0.0.0"                  , typeof(Version), InvariantCulture, true , new Version(0, 0, 0)),
            ("0.0.0.0"                , typeof(Version), InvariantCulture, true , new Version(0, 0, 0, 0)),
            ("0.0.0.0.0"              , typeof(Version), InvariantCulture, false, null),
            ("-1.0.0.0   "            , typeof(Version), InvariantCulture, false, null),
            ("1.2.3.4"                , typeof(Version), InvariantCulture, true , new Version(1, 2, 3, 4)),
            ("\t\n 1\t\n .\t\n 2\t\n ", typeof(Version), InvariantCulture, true , new Version(1, 2)),
            (".1.2"                   , typeof(Version), InvariantCulture, false , null),
            ("1.2."                   , typeof(Version), InvariantCulture, false , null),

            // Uri
            (""                             , typeof(Uri), InvariantCulture, false, null),
            ("0"                            , typeof(Uri), InvariantCulture, false, null),
            ("+1"                           , typeof(Uri), InvariantCulture, false, null),
            ("-1"                           , typeof(Uri), InvariantCulture, false, null),
            ("0x10"                         , typeof(Uri), InvariantCulture, false, null),
            ("1,234"                        , typeof(Uri), InvariantCulture, false, null),
            ("1,234.56"                     , typeof(Uri), InvariantCulture, false, null),
            (null                           , typeof(Uri), InvariantCulture, true , null),
            ("http://example.com"           , typeof(Uri), InvariantCulture, true, new Uri("http://example.com/")),
            ("http://example.com"           , typeof(Uri), null            , true, new Uri("http://example.com/")),
            ("\t\n http://example.com \t\n ", typeof(Uri), InvariantCulture, true, new Uri("http://example.com/")),
            ("http://にほんご.jp"           , typeof(Uri), InvariantCulture, true, new Uri("http://にほんご.jp/")),
            ("ftp://example.com"            , typeof(Uri), InvariantCulture, true, new Uri("ftp://example.com/")),
            ("file:///d:/temp.txt"          , typeof(Uri), InvariantCulture, true, new Uri("file:///d:/temp.txt")),

            // Custom class
            (""                             , typeof(Foo), InvariantCulture, false, null),
            ("0"                            , typeof(Foo), InvariantCulture, false, null),
            ("+1"                           , typeof(Foo), InvariantCulture, false, null),
            ("-1"                           , typeof(Foo), InvariantCulture, false, null),
            ("0x10"                         , typeof(Foo), InvariantCulture, false, null),
            ("1,234"                        , typeof(Foo), InvariantCulture, false, null),
            ("1,234.56"                     , typeof(Foo), InvariantCulture, false, null),
            (null                           , typeof(Foo), InvariantCulture, true , null),
        };

        private enum TestByteEnum : byte {
            None = 0,
            Alpha = 1,
            Beta = 2,
        }

        private enum TestIntEnum : int {
            None = 0,
            Gamma = 3,
            Delta = 4,
        }

        private class Foo {
        }
    }
}