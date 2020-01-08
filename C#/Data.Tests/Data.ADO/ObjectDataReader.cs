using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Data.ADO
{
    ///<summary>
    /// A minimal implementation of IDataReader. 
    /// The only Methods and Properties that are implemented are those that are needed for SqlBulkCopy
    /// </summary>
    /// <remarks>
    /// Implementation taken from http://www.developerfusion.com/article/122498/using-sqlbulkcopy-for-high-performance-inserts/
    /// </remarks>
    /// <typeparam name="T">The type to read</typeparam>
    public class ObjectDataReader<T>: IDataReader
    {
        #region Fields
        private IEnumerator<T> _dataEnumerator;
        private Func<T, object>[] _accessors;
        private Dictionary<string, int> _ordinalLookup;
        #endregion

        #region Constructor
        public ObjectDataReader(IEnumerable<T> data)
        {
            _dataEnumerator = data.GetEnumerator();
            var propertyAccessors = typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.CanRead)
            .Select((p, i) => new
            {
               Index    = i,
               Property = p,
               Accessor = CreatePropertyAccessor(p)
            }).ToArray();

            _accessors = propertyAccessors.Select(p => p.Accessor).ToArray();
            _ordinalLookup = propertyAccessors.ToDictionary(
               p => p.Property.Name,
               p => p.Index,
               StringComparer.OrdinalIgnoreCase);
        }
        #endregion

        #region IDataReader Implementation
        bool IDataReader.NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            if (_dataEnumerator == null)
            {
                throw new ObjectDisposedException("ObjectDataReader");
            }
            return _dataEnumerator.MoveNext();
        }

        int IDataReader.Depth
        {
            get { throw new NotImplementedException(); }
        }

        public void Close()
        {
            Dispose();
        }

        DataTable IDataReader.GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { return _dataEnumerator == null; }
        }

        int IDataReader.RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            if (_dataEnumerator != null)
            {
                _dataEnumerator.Dispose();
                _dataEnumerator = null;
            }

            GC.SuppressFinalize(this);
        }
        #endregion

        #region IDataRecord Used Implementation
        bool IDataRecord.IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public int FieldCount
        {
            get { return _accessors.Length; }
        }

        object IDataRecord.this[int i]
        {
            get { throw new NotImplementedException(); }
        }

        object IDataRecord.this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        int IDataRecord.GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            int ordinal;
            
            if (!_ordinalLookup.TryGetValue(name, out ordinal))
            {
                throw new InvalidOperationException("Unknown parameter name " + name);
            }
            
            return ordinal;
        }

        bool IDataRecord.GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        byte IDataRecord.GetByte(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        char IDataRecord.GetChar(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
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

        float IDataRecord.GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        double IDataRecord.GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetString(int i)
        {
            throw new NotImplementedException();
        }

        decimal IDataRecord.GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        DateTime IDataRecord.GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        IDataReader IDataRecord.GetData(int i)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetName(int i)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        Type IDataRecord.GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            if (_dataEnumerator == null)
            {
                throw new ObjectDisposedException("ObjectDataReader");
            }
            return _accessors[i](_dataEnumerator.Current);
        }
        #endregion

        #region Private Methods
        private Func<T, object> CreatePropertyAccessor(PropertyInfo p)
        {
            // Define the parameter that will be passed - will be the current object
            var parameter = Expression.Parameter(typeof(T), "input");
            // Define an expression to get the value from the property
            var propertyAccess = Expression.Property(parameter, p.GetGetMethod());
            // Make sure the result of the get method is cast as an object
            var castAsObject = Expression.TypeAs(propertyAccess, typeof(object));
            // Create a lambda expression for the property access and compile it
            var lamda = Expression.Lambda<Func<T, object>>(castAsObject, parameter);
            
            return lamda.Compile();
        }
        #endregion
    }
}
