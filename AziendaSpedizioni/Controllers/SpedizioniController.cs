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
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "INSERT INTO Spedizioni (IdCliente, codTracciamento, dataSpedizione, pesoSpedizione, cittaDestinazione, indirizzoDestinazione, nominativoDestinatario, costoSpedizione, dataConsegna) VALUES (@IdCliente, @codTracciamento, @dataSpedizione, @pesoSpedizione, @cittaDestinazione, @indirizzoDestinazione, @nominativoDestinatario, @costoSpedizione, @dataConsegna)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdCliente", s.IdCliente);
                cmd.Parameters.AddWithValue("@codTracciamento", s.codTracciamento);
                cmd.Parameters.AddWithValue("@dataSpedizione", s.dataSpedizione);
                cmd.Parameters.AddWithValue("@pesoSpedizione", s.pesoSpedizione);
                cmd.Parameters.AddWithValue("@cittaDestinazione", s.cittaDestinazione);
                cmd.Parameters.AddWithValue("@indirizzoDestinazione", s.indirizzoDestinazione);
                cmd.Parameters.AddWithValue("@nominativoDestinatario", s.nominativoDestinatario);
                cmd.Parameters.AddWithValue("@costoSpedizione", s.costoSpedizione);
                cmd.Parameters.AddWithValue("@dataConsegna", s.dataConsegna);
                cmd.Parameters.AddWithValue("@IdStatoSpedizione", 1);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult SpedizioniOggi()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Spedizioni> spedizioni = new List<Spedizioni>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Spedizioni WHERE IdStatoSpedizione = 2";
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
        [Authorize(Roles = "Admin")]
        public ActionResult SpedizioniInCorso()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Spedizioni> spedizioni = new List<Spedizioni>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Spedizioni WHERE IdStatoSpedizione = 2 OR idStatoSpedizione = 1";
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
                    
                    s.IdStatoSpedizione = Convert.ToInt32(dr["IdStatoSpedizione"]);
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
        [Authorize(Roles = "Admin")]
        public ActionResult SpedizioniPerCitta()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ConnectionString.ToString();
             SqlConnection conn = new SqlConnection(connectionString);
                List<Spedizioni> spedizioni = new List<Spedizioni>();
            try
                {
                    conn.Open();
                    string query = "SELECT cittaDestinazione, COUNT(*) as NumeroSpedizioni FROM Spedizioni GROUP BY cittaDestinazione";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        Spedizioni s = new Spedizioni();
                        s.IdSpedizione = Convert.ToInt32(dr["NumeroSpedizioni"]);
                        
                        s.cittaDestinazione = dr["cittaDestinazione"].ToString();
                        
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
        public ActionResult Consegnato(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "UPDATE Spedizioni SET IdStatoSpedizione = 3 WHERE IdSpedizione =" +id;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("SpedizioniInCorso");
        }
        public ActionResult InConsegna(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "UPDATE Spedizioni SET IdStatoSpedizione = 2 WHERE IdSpedizione =" + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("SpedizioniInCorso");
        }


    }
}