using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphiQl;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using Web.Host.OData.Models;

namespace Web.Host
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<Web.Host.Data.ODataGraphQLDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ODataGraphQLDbContext")).EnableSensitiveDataLogging().EnableDetailedErrors());

      services.AddOData(); // 1
      services.AddMvc(options =>
      {        
        options.EnableEndpointRouting = false;
      }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();

      var models = GetEdmModels();

      app.UseMvc(routes => // 3
      {
        routes.MapODataServiceRoute("odata", "v1/", models);
        routes.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

        routes.EnableDependencyInjection();
      });

      app.UseGraphiQl("/graphql");

      app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());
      app.UseMvc();
    }

    private IEdmModel GetEdmModels() // 2
    {
      ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
      builder.EntitySet<Author>("Authors");
     
      var model = builder.GetEdmModel();
      return model;

    }
  }
}
