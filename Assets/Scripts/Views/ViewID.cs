using System;

namespace Mvc.Scripts.Views
{
    public class ViewID: IEquatable<ViewID>
    {
        public int Id { get; }

        public ViewID(int id)
        {
            Id = id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ViewID)obj);
        }

        public bool Equals(ViewID other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }
    }
}