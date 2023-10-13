using System.Data;
using System.Data.SqlTypes;

namespace DataBaseNull
{
    public static class DataReaderNull
    {
        public static byte? TryGetByte(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (byte?)reader.GetByte(index) : null;
        }

        public static short? TryGetInt16(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (short?)reader.GetInt16(index) : null;
        }

        public static int? TryGetInt32(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (int?)reader.GetInt32(index) : null;
        }

        public static long? TryGetInt64(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (long?)reader.GetInt64(index) : null;
        }

        public static string TryGetString(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? reader.GetString(index) : null;
        }

        public static float? TryGetFloat(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (float?)reader.GetFloat(index) : null;
        }

        public static double? TryGetDouble(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (double?)reader.GetDouble(index) : null;
        }

        public static decimal? TryGetDecimal(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (decimal?)reader.GetDecimal(index) : null;
        }

        public static char? TryGetChar(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            if (!reader.IsDBNull(index))
            {
                string value = reader.GetString(index);
                return !string.IsNullOrEmpty(value) ? value[0] : (char?)null;
            }

            return null;
        }

        public static bool? TryGetBoolean(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (bool?)reader.GetBoolean(index) : null;
        }
    }

    public static class DataReaderExtensions
    {
        public static bool GetBoolean(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetBoolean method.");
            }

            return reader.GetBoolean(ordinalOrThrow);
        }

        public static DateTime GetDateTime(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetDateTime method.");
            }

