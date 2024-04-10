using EvaluationCriteria.Enums;
using EvaluationCriteria.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters.CodeLists
{
    public class CodeList : ICodeList
    {
        public int? ID { get; private set; }
        public string Name { get; private set; }
        public CodeListType ListType { get; private set; }
        public CodeValue Codes { get; private set; }
        public List<string> Strings { get; private set; }

        public CodeList(string Name, CodeListType ListType)
        {
            this.ID = null;
            this.Name = Name;
            this.ListType = ListType;
            this.Codes = new CodeValue();
            this.Strings = new List<string>();
        }
        public CodeList(SqlDataReader sdr)
        {
            this.ID = (int)sdr["CID"];
            this.Name = sdr["DESCRIPTION"].ToString();
            this.ListType = (CodeListType)Enum.ToObject(typeof(CodeListType), Convert.ToByte(sdr["TYPE"]));
            this.Codes = new CodeValue();
            this.Strings = new List<string>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM {Settings.Properties.SQLCodes} WHERE CID={this.ID.Value}", conn))
                {
                    conn.Open();
                    SqlDataReader sdr2 = cmd.ExecuteReader();
                    while (sdr2.Read())
                    {
                        this.Codes.Add(Convert.ToInt32(sdr2["CODE"]), Convert.ToByte(sdr2["CONSTRAINT"]), sdr2["DAYS"] == DBNull.Value ? 0 : Convert.ToInt32(sdr2["DAYS"]));
                    }
                    sdr2.Close();
                }
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM {Settings.Properties.SQLCodeStrings} WHERE CID={this.ID.Value}", conn))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    SqlDataReader sdr2 = cmd.ExecuteReader();
                    while (sdr2.Read())
                    {
                        this.Strings.Add(sdr2["CODE"].ToString());
                    }
                    sdr2.Close();
                }
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public void Commit()
        {
            if (this.ID == null)
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
                {
                    using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Add_CodeList, conn) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@NAME", System.Data.SqlDbType.NVarChar, 50) { Value = this.Name });
                        cmd.Parameters.Add(new SqlParameter("@TYPE", System.Data.SqlDbType.Bit) { Value = this.ListType });
                        cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        this.ID = Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                    }
                }
            }
        }
        public void SaveCodes()
        {
            this.Commit();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                if (this.ListType == CodeListType.Numeric)
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter($"SELECT * FROM {Settings.Properties.SQLCodes} WHERE CID={this.ID.Value}", conn))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr.Delete();
                        }
                        foreach (CodeElements E in this.Codes.Values)
                        {
                            DataRow dr = dt.NewRow();
                            dr["CID"] = this.ID.Value;
                            dr["CODE"] = E.Code;
                            dr["CONSTRAINT"] = (byte)E.Param;
                            dr["DAYS"] = E.Value;
                            dt.Rows.Add(dr);
                        }

                        SqlCommandBuilder scb = new SqlCommandBuilder(sda);
                        sda.Update(dt);
                    }
                }
                else
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter($"SELECT * FROM {Settings.Properties.SQLCodeStrings} WHERE CID={this.ID.Value}", conn))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr.Delete();
                        }
                        foreach (string E in this.Strings)
                        {
                            DataRow dr = dt.NewRow();
                            dr["CID"] = this.ID.Value;
                            dr["CODE"] = E;
                            dt.Rows.Add(dr);
                        }

                        SqlCommandBuilder scb = new SqlCommandBuilder(sda);
                        sda.Update(dt);
                    }
                }
            }
        }


        #region ICodeList Interface

        public int? GetID()
        {
            return this.ID;
        }

        public string GetName()
        {
            return this.Name;
        }

        public CodeListType GetListType()
        {
            return this.ListType;
        }

        public CodeValue GetCodes()
        {
            return this.Codes;
        }

        public List<string> GetCodeStrings()
        {
            return this.Strings;
        }

        #endregion
    }
}
