using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public class AllergySqlDAO : IAllergyDAO
    {
        private string connectionString;
        public AllergySqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Allergy> GetAllergies()
        {
            List<Allergy> allergies = new List<Allergy>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                const string sql = @"SELECT *
                                            FROM allergies
                                            JOIN allergy_type ON allergies.allergy_type_id = allergy_type.allergy_type_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    allergies.Add(ParseRow(rdr));
                }
            }

            return allergies;
        }

        public List<Allergy> GetAllergiesByType(int typeId)
        {
            List<Allergy> allergies = new List<Allergy>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                const string sql = @"SELECT *
                                            FROM allergies
                                            JOIN allergy_type ON allergies.allergy_type_id = allergy_type.allergy_type_id
                                            WHERE allergy_type.allergy_type_id = @type_id";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@type_id", typeId);


                SqlDataReader rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    allergies.Add(ParseRow(rdr));
                }
            }

            return allergies;
        }

        private Allergy ParseRow(SqlDataReader rdr)
        {
            Allergy allergy = new Allergy();

            allergy.AllergyId = Convert.ToInt32(rdr["allergy_id"]);
            allergy.Name = Convert.ToString(rdr["name"]);
            allergy.AllergyTypeId = Convert.ToInt32(rdr["allergy_type_id"]);
            allergy.AllergyType = Convert.ToString(rdr["name"]);

            return allergy;
        }
    }
}
