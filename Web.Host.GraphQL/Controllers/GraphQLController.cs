using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Host.Data;
using Web.Host.GraphQL.Models;
using Web.Host.GraphQL.Queries;

namespace Web.Host.GraphQL.Controllers
{
  [Route("graphql")]
  [ApiController]
  public class GraphQLController : Controller
  {
    private readonly ODataGraphQLDbContext _dbContext;

    public GraphQLController(ODataGraphQLDbContext dbContext) => _dbContext = dbContext;

    public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
    {
      var inputs = query.Variables.ToInputs();

      var schema = new Schema
      {
        Query = new AuthorQuery(_dbContext)
      };

      var result = await new DocumentExecuter().ExecuteAsync(_ =>
      {
        _.Schema = schema;
        _.Query = query.Query;
        _.OperationName = query.OperationName;
        _.Inputs = inputs;
      });

      if (result.Errors?.Count > 0)
      {
        return BadRequest(result.Errors?.ToString());
      }

      return Ok(result);
    }
  }
}

