using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoApi.Data;
using MarcasAutosApi.Models;
using Xunit;
using AutoApi.Controllers;

namespace MarcasAutosApi.Tests
{
    public class MarcasAutosControllerTests
    {
        [Fact]
        public async Task GetMarcasAutos_ReturnsListOfMarcas()
        {
            var options = new DbContextOptionsBuilder<AutoDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AutoDbContext(options))
            {
                context.MarcasAutos.Add(new MarcaAuto { Id = 1, Nombre = "Toyota" });
                context.MarcasAutos.Add(new MarcaAuto { Id = 2, Nombre = "Ford" });
                context.SaveChanges();
            }

            using (var context = new AutoDbContext(options))
            {
                var controller = new MarcasAutosController(context);
                var result = await controller.GetMarcasAutos();

                // Assert
                var actionResult = Assert.IsType<ActionResult<IEnumerable<MarcaAuto>>>(result);
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var marcas = Assert.IsAssignableFrom<IEnumerable<MarcaAuto>>(okResult.Value);
                Assert.Equal(2, marcas.Count());
            }
        }
    }
}
