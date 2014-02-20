﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Young.Web.Models
{
    public class CustomListModel : IList<CustomColumnModel>
    {
        public CustomListModel()
        {
            CustomColumnCollection = new List<CustomColumnModel>();
        }
        [Display(Name = "列表名")]
        public string Name { get; set; }

        [Display(Name = "用途描述")]
        public string Description { get; set; }

        public int ID { get; set; }

        private IList<CustomColumnModel> CustomColumnCollection { get; set; }

        public void Add(CustomColumnModel item)
        {
            CustomColumnCollection.Add(item);
        }

        public void Clear()
        {
            CustomColumnCollection.Clear();
        }

        public bool Contains(CustomColumnModel item)
        {
            return CustomColumnCollection.Contains(item);
        }

        public void CopyTo(CustomColumnModel[] array, int arrayIndex)
        {
            CustomColumnCollection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return CustomColumnCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return CustomColumnCollection.IsReadOnly; }
        }

        public bool Remove(CustomColumnModel item)
        {
            return CustomColumnCollection.Remove(item);
        }

        public IEnumerator<CustomColumnModel> GetEnumerator()
        {
            return CustomColumnCollection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return CustomColumnCollection.GetEnumerator();
        }

        public int IndexOf(CustomColumnModel item)
        {
            return CustomColumnCollection.IndexOf(item);
        }

        public void Insert(int index, CustomColumnModel item)
        {
            CustomColumnCollection.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            CustomColumnCollection.RemoveAt(index);
        }

        public CustomColumnModel this[int index]
        {
            get
            {
                return CustomColumnCollection[index];
            }
            set
            {
                CustomColumnCollection[index] = value;
            }
        }
    }
}