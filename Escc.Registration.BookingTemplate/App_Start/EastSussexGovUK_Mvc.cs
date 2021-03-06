using System.Web.Mvc;
using System.Web.Routing;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Escc.Registration.BookingTemplate.EastSussexGovUkMvc), "PostStart")]

namespace Escc.Registration.BookingTemplate {

    /// <summary>
    /// Register the virtual path provider which makes available the embedded views from Escc.EastSussexGovUK.Mvc
    /// </summary>
    public static class EastSussexGovUkMvc 
	{
  		/// <summary>
		/// Wire up the provider at the end of the application startup process, and the route to HTTP status pages
		/// </summary>
	    public static void PostStart() 
		{
            System.Web.Hosting.HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedResourceVirtualPathProvider.Vpp(typeof(Escc.Metadata.Metadata).Assembly));
            System.Web.Hosting.HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedResourceVirtualPathProvider.Vpp(typeof(Escc.EastSussexGovUK.Mvc.BaseViewModel).Assembly));

            if (RouteTable.Routes["HttpStatus"] == null)
            {
				RouteTable.Routes.MapRoute("HttpStatus", "{controller}/{action}", null, new { controller = "HttpStatus" });
			}

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        }
    }
}