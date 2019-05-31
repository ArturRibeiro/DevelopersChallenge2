using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.Prova.Domain.SeedWork
{
    public abstract class Entity<T>
    {
        private int? _requestedHashCode;

        public virtual T Id
        {
            get;
            protected set;
        }

        public bool IsTransient => Id == null || Id.Equals(default(T));

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<T>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            Entity<T> item = (Entity<T>)obj;

            if (item.IsTransient || IsTransient)
                return false;
            else
                return item.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if (!IsTransient)
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
    }
}