            return reader.GetDateTime(ordinalOrThrow);
        }

        public static decimal GetDecimal(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetDecimal method.");
            }

            return reader.GetDecimal(ordinalOrThrow);
        }

        public static double GetDouble(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetDouble method.");
            }

            return reader.GetDouble(ordinalOrThrow);
        }

        public static float GetFloat(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetFloat method.");
            }

            return reader.GetFloat(ordinalOrThrow);
        }

        public static Guid GetGuid(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetGuid method.");
            }

            return reader.GetGuid(ordinalOrThrow);
        }

        public static short GetInt16(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetInt16 method.");
            }

            return reader.GetInt16(ordinalOrThrow);
        }

        public static int GetInt32(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetInt32 method.");
            }

            return reader.GetInt32(ordinalOrThrow);
        }

        public static long GetInt64(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetInt64 method.");
            }

            return reader.GetInt64(ordinalOrThrow);
        }

        public static string GetString(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetString method.");
            }

            return reader.GetString(ordinalOrThrow);
        }

        public static void TryGetBoolean(this IDataReader reader, string name, Action<bool> action)
        {
            reader.TryGetBoolean(name, action, mustExist: true);
        }

        public static void TryGetBoolean(this IDataReader reader, string name, Action<bool> action, bool mustExist)
        {
            if (reader.TryGetOrdinal(name, mustExist, out var index))
            {
                reader.TryGetBoolean(index, action);
            }
        }

        public static void TryGetBoolean(this IDataReader reader, int index, Action<bool> action)
        {
            if (!reader.IsDBNull(index))
            {
                action(reader.GetBoolean(index));
            }
        }

        public static void TryGetDateTime(this IDataReader reader, string name, Action<DateTime> action)
        {
            reader.TryGetDateTime(name, action, mustExist: true);
        }

        public static void TryGetDateTime(this IDataReader reader, string name, Action<DateTime> action, bool mustExist)
        {
            if (reader.TryGetOrdinal(name, mustExist, out var index))
            {
                reader.TryGetDateTime(index, action);
            }
        }

        public static void TryGetDateTime(this IDataReader reader, int index, Action<DateTime> action)
        {
            if (!reader.IsDBNull(index))
            {
                action(reader.GetDateTime(index));
            }
        }

        public static void TryGetDecimal(this IDataReader reader, string name, Action<decimal> action)
        {
            reader.TryGetDecimal(name, action, mustExist: true);
        }

        public static void TryGetDecimal(this IDataReader reader, string name, Action<decimal> action, bool mustExist)
        {
            if (reader.TryGetOrdinal(name, mustExist, out var index))
            {
                reader.TryGetDecimal(index, action);
            }
        }

        public static void TryGetDecimal(this IDataReader reader, int index, Action<decimal> action)
        {
            if (!reader.IsDBNull(index))
            {
                action(reader.GetDecimal(index));
            }
        }

        public static void TryGetDouble(this IDataReader reader, string name, Action<double> action)
        {
            reader.TryGetDouble(name, action, mustExist: true);
        }

        public static void TryGetDouble(this IDataReader reader, string name, Action<double> action, bool mustExist)
        {
            if (reader.TryGetOrdinal(name, mustExist, out var index))
            {
                reader.TryGetDouble(index, action);
            }
        }

        public static void TryGetDouble(this IDataReader reader, int index, Action<double> action)
        {
            if (!reader.IsDBNull(index))
            {
                action(reader.GetDouble(index));
            }
        }

        public static void TryGetFloat(this IDataReader reader, string name, Action<float> action)
        {
            reader.TryGetFloat(name, action, mustExist: true);
        }

        public static void TryGetFloat(this IDataReader reader, string name, Action<float> action, bool mustExist)
        {
            if (reader.TryGetOrdinal(name, mustExist, out var index))
            {
                reader.TryGetFloat(index, action);
            }
        }

        public static void TryGetFloat(this IDataReader reader, int index, Action<float> action)
        {
            if (!reader.IsDBNull(index))
            {
                action(reader.GetFloat(index));
            }
        }

        public static void TryGetGuid(this IDataReader reader, string name, Action<Guid> action)
        {
            reader.TryGetGuid(name, action, mustExist: true);
        }

        public static void TryGetGuid(this IDataReader reader, string name, Action<Guid> action, bool mustExist)
        {
            if (reader.TryGetOrdinal(name, mustExist, out var index))
            {
                reader.TryGetGuid(index, action);
            }
        }

        public static void TryGetGuid(this IDataReader reader, int index, Action<Guid> action)
        {
            if (!reader.IsDBNull(index))
            {
                action(reader.GetGuid(index));
            }
        }

        public static void TryGetInt16(this IDataReader reader, string name, Action<short> action)
        {
            reader.TryGetInt16(name, action, mustExist: true);
        }

        public static void TryGetInt16(this IDataReader reader, string name, Action<short> action, bool mustExist)
        {
            if (reader.TryGetOrdinal(name, mustExist, out var index))
            {
                reader.TryGetInt16(index, action);
            }
        }

        public static void TryGetInt16(this IDataReader reader, int index, Action<short> action)
        {
            if (!reader.IsDBNull(index))
            {
                action(reader.GetInt16(index));
            }
        }

        public static void TryGetInt32(this IDataReader reader, string name, Action<int> action)
        {
            reader.TryGetInt32(name, action, mustExist: true);
        }

        public static void TryGetInt32(this IDataReader reader, string name, Action<int> action, bool mustExist)
        {
            if (reader.TryGetOrdinal(name, mustExist, out var index))
            {
                reader.TryGetInt32(index, action);
            }
        }

        public static void TryGetInt32(this IDataReader reader, int index, Action<int> action)
        {
            if (!reader.IsDBNull(index))
            {
                action(reader.GetInt32(index));
            }
        }

        public static void TryGetInt64(this IDataReader reader, string name, Action<long> action)
        {
            reader.TryGetInt64(name, action, mustExist: true);
        }

        public static void TryGetInt64(this IDataReader reader, string name, Action<long> action, bool mustExist)
        {
            if (reader.TryGetOrdinal(name, mustExist, out var index))
            {
                reader.TryGetInt64(index, action);
            }
        }

        public static void TryGetInt64(this IDataReader reader, int index, Action<long> action)
        {
            if (!reader.IsDBNull(index))
            {
                action(reader.GetInt64(index));
            }
        }

        public static void TryGetString(this IDataReader reader, string name, Action<string> action)
        {
            reader.TryGetString(name, action, mustExist: true);
        }

        public static void TryGetString(this IDataReader reader, string name, Action<string> action, bool mustExist)
        {
            if (!reader.TryGetOrdinal(name, mustExist, out var index))
            {
                action(string.Empty);
            }
            else
            {
                reader.TryGetString(index, action);
            }
        }

        public static void TryGetString(this IDataReader reader, int index, Action<string> action)
        {
            if (reader.IsDBNull(index))
            {
                action(string.Empty);
            }
            else
            {
                action(reader.GetString(index));
            }
        }

        private static int GetOrdinalOrThrow(this IDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            if (ordinal == -1)
            {
                string message = $"Column \"{name}\" does not exist in the result set.";
                throw new ArgumentOutOfRangeException("name", message);
            }

            return ordinal;
        }

        private static bool TryGetOrdinal(this IDataReader reader, string name, bool mustExist, out int index)
        {
            if (mustExist)
            {
                index = reader.GetOrdinalOrThrow(name);
                return true;
            }

            index = -1;
            int i = 0;
            for (int fieldCount = reader.FieldCount; i < fieldCount; i++)
            {
                if (index != -1)
                {
                    break;
                }

                if (name.Equals(reader.GetName(i), StringComparison.Ordinal))
                {
                    index = i;
                }
            }

            return index != -1;
        }
    }
}