using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace Nerivela
{
    #region Calificaciones
    public class Calificaciones
    {
        #region Member Variables
        protected int _idCalificaciones;
        protected double _CalificacionMen;
        protected int _idAlumno;
        protected string _Mes;
        protected int _idMaterias;
        #endregion
        #region Constructors
        public Calificaciones() { }
        public Calificaciones(double CalificacionMen, int idAlumno, string Mes, int idMaterias)
        {
            this._CalificacionMen=CalificacionMen;
            this._idAlumno=idAlumno;
            this._Mes=Mes;
            this._idMaterias=idMaterias;
        }
        #endregion
        #region Public Properties
        public virtual int IdCalificaciones
        {
            get {return _idCalificaciones;}
            set {_idCalificaciones=value;}
        }
        public virtual double CalificacionMen
        {
            get {return _CalificacionMen;}
            set {_CalificacionMen=value;}
        }
        public virtual int IdAlumno
        {
            get {return _idAlumno;}
            set {_idAlumno=value;}
        }
        public virtual string Mes
        {
            get {return _Mes;}
            set {_Mes=value;}
        }
        public virtual int IdMaterias
        {
            get {return _idMaterias;}
            set {_idMaterias=value;}
        }
        #endregion
    }
    #endregion
}