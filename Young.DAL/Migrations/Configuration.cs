using Young.Model;

namespace Young.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Young.DAL.DataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Young.DAL.DataBaseContext context)
        {
            
            var systemPropertyType = new[]
                {
                    new PropertyTypeEntity
                        {
                            Name = "创建时间",
                            Type = PropertyType.Date,
                            Description = "对象创建时间",
                            IsSystemProperty = true
                        },
                    new PropertyTypeEntity
                        {
                            Name = "修改时间",
                            Type = PropertyType.Date,
                            Description = "对象修改时间",
                            IsSystemProperty = true
                        },
                    new PropertyTypeEntity
                        {
                            Name = "标题",
                            Type = PropertyType.String,
                            Description = "对象标题",
                            IsSystemProperty = true
                        }
                };
            var userType = new ObjectTypeEntity
                {
                    Name = "用户对象",
                    Description = "系统内建用户对象",
                    PropertyEntities = systemPropertyType.ToList()
                };
            //context.ObjectEntities.AddOrUpdate(p => p.Name, userType);

            var userProperty = new BasePropertyEntity[]
                {
                    new DatePropertyEntity
                        {
                            PropertyTypeEntity = systemPropertyType[0],
                            Value = DateTime.Now
                        },
                    new DatePropertyEntity
                        {
                            PropertyTypeEntity = systemPropertyType[1],
                            Value = DateTime.Now
                        },
                    new StringPropertyEntity
                        {
                            PropertyTypeEntity = new PropertyTypeEntity
                                {
                                    Name = "用户名",
                                    Type = PropertyType.String,
                                    Description = "用户显示名",
                                    IsSystemProperty = false
                                },
                            Value = "zackwoo"
                        },
                    new StringPropertyEntity
                        {
                            PropertyTypeEntity = new PropertyTypeEntity
                                {
                                    Name = "密码",
                                    Type = PropertyType.String,
                                    Description = "用户登录密码",
                                    IsSystemProperty = false
                                },
                            Value = "123456"
                        },
                    new StringPropertyEntity
                        {
                            PropertyTypeEntity = new PropertyTypeEntity
                                {
                                    Name = "用户登录名",
                                    Type = PropertyType.String,
                                    Description = "用户登录名",
                                    IsSystemProperty = false
                                },
                            Value = "zack"
                        }


                };

            var userInstance = new ObjectInstanceEntity
                {
                    ObjectTypeEntity = userType,
                    BasePropertyEntities = userProperty.ToList()
                };

            context.ObjectInstanceEntities.Add(userInstance);

        }
    }
}
