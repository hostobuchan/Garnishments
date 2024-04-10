using System;

namespace EvaluationCriteria.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyAssociationAttribute : System.Attribute
    {
        public readonly string SqlAssociation;
        public readonly Type ParameterType;
        public string Description { get; set; }
        public int ParamNumber { get; set; } = 1;

        public PropertyAssociationAttribute(string sqlAssociation, Type parameterType)
        {
            this.SqlAssociation = sqlAssociation;
            this.ParameterType = parameterType;
        }
    }
}
