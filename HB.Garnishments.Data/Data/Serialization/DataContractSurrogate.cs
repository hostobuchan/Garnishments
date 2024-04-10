using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Serialization
{
    public class DataContractSurrogate : IDataContractSurrogate
    {
        private Dictionary<Type, Type> _DataContractTypes = new Dictionary<Type, Type>()
        {
            
        };

        /// <summary>
        /// Data Contract Surrogate for Garnishment XML Data
        /// </summary>
        public DataContractSurrogate()
        {

        }

        #region Unneeded
        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            throw new NotImplementedException();
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
        #endregion

        public Type GetDataContractType(Type type)
        {
            if (_DataContractTypes.ContainsKey(type)) return _DataContractTypes[type];

            return type;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            throw new NotImplementedException();
            //if (obj is RequestSurrogate) return new HB.Garnishments.Data.Request(this.AssetUsers, this.GarnUsers, this.CancelReasons, obj as RequestSurrogate);

            //return obj;
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
            //customDataTypes.Add(typeof(RequestSurrogate));
        }
    }
}
