﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace Vulcan.DataAccess
{
    public static class ConnectionFactoryHelper
    {
        private static IConnectionFactory _default;
        public static void Configure(IConnectionFactory factory)
        {
            _default = factory;
        }
        public static IDbConnection CreateDefaultDbConnection(string connectionString)
        {
            if(_default != null)
            {
                return _default.CreateDbConnection(connectionString);
            }

            throw new NullReferenceException("默认的IConnectionFactory没有设置，请在应用程序启动时设置");
        }
    }

    public interface IConnectionFactory
    {
        IDbConnection CreateDbConnection(string connectionString);
    }

    public class SqlConnectionFactory : IConnectionFactory
    {
        public IDbConnection CreateDbConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}