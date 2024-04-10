using System;

namespace EvaluationCriteria.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DerivedPropertyAssociationAttribute : PropertyAssociationAttribute
    {
        public readonly string DerivedParameter;

        public DerivedPropertyAssociationAttribute(string sqlAssociation, Type parameterType, string derivedParameter) : base(sqlAssociation, parameterType)
        {
            this.DerivedParameter = derivedParameter;
        }
    }
}
