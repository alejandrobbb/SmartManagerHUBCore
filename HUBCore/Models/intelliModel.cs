using Microsoft.AspNetCore.Mvc;
using System;


namespace HUBCore.Models
{
    public class intelliModel
    {
        public int ID { get; set; }
        public string categoria { get; set; } = String.Empty;
        public DateTime fecha { get; set; }

    }
}
