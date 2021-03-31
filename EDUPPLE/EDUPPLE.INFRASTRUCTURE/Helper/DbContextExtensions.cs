using EDUPPLE.INFRASTRUCTURE.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace EDUPPLE.INFRASTRUCTURE.Helper
{
    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this EduppleDbContext context)
        {
            //var applied = context.GetService<IHistoryRepository>()
            //    .GetAppliedMigrations()
            //    .Select(m => m.MigrationId);
            //var total = context.GetService<IMigrationsAssembly>()
            //    .Migrations
            //    .Select(m => m.Key);
            //return !total.Except(applied).Any();
            return true;
        }

        private static Model.EmailTemplate GetResourceTemplate(string templateKey)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"EDUPPLE.INFRASTRUCTURE.Template.{templateKey}.yml";
            if (!string.IsNullOrEmpty(resourceName))
            {
                using var stream = assembly.GetManifestResourceStream(resourceName);
                if (stream == null)
                    return null;

                using var reader = new StreamReader(stream);
                var deserializer = new DeserializerBuilder().WithNamingConvention(namingConvention: new CamelCaseNamingConvention())
                                                            .Build();
                return deserializer.Deserialize<Model.EmailTemplate>(reader);
            }
            return default;
        }
       
       
        private static void SeedRoles(RoleManager<DOMAIN.Entities.Role> roleManager, List<DOMAIN.Entities.Role> roles)
        {
            foreach (var item in roles)
            {
                if (!roleManager.RoleExistsAsync(item.Name).Result) _ = roleManager.CreateAsync(item).Result;              
            }
        }
        private static void SeedCountries(DbSet<DOMAIN.Entities.Country> db, List<DOMAIN.Entities.Country> countries)
        {
            foreach (var item in countries)
            {
                if (!db.Where(x => x.Name.Equals(item.Name)).Any())
                {
                    db.Add(item);
                }               
            }
        }

        private static void SeedUsers(UserManager<DOMAIN.Entities.User> userManager, List<DOMAIN.Entities.User> users)
        {
            foreach (var item in users)
            {
                if (userManager.FindByEmailAsync(item.Email).Result == null)
                {

                    IdentityResult result = userManager.CreateAsync(item, "P@ssw0rd123").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(item, "Administrator").Wait();
                    }
                }
            }
        }

        public static void EnsureSeeded(this EduppleDbContext context, UserManager<DOMAIN.Entities.User> userManager, RoleManager<DOMAIN.Entities.Role> roleManager)
        {
            try
            {
                #region Email Template
                if (context.CheckTableExists<DOMAIN.Entities.EmailTemplate>())
                {
                    var dbSet = context.Set<DOMAIN.Entities.EmailTemplate>();
                    if (!dbSet.Any(x => x.Key.Equals("forgot-password")))
                    {
                        var result = GetResourceTemplate("forgot-password");
                        if (result != null) {
                            var email = new DOMAIN.Entities.EmailTemplate()
                            {
                                Key = "forgot-password",
                                FromAddress = result.FromAddress,
                                FromName = result.FromName,
                                ReplyToAddress = result.ReplyToAddress,
                                ReplyToName = result.ReplyToName,
                                Subject = result.Subject,
                                TextBody = result.TextBody,
                                HtmlBody = result.HtmlBody
                            };
                            dbSet.Add(email);
                        }
                        context.SaveChanges();
                    }

                }
                #endregion
                #region Roles
                if (context.CheckTableExists<DOMAIN.Entities.Role>())
                {
                    if (!context.Set<DOMAIN.Entities.Role>().Any())
                    {
                        var roles = JsonConvert.DeserializeObject<List<DOMAIN.Entities.Role>>(File.ReadAllText("DataSeeds" + Path.DirectorySeparatorChar + "Roles.json"));
                        SeedRoles(roleManager, roles);
                    }
                }

                #endregion
                #region Users
                if (context.CheckTableExists<DOMAIN.Entities.User>())
                {
                    var dbSet = context.Set<DOMAIN.Entities.User>();
                    if (!dbSet.Any())
                    {                        
                        var users = JsonConvert.DeserializeObject<List<DOMAIN.Entities.User>>(File.ReadAllText("DataSeeds" + Path.DirectorySeparatorChar + "Users.json"));
                        SeedUsers(userManager, users);
                    }
                }

                #endregion
                #region Country
                if (context.CheckTableExists<DOMAIN.Entities.Country>())
                {
                    var dbSet = context.Set<DOMAIN.Entities.Country>();
                    var countries = JsonConvert.DeserializeObject<List<DOMAIN.Entities.Country>>(File.ReadAllText("DataSeeds" + Path.DirectorySeparatorChar + "Countries.json"));
                    SeedCountries(dbSet, countries);
                    context.SaveChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {                
                throw ex;
            }

        }

        public static bool CheckTableExists<M>(this EduppleDbContext context) where M : class
        {
            try
            {
                context.Set<M>().Count();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }


        public static DbCommand LoadStoredProc(this EduppleDbContext context, string storedProcName, bool prependDefaultSchema = true, short commandTimeout = 30)
        {
            try
            {
                using (var cmd = context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandTimeout = commandTimeout;
                    if (prependDefaultSchema)
                    {
                        var schemaName = context.Model.GetDefaultSchema();
                        if (schemaName != null)
                        {
                            storedProcName = $"{schemaName}.{storedProcName}";
                        }
                    }

                    cmd.CommandText = storedProcName;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    return cmd;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName, object paramValue, Action<DbParameter> configureParam = null)
        {
            if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != System.Data.CommandType.StoredProcedure)
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Value = (paramValue != null ? paramValue : DBNull.Value);
            configureParam?.Invoke(param);
            cmd.Parameters.Add(param);
            return cmd;
        }
        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName, Action<DbParameter> configureParam = null)
        {
            if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != System.Data.CommandType.StoredProcedure)
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            configureParam?.Invoke(param);
            cmd.Parameters.Add(param);
            return cmd;
        }
        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName, SqlParameter parameter)
        {

            if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != System.Data.CommandType.StoredProcedure)
                throw new InvalidOperationException("Call LoadStoredProc before using this method");
            cmd.Parameters.Add(parameter);
            return cmd;
        }
        public class SprocResults
        {

            //  private DbCommand _command;
            private DbDataReader _reader;

            public SprocResults(DbDataReader reader)
            {
                // _command = command;
                _reader = reader;
            }

            public IList<T> ReadToList<T>()
            {
                return MapToList<T>(_reader);
            }

            public T? ReadToValue<T>() where T : struct
            {
                return MapToValue<T>(_reader);
            }

            public Task<bool> NextResultAsync()
            {
                return _reader.NextResultAsync();
            }

            public Task<bool> NextResultAsync(CancellationToken ct)
            {
                return _reader.NextResultAsync(ct);
            }

            public bool NextResult()
            {
                return _reader.NextResult();
            }

            /// <summary>
            /// Retrieves the column values from the stored procedure and maps them to <typeparamref name="T"/>'s properties
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="dr"></param>
            /// <returns>IList<<typeparamref name="T"/>></returns>
            private IList<T> MapToList<T>(DbDataReader dr)
            {
                var objList = new List<T>();
                var props = typeof(T).GetRuntimeProperties().ToList();

                var colMapping = dr.GetColumnSchema()
                    .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                    .ToDictionary(key => key.ColumnName.ToLower());

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        T obj = Activator.CreateInstance<T>();
                        foreach (var prop in props)
                        {
                            if (colMapping.ContainsKey(prop.Name.ToLower()))
                            {
                                var column = colMapping[prop.Name.ToLower()];

                                if (column?.ColumnOrdinal != null)
                                {
                                    var val = dr.GetValue(column.ColumnOrdinal.Value);
                                    prop.SetValue(obj, val == DBNull.Value ? null : val);
                                }

                            }
                        }
                        objList.Add(obj);
                    }
                }
                return objList;
            }

            /// <summary>
            ///Attempts to read the first value of the first row of the resultset.
            /// </summary>
            private T? MapToValue<T>(DbDataReader dr) where T : struct
            {
                if (!dr.HasRows) return new T?();
                if (dr.Read())
                {
                    return dr.IsDBNull(0) ? new T?() : new T?(dr.GetFieldValue<T>(0));
                }
                return new T?();
            }
        }
        public static void ExecuteStoredProc(this DbCommand command, Action<SprocResults> handleResults, System.Data.CommandBehavior commandBehaviour = System.Data.CommandBehavior.Default, bool manageConnection = true)
        {

            if (handleResults == null)
            {
                throw new ArgumentNullException(nameof(handleResults));
            }

            using (command)
            {
                if (manageConnection && command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();
                try
                {

                    using (var reader = command.ExecuteReader(commandBehaviour))
                    {
                        var sprocResults = new SprocResults(reader);
                        // return new SprocResults();
                        handleResults(sprocResults);
                    }
                }
                finally
                {
                    if (manageConnection)
                    {
                        command.Connection.Close();
                    }
                }
            }
        }
        public static async Task ExecuteStoredProcAsync(this DbCommand command, Action<SprocResults> handleResults, System.Data.CommandBehavior commandBehaviour = System.Data.CommandBehavior.Default, CancellationToken ct = default(CancellationToken), bool manageConnection = true)
        {


            if (handleResults == null)
            {
                throw new ArgumentNullException(nameof(handleResults));
            }

            using (command)
            {
                if (manageConnection && command.Connection.State == System.Data.ConnectionState.Closed)
                    await command.Connection.OpenAsync(ct).ConfigureAwait(false);
                try
                {
                    using (var reader = await command.ExecuteReaderAsync(commandBehaviour, ct).ConfigureAwait(false))
                    {
                        var sprocResults = new SprocResults(reader);
                        handleResults(sprocResults);
                    }
                }
                finally
                {
                    if (manageConnection)
                    {
                        command.Connection.Close();
                    }
                }
            }
        }
        public static int ExecuteStoredNonQuery(this DbCommand command, System.Data.CommandBehavior commandBehaviour = System.Data.CommandBehavior.Default, bool manageConnection = true)
        {
            var numberOfRecordsAffected = -1;

            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                {
                    command.Connection.Open();
                }

                try
                {
                    numberOfRecordsAffected = command.ExecuteNonQuery();
                }
                finally
                {
                    if (manageConnection)
                    {
                        command.Connection.Close();
                    }
                }
            }

            return numberOfRecordsAffected;
        }
        public static async Task<int> ExecuteStoredNonQueryAsync(this DbCommand command, System.Data.CommandBehavior commandBehaviour = System.Data.CommandBehavior.Default, CancellationToken ct = default(CancellationToken), bool manageConnection = true)
        {
            var numberOfRecordsAffected = -1;

            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                {
                    await command.Connection.OpenAsync(ct).ConfigureAwait(false);
                }

                try
                {
                    numberOfRecordsAffected = await command.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
                }
                finally
                {
                    if (manageConnection)
                    {
                        command.Connection.Close();
                    }
                }
            }

            return numberOfRecordsAffected;
        }




        private static object GetPropertyValue<T>(T item, string name) where T : class
        {
            var t = item.GetType();
            var prop = t.GetProperty(name);
            var propertyValue = prop.GetValue(item);
            return propertyValue;
            //return item.GetType().GetProperty(name).GetValue(item, null);
        }
    }
}
