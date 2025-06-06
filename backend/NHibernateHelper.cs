using NHibernate;
using NHibernate.Cfg;
using System.IO;

public static class NHibernateHelper
{
    private static ISessionFactory? _sessionFactory;

    public static ISessionFactory SessionFactory
    {
        get
        {
            if (_sessionFactory == null)
            {
                var configuration = new Configuration();
                configuration.Configure(); 
                configuration.AddDirectory(new DirectoryInfo("NHibernateMappings")); 
                _sessionFactory = configuration.BuildSessionFactory();
            }
            return _sessionFactory;
        }
    }

    public static NHibernate.ISession OpenSession()
    {
        return SessionFactory.OpenSession();
    }
}