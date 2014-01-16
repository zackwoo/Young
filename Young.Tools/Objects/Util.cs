using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.DAL;
using Young.Model;

namespace Young.Tools.Objects
{
    /// <summary>
    /// Young 对象操作类
    /// </summary>
    public static class Util
    {
        public static ObjectInstanceEntity QueryInstanceById(int id)
        {
            using (var db = new DataBaseContext())
            {
                return db.ObjectInstanceEntities.SingleOrDefault(f => f.ID == id);
            }
        }

        public static ObjectInstanceEntity QueryInstanceByProperty(string objectType, string propertyName, object propertyValue)
        {
            using (var db = new DataBaseContext())
            {
                //Get ObjectType List
                var list = db.ObjectInstanceEntities.Where(
                    f => f.ObjectTypeEntity.Name.Equals(objectType, StringComparison.OrdinalIgnoreCase));

                var foo = from bar in list
                          select
                              bar.BasePropertyEntities.FirstOrDefault(
                                  f =>
                                  f.PropertyTypeEntity.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
                var result = new List<ObjectInstanceEntity>();
                foreach (var basePropertyEntity in foo)
                {
                    if (basePropertyEntity.IsBoolean && Convert.ToBoolean(((BooleanPropertyEntity)basePropertyEntity).Value) == Convert.ToBoolean(propertyValue))
                    {
                        result.Add(basePropertyEntity.ObjectInstanceEntity);
                    }
                    else if (basePropertyEntity.IsDate && Convert.ToDateTime(((DatePropertyEntity)basePropertyEntity).Value) == Convert.ToDateTime(propertyValue))
                    {
                        result.Add(basePropertyEntity.ObjectInstanceEntity);
                    }
                    else if (basePropertyEntity.IsDouble && Convert.ToDouble(((DoublePropertyEntity)basePropertyEntity).Value) == Convert.ToDouble(propertyValue))
                    {
                        result.Add(basePropertyEntity.ObjectInstanceEntity);
                    }
                    else if (basePropertyEntity.IsInt && Convert.ToInt32(((IntPropertyEntity)basePropertyEntity).Value) == Convert.ToInt32(propertyValue))
                    {
                        result.Add(basePropertyEntity.ObjectInstanceEntity);
                    }
                    else if (basePropertyEntity.IsString && string.Equals(((StringPropertyEntity)basePropertyEntity).Value, propertyValue))
                    {
                        result.Add(basePropertyEntity.ObjectInstanceEntity);
                    }
                    else if (basePropertyEntity.IsTerm)
                    {
                        //TODO:术语比较需要调整
                        if (((TermPropertyEntity)basePropertyEntity).Value.ID == ((TermEntity)propertyValue).ID)
                        {
                            result.Add(basePropertyEntity.ObjectInstanceEntity);
                        }                        
                    }
                }
                return result.Any() ? result[0] : null;
            }
        }
    }
}
