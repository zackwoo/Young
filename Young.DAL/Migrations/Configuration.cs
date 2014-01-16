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
                            Name = "����ʱ��",
                            Type = PropertyType.Date,
                            Description = "���󴴽�ʱ��",
                            IsSystemProperty = true
                        },
                    new PropertyTypeEntity
                        {
                            Name = "�޸�ʱ��",
                            Type = PropertyType.Date,
                            Description = "�����޸�ʱ��",
                            IsSystemProperty = true
                        },
                    new PropertyTypeEntity
                        {
                            Name = "����",
                            Type = PropertyType.String,
                            Description = "�������",
                            IsSystemProperty = true
                        }
                };
            var userType = new ObjectTypeEntity
                {
                    Name = "�û�����",
                    Description = "ϵͳ�ڽ��û�����",
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
                                    Name = "�û���",
                                    Type = PropertyType.String,
                                    Description = "�û���ʾ��",
                                    IsSystemProperty = false
                                },
                            Value = "zackwoo"
                        },
                    new StringPropertyEntity
                        {
                            PropertyTypeEntity = new PropertyTypeEntity
                                {
                                    Name = "����",
                                    Type = PropertyType.String,
                                    Description = "�û���¼����",
                                    IsSystemProperty = false
                                },
                            Value = "123456"
                        },
                    new StringPropertyEntity
                        {
                            PropertyTypeEntity = new PropertyTypeEntity
                                {
                                    Name = "�û���¼��",
                                    Type = PropertyType.String,
                                    Description = "�û���¼��",
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
