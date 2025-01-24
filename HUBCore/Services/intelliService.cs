using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HUBCore.Tools;
using HUBCore.Models;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Data;


namespace HUBCore.Services
{

    public class intelliService
    {
        public Collection<intelliModel> getCollectionIntelli()
        {
            var lstData = new Collection<intelliModel>();
            string query = "select * from intelliAnalytics";
            using (var conex = new SqlConnection(ConnDB.CadenaConexion))
            {
                using (var adapter = new SqlDataAdapter(query, conex))
                {
                    conex.Open();
                    var reader = adapter.SelectCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var _data = new intelliModel();
                        _data.categoria = reader.GetString(1);
                       // _data.valor = reader.GetInt32(2);
                        _data.fecha = reader.GetDateTime(2);


                        lstData.Add(_data);
                    }
                }
            }
            return lstData;
        }

        public bool insertIntelliRecord(intelliModel model)
        {
            string query = "INSERT INTO intelliAnalytics (categoria, Fecha) VALUES (@categoria, @fecha)";
            using (var conex = new SqlConnection(ConnDB.CadenaConexion))
            {
                using (var command = new SqlCommand(query, conex))
                {
                    command.Parameters.AddWithValue("@categoria", model.categoria);
                   // command.Parameters.AddWithValue("@valor", model.valor);
                    command.Parameters.AddWithValue("@fecha", model.fecha);

                    conex.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;  // Devuelve true si la inserción fue exitosa
                }
            }
        }

    }
}
