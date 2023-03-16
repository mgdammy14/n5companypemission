using ApiBusinessLogic.Implementation.Permissions;
using ApiDataAccess.General;
using ApiModel.General;
using ApiModel.Permissions;
using ApiUnitOfWork.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Web_ApiCore.Controllers.Permissions;

namespace TestN5Company
{
    public class PermissionTesting
    {
        private readonly IConfiguration _config;
        private readonly PermissionLogic _permissionLogic;
        private readonly PermissionController _permissionController;
        private readonly IUnitOfWork _unitOfWork;

        public PermissionTesting()
        {
            // Crea una instancia de IConfiguration y config�rala para que lea appsettings.json
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _config = builder.Build();

            // Crea una instancia de tu objeto singleton a partir de la cadena de conexi�n en appsettings.json
            _unitOfWork = new UnitOfWork(_config.GetConnectionString("develop-azure"));
            _permissionLogic = new PermissionLogic(_unitOfWork);
            _permissionController = new PermissionController(_permissionLogic);
        }

        [Fact]
        public void GetPermissionsOk()
        {
            var result = _permissionController.GetPermissions();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetPermissionsQuantity()
        {
            var result = (OkObjectResult)_permissionController.GetPermissions();
            var responseDto = Assert.IsType<ResponseDTO>(result.Value);
            var permissionsList = (List<Permission>)responseDto.objModel;
            Assert.True(permissionsList.Count > 0);
        }

        [Fact]
        public void PostPermission()
        {
            var dto = new Permission()
            {
                NombreEmpleado = "Ricardo",
                ApellidoEmpleado = "Arbildo",
                FechaPermiso = DateTime.Now,
                TipoPermiso = 1
            };
            var result = (OkObjectResult)_permissionController.RequestPermission(dto);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PutPermission()
        {
            var dto = new Permission()
            {
                Id = 1,
                NombreEmpleado = "Gonzalo",
                ApellidoEmpleado = "Pingo",
                FechaPermiso = DateTime.Now,
                TipoPermiso = 1
            };
            var result = (OkObjectResult)_permissionController.ModifyPermission(dto);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}