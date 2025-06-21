using System.Net;

namespace SimulatorApi.Models;

public class Response
{
    public HttpStatusCode StatusCode { get; set; }
    public string Method { get; set; }
    public string ContentType { get; set; }
    public string Content { get; set; }
}