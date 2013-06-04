using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using NHibernate;
using NHibernate.Cfg;

namespace MySales.DL
{
    public class DbHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory(string type)
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(System.Reflection.Assembly.GetAssembly(type: Type.GetType(type)));
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
            
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
