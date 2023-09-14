using Microsoft.Data.SqlClient;

namespace DataBaseNull
{
    public static class DataReaderNull
    {
        public static byte? GetValueOrNullByte(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader[columnIndex] != DBNull.Value ? (byte?)reader.GetByte(columnIndex) : null;
        }

        public static short? GetValueOrNullShort(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader[columnIndex] != DBNull.Value ? (short?)reader.GetInt16(columnIndex) : null;
        }

        public static int? GetValueOrNullInt(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader[columnIndex] != DBNull.Value ? (int?)reader.GetInt32(columnIndex) : null;
        }

        public static long? GetValueOrNullLong(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader[columnIndex] != DBNull.Value ? (long?)reader.GetInt64(columnIndex) : null;
        }

        public static string? GetValueOrNullString(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader[columnIndex] != DBNull.Value ? reader.GetString(columnIndex) : null;
        }

        public static float? GetValueOrNullFloat(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader[columnIndex] != DBNull.Value ? (float?)reader.GetFloat(columnIndex) : null;
        }

        public static double? GetValueOrNullDouble(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader[columnIndex] != DBNull.Value ? (double?)reader.GetDouble(columnIndex) : null;
        }

        public static decimal? GetValueOrNullDecimal(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader[columnIndex] != DBNull.Value ? (decimal?)reader.GetDecimal(columnIndex) : null;
        }

        public static char? GetValueOrNullChar(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);

            if (reader[columnIndex] != DBNull.Value)
            {
                string value = reader.GetString(columnIndex);
                return !string.IsNullOrEmpty(value) ? value[0] : (char?)null;
            }

            return null;
        }

        public static bool? GetValueOrNullBool(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader[columnIndex] != DBNull.Value ? (bool?)reader.GetBoolean(columnIndex) : null;
        }

    }
}