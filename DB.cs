using Login_InfoToolsSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_InfoToolsSV
{
    public class DB
    {

        public static string Conectando()
        {
            string conexion;
            conexion = "Server =" + DesEncriptarCadena(Conexiones.StrConexion(1)) + ";Database=" + DesEncriptarCadena(Conexiones.StrConexion(2)) + ";User Id=" + DesEncriptarCadena(Conexiones.StrConexion(3)) + ";Password=" + DesEncriptarCadena( Conexiones.StrConexion(4)) + ";Connection Timeout=900;max pool size=1000;";
            return conexion;
        }

        public static string DesEncriptarCadena(string cadena)
        {
            int idx;
            string result = "";
            for (idx = 0; idx <= cadena.Length - 1; idx++)
                result += DesEncriptarCaracter(cadena.Substring(idx, 1), cadena.Length, idx);
            return result;
        }

        public static string DesEncriptarCaracter(string caracter, int variable, int a_indice)
        {
            string patron_busqueda = HttpUtility.HtmlDecode("qpwoeirutyQPWOEIRUTYa&ntilde;sld1234567890kfjghA&Ntilde;SLDKFJGHzmxncbvZMXNCBV.");
            string Patron_encripta = HttpUtility.HtmlDecode("zmxncbvZMXNCBVa&ntilde;sldkfjghA&Ntilde;.SLDKFJGHqpwoeirutyQPWOEIRUTY0987654321");



            int indice;
            if (Patron_encripta.IndexOf(caracter) != -1)
            {
                if ((Patron_encripta.IndexOf(caracter) - variable - a_indice) > 0)
                    indice = (Patron_encripta.IndexOf(caracter) - variable - a_indice) % Patron_encripta.Length;
                else
                    indice = (patron_busqueda.Length) + ((Patron_encripta.IndexOf(caracter) - variable - a_indice) % Patron_encripta.Length);
                indice = indice % Patron_encripta.Length;
                return patron_busqueda.Substring(indice, 1);
            }
            else
                return caracter;
        }

    }

}