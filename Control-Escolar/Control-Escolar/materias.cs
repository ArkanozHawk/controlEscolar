using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace Nerivela
{
    #region Materias
    public class Materias
    {
        #region Member Variables
        protected int _idMaterias;
        protected string _nombre;
        protected int _idGrado;
        #endregion
        #region Constructors
        public Materias() { }
        public Materias(string nombre, int idGrado)
        {
            this._nombre=nombre;
            this._idGrado=idGrado;
        }
        #endregion
        #region Public Properties
        public virtual int IdMaterias
        {
            get {return _idMaterias;}
            set {_idMaterias=value;}
        }
        public virtual string Nombre
        {
            get {return _nombre;}
            set {_nombre=value;}
        }
        public virtual int IdGrado
        {
            get {return _idGrado;}
            set {_idGrado=value;}
        }
        #endregion
    }
    #endregion
}