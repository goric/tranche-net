using System;

namespace AbstractSyntaxTree
{
    public class DereferenceField : LValue
    {
        public Expression Object { get; set; }
        public String Field { get; set; }

        public DereferenceField (Expression obj, String field)
        {
            Object = obj;
            Field = field;
        }
        
        public override String Print(int depth)
        {
            return Object.Print(depth) + "." + Field;
        }

        public override void Visit (Visitor v)
        {
            v.VisitDereferenceField(this);
        }
    }
}
