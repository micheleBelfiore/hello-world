using ContosoUniversity.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace ContosoUniversity
{
    public class MyTempTableDataReader : IDataReader
    {
        private readonly IEnumerator<int> _enumerator;

        public int FieldCount => 1;

        public MyTempTableDataReader(
           IEnumerable<int> values)
        {
           _enumerator = values.GetEnumerator();
        }

        

        public bool Read()
        {
            return _enumerator.MoveNext();
        }



        public object GetValue(int i)
        {
            if (i == 0)
                return _enumerator.Current;

            throw new ArgumentOutOfRangeException();
        }

        public void Dispose()
        {
            _enumerator.Dispose();
        }

        // all other members throw NotImplementedException
        public int Depth => throw new NotImplementedException();

        int IDataReader.Depth => throw new NotImplementedException();

        bool IDataReader.IsClosed => throw new NotImplementedException();

        int IDataReader.RecordsAffected => throw new NotImplementedException();

        int IDataRecord.FieldCount => FieldCount;

        object IDataRecord.this[string name] => throw new NotImplementedException();

        object IDataRecord.this[int i] => throw new NotImplementedException();

        public object this[int i] => throw new NotImplementedException();
        public bool GetBoolean(int i) => throw new NotImplementedException();

        void IDataReader.Close()
        {
            throw new NotImplementedException();
        }

        DataTable? IDataReader.GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        bool IDataReader.NextResult()
        {
            throw new NotImplementedException();
        }

        

        bool IDataRecord.GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        byte IDataRecord.GetByte(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[]? buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        char IDataRecord.GetChar(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetChars(int i, long fieldoffset, char[]? buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        IDataReader IDataRecord.GetData(int i)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        DateTime IDataRecord.GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        decimal IDataRecord.GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        double IDataRecord.GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        Type IDataRecord.GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        float IDataRecord.GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        Guid IDataRecord.GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        short IDataRecord.GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        int IDataRecord.GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetName(int i)
        {
            throw new NotImplementedException();
        }

        int IDataRecord.GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetString(int i)
        {
            throw new NotImplementedException();
        }

        int IDataRecord.GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        bool IDataRecord.IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        
    }
}
