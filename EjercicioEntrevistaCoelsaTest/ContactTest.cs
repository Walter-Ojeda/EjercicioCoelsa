

using Datos.Implementaciones;
using EjercicioEntrevistaCoelsa.Controllers;
using EjercicioEntrevistaCoelsa.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Servicio.Implementaciones;
using Servicio.Interfaces;
using System;

namespace EjercicioEntrevistaCoelsaTest
{
    [TestClass]
    public class ContactTest
    {
        [TestMethod]
        public void TestPostContact_Err()
        {
            //Preparacion            
            IServicioContacts mockService = new Mock<ServicioContacts>(new Mock<DatosContacts>().Object).Object;
            ILogger<ContactsController> mockLogger = new Mock<ILogger<ContactsController>>().Object;

            ContactModel model = new ContactModel();
            //model.FirstName = "Juan"; Campo requerido
            model.LastName = "Perez";
            model.Email = "Juan@perez.com";
            model.Company = "Test";
            model.PhoneNumber = "(011) 11 2233 4455";

            ResponseModel responseExpected = new ResponseModel()
            {
                Codigo = ResponseModel.CodigosDeEstado.InvalidRequest,
            };

            //Prueba
            ContactsController controller = new ContactsController(mockLogger, mockService);
            controller.ModelState.AddModelError("FirstName", "");
            ResponseModel response = controller.Post(model);

            //Verificacion
            Assert.AreEqual(responseExpected.Codigo, response.Codigo);
        }
        [TestMethod]
        public void TestPostContact_Ok()
        {
            //Preparacion            
            IServicioContacts mockService = new Mock<ServicioContacts>(new Mock<DatosContacts>().Object).Object;
            ILogger<ContactsController> mockLogger = new Mock<ILogger<ContactsController>>().Object;


            ContactModel model = new ContactModel();
            model.FirstName = "Juan";
            model.LastName = "Perez";
            model.Email = $"Juan{new Random().Next(1, 999)}@perez{new Random().Next(1, 999)}.com";
            model.Company = "Test";
            model.PhoneNumber = $"(011) 15 {new Random().Next(10000000, 99999999)}";

            ResponseModel responseExpected = new ResponseModel()
            {
                Codigo = ResponseModel.CodigosDeEstado.Ok
            };

            //Prueba
            ContactsController controller = new ContactsController(mockLogger, mockService);
            ResponseModel response = controller.Post(model);

            //Verificacion
            Assert.AreEqual(responseExpected.Codigo, response.Codigo);
        }

        [TestMethod]
        public void TestGetContacts_Err()
        {
            //Preparacion            
            IServicioContacts mockService = null;
            ILogger<ContactsController> mockLogger = new Mock<ILogger<ContactsController>>().Object;

            ResponseModel responseExpected = new ResponseModel()
            {
                Codigo = ResponseModel.CodigosDeEstado.Error
            };

            //Prueba
            ContactsController controller = new ContactsController(mockLogger, mockService);
            ResponseModel response = controller.Get();

            //Verificacion
            Assert.AreEqual(responseExpected.Codigo, response.Codigo);
        }
        
        [TestMethod]
        public void TestGetContact_Ok()
        {
            //Preparacion                        
            IServicioContacts mockService = new Mock<ServicioContacts>(new Mock<DatosContacts>().Object).Object;
            ILogger<ContactsController> mockLogger = new Mock<ILogger<ContactsController>>().Object;

            ResponseModel responseExpected = new ResponseModel()
            {
                Codigo = ResponseModel.CodigosDeEstado.Ok
            };

            //Prueba
            ContactsController controller = new ContactsController(mockLogger, mockService);
            ResponseModel response = controller.Get();

            //Verificacion
            Assert.AreEqual(responseExpected.Codigo, response.Codigo);
        }               

        [TestMethod]
        public void TestPutContact_Err()
        {
            //Preparacion            
            IServicioContacts mockService = new Mock<ServicioContacts>(new Mock<DatosContacts>().Object).Object;
            ILogger<ContactsController> mockLogger = new Mock<ILogger<ContactsController>>().Object;

            ContactModel model = new ContactModel();
            //model.FirstName = "Juan"; Campo requerido
            model.LastName = "Perez";
            model.Email = "Juan@perez.com";
            model.Company = "Test";
            model.PhoneNumber = "(011) 11 2233 4455";

            ResponseModel responseExpected = new ResponseModel()
            {
                Codigo = ResponseModel.CodigosDeEstado.InvalidRequest,
            };

            //Prueba
            ContactsController controller = new ContactsController(mockLogger, mockService);
            controller.ModelState.AddModelError("FirstName", "");
            ResponseModel response = controller.Put(model);

            //Verificacion
            Assert.AreEqual(responseExpected.Codigo, response.Codigo);
        }
        [TestMethod]
        public void TestPutContact_Ok()
        {
            //Preparacion            
            IServicioContacts mockService = new Mock<ServicioContacts>(new Mock<DatosContacts>().Object).Object;
            ILogger<ContactsController> mockLogger = new Mock<ILogger<ContactsController>>().Object;

            var allContacts = mockService.GetAll();
            var contactToUpdate = allContacts[allContacts.Count - 1];

            ContactModel model = new ContactModel();
            model.FirstName = "Test";
            model.LastName = "Test";
            model.Email = "Test@Test.com";
            model.Company = "Test";
            model.PhoneNumber = "(011) 11 2233 4455";
            model.Id = contactToUpdate.Id;

            ResponseModel responseExpected = new ResponseModel()
            {
                Codigo = ResponseModel.CodigosDeEstado.Ok
            };

            //Prueba
            ContactsController controller = new ContactsController(mockLogger, mockService);
            ResponseModel response = controller.Put(model);

            //Verificacion
            Assert.AreEqual(responseExpected.Codigo, response.Codigo);
        }

        

        [TestMethod]
        public void TestDeleteContact_Err()
        {
            //Preparacion            
            IServicioContacts mockService = new Mock<ServicioContacts>(new Mock<DatosContacts>().Object).Object;
            ILogger<ContactsController> mockLogger = new Mock<ILogger<ContactsController>>().Object;

            var allContacts = mockService.GetAll();

            int contactIdToDelete = allContacts[allContacts.Count - 1].Id + 99;

            ResponseModel responseExpected = new ResponseModel()
            {
                Codigo = ResponseModel.CodigosDeEstado.Error,
            };

            //Prueba
            ContactsController controller = new ContactsController(mockLogger, mockService);
            ResponseModel response = controller.Delete(contactIdToDelete);

            //Verificacion
            Assert.AreEqual(responseExpected.Codigo, response.Codigo);
        }


    }
}
