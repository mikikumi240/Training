using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using System.Data;
using System.Data.Common;
using System.Data.OracleClient;

namespace Tyuta
{
    static class OracleDataLayer
    {
        static public OracleConnection conn;

        public static void setConnection(string connectionString)
        {
            OracleDataLayer.conn = new OracleConnection(connectionString);
        }

        public static void ExecuteNonQuery(string Sql)
        {
            if (OracleDataLayer.conn.State != ConnectionState.Open)
                OracleDataLayer.conn.Open();

            OracleCommand cmd = new OracleCommand(Sql, conn);
            cmd.CommandType = CommandType.Text;
           
            cmd.ExecuteNonQuery();
        }

        public static void ExecuteNonQuery(OracleCommand cmd)
        {
            if (OracleDataLayer.conn.State != ConnectionState.Open)
                OracleDataLayer.conn.Open();

            cmd.Connection = OracleDataLayer.conn;
            cmd.ExecuteNonQuery();
        }

        public static OracleDataReader ExecuteReader(string Sql)
        {
            if (OracleDataLayer.conn.State != ConnectionState.Open)
                OracleDataLayer.conn.Open();

            OracleCommand cmd = new OracleCommand(Sql, conn);
            cmd.CommandType = CommandType.Text;
            return cmd.ExecuteReader();
        }

        public static DataSet ExecuteDataset(string Sql, string connString)
        {
            var ds = new DataSet();

            using (var conn = new OracleConnection(connString))
            {
                conn.Open();
                var command = new OracleCommand(Sql, conn);
                var adapter = new OracleDataAdapter(command);

                adapter.Fill(ds);
            }

            return ds;
        }
    }
}
