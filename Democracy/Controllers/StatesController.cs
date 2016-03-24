using Democracy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Democracy.Controllers
{
    //Instrucccion para activar la migracion automatica de base de datos = Enable-Migrations -ContextTypeName DemocracyContext
    //luego de ejecutar este comando se ejecuta elsiguiente comando =Add-Migration NOmbre_Migracion  recomendable poner la fecha de migracion
    //Luego ejecutamos el comando= Update-Databese
    //o se puede habilitar las migraciones automaticas con este comando= Enable-Migrations -ContextTypeName DemocracyContext -EnableAutomaticMigration -Force

    /// <summary>
    /// esta etiqueta quiere desir que todas las acciones del controlador necesitan autorización
    /// tambien sele puede definir a solo una accion obliganto a la accion que tiene la etiqueta Autorize necesita autoriazion para utilizar
    /// </summary>
    [Authorize]
    public class StatesController : Controller
    {
        /// <summary>
        /// Concexion a la base de datos
        /// </summary>
        private DemocracyContext db = new DemocracyContext();

        /// <summary>
        /// Vista Index de State y muestra todos los estados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.State.ToList());//muestra la lista de estados en la vista
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(State state)
        {
            if(!ModelState.IsValid)//Si el modelo es invalido retornamos a la vista Create
            {
                return View(state);
            }
            db.State.Add(state);//agregamos el objeto al modelo de la DB
            db.SaveChanges();//guardamos los cambios 
            return RedirectToAction("Index");//redireccionamos al index del state
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            //si el id llega nulo notificamos el error 
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //consultamos el estado en la db
            var state = db.State.Find(id);

            //si el estallo regresa null es por que no existe y siendo el caso tambien notificamos el error 
            if (state == null)
            {
                return HttpNotFound();
            }
            //si todo sale bien enviamos ese estado a la vista para que pueda ser editado
            return View(state);
        }

        [HttpPost]
        public ActionResult Edit(State state)
        {
            if (!ModelState.IsValid)
            {
                return View(state);
            }
            db.Entry(state).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            //si el id llega nulo notificamos el error 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //consultamos el estado en la db
            var state = db.State.Find(id);

            //si el estallo regresa null es por que no existe y siendo el caso tambien notificamos el error 
            if (state == null)
            {
                return HttpNotFound();
            }
            //si todo sale bien enviamos ese estado a la vista para que pueda ser editado
            return View(state);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            //si el id llega nulo notificamos el error 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //consultamos el estado en la db
            var state = db.State.Find(id);

            //si el estallo regresa null es por que no existe y siendo el caso tambien notificamos el error 
            if (state == null)
            {
                return HttpNotFound();
            }
            //si todo sale bien enviamos ese estado a la vista para que pueda ser editado
            return View(state);
        }
        [HttpPost]
        public ActionResult Delete(int id,State state)
        {
            //consultamos el estado en la db
            state = db.State.Find(id);
            //si el estallo regresa null es por que no existe y siendo el caso tambien notificamos el error 
            if (state == null)
            {
                return HttpNotFound();
            }
            db.State.Remove(state);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ViewBag.Error = "El estado no se puede eliminar por que tiene registros relacionados";
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
                return View(state);
            }
           
            return RedirectToAction("Index");
        }



        /// <summary>
        /// Metodo que se encarga de cerrar la conexion con la base de datos.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}