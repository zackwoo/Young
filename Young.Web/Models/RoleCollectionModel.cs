using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models
{
    public class RoleCollectionModel : ICollection<RoleModel>
    {
        private readonly ICollection<RoleModel> _collection = new List<RoleModel>();
        public void Add(RoleModel item)
        {
            _collection.Add(item);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(RoleModel item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(RoleModel[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return _collection.IsReadOnly; }
        }

        public bool Remove(RoleModel item)
        {
            return _collection.Remove(item);
        }

        public IEnumerator<RoleModel> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }

    public class RoleModel
    {
        public string RoleName { get; set; }

        public bool IsSystem { get; set; }
    }
}