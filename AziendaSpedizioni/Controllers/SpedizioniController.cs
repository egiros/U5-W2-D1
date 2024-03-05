using AziendaSpedizioni.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AziendaSpedizioni.Controllers
{
    [Authorize]
    public class SpedizioniController : Controller
    {
        // GET: Spedizioni
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InserisciSpedizione()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InserisciSpedizione(Spedizioni s)
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult SpedizioniOggi()
        {
            
                    return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult SpedizioniInCorso()
        {

            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult SpedizioniPerCitta()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ConnectionString.ToString();
             SqlConnection conn = new SqlConnection(connectionString);
                List<Spedizioni> spedizioni = new List<Spedizioni>();
            try
                {
                    conn.Open();
                    string query = "SELECT * FROM Spedizioni";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        Spedizioni s = new Spedizioni();
                        s.IdSpedizione = Convert.ToInt32(dr["IdSpedizione"]);
                        s.IdCliente = Convert.ToInt32(dr["IdCliente"]);
                        s.codTracciamento = dr["codTracciamento"].ToString();
                        s.dataSpedizione = Convert.ToDateTime(dr["dataSpedizione"]);
                        s.pesoSpedizione = Convert.ToDecimal(dr["pesoSpedizione"]);
                        s.cittaDestinazione = dr["cittaDestinazione"].ToString();
                        s.indirizzoDestinazione = dr["indirizzoDestinazione"].ToString();
                        s.nominativoDestinatario = dr["nominativoDestinatario"].ToString();
                        s.costoSpedizione = Convert.ToDecimal(dr["costoSpedizione"]);
                        s.dataConsegna = Convert.ToDateTime(dr["dataConsegna"]);
                        spedizioni.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
                finally
                {
                   conn.Close();
                }
            return View(spedizioni);
        }
    }
}