using Datos.Interfaces;
using Entidades;
using Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servicio.Implementaciones
{
    public class ServicioContacts : ServicioBase<Contact, IDatosContacts>, IServicioContacts
    {
        public ServicioContacts(IDatosContacts _datos) : base(_datos)
        {

        }

        public void Delete(int id)
        {
            this._datos.Delete(id);
        }

        public List<Contact> GetAll()
        {
            List<Contact> contacts = null;
            contacts = this._datos.GetAll();
            return contacts;
        }

        public Contact GetById(int id)
        {
            Contact contact = null;

            contact = this._datos.GetById(id);
            return contact;
        }

        public List<Contact> GetListByFilter(Filtro<Contact> filtro, out int totalRows)
        {
            List<Contact> contacts = null;
            contacts = this._datos.GetListByFilter(filtro, out totalRows);
            return contacts;
        }

        public int Insert(Contact Entidad)
        {
            int newContactId = this._datos.Insert(Entidad);
            return newContactId;
        }

        public Contact Update(Contact Entidad)
        {
            Contact updatedContact = null;
            updatedContact = this._datos.Update(Entidad);
            return updatedContact;
        }
    }
}
