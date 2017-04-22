﻿namespace Vulcan.DataAccess.ORMapping.MSSql
{
    public class MSSqlEntity : AbstractBaseEntity
    {
        private static readonly MSSqlSQLBuilder _builder = new MSSqlSQLBuilder();

        protected override ISQLBuilder SQLBuilder
        {
            get { return _builder; }
        }
    }
}