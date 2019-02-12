using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Escc.EastSussexGovUK.Mvc;
using Exceptionless;

namespace Escc.Registration.BookingTemplate
{
    public class TemplateController : Controller
    {
        // GET: Template
        public async Task<ActionResult> Index()
        {
            var model = new StopfordModel();
            var templateRequest = new EastSussexGovUKTemplateRequest(Request);
            try
            {
                // Do this if you want your page to support web chat. It should, unless you have a reason not to.
                model.WebChat = await templateRequest.RequestWebChatSettingsAsync();
            }
            catch (Exception ex)
            {
                // Catch and report exceptions - don't throw them and cause the page to fail
                ex.ToExceptionless().Submit();
            }
            try
            {
                // Do this to load the template controls.
                model.TemplateHtml = await templateRequest.RequestTemplateHtmlAsync();
            }
            catch (Exception ex)
            {
                // Catch and report exceptions - don't throw them and cause the page to fail
                ex.ToExceptionless().Submit();
            }


            // Get the HTML that would normally be rendered for the view.
            string html = RenderViewToString(ControllerContext,
            "~/views/Template/Index.cshtml",
            model);

            // Remove properties which specify the URL, which will be the URL of the template rather than the consuming page.
            // These properties are rendered by a control in the layout view, which is why they can't simply be removed in the Index view.
            html = Regex.Replace(html, "<meta name=\"DC.identifier\"[^>]+>", String.Empty);
            html = Regex.Replace(html, "<meta rel=\"canonical\"[^>]+>", String.Empty);
            html = Regex.Replace(html, "<meta property=\"og:url\"[^>]+>", String.Empty);

            // Pass the modified HTML to a separate view which simply displays it and nothing else.
            ViewBag.ModifiedHtml = html;
            return View("~/Views/Template/ModifiedHtml.cshtml");
        }

        /// <summary>
        /// Renders a view to a string. Code from http://www.codemag.com/article/1312081
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="viewPath">The view path.</param>
        /// <param name="model">The model.</param>
        /// <param name="partial">if set to <c>true</c> render as a partial view.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">View cannot be found.</exception>
        static string RenderViewToString(ControllerContext context,
                                    string viewPath,
                                    object model = null,
                                    bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                                            context.Controller.ViewData,
                                            context.Controller.TempData,
                                            sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }
    }
}