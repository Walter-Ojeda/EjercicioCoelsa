using EjercicioEntrevistaCoelsa.Models;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EjercicioEntrevistaCoelsa.Controllers
{

    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IServicioContacts _servicioContacts;

        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ILogger<ContactsController> logger, IServicioContacts servicioContacts)
        {
            _logger = logger;
            _servicioContacts = servicioContacts;
        }

        [HttpGet]
        public ResponseModel Get()
        {
            List<Contact> contacts = null;
            List<ContactModel> contactModel = null;
            ResponseModel response = new ResponseModel();
            try
            {
                contacts = this._servicioContacts.GetAll();
                if (contacts == null)
                {
                    response.Codigo = ResponseModel.CodigosDeEstado.Ok;
                    response.Descripcion = "No se encontraron registros";
                    response.Data = new { contacts = contacts, totalRows = 0 };
                    return response;
                }

                contactModel = contacts.Select(x => new ContactModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Company = x.Company,
                    PhoneNumber = x.PhoneNumber
                }).ToList();

                response.Codigo = ResponseModel.CodigosDeEstado.Ok;
                response.Descripcion = "OK";
                response.Data = new { contacts = contactModel, totalRows = contactModel.Count };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }

        [HttpGet]
        [Route("{companyName}/{pageIndex}/{pageSize}")]
        public ResponseModel GetByCompany(string companyName, int pageIndex, int pageSize)
        {
            List<Contact> contactsByCompanyName = null;
            List<ContactModel> contactsByCompanyNameModel = null;
            ResponseModel response = new ResponseModel();
            try
            {
                Filtro<Contact> filtro = new Filtro<Contact>();
                filtro.Map = new Contact();
                filtro.Map.Company = companyName;
                filtro.PageIndex = pageIndex;
                filtro.PageSize = pageSize;
                int totalRows = 0;
                contactsByCompanyName = this._servicioContacts.GetListByFilter(filtro, out totalRows);

                if (contactsByCompanyName == null)
                {
                    response.Descripcion = "No se encontraron registros";
                    return response;
                }

                contactsByCompanyNameModel = contactsByCompanyName.Select(x => new ContactModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Company = x.Company,
                    PhoneNumber = x.PhoneNumber
                }).ToList();

                response = new ResponseModel();
                response.Codigo = ResponseModel.CodigosDeEstado.Ok;
                response.Descripcion = "OK";
                response.Data = new { contacts = contactsByCompanyNameModel, totalRows = totalRows };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                contactsByCompanyNameModel = null;
            }
            return response;
        }

        [HttpPost]
        public ResponseModel Post([FromBody] ContactModel body)
        {
            ResponseModel response = new ResponseModel();
            try
            {

                if (!ModelState.IsValid)
                {
                    response.Codigo = ResponseModel.CodigosDeEstado.InvalidRequest;
                    response.Descripcion = "Invalid Request";
                    response.Data = ModelState.Values.Select(x => x.Errors.Select(y => y.ErrorMessage));
                    return response;
                }

                Filtro<Contact> filtro = new Filtro<Contact>();
                filtro.Map = new Contact();
                filtro.Map.Email = body.Email;
                filtro.PageIndex = 1;
                filtro.PageSize = 1;
                int totalRows;

                List<Contact> contactExist = _servicioContacts.GetListByFilter(filtro, out totalRows);
                if (contactExist != null && contactExist.FirstOrDefault() != null)
                {
                    response.Descripcion = "Ya existe un registro con el Email Indicado";
                    return response;
                }

                Contact contact = new Contact();
                contact.FirstName = body.FirstName;
                contact.LastName = body.LastName;
                contact.Email = body.Email;
                contact.PhoneNumber = body.PhoneNumber;
                contact.Company = body.Company;

                int newContactId = _servicioContacts.Insert(contact);
                contact.Id = newContactId;
                response = new ResponseModel();
                response.Codigo = ResponseModel.CodigosDeEstado.Ok;
                response.Descripcion = "Contacto guardado con exito";
                response.Data = new { contact = contact };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }

        [HttpPut]
        public ResponseModel Put([FromBody] ContactModel body)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.Codigo = ResponseModel.CodigosDeEstado.InvalidRequest;
                    response.Descripcion = "Invalid Request";
                    response.Data = ModelState.Values.Select(x => x.Errors.Select(y => y.ErrorMessage));
                    return response;
                }

                Contact contactToBeUpdated = _servicioContacts.GetById(body.Id);
                if (contactToBeUpdated == null)
                {
                    response.Descripcion = "No existe registro para el Id indicado";
                    return response;
                }


                contactToBeUpdated.FirstName = body.FirstName;
                contactToBeUpdated.LastName = body.LastName;
                contactToBeUpdated.Email = body.Email;
                contactToBeUpdated.PhoneNumber = body.PhoneNumber;
                contactToBeUpdated.Company = body.Company;

                Contact contactUpdated = _servicioContacts.Update(contactToBeUpdated);
                response = new ResponseModel();
                response.Codigo = ResponseModel.CodigosDeEstado.Ok;
                response.Descripcion = "Contacto actualizado correctamente";
                response.Data =new { contact = contactUpdated };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }

        [HttpDelete]
        [Route("{id}")]
        public ResponseModel Delete(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                Contact contactToBeDeleted = _servicioContacts.GetById(id);
                if (contactToBeDeleted == null)
                {
                    response.Descripcion = "No existe registro para el Id indicado";
                    return response;
                }

                _servicioContacts.Delete(id);
                response = new ResponseModel();
                response.Codigo = ResponseModel.CodigosDeEstado.Ok;
                response.Descripcion = "Contacto eliminado correctamente";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }
    }
}
