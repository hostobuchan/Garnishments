namespace RecordTypes.MergePops.Attributes
{
    public class MergePopRequiredGroupFieldAttribute : MergePopFieldAttribute
    {
        public override bool Required { get { return false; } }
        public int GroupNumber { get; private set; }

        /// <summary>
        /// Attribute to Identify Elements of a Group Requiring At Minimum One Member Populated
        /// </summary>
        /// <param name="HeaderText">Header Text</param>
        /// <param name="Group">Group Membership</param>
        public MergePopRequiredGroupFieldAttribute(string HeaderText, int Order, int Group) : base(HeaderText, Order)
        {
            this.GroupNumber = Group;
        }
    }
}
