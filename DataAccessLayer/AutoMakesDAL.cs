using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudWithPostgreSql.ViewModel;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;

namespace CrudWithPostgreSql.DataAccessLayer
{    
    class AutoMakesDAL
    {
        // install the following from nuget package manager - Microsoft.Extensions.Configuration, Dapper, Npgsql
        IConfiguration _iconfiguration;

        public AutoMakesDAL(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        internal IDbConnection Connection
        {
            get
            {
#if DEBUG
                return new NpgsqlConnection(_iconfiguration.GetConnectionString("PSQLDev"));
#else
                return new NpgsqlConnection(_iconfiguration.GetConnectionString("PSQLProd"));
#endif

            }
        }

        public async Task<List<AutoMakeVM>> GetAutoMakesAsync()
        {
            List<AutoMakeVM> data = new List<AutoMakeVM>();

            StringBuilder query = new StringBuilder();

            query.Append("select id,auto_make_name from auto_makes order by auto_make_name");


            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                var result = await dbConnection.QueryAsync<AutoMakeVM>(query.ToString());

                data = result.AsList();

                return data;
            }

        }
    }
}
