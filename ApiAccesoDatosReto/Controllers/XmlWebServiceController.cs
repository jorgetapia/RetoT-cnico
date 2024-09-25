using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Net;
using System.Text;
using System.Xml;
using TimbradorCEPDI;

namespace ApiAccesoDatosReto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XmlWebServiceController(IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;

        

        [HttpPost]
        public async Task<IActionResult> TimbradorWSPDF([FromBody] XmlRequest request)
        {

            try
            {
                // Decodificar el Base64 a texto XML
                byte[] xmlBytes = Convert.FromBase64String(request.XmlBase64);
                string xmlContent = Encoding.UTF8.GetString(xmlBytes);

                // Procesamos el XML
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent); 

                // Creamos un manejador de espacios de nombres
                XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
                namespaceManager.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");


                //Ubicamos el nodo y obtenemos el valor del atributo UUID
                XmlNode uuidNode = xmlDoc.SelectSingleNode("//tfd:TimbreFiscalDigital/@UUID", namespaceManager);

                if (uuidNode == null)
                {
                    return BadRequest(new { success = false, message = "UUID no encontrado en el XML." });
                }

                string uuid = uuidNode.Value;

                // obtenemos el PDF a traves del UUID
                string base64Pdf = await ObtenerPDFDelWebService(uuid);


                //Enviamos el archivo pdf en Base64 para su tratamiento y manejo en el front
                return new JsonResult(new { success = true, pdfBase64 = base64Pdf });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        // Clase para recibir el JSON con el XML Base64
        public class XmlRequest
        {
            public string XmlBase64 { get; set; }
        }

        // Método para consumir el Web Service SOAP y obtener el PDF en Base64
        private async Task<string> ObtenerPDFDelWebService(string uuid)
        {
            //Parametros para facilitar la escalibiliad y mantemiento del codigo, si se llegara a cambiar la ruta del WS o los datos de acceso bastaría
            //con modificar en el AppSetting y no una recompilación y publicación del ensamblado
            string urlWSTimbrador = _configuration["AppSettings:urlWSTimbrador"];
            string usuario = _configuration["AppSettings:usuario"];
            string password = _configuration["AppSettings:password"];


            var client = new WSClient();
            client.Endpoint.Address= new System.ServiceModel.EndpointAddress(urlWSTimbrador);//reemplazamos el url del WS
            ObtenerPDFRequest request = new ObtenerPDFRequest();

            request.Usuario = usuario;
            request.Password = password;
            request.uuid = uuid;




            var response =  client.ObtenerPDF(request);


           
            if (response.@return.Exitoso)
            {
                // Convertimos el arreglo de bytes del PDF en una cadena Base64
                return Convert.ToBase64String(response.@return.PDF);
            }
            else
            {
                throw new Exception($"{response.@return.MensajeError} Para mayor referencia se proporciona el UUID del XML cargado: {uuid}");
            }
        }
    }
}
