﻿using KOU_RFID_Plaka.Utils;
using KOU_RFID_Plaka.Utils.QueryBuilder;
using KOU_RFID_Plaka.Utils.QueryBuilder.Enums;
using System;

namespace KOU_RFID_Plaka.Models
{
    class Faculty
    {
        protected static readonly string _table = "faculties";
        protected static readonly int limit = 50;
        public static bool addValue(string value)
        {
            if (!Models.Faculty.getValue(value).hasRows)
            {
                InsertQueryBuilder query = new InsertQueryBuilder();
                query.Table = _table;
                query.addValue("value", value);
                query.addValue("created_at", Helper.GetDatabaseDateFormat());
                string cmd = query.BuildQuery();
                bool result = Utils.Mysql.execute(cmd);
                return result;
            }
            return false;
        }

        public static MysqlDatas getValue(string value)
        {
            SelectQueryBuilder query = new SelectQueryBuilder();
            query.SelectFromTable(_table);
            query.AddWhere("value", Comparison.Equals, value);
            string cmd = query.BuildQuery();
            MysqlDatas result = Utils.Mysql.query(cmd);
            return result;
        }

        public static MysqlDatas getValueById(string id)
        {
            SelectQueryBuilder query = new SelectQueryBuilder();
            query.SelectFromTable(_table);
            query.AddWhere("id", Comparison.Equals, id);
            string cmd = query.BuildQuery();
            MysqlDatas result = Utils.Mysql.query(cmd);
            return result;
        }
        public static MysqlDatas getValues()
        {
            SelectQueryBuilder query = new SelectQueryBuilder();
            query.SelectFromTable(_table);
            string cmd = query.BuildQuery();
            MysqlDatas result = Utils.Mysql.query(cmd);
            return result;
        }

        public static MysqlDatas getValues(string value)
        {
            SelectQueryBuilder query = new SelectQueryBuilder();
            query.SelectFromTable(_table);
            query.AddWhere("value", Comparison.Like, "%" + value + "%");
            string cmd = query.BuildQuery();
            MysqlDatas result = Utils.Mysql.query(cmd);
            return result;
        }
        public static bool deleteValue(int id)
        {
            DeleteQueryBuilder query = new DeleteQueryBuilder();
            query.Table = _table;
            query.AddWhere("id", Comparison.Equals, id);
            string cmd = query.BuildQuery();
            bool result = Utils.Mysql.execute(cmd);
            if (result)
            {
                UpdateQueryBuilder update = new UpdateQueryBuilder();
                update.Table = "users";
                update.addSet("faculty", "0");
                update.AddWhere("faculty", Comparison.Equals,id);
                cmd = update.BuildQuery();
                Utils.Mysql.execute(cmd);
            }
            return result;
        }

        public static int lastInsertedId()
        {
            int id = Convert.ToInt32(Utils.Mysql.executeScalar("SELECT LAST_INSERT_ID()"));
            return id;
        }
    }
}
