using ErgoSalud.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ErgoSalud.Controllers
{
    [AllowAnonymous]
    public class ChecklistController : Controller
    {
        private Check035Entities db = new Check035Entities();
        private int? id_empresa_sp;

    
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult add_file(int id)

        {

            if (id == null)
            { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }

            Check_checklist_medidas_acciones_N01 check_checklist_medidas_acciones_N01 = db.Check_checklist_medidas_acciones_N01.Find(id);

            if (check_checklist_medidas_acciones_N01 == null)
            {
                return HttpNotFound();
            }
             
            List<SelectListItem> lst = new List<SelectListItem>(); 
            lst.Add(new SelectListItem() { Text = "Agendado", Value = "Agendado" });
            lst.Add(new SelectListItem() { Text = "En proceso", Value = "En proceso" });
            lst.Add(new SelectListItem() { Text = "completado", Value = "Completado" });
            ViewBag.Opciones = lst;
            return View(check_checklist_medidas_acciones_N01);

        }
        [HttpPost]

        public ActionResult add_file(Check_checklist_medidas_acciones_N01 info, HttpPostedFileBase PostedFile)
        {
            if (PostedFile != null && PostedFile.ContentLength > 0)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var filename = Path.GetFileName(PostedFile.FileName);
                        filename = info.id.ToString() + "_Evidencia_" + filename;
                        string filename_trim = filename.ToString().Trim();
                        filename_trim = filename_trim.Replace("#", "").Replace("+", "&").Replace("/", "_").Replace("\\", "_").Replace("*", "_").Replace("%", "_");
                        string directorio_path_server = Server.MapPath("~/_App_Uploaded_Files/Check035_Medidas_Evidencias/");
                        string directorio = "https://identifica.check035.com/_App_Uploaded_Files/Check035_Medidas_Evidencias/";
                        info.file_evidencia = directorio + filename_trim;

                        PostedFile.SaveAs(directorio_path_server + filename_trim);
                        db.Entry(info).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                catch(Exception ex) {
                    throw ex;
                }
               
                return RedirectToAction("Index", "Medidas_Estatus");
            }

            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "Agendado", Value = "Agendado" });
            lst.Add(new SelectListItem() { Text = "En proceso", Value = "En proceso" });
            lst.Add(new SelectListItem() { Text = "completado", Value = "Completado" });
            TempData["error"] = "Problemas al guardar registro"; 
            ViewBag.Opciones = lst;
            return RedirectToAction("add_file", new { id = info.id });
          
       
        }
        public ActionResult edit_file(int id)

        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Check_checklist_medidas_acciones_N01 check_checklist_medidas_acciones_N01 = db.Check_checklist_medidas_acciones_N01.Find(id);
            if (check_checklist_medidas_acciones_N01 == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "Agendado", Value = "Agendado" });
            lst.Add(new SelectListItem() { Text = "En proceso", Value = "En proceso" });
            lst.Add(new SelectListItem() { Text = "completado", Value = "Completado" });
            ViewBag.Opciones = lst;
            return View(check_checklist_medidas_acciones_N01);
        }

        public ActionResult Create_medidas(int? id,int? id_medida,int id_checklist_37)
        {
            ViewBag.id_pregunta = id;
            List<SelectListItem> lst_nivel = new List<SelectListItem>();
            lst_nivel.Add(new SelectListItem() { Text = "Nivel I", Value = "1" });
            lst_nivel.Add(new SelectListItem() { Text = "Nivel II", Value = "2" });
            lst_nivel.Add(new SelectListItem() { Text = "Nivel III", Value = "3" });
            ViewBag.Opciones_Nivel = lst_nivel;

            List<SelectListItem> lst_status = new List<SelectListItem>();
            lst_status.Add(new SelectListItem() { Text = "Agendado", Value = "Agendado" });
            lst_status.Add(new SelectListItem() { Text = "En proceso", Value = "En proceso" });
            lst_status.Add(new SelectListItem() { Text = "Completado", Value = "Completado" });
            ViewBag.Opciones_Estatus = lst_status;
            ViewBag.id_medida_v = id_medida;
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "1. ¿Ya ha capacitado y sensibilizado a directivos, gerentes y supervisores para la prevención de los factores de riesgo psicosocial?", Value = "1" });
            lst.Add(new SelectListItem() { Text = "2. ¿Ya ha capacitación y sensibilización en la promoción de entornos organizacionales favorables?", Value = "2" });
            lst.Add(new SelectListItem() { Text = "3. ¿Crea reuniones para abordar las áreas de oportunidad de mejora, a efecto de atender los problemas en el lugar de su trabajo y determinar sus soluciones?", Value = "3" });
            lst.Add(new SelectListItem() { Text = "4. ¿Realiza reuniones periódicas (semestrales o anuales) de seguimiento a las actividades establecidas para el apoyo social y, en su caso, extraordinarias si ocurren eventos que pongan en riesgo la salud del trabajador o al centro de trabajo?", Value = "4" });
            lst.Add(new SelectListItem() { Text = "5. ¿Tiene  actividades culturales y del deporte entre sus trabajadores y proporcionarles los equipos y útiles indispensables?", Value = "5" });
            lst.Add(new SelectListItem() { Text = "6. ¿Promociona de actividades de integración familiar en el trabajo, previo acuerdo con los trabajadores?", Value = "6" });
            lst.Add(new SelectListItem() { Text = "7. ¿Tiene mecanismos para reconocer el desempeño sobresaliente (superior al esperado) de los trabajadores?", Value = "7" });
            lst.Add(new SelectListItem() { Text = "8. ¿Tiene mecanismos para difundir los logros de los trabajadores sobresalientes?", Value = "8" });
            lst.Add(new SelectListItem() { Text = "9. ¿Tiene mecanismos para expresar al trabajador sus posibilidades de desarrollos?", Value = "9" });
            lst.Add(new SelectListItem() { Text = "10. ¿Tiene mecanismos para la participación proactiva y comunicación entre los colaboradores?", Value = "10" });
            lst.Add(new SelectListItem() { Text = "11. ¿Cuenta con mecanismos que permiten identificar la distribución adecuada de los tiempos de trabajo?", Value = "11" });
            lst.Add(new SelectListItem() { Text = "12. ¿Cuenta con mecanismos establecer y difundir instrucciones claras a los trabajadores para la atención de los problemas que impiden o limitan el desarrollo de su trabajo, cuando éstos se presenten?", Value = "12" });
            lst.Add(new SelectListItem() { Text = "13. ¿Cuenta con mecanismos para la  revisión y supervisión que la distribución de la carga de trabajo se realice de forma equitativa y considerando el número de trabajadores, actividades a desarrollar, alcance de la actividad y su capacitación?", Value = "13" });
            lst.Add(new SelectListItem() { Text = "14. ¿Cuenta con mecanismos o programas que faciliten la participación  en la mejora de las condiciones de trabajo y la productividad siempre que el proceso productivo lo permita y cuenten con la experiencia y capacitación para ello?", Value = "14" });
            lst.Add(new SelectListItem() { Text = "15. ¿Cuenta con mecanismos o sistemas de trabajo que permitan  acordar ymejorar el margen de libertad y control sobre su trabajo por parte de los trabajadores y el patrón?", Value = "15" });
            lst.Add(new SelectListItem() { Text = "16. ¿Cuenta con mecanismos o programas de desarrollo para impulsar que éstos desarrollen nuevas competencias o habilidades, considerando las limitaciones del proceso productivo?", Value = "16" });
            lst.Add(new SelectListItem() { Text = "17. ¿Tiene mecanismos para definir los lineamientos para establecer medidas y límites que eviten las jornadas de trabajo superiores a las previstas en la Ley Federal del Trabajo?", Value = "17" });
            lst.Add(new SelectListItem() { Text = "18. ¿Cuenta con una capacitación continua  para la adecuada realización de las tareas encomendadas?", Value = "18" });
            lst.Add(new SelectListItem() { Text = "19. ¿Cuenta con mecanismos para  definición precisa de responsabilidades para los miembros de la organización?", Value = "19" });
            lst.Add(new SelectListItem() { Text = "20. ¿Cuenta con la distribución adecuada de cargas de trabajo, con jornadas laborales regulares conforme a la Ley Federal del Trabajo?", Value = "20" });
            lst.Add(new SelectListItem() { Text = "21. ¿Cuenta con mecanismos  para la  determinación de prioridades en el trabajo?", Value = "21" });
            lst.Add(new SelectListItem() { Text = "22. ¿Cuenta con mecanismos para planificar el trabajo, considerando el proceso productivo, de manera que se tengan las pausas o periodos necesarios de descanso, rotación de tareas y otras medidas necesarias para evitar ritmos de trabajo acelerados?", Value = "22" });
            lst.Add(new SelectListItem() { Text = "23. ¿Cuenta con Instructivos o procedimientos que definan claramente las tareas y responsabilidades?", Value = "23" });
            lst.Add(new SelectListItem() { Text = "24. ¿Cuenta con mecanismos  para involucrar a los trabajadores en la definición de los horarios de trabajo cuando las condiciones del trabajo lo permitan?", Value = "24" });
            lst.Add(new SelectListItem() { Text = "25. ¿Cuenta con mecanismos  para la  determinación de prioridades en el trabajo?", Value = "25" });
            lst.Add(new SelectListItem() { Text = "26. ¿Cuenta con mecanismos para fomentar la comunicación entre supervisores o gerentes y trabajadores, así como entre los trabajadores?", Value = "26" });
            lst.Add(new SelectListItem() { Text = "27. ¿Cuenta con mecanismos o programas  para involucrar a los trabajadores en la toma de decisiones sobre la organización de su trabajo?", Value = "27" });
            lst.Add(new SelectListItem() { Text = "28. ¿Cuenta con mecanismos o programas  para involucrar a los trabajadores en la toma de decisiones sobre la organización de su trabajo?", Value = "28" });
            lst.Add(new SelectListItem() { Text = "29. ¿Cuenta con mecanismos para establecer procedimientos de actuación y seguimiento para tratar problemas relacionados con la violencia laboral, y capacitar al responsable de su implementación?", Value = "29" });
            lst.Add(new SelectListItem() { Text = "30. ¿Cuenta con mecanismos o programas  para definir lineamientos para prohibir la discriminación y fomentar la equidad y el respeto?", Value = "30" });
            lst.Add(new SelectListItem() { Text = "31. ¿Ya ha proporcionado la capacitación y sensibilización en la promoción de entornos organizacionales favorables?", Value = "31" });
            lst.Add(new SelectListItem() { Text = "32. ¿Cuenta con mecanismos o programas para promover la ayuda mutua y el intercambio de conocimientos y experiencias entre los trabajadores?", Value = "32" });
            lst.Add(new SelectListItem() { Text = "33. ¿Cuenta con mecansimos o programas  de apoyos a los trabajadores, de manera que puedan atender emergencias familiares, mismas que el trabajador tendrá que comprobar?", Value = "33" });
            lst.Add(new SelectListItem() { Text = "34. ¿Cuenta con mecanismos, capacitación o programas para  difundir información para sensibilizar sobre la violencia laboral, tanto a trabajadores como a directivos, gerentes y supervisores?", Value = "34" });
            lst.Add(new SelectListItem() { Text = "35. ¿Cuenta con mecanismos para  Informar sobre la forma en que se deben denunciar actos de violencia laboral?", Value = "35" });
            lst.Add(new SelectListItem() { Text = "36. ¿Cuenta con mecanismos  seguros y confidenciales para la recepción de quejas por prácticas opuestas al entorno organizacional favorable y para denunciar actos de violencia laboral?", Value = "36" });
            lst.Add(new SelectListItem() { Text = "37. ¿Cuenta con programas que promuevan el sentido de pertenencia de los trabajadores a la organización?", Value = "37" });
            ViewBag.id_medida = lst;
      
            Check_checklist_medidas_acciones_N01 info = new Check_checklist_medidas_acciones_N01();
            info.id_checklist_37 = id_checklist_37;
            ViewBag.id_checklist_37 = id_checklist_37;
            return View(info);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_medidas( Check_checklist_medidas_acciones_N01 medidas_acciones_N01)
        {
           
            if (ModelState.IsValid)
            {
                DateTime today = DateTime.Today;
                //medidas_acciones_N01.created_at = today;
                //medidas_acciones_N01.updated_at = today;
                db.Check_checklist_medidas_acciones_N01.Add(medidas_acciones_N01);
                db.SaveChanges();
                return RedirectToAction("Index", "Medidas_Estatus");
            }



            return View(medidas_acciones_N01);
        }

        [HttpPost]
        public ActionResult edit_file(Check_checklist_medidas_acciones_N01 info) 
        {
            try
            {
                if (ModelState.IsValid)
                { 
                    db.Entry(info).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
             

            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "Agendado", Value = "Agendado" });
            lst.Add(new SelectListItem() { Text = "En proceso", Value = "En proceso" });
            lst.Add(new SelectListItem() { Text = "completado", Value = "Completado" });
            ViewBag.Opciones = lst; 
            return RedirectToAction("Index", "Medidas_Estatus");
        }
        public ActionResult listado_Evidencia(Check_checklist_medidas_N01 model)
        {
            ViewBag.Acciones = db.Check_checklist_medidas_acciones_N01.Where(x => x.id_checklist_37 == model.id).ToList(); 
            ViewBag.CT1 = model.Cat_I;
            ViewBag.CT2 = model.Cat_II; 
            ViewBag.CT3 = model.Cat_III;
            ViewBag.CT4 = model.Cat_IV;
            ViewBag.CT5 = model.Cat_V; 
            ViewBag.id_checklist_37 = model.id;
            return View(model);
        }
        public ActionResult Survery()
        {
            return View();
        }
        [Authorize]
        public ActionResult Resultados()
        {
            string UserName = User.Identity.Name;
            int id_usuario = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName  select E.id_user).FirstOrDefault();


            var medidas = db.Check_checklist_medidas_N01.Where(x => x.id_user == id_usuario).FirstOrDefault();
            var acciones = db.Check_checklist_medidas_acciones_N01.Where(x => x.id_checklist_37 == medidas.id).ToList();
             string cadenadeactivo = medidas.Q1.ToString() + medidas.Q2.ToString() + medidas.Q3.ToString() + medidas.Q4.ToString() + medidas.Q5.ToString() + medidas.Q6.ToString() + medidas.Q7.ToString() + medidas.Q8.ToString() + medidas.Q9.ToString() + medidas.Q10.ToString() + medidas.Q11.ToString() + medidas.Q12.ToString() + medidas.Q13.ToString() + medidas.Q14.ToString() + medidas.Q15.ToString() + medidas.Q16.ToString() + medidas.Q17.ToString() + medidas.Q18.ToString() + medidas.Q19.ToString() + medidas.Q20.ToString() + medidas.Q21.ToString() + medidas.Q22.ToString() + medidas.Q23.ToString() + medidas.Q24.ToString() + medidas.Q25.ToString() + medidas.Q26.ToString() + medidas.Q27.ToString() + medidas.Q28.ToString() + medidas.Q29.ToString() + medidas.Q30.ToString() + medidas.Q31.ToString() + medidas.Q32.ToString() + medidas.Q33.ToString() + medidas.Q34.ToString() + medidas.Q35.ToString() + medidas.Q36.ToString() + medidas.Q37.ToString();
            //int Cat_I = medidas.Q1 + medidas.Q2 + medidas.Q3 + medidas.Q4 + medidas.Q5 + medidas.Q6 + medidas.Q7 + medidas.Q8 + medidas.Q9 + medidas.Q10;
            //int Cat_II = medidas.Q11 + medidas.Q12 + medidas.Q13 + medidas.Q14 + medidas.Q15 + medidas.Q16 + medidas.Q17 + medidas.Q18 + medidas.Q19 + medidas.Q20;
            //int Cat_III = medidas.Q21 + medidas.Q22 + medidas.Q23 + medidas.Q24;
            //int Cat_IV = medidas.Q25 + medidas.Q26 + medidas.Q27 + medidas.Q28 + medidas.Q29;
            //int Cat_V = medidas.Q30 + medidas.Q31 + medidas.Q32 + medidas.Q33 + medidas.Q34 + medidas.Q35 + medidas.Q36 + medidas.Q37;

            //int QTY_Q1 = medida1 == null ? 0 : medida1.Count();
            //int QTY_Q2 = medida2 == null ? 0 : medida2.Count();
            //int QTY_Q3 = medida3 == null ? 0 : medida3.Count();
            //int QTY_Q4 = medida4 == null ? 0 : medida4.Count();
            //int QTY_Q5 = medida5 == null ? 0 : medida5.Count();
            //int QTY_Q6 = medida6 == null ? 0 : medida6.Count();
            //int QTY_Q7 = medida7 == null ? 0 : medida7.Count();
            //int QTY_Q8 = medida8 == null ? 0 : medida8.Count();
            //int QTY_Q9 = medida9 == null ? 0 : medida9.Count();
            //int QTY_Q10 = medida10 == null ? 0 : medida10.Count();
            //int QTY_Q11 = medida11 == null ? 0 : medida11.Count();
            //int QTY_Q12 = medida12 == null ? 0 : medida12.Count();
            //int QTY_Q13 = medida13 == null ? 0 : medida13.Count();
            //int QTY_Q14 = medida14 == null ? 0 : medida14.Count();
            //int QTY_Q15 = medida15 == null ? 0 : medida15.Count();
            //int QTY_Q16 = medida16 == null ? 0 : medida16.Count();
            //int QTY_Q17 = medida17 == null ? 0 : medida17.Count();
            //int QTY_Q18 = medida18 == null ? 0 : medida18.Count();
            //int QTY_Q19 = medida19 == null ? 0 : medida19.Count();
            //int QTY_Q20 = medida20 == null ? 0 : medida20.Count();
            //int QTY_Q21 = medida21 == null ? 0 : medida21.Count();
            //int QTY_Q22 = medida22 == null ? 0 : medida22.Count();
            //int QTY_Q23 = medida23 == null ? 0 : medida23.Count();
            //int QTY_Q24 = medida24 == null ? 0 : medida24.Count();
            //int QTY_Q25 = medida25 == null ? 0 : medida25.Count();
            //int QTY_Q26 = medida26 == null ? 0 : medida26.Count();
            //int QTY_Q27 = medida27 == null ? 0 : medida27.Count();
            //int QTY_Q28 = medida28 == null ? 0 : medida28.Count();
            //int QTY_Q29 = medida29 == null ? 0 : medida29.Count();
            //int QTY_Q30 = medida30 == null ? 0 : medida30.Count();
            //int QTY_Q31 = medida31 == null ? 0 : medida31.Count();
            //int QTY_Q32 = medida32 == null ? 0 : medida32.Count();
            //int QTY_Q33 = medida33 == null ? 0 : medida33.Count();
            //int QTY_Q34 = medida34 == null ? 0 : medida34.Count();
            //int QTY_Q35 = medida35 == null ? 0 : medida35.Count();
            //int QTY_Q36 = medida36 == null ? 0 : medida36.Count();
            //int QTY_Q37 = medida37 == null ? 0 : medida37.Count();

            return RedirectToAction("Checklist_Results_37", "Home", new
            {
                CT1 = medidas.Cat_I,
                CT2 = medidas.Cat_II,
                CT3 = medidas.Cat_III,
                CT4 = medidas.Cat_IV,
                CT5 = medidas.Cat_V,
                seleccionados = cadenadeactivo,
                //Q1 = medidas.Q1,
                //Q2 = QTY_Q2,
                //Q3 = QTY_Q3,
                //Q4 = QTY_Q4,
                //Q5 = QTY_Q5,
                //Q6 = QTY_Q6,
                //Q7 = QTY_Q7,
                //Q8 = QTY_Q8,
                //Q9 = QTY_Q9,
                //Q10 = QTY_Q10,
                //Q11 = QTY_Q11,
                //Q12 = QTY_Q12,
                //Q13 = QTY_Q13,
                //Q14 = QTY_Q14,
                //Q15 = QTY_Q15,
                //Q16 = QTY_Q16,
                //Q17 = QTY_Q17,
                //Q18 = QTY_Q18,
                //Q19 = QTY_Q19,
                //Q20 = QTY_Q20,
                //Q21 = QTY_Q21,
                //Q22 = QTY_Q22,
                //Q23 = QTY_Q23,
                //Q24 = QTY_Q24,
                //Q25 = QTY_Q25,
                //Q26 = QTY_Q26,
                //Q27 = QTY_Q27,
                //Q28 = QTY_Q28,
                //Q29 = QTY_Q29,
                //Q30 = QTY_Q30,
                //Q31 = QTY_Q31,
                //Q32 = QTY_Q32,
                //Q33 = QTY_Q33,
                //Q34 = QTY_Q34,
                //Q35 = QTY_Q35,
                //Q36 = QTY_Q36,
                //Q37 = QTY_Q37
            }); 
        }
        [Authorize]
        public ActionResult Medidas37()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Medidas37(Medidas medidas, string[] medida1 = null, string[] medida2 = null, string[] medida3 = null, string[] medida4 = null, string[] medida5 = null, string[] medida6 = null, string[] medida7 = null, string[] medida8 = null, string[] medida9 = null, string[] medida10 = null, string[] medida11 = null, string[] medida12 = null, string[] medida13 = null, string[] medida14 = null, string[] medida15 = null,
            string[] medida16 = null, string[] medida17 = null, string[] medida18 = null, string[] medida19 = null, string[] medida20 = null, string[] medida21 = null, string[] medida22 = null, string[] medida23 = null, string[] medida24 = null, string[] medida25 = null, string[] medida26 = null, string[] medida27 = null, string[] medida28 = null, string[] medida29 = null, string[] medida30 = null, string[] medida31 = null, string[] medida32 = null,
            string[] medida33 = null, string[] medida34 = null, string[] medida35 = null, string[] medida36 = null, string[] medida37 = null)
        {
      

            var contador_campos = 1;
            List<List<string>> fileds = new List<List<string>>() {
               medida1 is null ? new List<string>(){}:medida1.ToList() ,medida2 is null ? new List<string>(){}:medida2.ToList(),medida3 is null ? new List<string>(){}:medida3.ToList(),medida4 is null ? new List<string>(){}:medida4.ToList(),medida5 is null ? new List<string>(){}:medida5.ToList(),medida6 is null ? new List<string>(){}:medida6.ToList(),medida7 is null ? new List<string>(){}:medida7.ToList(),medida8 is null ? new List<string>(){}:medida8.ToList(),medida9 is null ? new List<string>(){}:medida9.ToList(),medida10 is null ? new List<string>(){}:medida10.ToList(),medida11 is null ? new List<string>(){}:medida11.ToList(),medida12 is null ? new List<string>(){}:medida12.ToList(),
                medida13 is null ? new List<string>(){}:medida13.ToList(),medida14 is null ? new List<string>(){}:medida14.ToList(),medida15 is null ? new List<string>(){}:medida15.ToList(),medida16 is null ? new List<string>(){}:medida16.ToList(),medida17 is null ? new List<string>(){}:medida17.ToList(),medida18 is null ? new List<string>(){}:medida18.ToList(),medida19 is null ? new List<string>(){}:medida19.ToList(),medida20 is null ? new List<string>(){}:medida20.ToList(),medida21 is null ? new List<string>(){}:medida21.ToList(),medida22 is null ? new List<string>(){}:medida22.ToList(),medida23 is null ? new List<string>(){}:medida23.ToList(),medida24 is null ? new List<string>(){}:medida24.ToList(),
                medida25 is null ? new List<string>(){}:medida25.ToList(),medida26 is null ? new List<string>(){}:medida26.ToList(),medida27 is null ? new List<string>(){}:medida27.ToList(),medida28 is null ? new List<string>(){}:medida28.ToList(),medida29 is null ? new List<string>(){}:medida29.ToList(),medida30 is null ? new List<string>(){}:medida30.ToList(),medida31 is null ? new List<string>(){}:medida31.ToList(),medida32 is null ? new List<string>(){}:medida32.ToList(),medida33 is null ? new List<string>(){}:medida33.ToList(),medida34 is null ? new List<string>(){}:medida34.ToList(),medida35 is null ? new List<string>(){}:medida35.ToList(),medida36 is null ? new List<string>(){}:medida36.ToList(),
                medida37 is null ? new List<string>(){}:medida37.ToList()
            }; 
            string UserName = User.Identity.Name;

            if (User.IsInRole("Admin-Guest"))
            {
                id_empresa_sp = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName && E.id_rol == 3 select E.id_empresa).FirstOrDefault();
            }
            else if (User.IsInRole("Admin_Centro"))
            {
                id_empresa_sp = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName && E.id_rol == 5 select E.id_empresa).FirstOrDefault();
            }
            else if (User.IsInRole("Admin"))
            {
                id_empresa_sp = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName && E.id_rol == 1 select E.id_empresa).FirstOrDefault();
            }
            else if (User.IsInRole("Admin_SyS"))
            {
                id_empresa_sp = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName && E.id_rol == 4 select E.id_empresa).FirstOrDefault();
            }
            int Cat_I = medidas.Q1 + medidas.Q2 + medidas.Q3 + medidas.Q4 + medidas.Q5 + medidas.Q6 + medidas.Q7 + medidas.Q8 + medidas.Q9 + medidas.Q10;
            int Cat_II = medidas.Q11 + medidas.Q12 + medidas.Q13 + medidas.Q14 + medidas.Q15 + medidas.Q16 + medidas.Q17 + medidas.Q18 + medidas.Q19 + medidas.Q20;
            int Cat_III = medidas.Q21 + medidas.Q22 + medidas.Q23 + medidas.Q24 ;
            int Cat_IV = medidas.Q25 + medidas.Q26 + medidas.Q27 + medidas.Q28 + medidas.Q29;
            int Cat_V = medidas.Q30+ medidas.Q31 + medidas.Q32 + medidas.Q33 + medidas.Q34 + medidas.Q35 + medidas.Q36 + medidas.Q37;

            int QTY_Q1 = medida1 == null ? 0 : medida1.Count(); 
            int QTY_Q2 = medida2 == null ? 0 : medida2.Count();
            int QTY_Q3 = medida3 == null ? 0 : medida3.Count();
            int QTY_Q4 = medida4 == null ? 0 : medida4.Count();
            int QTY_Q5 = medida5 == null ? 0 : medida5.Count();
            int QTY_Q6 = medida6 == null ? 0 : medida6.Count();
            int QTY_Q7 = medida7 == null ? 0 : medida7.Count();
            int QTY_Q8 = medida8 == null ? 0 : medida8.Count();
            int QTY_Q9 = medida9 == null ? 0 : medida9.Count();
            int QTY_Q10 = medida10 == null ? 0 : medida10.Count();
            int QTY_Q11 = medida11 == null ? 0 : medida11.Count();
            int QTY_Q12 = medida12 == null ? 0 : medida12.Count();
            int QTY_Q13 = medida13 == null ? 0 : medida13.Count();
            int QTY_Q14 = medida14 == null ? 0 : medida14.Count();
            int QTY_Q15 = medida15 == null ? 0 : medida15.Count();
            int QTY_Q16 = medida16 == null ? 0 : medida16.Count();
            int QTY_Q17 = medida17 == null ? 0 : medida17.Count();
            int QTY_Q18 = medida18 == null ? 0 : medida18.Count();
            int QTY_Q19 = medida19 == null ? 0 : medida19.Count();
            int QTY_Q20 = medida20 == null ? 0 : medida20.Count();
            int QTY_Q21 = medida21 == null ? 0 : medida21.Count();
            int QTY_Q22 = medida22 == null ? 0 : medida22.Count();
            int QTY_Q23 = medida23 == null ? 0 : medida23.Count();
            int QTY_Q24 = medida24 == null ? 0 : medida24.Count();
            int QTY_Q25 = medida25 == null ? 0 : medida25.Count();
            int QTY_Q26 = medida26 == null ? 0 : medida26.Count();
            int QTY_Q27 = medida27 == null ? 0 : medida27.Count();
            int QTY_Q28 = medida28 == null ? 0 : medida28.Count();
            int QTY_Q29 = medida29 == null ? 0 : medida29.Count();
            int QTY_Q30 = medida30 == null ? 0 : medida30.Count();
            int QTY_Q31 = medida31 == null ? 0 : medida31.Count();
            int QTY_Q32 = medida32 == null ? 0 : medida32.Count();
            int QTY_Q33 = medida33 == null ? 0 : medida33.Count();
            int QTY_Q34 = medida34 == null ? 0 : medida34.Count();
            int QTY_Q35 = medida35 == null ? 0 : medida35.Count();
            int QTY_Q36 = medida36 == null ? 0 : medida36.Count();
            int QTY_Q37 = medida37 == null ? 0 : medida37.Count();
            var commandText = "EXECUTE sp_checklist_Medidas_NOM035 @Q1 = @Q1,@Q2 = @Q2,@Q3 = @Q3,@Q4 = @Q4,@Q5 = @Q5,@Q6 = @Q6,@Q7 = @Q7,@Q8 = @Q8,@Q9 = @Q9,@Q10 = @Q10,  @Q11 = @Q11,@Q12 = @Q12,@Q13 = @Q13,@Q14 = @Q14,@Q15 = @Q15,@Q16 = @Q16,@Q17 = @Q17,@Q18 = @Q18,@Q19 = @Q19,@Q20 = @Q20,  @Q21 = @Q21,@Q22 = @Q22,@Q23 = @Q23,@Q24 = @Q24,@Q25 = @Q25,@Q26 = @Q26,@Q27 = @Q27,@Q28 = @Q28,@Q29 = @Q29,@Q30 = @Q30,  @Q31 = @Q31,@Q32 = @Q32,@Q33 = @Q33,@Q34 = @Q34,@Q35 = @Q35,@Q36 = @Q36,@Q37 = @Q37, @QR1 = @QR1,@QR2 = @QR2,@QR3 = @QR3,@QR4 = @QR4,@QR5 = @QR5,@QR6 = @QR6,@QR7 = @QR7,@QR8 = @QR8,@QR9 = @QR9,@QR10 = @QR10,  @QR11 = @QR11,@QR12 = @QR12,@QR13 = @QR13,@QR14 = @QR14,@QR15 = @QR15,@QR16 = @QR16,@QR17 = @QR17,@QR18 = @QR18,@QR19 = @QR19,@QR20 = @QR20,  @QR21 = @QR21,@QR22 = @QR22,@QR23 = @QR23,@QR24 = @QR24,@QR25 = @QR25,@QR26 = @QR26,@QR27 = @QR27,@QR28 = @QR28,@QR29 = @QR29,@QR30 = @QR30,  @QR31 = @QR31,@QR32 = @QR32,@QR33 = @QR33,@QR34 = @QR34,@QR35 = @QR35,@QR36 = @QR36,@QR37 = @QR37, @id_user=@id_user";
            var _Q1 = new SqlParameter("@Q1", medidas.Q1);
            var _Q2 = new SqlParameter("@Q2", medidas.Q2);
            var _Q3 = new SqlParameter("@Q3", medidas.Q3);
            var _Q4 = new SqlParameter("@Q4", medidas.Q4);
            var _Q5 = new SqlParameter("@Q5", medidas.Q5);
            var _Q6 = new SqlParameter("@Q6", medidas.Q6);
            var _Q7 = new SqlParameter("@Q7", medidas.Q7);
            var _Q8 = new SqlParameter("@Q8", medidas.Q8);
            var _Q9 = new SqlParameter("@Q9", medidas.Q9);
            var _Q10 = new SqlParameter("@Q10", medidas.Q10);
            var _Q11 = new SqlParameter("@Q11", medidas.Q11);
            var _Q12 = new SqlParameter("@Q12", medidas.Q12);
            var _Q13 = new SqlParameter("@Q13", medidas.Q13);
            var _Q14 = new SqlParameter("@Q14", medidas.Q14);
            var _Q15 = new SqlParameter("@Q15", medidas.Q15);
            var _Q16 = new SqlParameter("@Q16", medidas.Q16);
            var _Q17 = new SqlParameter("@Q17", medidas.Q17);
            var _Q18 = new SqlParameter("@Q18", medidas.Q18);
            var _Q19 = new SqlParameter("@Q19", medidas.Q19);
            var _Q20 = new SqlParameter("@Q20", medidas.Q20);
            var _Q21 = new SqlParameter("@Q21", medidas.Q21);
            var _Q22 = new SqlParameter("@Q22", medidas.Q22);
            var _Q23 = new SqlParameter("@Q23", medidas.Q23);
            var _Q24 = new SqlParameter("@Q24", medidas.Q24);
            var _Q25 = new SqlParameter("@Q25", medidas.Q25);
            var _Q26 = new SqlParameter("@Q26", medidas.Q26);
            var _Q27 = new SqlParameter("@Q27", medidas.Q27);
            var _Q28 = new SqlParameter("@Q28", medidas.Q28);
            var _Q29 = new SqlParameter("@Q29", medidas.Q29);
            var _Q30 = new SqlParameter("@Q30", medidas.Q30);
            var _Q31 = new SqlParameter("@Q31", medidas.Q31);
            var _Q32 = new SqlParameter("@Q32", medidas.Q32);
            var _Q33 = new SqlParameter("@Q33", medidas.Q33);
            var _Q34 = new SqlParameter("@Q34", medidas.Q34);
            var _Q35 = new SqlParameter("@Q35", medidas.Q35);
            var _Q36 = new SqlParameter("@Q36", medidas.Q36);
            var _Q37 = new SqlParameter("@Q37", medidas.Q37);  
            var _QR1 = new SqlParameter("@QR1", "");
            var _QR2 = new SqlParameter("@QR2", "");
            var _QR3 = new SqlParameter("@QR3", "");
            var _QR4 = new SqlParameter("@QR4", "");
            var _QR5 = new SqlParameter("@QR5", "");
            var _QR6 = new SqlParameter("@QR6", "");
            var _QR7 = new SqlParameter("@QR7", "");
            var _QR8 = new SqlParameter("@QR8", "");
            var _QR9 = new SqlParameter("@QR9", "");
            var _QR10 = new SqlParameter("@QR10","");
            var _QR11 = new SqlParameter("@QR11","");
            var _QR12 = new SqlParameter("@QR12","");
            var _QR13 = new SqlParameter("@QR13","");
            var _QR14 = new SqlParameter("@QR14","");
            var _QR15 = new SqlParameter("@QR15","");
            var _QR16 = new SqlParameter("@QR16","");
            var _QR17 = new SqlParameter("@QR17","");
            var _QR18 = new SqlParameter("@QR18","");
            var _QR19 = new SqlParameter("@QR19","");
            var _QR20 = new SqlParameter("@QR20","");
            var _QR21 = new SqlParameter("@QR21","");
            var _QR22 = new SqlParameter("@QR22","");
            var _QR23 = new SqlParameter("@QR23","");
            var _QR24 = new SqlParameter("@QR24","");
            var _QR25 = new SqlParameter("@QR25","");
            var _QR26 = new SqlParameter("@QR26","");
            var _QR27 = new SqlParameter("@QR27","");
            var _QR28 = new SqlParameter("@QR28","");
            var _QR29 = new SqlParameter("@QR29","");
            var _QR30 = new SqlParameter("@QR30","");
            var _QR31 = new SqlParameter("@QR31","");
            var _QR32 = new SqlParameter("@QR32","");
            var _QR33 = new SqlParameter("@QR33","");
            var _QR34 = new SqlParameter("@QR34","");
            var _QR35 = new SqlParameter("@QR35","");
            var _QR36 = new SqlParameter("@QR36","");
            var _QR37 = new SqlParameter("@QR37","");
            var info_user = db.ERGOS_Usuarios_N01.Where(x => x.User_Nombre == UserName).FirstOrDefault();
            var _id_user = new SqlParameter("@id_user", info_user.id_user); 
            db.Database.ExecuteSqlCommand(commandText, _Q1, _Q2, _Q3, _Q4, _Q5, _Q6, _Q7, _Q8, _Q9, _Q10, _Q11, _Q12, _Q13, _Q14, _Q15, _Q16, _Q17, _Q18, _Q19, _Q20, _Q21, _Q22, _Q23, _Q24, _Q25, _Q26, _Q27, _Q28, _Q29, _Q30, _Q31, _Q32, _Q33, _Q34, _Q35, _Q36, _Q37, _QR1, _QR2, _QR3, _QR4, _QR5, _QR6, _QR7, _QR8, _QR9, _QR10, _QR11, _QR12, _QR13, _QR14, _QR15, _QR16, _QR17, _QR18, _QR19, _QR20, _QR21, _QR22, _QR23, _QR24, _QR25, _QR26, _QR27, _QR28, _QR29, _QR30, _QR31, _QR32, _QR33, _QR34, _QR35, _QR36, _QR37, _id_user);
            db.SaveChanges();
            var LastRecord = db.Check_checklist_medidas_N01
            .OrderByDescending(x => x.created_at)
            .First().id;
   
                foreach (var item in fileds)
                {
                    if (item != null)
                    {
                        foreach (var item2 in item)
                        {
                            if (item2 != "")
                            {
                                var comando_ejecu = "EXECUTE sp_create_medidas @ID_Checklist = @ID_Checklist,@medida=@medida,@ID_medida=@ID_medida,@id_empresa=@id_empresa";
                                var _ID_Checklist = new SqlParameter("@ID_Checklist", LastRecord); 
                                var _medida = new SqlParameter("@medida", item2);
                                var _ID_medida = new SqlParameter("@ID_medida", contador_campos); 
                                var _id_empresa = new SqlParameter("@id_empresa", id_empresa_sp);
                                db.Database.ExecuteSqlCommand(comando_ejecu, _ID_Checklist, _medida, _ID_medida, _id_empresa);
                                db.SaveChanges();
                            }
                        }
                        contador_campos = contador_campos + 1;
                    }


                }
            

            try
                {
                    MimeMessage mail = new MimeMessage();
                    mail.From.Add(new MailboxAddress("Identifica NOM-035", "check035@check035.com"));
                    //mail.Bcc.Add(new MailboxAddress("Identifica NOM-035", "soporte@castsoft.com.mx"));
                    //mail.Bcc.Add(new MailboxAddress("Identifica NOM-035", "soporte@castsoft.com.mx"));
                    mail.To.Add(new MailboxAddress("Customer", "soporte@castsoft.com.mx"));
                    mail.Subject = "Check List 035 - 37 MEDIDAS";
                    BodyBuilder cuerpo_correo = new BodyBuilder();

                    cuerpo_correo.HtmlBody =
                      "<html>" +
                        "<head> " +
                            "<meta http-equiv='X-UA-Compatible' content='IE=edge'> " +
                            "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'> " +
                            "<meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'> " +
                            "<title>Check 035</title> " +
                        "</head>" +
                        "&nbsp;" +
                        "<table style='background: #FFFFFF;' border='0' width='100%' cellspacing='0' cellpadding='0' align='center' bgcolor='#3f3f3f'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='border-bottom: #f8f8f8 solid 1px;' align='center' valign='top' bgcolor='#FFFFFF'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td class='two-left' align='center' valign='top' width='170'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 2px; line-height: 2px;' height='2'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 16px; color: #34435a; font-weight: normal;' align='center' valign='top'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "<td class='two-left' align='center' valign='top' width='260'>" +
                        "<table border='0' width='125' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='middle'><a href='#'><img src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='' width='130' height='60' /></a></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "<td class='two-left' align='center' valign='top' width='170'></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top' bgcolor='#FFFFFF'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 45px; line-height: 45px;' align='center' valign='top' height='45'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'> Basado en tus respuestas, te presentamos los resultados del estado de cumplimiento de la NOM 035 en tu organización:</td>" +
                        "</tr>" + 
                        "<tr>" +
                        "<td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family:Poppins,sans-serif,Verdana;font-size:18px;color:#5c17e6;font-weight:bold;'align='center'valign='top'>Conoce las categorías y el status de cumplimiento:</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-size: 55px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 14px; color: #34435a; font-weight: normal; line-height:25px;' align='left' valign='top'>" +
                        "  <ol>" +
                        "  <li><strong>Ambiente de trabajo: " + Cat_I + " de 10</strong></li>" +
                        "  <li><strong>Factores propios de la actividad: "+Cat_II+" de 10</strong></li>" +
                        "  <li><strong>Organizaci&oacute;n del tiempo de trabajo: " + Cat_III + " de 4</strong></li>" +
                        "  <li><strong>Liderazgo y relaciones de trabajo: " + Cat_IV + " de 5</strong></li>" + 
                        "  <li><strong>Entorno organizacional: " + Cat_IV + " de 8</strong></li>" + 
                        "  </ol>" +
                        "</td>" +
                        "</tr>" +
                        "<tr>" +
                         "          <td valign='top' align='center'><table width='300' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                          "               <tbody><tr>" +
                           "                <td style='background:#ff5757; -moz-border-radius: 25px; border-radius: 25px; font-family:'Poppins', Verdana, Arial; font-size:14px; font-weight:bold; text-transform:uppercase; color:#FFF; ' mc:edit='wm-77' valign='middle' height='45' align='center'><multiline><a href='https://check035.com' style='text-decoration:none; color:#FFF;'>Descargar reporte PROXIMAMENTE</a></multiline></td>" +
                            "             </tr>" +
                             "          </tbody></table></td>" +
                              "     </tr>" +
                               "    <tr>" +
                                "   <td style='font-size: 55px; line-height: 55px;' align='center' valign='top' height='55'></td>" +
                                 "  </tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "</tr> " +
                        "<tr>" +
                        "<td style='font-size: 55px; line-height: 55px;' align='center' valign='top' height='55'></td>" +
                        "</tr> " +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table> " +
                        "<table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'> " +
                "<tbody> " +
                "<tr> " +
                "<td valign='top' align='center'> " +
                    "<table class='main' width='800' cellspacing='0' cellpadding='0' border='0' align='center'> " +
                     "   <tbody> " +
                      "      <tr> " +
                       "         <td valign='middle' bgcolor='#FFFFFF' align='center'> " +
                        "            <table width='100%' cellspacing='0' cellpadding='0' border='0'> " +
                         "               <tbody> " +
                          "                  <tr> " +
                           "                     <td class='two-left' valign='top' align='center'><img mc:edit='wm-129' editable='true' alt='' style='display:block;width:100% !important; height:auto !important; ' src='https://vivegrow.com/00_JP_Mailing/Check035/Curso035.png' width='400' height='440'></td> " +
                            "                    <td class='two-left' width='400' valign='middle' bgcolor='#F01141' align='center'> " +
                             "                       <table width='280' cellspacing='0' cellpadding='0' border='0'> " +
                              "                          <tbody> " +
                               "                             <tr> " +
                                "                                <td style='font-size:60px; line-height:60px;' valign='top' height='60' align='left'>&nbsp;</td> " +
                                 "                           </tr> " +
                                  "                          <tr> " +
                                   "                             <td valign='top' align='left'> " +
                                    "                                <table class='full' width='100%' cellspacing='0' cellpadding='0' border='0' align='left'> " +
                                     "                                   <tbody> " +
                                      "                                      <tr> " +
                                       "                                         <td valign='top' align='left'> " +
                                       "                                             <table width='50' cellspacing='0' cellpadding='0' border='0' align='left'> " +
                                        "                                                <tbody> " +
                                         "                                                   <tr> " +
                                          "                                                      <td valign='top' align='center'>&nbsp;</td> " +
                                           "                                                 </tr> " +
                                            "                                            </tbody> " +
                                             "                                       </table> " +
                                              "                                  </td> " +
                                               "                             </tr> " +
                                                "                            <tr> " +
                                                 "                               <td mc:edit='wm-131' style='font-family:'Poppins', sans-serif, Verdana; font-size:18px; color:#FFF; line-height:25px;' valign='top' align='left'> " +
                                                  "                                  <p>Un formato de política para factor de riesgos<br><br>Un Manual de implementación para revisar y ocupar lo que consideres relevante.<br><br>Un formato para registro de evidencias<br><br>Un Checklist para el protocolo de aplicación de encuesta<br><br>Guía de referencia para la aplicación de encuesta<br><br>Tips para el bienestar de los trabajadores</p> " +
                                                   "                             </td> " +
                                                   "                         </tr>  " +
                                                                           " <tr> " +
                                                                            "    <td style=' font-size:35px;line-height:35px;' valign='top' height='35' align='left'>&nbsp;</td> " +
                                                                            "</tr> " +
                                                                            "<tr> " +
                                                                             "   <td mc:edit='wm-77' style='margin-top:60px; background:#31BACC; -moz-border-radius: 25px; border-radius: 25px; font-family:'Poppins', Verdana, Arial; font-size:14px; font-weight:bold; text-transform:uppercase; color:#78797B; ' valign='middle' height='45' align='center'> " +
                                                                              "      <multiline><a style='text-decoration:none; color:#FFF;' href='https://check035.com/proceso-completo-para-la-implementacion-de-la-nom-035/'>Inscribirme ahora</a></multiline> " +
                                                                               " </td> " +
                                                                            "</tr> " +
                                                               "         </tbody> " +
                                                              "      </table> " +
                                                             "   </td> " +
                                                            "</tr> " +
                                                           " <tr> " +
                                                          "      <td style='font-size:60px; line-height:60px;' valign='top' height='60' align='left'>&nbsp;</td> " +
                                                         "   </tr> " +
                                                        "</tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table> " +
                         "<table width = '100%' cellspacing = '0' cellpadding = '0' border = '0' align = 'center'>       " +
                                   "<tbody><tr>         " +
                                     "<td valign = 'top' align='center'><table class='main' width='800' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                              "<tbody><tr>" +
                                "<td valign = 'middle' bgcolor='#36a50a' align='center'><table width = '100%' cellspacing='0' cellpadding='0' border='0'>" +
                                  "<tbody><tr> " +
                                   " <td class='two-left' width='400' valign='middle' bgcolor='#6216f1' align='center'><table width = '280' cellspacing='0' cellpadding='0' border='0'>" +
                                    "  <tbody><tr>" +
                                     "   <td style = 'font-size:60px; line-height:60px;' valign='top' height='60' align='left'>&nbsp;</td>" +
                                    "  </tr>" +
                                      "<tr>" +
                                    "    <td valign = 'top' align='left'><table class='full' width='100%' cellspacing='0' cellpadding='0' border='0' align='left'>" +
                                          "<tbody>" +
                                          "<tr>" +
                                          "  <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:26px; color:#FFF; line-height:38px; font-weight:bold;' mc:edit='wm-131' valign='top' align='left'><multiline>¡Alcanza el cumplimiento total!</multiline></td>" +
                                          "</tr>" +
                                         " <tr>" +
                                         "   <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:20px; color:#FFF; line-height:30px; font-weight:normal;' mc:edit='wm-133' valign='top' align='left'><b style = 'font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#FFF; font-weight:bold;'>CON</b></td>" +
                                         " </tr>" +
                                        "</tbody></table></td>" +
                                      "</tr>" +
                                      "<tr>" +
                                     "   <td style = 'font-size:60px; line-height:60px;' valign='top' height='60' align='left'>&nbsp;</td>" +
                                    "  </tr>" +
                                   " </tbody></table></td>" +
                                    "<td class='two-left' valign='top' align='center'><img mc:edit='wm-129' editable='true' src='https://vivegrow.com/00_JP_Mailing/Check035/Check035Mock.jpg' alt='' style='display:block;width:100% !important; height:auto !important; ' width='400' height='440'></td>  " +
                               "    </tr> " +
                         "        </tbody></table></td> " +
                         "      </tr> " +
                        "     </tbody></table> </td> " +
                      "     </tr> " +
                       "  </tbody></table> " +
                       "  <table border = '0' width='100%' cellspacing='0' cellpadding='0' align='center'> " +
                       "  <tbody> " +
                        " <tr>" +
                       "  <td align = 'center' valign='top'>" +
                      "   <table border = '0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                       " <tbody>" +
                        "<tr>" +
                        "<td align = 'center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                       " <tr>" +
                       "                     <td align = 'center' valign='middle' bgcolor='#ffffff'><!--Section-->" +
                       "                     <table border = '0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                       "                     <tbody>" +
                       "                     <tr>" +
                       "                     <td align = 'center' valign='top'>" +
                       "                     <table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                       "                     <tbody>" +
                       "                     <tr>" +
                       "                     <td align = 'center' valign='top' bgcolor='#ff5757'>" +
                       "                     <table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                       "                     <tbody>" +
                       "                     <tr>" +
                       "                     <td align = 'center' valign='top' height='40'></td>" +
                       "                     </tr>" +
                       "                     <tr>" +
                       "                     <td align = 'center' valign='top'>" +
                       "                     <table border = '0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                       "                     <tbody>" +
                       "                     <tr>" +
                       "                     <td style = 'font-size: 20px; line-height: 20px;' align='center' valign='top' height='20'></td>" +
                       "                     </tr>" +
                       "                     <tr>" +
                       "                     <td style = 'font-family: Poppins, sans-serif, Verdana; font-size: 22px; color: #fff; font-weight: bold; line-height: 32px;' align='left' valign='top'>¿Qué es Check 035?</td>" +
                       "                     </tr>" +
                       "                     <tr>" +
                       "                     <td style = 'font-size: 10px; line-height: 10px;' align='center' valign='top' height='10'></td>" +
                       "                     </tr>" +
                       "                     <tr>" +
                       "                     <td style = 'font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #fff; font-weight: normal; line-height: 24px;' align='left' valign='top'>La plataforma que te permitir&aacute; <strong>optimizar el proceso de implementaci&oacute;n de la NOM 035 </strong ></td> " +
                       "                     </tr>" +
                       "                     <tr>" +
                       "                     <td align = 'center' valign='top'></td>" +
                       "                     </tr>" +
                       "                     </tbody>" +
                       "                     </table>" +
                       "                     </td>" +
                       "                     </tr>" +
                       "                     <tr>" +
                       "                     <td class='two-left' style='font-size: 30px; line-height: 30px;' align='center' valign='top' height='30'></td>" +
                       "                     </tr>" +
                       "                     </tbody>" +
                       "                     </table> " +
                       "                     <table width = '100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                       "                           <tbody><tr>" +
                       "                             <td valign = 'top' align='center'><table class='main' width='800' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                       "                               <tbody><tr>" +
                       "                                 <td valign = 'top' align='center'><table class='two-left-inner' width='600' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                       "                                   <tbody><tr>" +
                       "                                     <td class='two-left' width='280' valign='top' align='center'><table width = '280' cellspacing='0' cellpadding='0' border='0' align='left'>" +
                       "                                       <tbody><tr>" +
                       "                                         <td valign = 'top' align='left'><img mc:edit='wm-105' editable='true' src='https://vivegrow.com/00_JP_Mailing/Check035/Call.jpg' alt='' width='280' height='170'></td>" +
                       "                                       </tr>" +
                       "                                       <tr>" +
                       "                                         <td valign = 'top' height='25' align='left'>&nbsp;</td>" +
                       "                                       </tr>" +
                       "                                     </tbody></table></td>" +
                       "                                     <td class='two-left' width='40' valign='top' align='center'>&nbsp;</td>" +
                       "                                     <td class='two-left' width='280' valign='top' align='center'><table width = '280' cellspacing='0' cellpadding='0' border='0' align='left'>" +
                       "                                       <tbody><tr>" +
                       "                                         <td valign = 'top' align='left'><table width = '100%' cellspacing='0' cellpadding='0' border='0' align='left'>" +
                         "                                         <tbody>" +
                        "                                          <tr>" +
                        "                                            <td style = 'font-size:10px; line-height:10px;' valign='top' height='10' align='center'>&nbsp;</td>" +
                        "                                          </tr>" +
                        "                                          <tr>" +
                        "                                            <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:20px; color:#263940; font-weight:bold; line-height:25px;' mc:edit='wm-107' valign='top' align='left'><multiline>Cuenta con una línea exclusiva</multiline></td>" +
                        "                                          </tr>" +
                        "                                          <tr>" +
                        "                                            <td style = 'font-size:10px; line-height:10px;' valign='top' height='10' align='center'>&nbsp;</td>" +
                        "                                          </tr>" +
                        "                                          <tr>" +
                        "                                            <td class='two-left' style='font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#263940; font-weight:normal; line-height:26px;' mc:edit='wm-108' valign='top' align='left'><multiline>para escucha de denuncias por violencia y un buzón de quejas</ multiline ></td> " +
                        "                                          </tr>" +
                        "                                        </tbody></table></td>" +
                        "                                      </tr>" +
                        "                                      <tr>" +
                        "                                        <td style = 'font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
                        "                                      </tr>" +
                        "                                      <tr>" +
                        "                                        <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#82b440; font-weight:bold; text-transform:uppercase;' mc:edit='wm-109' valign='top' align='left'><multiline><a href = 'https://check035.com/linea-escucha/' style = 'color:#F8E16C; text-decoration:none;' > Me interesa </ a ></ multiline ></td> " +
                            "                                      </tr>" +
                        "                                      <tr>" +
                        "                                        <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#82b440; font-weight:bold; text-transform:uppercase;' valign= 'top' align= 'left' > &nbsp;</td>" +
                        "                                      </tr>" +
                        "                                    </tbody></table></td>" +
                        "                                  </tr>" +
                        "                                  <tr>" +
                        "                                   <td colspan = '3' style='font-size:70px; line-height:70px;' class='two-left' valign='top' height='70' align='center'>&nbsp;</td>" +
                        "                                    </tr>" +
                        "                                  </tbody></table></td>" +
                        "                              </tr>" +
                        "                            </tbody></table></td>" +
                        "                          </tr>" +
                        "                        </tbody></table>" +
                        "                        <table width = '100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                        "                              <tbody><tr>" +
                        "                                <td valign = 'top' align='center'><table class='main' width='800' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                        "                                  <tbody><tr>" +
                        "                                    <td valign = 'top' align='center'><table class='two-left-inner' width='600' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                        "                                      <tbody><tr>" +
                        "                                        <td class='two-left' width='280' valign='top' align='center'><table width = '280' cellspacing='0' cellpadding='0' border='0' align='left'> " +
                        "                                          <tbody><tr>" +
                        "                                            <td valign = 'top' align='left'><table width = '100%' cellspacing='0' cellpadding='0' border='0' align='left'>" +
                        "                                            <tbody>" +
                        "                                              <tr>" +
                         "                                                <td style = 'font-size:10px; line-height:10px;' valign='top' height='10' align='center'>&nbsp;</td>" +
                         "                                             </tr>" +
                         "                                             <tr>" +
                         "               <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:20px; color:#263940; font-weight:bold; line-height:25px;' mc:edit='wm-111' valign='top' align='left'><multiline>Desarrolla y fomenta la cultura de bienestar</multiline></td>" +
                         "                                             </tr>" +
                         "                                             <tr>" +
                         "                                                <td style = 'font-size:10px; line-height:10px;' valign='top' height='10' align='center'>&nbsp;</td>" +
                         "                                             </tr>" +
                         "                                             <tr>" +
                          "<td class='two-left' style='font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#263940; font-weight:normal; line-height:26px;' mc:edit='wm-112' valign='top' align='left'><multiline>en organizaciones de cualquier tamaño a través de cursos 100% online </ multiline ></td>" +
                          "                                            </tr>" +
                          "                                          </tbody></table></td>" +
                          "                                        </tr>" +
                          "                                        <tr>" +
                          "                                          <td style = 'font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
                          "                                        </tr>" +
                          "                                        <tr>" +
                            "                                       <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#82b440; font-weight:bold; text-transform:uppercase;' mc:edit='wm-113' valign='top' align='left'><multiline><a href = 'https://aprende.check035.com/' style = 'color:#F8E16C; text-decoration:none;'> SABER MÁS </ a ></ multiline ></td>" +
                               "                                       </tr>" +
                           "                                       <tr>" +
                           "                                         <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#960298; font-weight:bold; text-transform:uppercase;' valign= 'top' align= 'left' > &nbsp;</td>" +
                           "                                       </tr>" +
                           "                                     </tbody></table></td>" +
                           "                                     <td class='two-left' width='40' valign='top' align='center'>&nbsp;</td>" +
                           "                                     <td class='two-left' width='280' valign='top' align='center'><table width = '280' cellspacing='0' cellpadding='0' border='0' align='left'>" +
                           "                                       <tbody><tr>" +
                               "<td valign = 'top' align='left'><img mc:edit='wm-114' editable='true' src='https://vivegrow.com/00_JP_Mailing/Check035/CultureClub.jpg' alt='' width='280' height='170'></td>" +
                            "                                      </tr>" +
                            "                                      <tr>" +
                            "                                        <td valign = 'top' height='25' align='left'>&nbsp;</td>" +
                            "                                      </tr>" +
                            "                                      </tbody></table></td>" +
                            "                                  </tr>" +
                            "                                  <tr>" +
                            "                                    <td colspan = '3' style='font-size:120px; line-height:120px;' class='two-left' valign='top' height='120' align='center'>&nbsp;</td>" +
                           "                                     </tr>" +
                           "                                   </tbody></table></td>" +
                           "                               </tr>" +
                           "                             </tbody></table></td>" +
                           "                           </tr>" +
                           "                         </tbody></table>" +
                           "                 </td>" +
                           "                 </tr>" +
                           "                 </tbody>" +
                           "                 </table>" +
                           "                 </td>" +
                           "                 </tr>" +
                           "                 </tbody>" +
                           "                 </table>" +
                           "                 <table width = '100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                           "                       <tbody><tr>" +
                           "                         <td valign = 'top' align='center'><table class='main' width='800' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                           "                           <tbody><tr>" +
                           "                             <td valign = 'top' bgcolor='#FFFFFF' align='center'><table class='two-left-inner' width='600' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                           "                               <tbody><tr>" +
                           "                                  <td style = ' font-size:90px;line-height:90px;' valign='top' height='90' align='center'>&nbsp;</td>" +
                           "                               </tr>" +
                           "                               <tr>" +
                           "                                 <td valign = 'top' align='center'><table width = '100%' cellspacing='0' cellpadding='0' border='0' align='left'>" +
                           "                                   <tbody><tr>" +
                           "                                     <td class='two-left' width='245' valign='top' align='left'><table width = '100%' cellspacing='0' cellpadding='0' border='0' align='left'>" +
                           "                                       <tbody><tr>" +
                           "                                         <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:18px; color:#4C5B61; font-weight:normal;' mc:edit='wm-151' valign='top' align='left'><multiline>¿Cuáles son los</multiline></td>" +
                           "                                       </tr>" +
                           "                                       <tr>" +
                           "                                         <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:35px; color:#4C5B61; font-weight:bold; line-height:50px;' mc:edit='wm-152' valign='top' align='left'><multiline>beneficios?</multiline></td>" +
                           "                                       </tr>" +
                           "                                     </tbody></table></td>" +
                           "                                     <td class='menu-icon' style='border-left:#d4d1d4 solid 1px;' width='40' valign='top' align='left'>&nbsp;</td>" +
                           "                                     <td class='two-left' style='font-family:Poppins, sans-serif, Verdana; font-size:16px; color:#4C5B61; font-weight:normal; line-height:30px;' mc:edit='wm-153' width='315' valign='top' align='left'><multiline>Además de no ser sujeto a multas, el cumplimiento de la NOM035 favorece:</ multiline ></td>" +
                           "                                   </tr>" +
                           "                                 </tbody></table></td>" +
                           "                               </tr>" +
                           "                               <tr>" +
                           "                                 <td style = ' font-size:55px;line-height:55px;' valign='top' height='65' align='left'>&nbsp;</td>" +
                           "                               </tr>" +
                           "                               </tbody></table></td>" +
                           "                           </tr>" +
                           "                         </tbody></table></td>" +
                           "                       </tr>" +
                           "                     </tbody></table>" +
                           "<table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                    "      <tbody><tr>" +
                    "        <td valign='top' align='center'><table class='main' width='800' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                    "          <tbody><tr>" +
                    "            <td valign='top' bgcolor='#FFFFFF' align='center'><table class='two-left-inner' width='600' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                    "              <tbody><tr>" +
                    "                <td valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0' border='0'>" +
                    "              <tbody><tr>" +
                    "                <td class='two-left' width='180' valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                    "                  <tbody><tr>" +
                    "                    <td valign='top' align='left'><img mc:edit='wm-16' editable='true' src='https://vivegrow.com/00_JP_Mailing/Check035/Productividad.png' alt='' style='display:block;' width='64' height='64'></td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                    <td style='font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                   <td style='font-family:Poppins, sans-serif, Verdana; font-size:16px; color:#ff5757; line-height:22px; text-transform:uppercase; font-weight:bold;' mc:edit='wm-17' valign='top' align='left'><multiline>Mayor productividad</multiline></td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                    <td style='font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                   <td colspan='2' style='font-family:Poppins, sans-serif, Verdana; font-size:13px; color:#818685; line-height:24px;' mc:edit='wm-18' valign='top' align='left'><multiline>e incremento en las utilidades</multiline></td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                    <td style='font-size:30px; line-height:30px;' valign='top' height='30' align='center'>&nbsp;</td>" +
                    "                  </tr>" +
                    "                </tbody></table></td>" +
                    "                <td class='two-left' width='20' valign='top' align='center'>&nbsp;</td>" +
                    "                <td class='two-left' width='180' valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                    "                  <tbody><tr>" +
                    "                    <td valign='top' align='left'><img mc:edit='wm-19' editable='true' src='https://vivegrow.com/00_JP_Mailing/Check035/Entorno.png' alt='' style='display:block;' width='64' height='64'></td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                    <td style='font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                   <td style='font-family:Poppins, sans-serif, Verdana; font-size:16px; color:#ff5757; line-height:22px; text-transform:uppercase; font-weight:bold;' mc:edit='wm-20' valign='top' align='left'><multiline>Mejora</multiline></td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                    <td style='font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                   <td colspan='2' style='font-family:Poppins, sans-serif, Verdana; font-size:13px; color:#818685; line-height:24px;' mc:edit='wm-21' valign='top' align='left'><multiline>el entorno organizacional</multiline></td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                    <td style='font-size:30px; line-height:30px;' valign='top' height='30' align='center'>&nbsp;</td>" +
                    "                  </tr>" +
                    "                </tbody></table></td>" +
                    "                <td class='two-left' width='20' valign='top' align='center'>&nbsp;</td>" +
                    "                <td class='two-left' width='180' valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                    "                  <tbody><tr>" +
                    "                    <td valign='top' align='left'><img mc:edit='wm-22' editable='true' src='https://vivegrow.com/00_JP_Mailing/Check035/Prevencion.png' alt='' style='display:block;' width='64' height='64'></td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                    <td style='font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                   <td style='font-family:Poppins, sans-serif, Verdana; font-size:16px; color:#ff5757; line-height:22px; text-transform:uppercase; font-weight:bold;' mc:edit='wm-23' valign='top' align='left'><multiline>Prevención</multiline></td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                    <td style='font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "              <td colspan='2' style='font-family:Poppins, sans-serif, Verdana; font-size:13px; color:#818685; line-height:24px;' mc:edit='wm-24' valign='top' align='left'><multiline>de las condiciones que pudieran afectar la salud mental de los colaboradores</multiline></td>" +
                    "                  </tr>" +
                    "                  <tr>" +
                    "                    <td style='font-size:30px; line-height:30px;' valign='top' height='30' align='center'>&nbsp;</td>" +
                    "                  </tr> " +
                    "                </tbody></table></td>" +
                    "              </tr> " +
                    "            </tbody></table></td>" +
                    "              </tr>" +
                    "              <tr>" +
                    "                                   <td valign='top' align='center'><table width='300' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                    "                                         <tbody><tr>" +
                     " <td style='background:#ff5757; -moz-border-radius: 25px; border-radius: 25px; font-family:Poppins, Verdana, Arial; font-size:14px; font-weight:bold; text-transform:uppercase; color:#FFF; ' mc:edit='wm-77' valign='middle' height='45' align='center'>" +
                      "<multiline><a href='https://store.vivegrow.com.mx' style='text-decoration:none; color:#FFF;'>Visitar sitio web</a></multiline></td>" +
                    "                                         </tr>" +
                    "                                       </tbody></table></td>" +
                    "                                   </tr>" +
                    "              <tr>" +
                    "                <td style=' font-size:75px;line-height:75px;' valign='top' height='75' align='left'>&nbsp;</td>" +
                    "              </tr>" +
                    "              </tbody></table></td>" +
                    "          </tr>" +
                    "        </tbody></table></td>" +
                    "      </tr>" +
                    "</tbody></table>" +
                    "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td align='center' valign='top' bgcolor='#5c17e6'>" +
                    "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td style='font-size: 100px; line-height: 100px;' align='center' valign='top' height='100'></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td align='center' valign='top'>" +
                          "<table class='two-left-inner' border='0' width='520' cellspacing='0' cellpadding='0' align='center'>" +
                          "<tbody>" +
                          "<tr>" +
                          "<td style = 'font-size: 15px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
                          "</tr>" +
                          "<tr>" +
                          "<td style = 'font-family: Poppins, sans-serif, Verdana; font-size: 20px; color: #fff; font-weight: bold; line-height: 30px;' align='center' valign='top'>¿Buscas tips o sugerencias?<br>¡Escucha nuestro podcast!</td>" +
                          "</tr>" +
                          "<tr>" +
                          "<td style = 'font-size: 25px; line-height: 25px;' align='left' valign='top' height='25'></td>" +
                          "</tr> " +
                          "<tr>" +
                          "<td style = 'line-height: 20px;' align='left' valign='top' height='20'><a href='https://check035.com/podcast/'><img style='display: block;' src='Podcast.jpg' alt=''  /></a></td>" +
                          "</tr> " +
                          "<tr>" +
                          "<td style = 'font-family: Poppins, sans-serif, Verdana; font-size: 20px; color: #fff; font-weight: bold; line-height: 30px;' align='center' valign='top'>Episodio 1: Lo mejor de la NOM 035</td>" +
                          "</tr>" +
                          "<tr>" +
                          "<td style = 'font-size: 25px; line-height: 25px;' align='left' valign='top' height='25'></td>" +
                          "</tr>" +
                          "<tr>" +
                          "<td align = 'center' valign='top'>" +
                          "<table border = '0' width='320' cellspacing='0' cellpadding='0' align='center'>" +
                          "<tbody>" +
                          "<tr>      <td style = 'background: #ba9ef3; -moz-border-radius: 25px; border-radius: 25px; font-family: Poppins, Verdana, Arial; font-size: 14px; font-weight: bold; text-transform: uppercase; color: #fff;' align='center' valign='middle' height='45'><a style = 'text-decoration: none; color: #fff;' href='https://check035.com/podcast/'>Escuchar podcast</a></td>" +
                          "</tr>" +
                          "</tbody>" +
                          "</table>" +
                    "</td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td style='font-size: 80px; line-height: 120px;' align='center' valign='top' height='120'></td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "</td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "</td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td align='center' valign='top'>" +
                    "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                    "</table>" +
                    "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td align='center' valign='top'>" +
                    "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td style='background: #fff;' align='left' valign='top' bgcolor='#292828'>" +
                    "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "</tr>" +
                    "<tr>" +
                    "<td align='left' valign='top'>" +
                    "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='right'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td align='center' valign='top'>" +
                    "<a href='hhttps://check035.com/wp-content/uploads/2021/11/logo-web.png'>" +
                    "<img class='alignnone' style='display: block;' src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='Logo Grow Incubadora de Mentes Creativas' width='130' height='60' />" +
                    "</a></td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='left'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000; font-weight: normal; padding-top: 20px;' align='center' valign='top'>Check 035</td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "</td>" +
                    "</tr>" +
                    "<tr>" +
                    "</tr>" +
                    "<tr>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "</td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "</td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "</td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "</td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "</td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "&nbsp;" +
                    "&nbsp;</td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "</html>";


                    mail.Body = cuerpo_correo.ToMessageBody();
                    SmtpClient SmtpServer = new SmtpClient();
                    SmtpServer.CheckCertificateRevocation = true;
                    SmtpServer.Connect("smtpout.secureserver.net", 465, true);
                    SmtpServer.Authenticate("check035@check035.com", "ZaZc3JnfTSH%*4c");
                    SmtpServer.Send(mail);
                    //db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails @user ='WEB_CHECKLIST',@mail_send_to = '" + Email + "',@Status = 1");
                    //db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails_fail @user ='WEB_CHECKLIST',@mail_send_to = '" + Email + "',@Status = 0, @comment = '" + ex.Message.ToString() + "'");
                    //db.SaveChanges();
                }
            string cadenadeactivo = medidas.Q1.ToString() + medidas.Q2.ToString() + medidas.Q3.ToString() + medidas.Q4.ToString() + medidas.Q5.ToString() + medidas.Q6.ToString() + medidas.Q7.ToString() + medidas.Q8.ToString() + medidas.Q9.ToString() + medidas.Q10.ToString() + medidas.Q11.ToString() + medidas.Q12.ToString() + medidas.Q13.ToString() + medidas.Q14.ToString() + medidas.Q15.ToString() + medidas.Q16.ToString() + medidas.Q17.ToString() + medidas.Q18.ToString() + medidas.Q19.ToString() + medidas.Q20.ToString() + medidas.Q21.ToString() + medidas.Q22.ToString() + medidas.Q23.ToString() + medidas.Q24.ToString() + medidas.Q25.ToString() + medidas.Q26.ToString() + medidas.Q27.ToString() + medidas.Q28.ToString() + medidas.Q29.ToString() + medidas.Q30.ToString() + medidas.Q31.ToString() + medidas.Q32.ToString() + medidas.Q33.ToString() + medidas.Q34.ToString() + medidas.Q35.ToString() + medidas.Q36.ToString() + medidas.Q37.ToString();
            return Json(new
            {
                redirectToUrl = Url.Action("Checklist_Results", "Home", new
                {
                    CT1 = Cat_I,
                    CT2 = Cat_II,
                    CT3 = Cat_III,
                    CT4 = Cat_IV,
                    CT5 = Cat_V,
                    seleccionados = cadenadeactivo,
                    Q1 = QTY_Q1,
                    Q2 = QTY_Q2,
                    Q3 = QTY_Q3,
                    Q4 = QTY_Q4,
                    Q5 = QTY_Q5,
                    Q6 = QTY_Q6,
                    Q7 = QTY_Q7,
                    Q8 = QTY_Q8,
                    Q9 = QTY_Q9,
                    Q10 = QTY_Q10,
                    Q11 = QTY_Q11,
                    Q12 = QTY_Q12,
                    Q13 = QTY_Q13,
                    Q14 = QTY_Q14,
                    Q15 = QTY_Q15,
                    Q16 = QTY_Q16,
                    Q17 = QTY_Q17,
                    Q18 = QTY_Q18,
                    Q19 = QTY_Q19,
                    Q20 = QTY_Q20,
                    Q21 = QTY_Q21,
                    Q22 = QTY_Q22,
                    Q23 = QTY_Q23,
                    Q24 = QTY_Q24,
                    Q25 = QTY_Q25,
                    Q26 = QTY_Q26,
                    Q27 = QTY_Q27,
                    Q28 = QTY_Q28,
                    Q29 = QTY_Q29,
                    Q30 = QTY_Q30,
                    Q31 = QTY_Q31,
                    Q32 = QTY_Q32,
                    Q33 = QTY_Q33,
                    Q34 = QTY_Q34,
                    Q35 = QTY_Q35,
                    Q36 = QTY_Q36,
                    Q37 = QTY_Q37
                })
            });
             
        }

        public ActionResult evidencia(int id,int medidas)
        {
            return View();
        }
        [HttpPost]
        public JsonResult checklist_js(int Q1, int Q2, int Q3, int Q4, int Q5, int Q6, int Q7, int Q8, int Q9, int Q10, string Email, string Empresa, string Telefono ,string Empleados, string Nombre)
        {
            string R1;
            string R2;
            string R3;
            string R4;
            string R5;
            string R6;
            string R7;
            string R8;
            string R9;
            string R10;
            var commandText = "EXECUTE sp_checklist_NOM035 @Q1 = @Q1,@Q2 = @Q2,@Q3 = @Q3,@Q4 = @Q4,@Q5 = @Q5,@Q6 = @Q6,@Q7 = @Q7,@Q8 = @Q8,@Q9 = @Q9,@Q10 = @Q10, @Email = @Email, @Empresa = @Empresa, @telefono = @telefono, @Empleados = @Empleados, @Nombre = @Nombre ";
            var _Q1 = new SqlParameter("@Q1", Q1);
            var _Q2 = new SqlParameter("@Q2", Q2);
            var _Q3 = new SqlParameter("@Q3", Q3);
            var _Q4 = new SqlParameter("@Q4", Q4);
            var _Q5 = new SqlParameter("@Q5", Q5);
            var _Q6 = new SqlParameter("@Q6", Q6);
            var _Q7 = new SqlParameter("@Q7", Q7);
            var _Q8 = new SqlParameter("@Q8", Q8);
            var _Q9 = new SqlParameter("@Q9", Q9);
            var _Q10 = new SqlParameter("@Q10", Q10);
            var _Email = new SqlParameter("@Email", Email);
            var _Empresa = new SqlParameter("@Empresa", Empresa);
            var _telefono = new SqlParameter("@telefono", Telefono);
            var _Empleados = new SqlParameter("@Empleados", Empleados);
            var _Nombre = new SqlParameter("@Nombre", Nombre);
            db.Database.ExecuteSqlCommand(commandText, _Q1, _Q2, _Q3, _Q4, _Q5, _Q6, _Q7, _Q8, _Q9, _Q10, _Email, _Empresa, _telefono, _Empleados, _Nombre);
            db.SaveChanges();

           

            if (Q1 == 1) { R1 = "<li>Dise&ntilde;ar una pol&iacute;tica de prevenci&oacute;n de riesgos por factor psicosocial</li>"; }
            else { R1 = "<li><strong style='color:red'>Dise&ntilde;ar una pol&iacute;tica de prevenci&oacute;n de riesgos por factor psicosocial</strong></li>"; }

            if (Q2 == 1) { R2 = "<li>Hacer campa&ntilde;as de comunicaci&oacute;n y capacitaci&oacute;n sobre la NOM y la pol&iacute;tica de prevenci&oacute;n de riesgos</li>"; }
            else   { R2 = "<li><strong style='color:red'>Hacer campa&ntilde;as de comunicaci&oacute;n y capacitaci&oacute;n sobre la NOM y la pol&iacute;tica de prevenci&oacute;n de riesgos</strong></li>"; }

            if (Q3 == 1) { R3 = "<li>Firmar el registro de capacitaci&oacute;n, comunicaci&oacute;n y pol&iacute;tica de prevenci&oacute;n de riesgos</li>"; }
            else  { R3 = "<li><strong style='color:red'>Firmar el registro de capacitaci&oacute;n, comunicaci&oacute;n y pol&iacute;tica de prevenci&oacute;n de riesgos</strong></li>"; }

            if (Q4 == 1) { R4 = "<li>Realizar un an&aacute;lisis de identificaci&oacute;n de riesgos por factor psicosocial</li>"; }
            else  { R4 = "<li><strong style='color:red'>Realizar un an&aacute;lisis de identificaci&oacute;n de riesgos por factor psicosocial</strong></li>"; }

            if (Q5 == 1) { R5 = "<li>Dar a conocer los resultados a todo el personal</li>"; }
            else { R5 = "<li><strong style='color:red'>Dar a conocer los resultados a todo el personal</strong></li>"; }

            if (Q6 == 1) { R6 = "<li>Contar con un buz&oacute;n de denuncia de violencia laboral</li>"; }
            else { R6 = "<li><strong style='color:red'>Contar con un buz&oacute;n de denuncia de violencia laboral</strong></li>"; }

            if (Q7 == 1) { R7 = "<li>Contar con un plan de medidas para la prevenci&oacute;n de riesgos por factor psicosocial</li>"; }
            else { R7 = "<li><strong style='color:red'>Contar con un plan de medidas para la prevenci&oacute;n de riesgos por factor psicosocial</strong></li>"; }

            if (Q8 == 1) { R8 = "<li>Comunicar el plan de medidas a todo el personal</li>"; }
            else { R8 = "<li><strong style='color:red'>Comunicar el plan de medidas a todo el personal</strong></li>"; }

            if (Q9 == 1) { R9 = "<li>Capacitar a los colaboradores seg&uacute;n sus resultados de encuesta</li>"; }
            else { R9 = "<li><strong style='color:red'>Capacitar a los colaboradores seg&uacute;n sus resultados de encuesta</strong></li>"; }

            if (Q10 == 1) { R10 = "<li>contar con un plan de desarrollo continuo de cultura de bienestar organizacional</li>"; }
            else { R10 = "<li><strong style='color:red'>contar con un plan de desarrollo continuo de cultura de bienestar organizacional</strong></li>"; }

            int calif = Q1 + Q2 + Q3 + Q4 + Q5 + Q6 + Q7 + Q8 + Q9 + Q10;

            if (Email != null)
            {

                try
                {
                    MimeMessage mail = new MimeMessage();
                     mail.From.Add(new MailboxAddress("Identifica NOM-035", "check035@check035.com"));
                    //mail.Bcc.Add(new MailboxAddress("Identifica NOM-035", "soporte@castsoft.com.mx"));
                    //mail.Bcc.Add(new MailboxAddress("Identifica NOM-035", "soporte@castsoft.com.mx"));
                    mail.To.Add(new MailboxAddress("Customer", Email));
                    mail.Subject = "Check List 035";
                    BodyBuilder cuerpo_correo = new BodyBuilder();

                    cuerpo_correo.HtmlBody =
                      "<html>" +
                        "<head> " +
                            "<meta http-equiv='X-UA-Compatible' content='IE=edge'> " +
                            "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'> " +
                            "<meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'> " +
                            "<title>Check 035</title> " +
                        "</head>" +
                        "&nbsp;" +
                        "<table style='background: #FFFFFF;' border='0' width='100%' cellspacing='0' cellpadding='0' align='center' bgcolor='#3f3f3f'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='border-bottom: #f8f8f8 solid 1px;' align='center' valign='top' bgcolor='#FFFFFF'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td class='two-left' align='center' valign='top' width='170'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 2px; line-height: 2px;' height='2'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 16px; color: #34435a; font-weight: normal;' align='center' valign='top'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "<td class='two-left' align='center' valign='top' width='260'>" +
                        "<table border='0' width='125' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='middle'><a href='#'><img src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='' width='130' height='60' /></a></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "<td class='two-left' align='center' valign='top' width='170'></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top' bgcolor='#FFFFFF'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 45px; line-height: 45px;' align='center' valign='top' height='45'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Hola, Aquí tienes los resultados de tu checklist para el cumplimiento de la NOM 035:</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family:Poppins, sans-serif, Verdana; font-size:22px; color:#ff5757; font-weight:bold; line-height:32px; padding-top:25px;' mc:edit='wm-75' valign='top' align='center'><multiline>" + calif + " de 10 puntos máximos posibles</multiline></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family:Poppins,sans-serif,Verdana;font-size:18px;color:#5c17e6;font-weight:bold;'align='center'valign='top'>Los diez puntos por cumplir son los siguientes:</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-size: 55px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 14px; color: #34435a; font-weight: normal; line-height:25px;' align='left' valign='top'>" +
                        "  <ol>" +
                        "  <li><strong>Dise&ntilde;ar una pol&iacute;tica de prevenci&oacute;n de riesgos por factor psicosocial</strong></li>" +
                        "  <li><strong>Hacer campa&ntilde;as de comunicaci&oacute;n y capacitaci&oacute;n sobre la NOM y la pol&iacute;tica de prevenci&oacute;n de riesgos</strong></li>" +
                        "  <li><strong>Firmar el registro de capacitaci&oacute;n, comunicaci&oacute;n y pol&iacute;tica de prevenci&oacute;n de riesgos</strong></li>" +
                        "  <li><strong>Realizar un an&aacute;lisis de identificaci&oacute;n de riesgos por factor psicosocial</strong></li>" +
                        "  <li><strong>Dar a conocer los resultados a todo el personal</strong></li>" +
                        "  <li><strong>Contar con un buz&oacute;n de denuncia de violencia laboral</strong></li>" +
                        "  <li><strong>Contar con un plan de medidas para la prevenci&oacute;n de riesgos por factor psicosocial</strong></li>" +
                        "  <li><strong>Comunicar el plan de medidas a todo el personal</strong></li>" +
                        "  <li><strong>Capacitar a los colaboradores seg&uacute;n sus resultados de encuesta</strong></li>" +
                        "  <li><strong>contar con un plan de desarrollo continuo de cultura de bienestar organizacional</strong></li>" +
                        "  </ol>" +
                        "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "</tr> " +
                        "<tr>" +
                        "<td style='font-size: 55px; line-height: 55px;' align='center' valign='top' height='55'></td>" +
                        "</tr> " +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table> " +
                         "<table width = '100%' cellspacing = '0' cellpadding = '0' border = '0' align = 'center'>       " +
                                   "<tbody><tr>         " +
                                     "<td valign = 'top' align='center'><table class='main' width='800' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                              "<tbody><tr>" +
                                "<td valign = 'middle' bgcolor='#36a50a' align='center'><table width = '100%' cellspacing='0' cellpadding='0' border='0'>" +
                                  "<tbody><tr> " +
                                   " <td class='two-left' width='400' valign='middle' bgcolor='#6216f1' align='center'><table width = '280' cellspacing='0' cellpadding='0' border='0'>" +
                                    "  <tbody><tr>" +
                                     "   <td style = 'font-size:60px; line-height:60px;' valign='top' height='60' align='left'>&nbsp;</td>" +
                                    "  </tr>" +
                                      "<tr>" +
                                    "    <td valign = 'top' align='left'><table class='full' width='100%' cellspacing='0' cellpadding='0' border='0' align='left'>" +
                                          "<tbody>" +
                                          "<tr>" +
                                          "  <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:26px; color:#FFF; line-height:38px; font-weight:bold;' mc:edit='wm-131' valign='top' align='left'><multiline>¡Alcanza el cumplimiento total!</multiline></td>" +
                                          "</tr>" +
                                         " <tr>" +
                                         "   <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:20px; color:#FFF; line-height:30px; font-weight:normal;' mc:edit='wm-133' valign='top' align='left'><b style = 'font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#FFF; font-weight:bold;'>CON</b></td>" +
                                         " </tr>" +
                                        "</tbody></table></td>" +
                                      "</tr>" +
                                      "<tr>" +
                                     "   <td style = 'font-size:60px; line-height:60px;' valign='top' height='60' align='left'>&nbsp;</td>" +
                                    "  </tr>" +
                                   " </tbody></table></td>" +
                                    "<td class='two-left' valign='top' align='center'><img mc:edit='wm-129' editable='true' src='https://vivegrow.com/00_JP_Mailing/Check035/Check035Mock.jpg' alt='' style='display:block;width:100% !important; height:auto !important; ' width='400' height='440'></td>  " +
                               "    </tr> " +
                         "        </tbody></table></td> " +
                         "      </tr> " +
                        "     </tbody></table> </td> " +
                      "     </tr> " +
                       "  </tbody></table> " +
                       "  <table border = '0' width='100%' cellspacing='0' cellpadding='0' align='center'> " +
                       "  <tbody> " +
                        " <tr>" +
                       "  <td align = 'center' valign='top'>" +
                      "   <table border = '0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                       " <tbody>" +
                        "<tr>" +
                        "<td align = 'center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                       " <tr>" +
   "                     <td align = 'center' valign='middle' bgcolor='#ffffff'><!--Section-->" +
   "                     <table border = '0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
   "                     <tbody>" +
   "                     <tr>" +
   "                     <td align = 'center' valign='top'>" +
   "                     <table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
   "                     <tbody>" +
   "                     <tr>" +
   "                     <td align = 'center' valign='top' bgcolor='#ff5757'>" +
   "                     <table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
   "                     <tbody>" +
   "                     <tr>" +
   "                     <td align = 'center' valign='top' height='40'></td>" +
   "                     </tr>" +
   "                     <tr>" +
   "                     <td align = 'center' valign='top'>" +
   "                     <table border = '0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
   "                     <tbody>" +
   "                     <tr>" +
   "                     <td style = 'font-size: 20px; line-height: 20px;' align='center' valign='top' height='20'></td>" +
   "                     </tr>" +
   "                     <tr>" +
   "                     <td style = 'font-family: Poppins, sans-serif, Verdana; font-size: 22px; color: #fff; font-weight: bold; line-height: 32px;' align='left' valign='top'>¿Qué es Check 035?</td>" +
   "                     </tr>" +
   "                     <tr>" +
   "                     <td style = 'font-size: 10px; line-height: 10px;' align='center' valign='top' height='10'></td>" +
   "                     </tr>" +
   "                     <tr>" +
   "                     <td style = 'font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #fff; font-weight: normal; line-height: 24px;' align='left' valign='top'>La plataforma que te permitir&aacute; <strong>optimizar el proceso de implementaci&oacute;n de la NOM 035 </strong ></td> " +
   "                     </tr>" +
   "                     <tr>" +
   "                     <td align = 'center' valign='top'></td>" +
   "                     </tr>" +
   "                     </tbody>" +
   "                     </table>" +
   "                     </td>" +
   "                     </tr>" +
   "                     <tr>" +
   "                     <td class='two-left' style='font-size: 30px; line-height: 30px;' align='center' valign='top' height='30'></td>" +
   "                     </tr>" +
   "                     </tbody>" +
   "                     </table> " +
   "                     <table width = '100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
   "                           <tbody><tr>" +
   "                             <td valign = 'top' align='center'><table class='main' width='800' cellspacing='0' cellpadding='0' border='0' align='center'>" +
   "                               <tbody><tr>" +
   "                                 <td valign = 'top' align='center'><table class='two-left-inner' width='600' cellspacing='0' cellpadding='0' border='0' align='center'>" +
   "                                   <tbody><tr>" +
   "                                     <td class='two-left' width='280' valign='top' align='center'><table width = '280' cellspacing='0' cellpadding='0' border='0' align='left'>" +
   "                                       <tbody><tr>" +
   "                                         <td valign = 'top' align='left'><img mc:edit='wm-105' editable='true' src='https://vivegrow.com/00_JP_Mailing/Check035/Call.jpg' alt='' width='280' height='170'></td>" +
   "                                       </tr>" +
   "                                       <tr>" +
   "                                         <td valign = 'top' height='25' align='left'>&nbsp;</td>" +
   "                                       </tr>" +
   "                                     </tbody></table></td>" +
   "                                     <td class='two-left' width='40' valign='top' align='center'>&nbsp;</td>" +
   "                                     <td class='two-left' width='280' valign='top' align='center'><table width = '280' cellspacing='0' cellpadding='0' border='0' align='left'>" +
   "                                       <tbody><tr>" +
   "                                         <td valign = 'top' align='left'><table width = '100%' cellspacing='0' cellpadding='0' border='0' align='left'>" +
     "                                         <tbody>" +
    "                                          <tr>" +
    "                                            <td style = 'font-size:10px; line-height:10px;' valign='top' height='10' align='center'>&nbsp;</td>" +
    "                                          </tr>" +
    "                                          <tr>" +
    "                                            <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:20px; color:#263940; font-weight:bold; line-height:25px;' mc:edit='wm-107' valign='top' align='left'><multiline>Cuenta con una línea exclusiva</multiline></td>" +
    "                                          </tr>" +
    "                                          <tr>" +
    "                                            <td style = 'font-size:10px; line-height:10px;' valign='top' height='10' align='center'>&nbsp;</td>" +
    "                                          </tr>" +
    "                                          <tr>" +
    "                                            <td class='two-left' style='font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#263940; font-weight:normal; line-height:26px;' mc:edit='wm-108' valign='top' align='left'><multiline>para escucha de denuncias por violencia y un buzón de quejas</ multiline ></td> " +
    "                                          </tr>" +
    "                                        </tbody></table></td>" +
    "                                      </tr>" +
    "                                      <tr>" +
    "                                        <td style = 'font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
    "                                      </tr>" +
    "                                      <tr>" +
    "                                        <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#82b440; font-weight:bold; text-transform:uppercase;' mc:edit='wm-109' valign='top' align='left'><multiline><a href = 'https://check035.com/linea-escucha/' style = 'color:#F8E16C; text-decoration:none;' > Me interesa </ a ></ multiline ></td> " +
        "                                      </tr>" +
    "                                      <tr>" +
    "                                        <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#82b440; font-weight:bold; text-transform:uppercase;' valign= 'top' align= 'left' > &nbsp;</td>" +
    "                                      </tr>" +
    "                                    </tbody></table></td>" +
    "                                  </tr>" +
    "                                  <tr>" +
    "                                   <td colspan = '3' style='font-size:70px; line-height:70px;' class='two-left' valign='top' height='70' align='center'>&nbsp;</td>" +
    "                                    </tr>" +
    "                                  </tbody></table></td>" +
    "                              </tr>" +
    "                            </tbody></table></td>" +
    "                          </tr>" +
    "                        </tbody></table>" +
    "                        <table width = '100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
    "                              <tbody><tr>" +
    "                                <td valign = 'top' align='center'><table class='main' width='800' cellspacing='0' cellpadding='0' border='0' align='center'>" +
    "                                  <tbody><tr>" +
    "                                    <td valign = 'top' align='center'><table class='two-left-inner' width='600' cellspacing='0' cellpadding='0' border='0' align='center'>" +
    "                                      <tbody><tr>" +
    "                                        <td class='two-left' width='280' valign='top' align='center'><table width = '280' cellspacing='0' cellpadding='0' border='0' align='left'> " +
    "                                          <tbody><tr>" +
    "                                            <td valign = 'top' align='left'><table width = '100%' cellspacing='0' cellpadding='0' border='0' align='left'>" +
    "                                            <tbody>" +
    "                                              <tr>" +
     "                                                <td style = 'font-size:10px; line-height:10px;' valign='top' height='10' align='center'>&nbsp;</td>" +
     "                                             </tr>" +
     "                                             <tr>" +
     "               <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:20px; color:#263940; font-weight:bold; line-height:25px;' mc:edit='wm-111' valign='top' align='left'><multiline>Desarrolla y fomenta la cultura de bienestar</multiline></td>" +
     "                                             </tr>" +
     "                                             <tr>" +
     "                                                <td style = 'font-size:10px; line-height:10px;' valign='top' height='10' align='center'>&nbsp;</td>" +
     "                                             </tr>" +
     "                                             <tr>" +
      "<td class='two-left' style='font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#263940; font-weight:normal; line-height:26px;' mc:edit='wm-112' valign='top' align='left'><multiline>en organizaciones de cualquier tamaño a través de cursos 100% online </ multiline ></td>" +
      "                                            </tr>" +
      "                                          </tbody></table></td>" +
      "                                        </tr>" +
      "                                        <tr>" +
      "                                          <td style = 'font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
      "                                        </tr>" +
      "                                        <tr>" +
        "                                       <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#82b440; font-weight:bold; text-transform:uppercase;' mc:edit='wm-113' valign='top' align='left'><multiline><a href = 'https://aprende.check035.com/' style = 'color:#F8E16C; text-decoration:none;'> SABER MÁS </ a ></ multiline ></td>" +
           "                                       </tr>" +
       "                                       <tr>" +
       "                                         <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:14px; color:#960298; font-weight:bold; text-transform:uppercase;' valign= 'top' align= 'left' > &nbsp;</td>" +
       "                                       </tr>" +
       "                                     </tbody></table></td>" +
       "                                     <td class='two-left' width='40' valign='top' align='center'>&nbsp;</td>" +
       "                                     <td class='two-left' width='280' valign='top' align='center'><table width = '280' cellspacing='0' cellpadding='0' border='0' align='left'>" +
       "                                       <tbody><tr>" +
           "<td valign = 'top' align='left'><img mc:edit='wm-114' editable='true' src='https://vivegrow.com/00_JP_Mailing/Check035/CultureClub.jpg' alt='' width='280' height='170'></td>" +
        "                                      </tr>" +
        "                                      <tr>" +
        "                                        <td valign = 'top' height='25' align='left'>&nbsp;</td>" +
        "                                      </tr>" +
        "                                      </tbody></table></td>" +
        "                                  </tr>" +
        "                                  <tr>" +
        "                                    <td colspan = '3' style='font-size:120px; line-height:120px;' class='two-left' valign='top' height='120' align='center'>&nbsp;</td>" +
       "                                     </tr>" +
       "                                   </tbody></table></td>" +
       "                               </tr>" +
       "                             </tbody></table></td>" +
       "                           </tr>" +
       "                         </tbody></table>" +
       "                 </td>" +
       "                 </tr>" +
       "                 </tbody>" +
       "                 </table>" +
       "                 </td>" +
       "                 </tr>" +
       "                 </tbody>" +
       "                 </table>" +
       "                 <table width = '100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
       "                       <tbody><tr>" +
       "                         <td valign = 'top' align='center'><table class='main' width='800' cellspacing='0' cellpadding='0' border='0' align='center'>" +
       "                           <tbody><tr>" +
       "                             <td valign = 'top' bgcolor='#FFFFFF' align='center'><table class='two-left-inner' width='600' cellspacing='0' cellpadding='0' border='0' align='center'>" +
       "                               <tbody><tr>" +
       "                                  <td style = ' font-size:90px;line-height:90px;' valign='top' height='90' align='center'>&nbsp;</td>" +
       "                               </tr>" +
       "                               <tr>" +
       "                                 <td valign = 'top' align='center'><table width = '100%' cellspacing='0' cellpadding='0' border='0' align='left'>" +
       "                                   <tbody><tr>" +
       "                                     <td class='two-left' width='245' valign='top' align='left'><table width = '100%' cellspacing='0' cellpadding='0' border='0' align='left'>" +
       "                                       <tbody><tr>" +
       "                                         <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:18px; color:#4C5B61; font-weight:normal;' mc:edit='wm-151' valign='top' align='left'><multiline>¿Cuáles son los</multiline></td>" +
       "                                       </tr>" +
       "                                       <tr>" +
       "                                         <td style = 'font-family:Poppins, sans-serif, Verdana; font-size:35px; color:#4C5B61; font-weight:bold; line-height:50px;' mc:edit='wm-152' valign='top' align='left'><multiline>beneficios?</multiline></td>" +
       "                                       </tr>" +
       "                                     </tbody></table></td>" +
       "                                     <td class='menu-icon' style='border-left:#d4d1d4 solid 1px;' width='40' valign='top' align='left'>&nbsp;</td>" +
       "                                     <td class='two-left' style='font-family:Poppins, sans-serif, Verdana; font-size:16px; color:#4C5B61; font-weight:normal; line-height:30px;' mc:edit='wm-153' width='315' valign='top' align='left'><multiline>Además de no ser sujeto a multas, el cumplimiento de la NOM035 favorece:</ multiline ></td>" +
       "                                   </tr>" +
       "                                 </tbody></table></td>" +
       "                               </tr>" +
       "                               <tr>" +
       "                                 <td style = ' font-size:55px;line-height:55px;' valign='top' height='65' align='left'>&nbsp;</td>" +
       "                               </tr>" +
       "                               </tbody></table></td>" +
       "                           </tr>" +
       "                         </tbody></table></td>" +
       "                       </tr>" +
       "                     </tbody></table>" +
       "<table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
"      <tbody><tr>" +
"        <td valign='top' align='center'><table class='main' width='800' cellspacing='0' cellpadding='0' border='0' align='center'>" +
"          <tbody><tr>" +
"            <td valign='top' bgcolor='#FFFFFF' align='center'><table class='two-left-inner' width='600' cellspacing='0' cellpadding='0' border='0' align='center'>" +
"              <tbody><tr>" +
"                <td valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0' border='0'>" +
"              <tbody><tr>" +
"                <td class='two-left' width='180' valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
"                  <tbody><tr>" +
"                    <td valign='top' align='left'><img mc:edit='wm-16' editable='true' src='https://vivegrow.com/00_JP_Mailing/Check035/Productividad.png' alt='' style='display:block;' width='64' height='64'></td>" +
"                  </tr>" +
"                  <tr>" +
"                    <td style='font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
"                  </tr>" +
"                  <tr>" +
"                   <td style='font-family:Poppins, sans-serif, Verdana; font-size:16px; color:#ff5757; line-height:22px; text-transform:uppercase; font-weight:bold;' mc:edit='wm-17' valign='top' align='left'><multiline>Mayor productividad</multiline></td>" +
"                  </tr>" +
"                  <tr>" +
"                    <td style='font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
"                  </tr>" +
"                  <tr>" +
"                   <td colspan='2' style='font-family:Poppins, sans-serif, Verdana; font-size:13px; color:#818685; line-height:24px;' mc:edit='wm-18' valign='top' align='left'><multiline>e incremento en las utilidades</multiline></td>" +
"                  </tr>" +
"                  <tr>" +
"                    <td style='font-size:30px; line-height:30px;' valign='top' height='30' align='center'>&nbsp;</td>" +
"                  </tr>" +
"                </tbody></table></td>" +
"                <td class='two-left' width='20' valign='top' align='center'>&nbsp;</td>" +
"                <td class='two-left' width='180' valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
"                  <tbody><tr>" +
"                    <td valign='top' align='left'><img mc:edit='wm-19' editable='true' src='https://vivegrow.com/00_JP_Mailing/Check035/Entorno.png' alt='' style='display:block;' width='64' height='64'></td>" +
"                  </tr>" +
"                  <tr>" +
"                    <td style='font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
"                  </tr>" +
"                  <tr>" +
"                   <td style='font-family:Poppins, sans-serif, Verdana; font-size:16px; color:#ff5757; line-height:22px; text-transform:uppercase; font-weight:bold;' mc:edit='wm-20' valign='top' align='left'><multiline>Mejora</multiline></td>" +
"                  </tr>" +
"                  <tr>" +
"                    <td style='font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
"                  </tr>" +
"                  <tr>" +
"                   <td colspan='2' style='font-family:Poppins, sans-serif, Verdana; font-size:13px; color:#818685; line-height:24px;' mc:edit='wm-21' valign='top' align='left'><multiline>el entorno organizacional</multiline></td>" +
"                  </tr>" +
"                  <tr>" +
"                    <td style='font-size:30px; line-height:30px;' valign='top' height='30' align='center'>&nbsp;</td>" +
"                  </tr>" +
"                </tbody></table></td>" +
"                <td class='two-left' width='20' valign='top' align='center'>&nbsp;</td>" +
"                <td class='two-left' width='180' valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'>" +
"                  <tbody><tr>" +
"                    <td valign='top' align='left'><img mc:edit='wm-22' editable='true' src='https://vivegrow.com/00_JP_Mailing/Check035/Prevencion.png' alt='' style='display:block;' width='64' height='64'></td>" +
"                  </tr>" +
"                  <tr>" +
"                    <td style='font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
"                  </tr>" +
"                  <tr>" +
"                   <td style='font-family:Poppins, sans-serif, Verdana; font-size:16px; color:#ff5757; line-height:22px; text-transform:uppercase; font-weight:bold;' mc:edit='wm-23' valign='top' align='left'><multiline>Prevención</multiline></td>" +
"                  </tr>" +
"                  <tr>" +
"                    <td style='font-size:10px; line-height:10px;' valign='top' height='10' align='left'>&nbsp;</td>" +
"                  </tr>" +
"                  <tr>" +
"              <td colspan='2' style='font-family:Poppins, sans-serif, Verdana; font-size:13px; color:#818685; line-height:24px;' mc:edit='wm-24' valign='top' align='left'><multiline>de las condiciones que pudieran afectar la salud mental de los colaboradores</multiline></td>" +
"                  </tr>" +
"                  <tr>" +
"                    <td style='font-size:30px; line-height:30px;' valign='top' height='30' align='center'>&nbsp;</td>" +
"                  </tr> " +
"                </tbody></table></td>" +
"              </tr> " +
"            </tbody></table></td>" +
"              </tr>" +
"              <tr>" +
"                                   <td valign='top' align='center'><table width='300' cellspacing='0' cellpadding='0' border='0' align='center'>" +
"                                         <tbody><tr>" +
 " <td style='background:#ff5757; -moz-border-radius: 25px; border-radius: 25px; font-family:Poppins, Verdana, Arial; font-size:14px; font-weight:bold; text-transform:uppercase; color:#FFF; ' mc:edit='wm-77' valign='middle' height='45' align='center'>" +
  "<multiline><a href='https://store.vivegrow.com.mx' style='text-decoration:none; color:#FFF;'>Visitar sitio web</a></multiline></td>" +
"                                         </tr>" +
"                                       </tbody></table></td>" +
"                                   </tr>" +
"              <tr>" +
"                <td style=' font-size:75px;line-height:75px;' valign='top' height='75' align='left'>&nbsp;</td>" +
"              </tr>" +
"              </tbody></table></td>" +
"          </tr>" +
"        </tbody></table></td>" +
"      </tr>" +
"</tbody></table>" +
"<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
"<tbody>" +
"<tr>" +
"<td align='center' valign='top' bgcolor='#5c17e6'>" +
"<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
"<tbody>" +
"<tr>" +
"<td style='font-size: 100px; line-height: 100px;' align='center' valign='top' height='100'></td>" +
"</tr>" +
"<tr>" +
"<td align='center' valign='top'>" +
      "<table class='two-left-inner' border='0' width='520' cellspacing='0' cellpadding='0' align='center'>" +
      "<tbody>" +
      "<tr>" +
      "<td style = 'font-size: 15px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
      "</tr>" +
      "<tr>" +
      "<td style = 'font-family: Poppins, sans-serif, Verdana; font-size: 20px; color: #fff; font-weight: bold; line-height: 30px;' align='center' valign='top'>¿Buscas tips o sugerencias?<br>¡Escucha nuestro podcast!</td>" +
      "</tr>" +
      "<tr>" +
      "<td style = 'font-size: 25px; line-height: 25px;' align='left' valign='top' height='25'></td>" +
      "</tr> " +
      "<tr>" +
      "<td style = 'line-height: 20px;' align='left' valign='top' height='20'><a href='https://check035.com/podcast/'><img style='display: block;' src='Podcast.jpg' alt=''  /></a></td>" +
      "</tr> " +
      "<tr>" +
      "<td style = 'font-family: Poppins, sans-serif, Verdana; font-size: 20px; color: #fff; font-weight: bold; line-height: 30px;' align='center' valign='top'>Episodio 1: Lo mejor de la NOM 035</td>" +
      "</tr>" +
      "<tr>" +
      "<td style = 'font-size: 25px; line-height: 25px;' align='left' valign='top' height='25'></td>" +
      "</tr>" +
      "<tr>" +
      "<td align = 'center' valign='top'>" +
      "<table border = '0' width='320' cellspacing='0' cellpadding='0' align='center'>" +
      "<tbody>" +
      "<tr>      <td style = 'background: #ba9ef3; -moz-border-radius: 25px; border-radius: 25px; font-family: Poppins, Verdana, Arial; font-size: 14px; font-weight: bold; text-transform: uppercase; color: #fff;' align='center' valign='middle' height='45'><a style = 'text-decoration: none; color: #fff;' href='https://check035.com/podcast/'>Escuchar podcast</a></td>" +
      "</tr>" +
      "</tbody>" +
      "</table>" +
"</td>" +
"</tr>" +
"<tr>" +
"<td style='font-size: 80px; line-height: 120px;' align='center' valign='top' height='120'></td>" +
"</tr>" +
"</tbody>" +
"</table>" +
"</td>" +
"</tr>" +
"</tbody>" +
"</table>" +
"</td>" +
"</tr>" +
"</tbody>" +
"</table>" +
"<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
"<tbody>" +
"<tr>" +
"<td align='center' valign='top'>" +
"<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
"</table>" +
"<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
"<tbody>" +
"<tr>" +
"<td align='center' valign='top'>" +
"<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
"<tbody>" +
"<tr>" +
"<td style='background: #fff;' align='left' valign='top' bgcolor='#292828'>" +
"<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
"<tbody>" +
"<tr>" +
"</tr>" +
"<tr>" +
"<td align='left' valign='top'>" +
"<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='right'>" +
"<tbody>" +
"<tr>" +
"<td align='center' valign='top'>" +
"<a href='hhttps://check035.com/wp-content/uploads/2021/11/logo-web.png'>" +
"<img class='alignnone' style='display: block;' src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='Logo Grow Incubadora de Mentes Creativas' width='130' height='60' />" +
"</a></td>" +
"</tr>" +
"</tbody>" +
"</table>" +
"<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='left'>" +
"<tbody>" +
"<tr>" +
"<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000; font-weight: normal; padding-top: 20px;' align='center' valign='top'>Check 035</td>" +
"</tr>" +
"</tbody>" +
"</table>" +
"</td>" +
"</tr>" +
"<tr>" +
"</tr>" +
"<tr>" +
"</tr>" +
"</tbody>" +
"</table>" +
"</td>" +
"</tr>" +
"</tbody>" +
"</table>" +
"</td>" +
"</tr>" +
"</tbody>" +
"</table>" +
"</td>" +
"</tr>" +
"</tbody>" +
"</table>" +
"</td>" +
"</tr>" +
"</tbody>" +
"</table>" +
"</td>" +
"</tr>" +
"</tbody>" +
"</table>" +
"&nbsp;" +
"&nbsp;</td>" +
"</tr>" +
"</tbody>" +
"</table>" +
"</html>";


                    mail.Body = cuerpo_correo.ToMessageBody();
                    SmtpClient SmtpServer = new SmtpClient();
                    SmtpServer.CheckCertificateRevocation = true;
                    SmtpServer.Connect("smtpout.secureserver.net", 465, true);
                    SmtpServer.Authenticate("check035@check035.com", "ZaZc3JnfTSH%*4c");
                    SmtpServer.Send(mail);
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails @user ='WEB_CHECKLIST',@mail_send_to = '" + Email + "',@Status = 1");
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails_fail @user ='WEB_CHECKLIST',@mail_send_to = '" + Email + "',@Status = 0, @comment = '" + ex.Message.ToString() + "'");
                    db.SaveChanges();
                }

                //##########################    ENVIADO CORREO A ADMINISTRADORES  ##########################

                try
                {
                    MimeMessage mail = new MimeMessage();
                    mail.From.Add(new MailboxAddress("Identifica NOM-035", "check035@check035.com"));
                    mail.Bcc.Add(new MailboxAddress("Identifica NOM-035", "rflores@vivegrow.com.mx"));
                    mail.Bcc.Add(new MailboxAddress("Identifica NOM-035", "jorge.ponce@vivegrow.com.mx"));
                    mail.Bcc.Add(new MailboxAddress("Identifica NOM-035", "soporte@vivegrow.com.mx"));
                    mail.To.Add(new MailboxAddress("Identifica NOM-035", "contacto@check035.com"));
                    mail.Subject = "Check List 035";
                    BodyBuilder cuerpo_correo = new BodyBuilder();

                    cuerpo_correo.HtmlBody = "<head>" +
                    "<meta http-equiv='X-UA-Compatible' content='IE=edge'>" +
                    "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" +
                    "<meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>" +
                    "<title>Check 035</title>" +
                    "</head>" +
                    "&nbsp;" +
                    "<table style='background: #FFFFFF;' border='0' width='100%' cellspacing='0' cellpadding='0' align='center' bgcolor='#3f3f3f'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td align='center' valign='top'>" +
                    "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td align='center' valign='top'>" +
                    "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td style='border-bottom: #f8f8f8 solid 1px;' align='center' valign='top' bgcolor='#FFFFFF'>" +
                    "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td align='center' valign='top'>" +
                    "<table border='0' width='100%' cellspacing='0' cellpadding='0'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td class='two-left' align='center' valign='top' width='170'>" +
                    "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td style='font-size: 2px; line-height: 2px;' height='2'></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 16px; color: #34435a; font-weight: normal;' align='center' valign='top'></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "</td>" +
                    "<td class='two-left' align='center' valign='top' width='260'>" +
                    "<table border='0' width='125' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td align='center' valign='middle'><a href='https://check035.com/'><img src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='' width='130' height='60' /></a></td>" +
                    "</tr></tbody></table></td>" +
                    "<td class='two-left' align='center' valign='top' width='170'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td align='center' valign='top'>" +
                    "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td align='center' valign='top' bgcolor='#FFFFFF'>" +
                    "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td style='font-size: 45px; line-height: 45px;' align='center' valign='top' height='45'></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td align='center' valign='top'>" +
                    "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>" +
                    "Se ha generado un nuevo registro de checklist en la plataforma identifica.</td>" +
                    "</tr>" +
                      "<tr>" +
                        "<td style='font-family:Poppins, sans-serif, Verdana; font-size:22px; color:#ff5757; font-weight:bold; line-height:32px; padding-top:25px;' mc:edit='wm-75' valign='top' align='center'><multiline>" + calif + " de 10 puntos máximos posibles</multiline></td>" +
                        "</tr>" +
                    "<tr>" +
                    "<td style='font-family:Poppins, sans-serif, Verdana; font-size:22px; color:#ff5757; font-weight:bold; line-height:32px; padding-top:25px; padding-bottom:25px;' mc:edit='wm-75' valign='top' align='center'><multiline>" +
                    "Los puntos no marcados del checklist fueron los siguientes resaltados en color rojo:</multiline></td>" +
                    "</tr> " +
                    "<tr><td valign='top' align='center'>" +
                    "                                                            <table width='300' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                    "                                                                <tbody>" +
                       "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 14px; color: #34435a; font-weight: normal; line-height:25px;' align='left' valign='top'>" +
                        "  <ol>" +
                                            R1 + R2+ R3+ R4+ R5+ R6+ R7+ R8+ R9+ R10 +
                        "  </ol>" + 
                        "  </td>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 14px; color: #34435a; font-weight: normal; line-height:25px;' align='left' valign='top'>" +
                        "  <ol>" +
                       
                        "  </ol>" +
                        "</td>" +
                        "</tr>" +

                    "                                                                </tbody>" +
                    "                                                            </table>" +
                    "                                                        </td>" +
                    "                                                        </tr> " +
                    "                                                        <tr>" +
                    "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
                    "                                                        </tr>" + 
                    "                                                        <tr>" +
                    "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
                    "                                                        </tr>" +
                    "<tr>" +
                    "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'><strong>Razón social:</strong> " + Empresa + "<br><strong>Teléfono: </strong>" + Telefono + "<br><strong>Correo: </strong>" + Email + " </td></tr><tr>" +
                    "<td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td></tr><tr>" +
                    "<td style='font-size: 55px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
                    "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
                    "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody><tr><td align='center' valign='top'><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                    "<tr><td align='center' valign='top'><table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody><tr><td align='center' valign='middle' bgcolor='#ffffff'><!--Section-->" +
                    "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody><tr><td align='center' valign='top'>" +
                    "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody><tr><td align='center' valign='top' bgcolor='#ff5757'>" +
                    "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody><tr><td align='center' valign='top' height='40'></td>" +
                    "</tr><tr><td align='center' valign='top'>" +
                    "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody><tr><td style='font-size: 20px; line-height: 20px;' align='center' valign='top' height='20'></td>" +
                    "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 22px; color: #fff; font-weight: bold; line-height: 32px;' align='left' valign='top'>Recomendamos el uso de los siguientes navegadores:</td>" +
                    "</tr><tr><td style='font-size: 10px; line-height: 10px;' align='center' valign='top' height='10'></td>" +
                    "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #fff; font-weight: normal; line-height: 24px;' align='left' valign='top'>" +
                    "  <ol>" +
                    "  <li>Chrome</li>" +
                    "  <li>Firefox</li>" +
                    "  <li>Opera</li>" +
                    "  <li>Edge (puede presentar conflictos con funciones de la aplicaci&oacute;n)</li>" +
                    "  </ol>" +
                    "</td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td align='center' valign='top'></td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table></td></tr><tr><td class='two-left' style='font-size: 30px; line-height: 30px;' align='center' valign='top' height='30'></td>" +
                    "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
                    "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td align='center' valign='top'>" +
                    "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td align='center' valign='top'>" +
                    "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td style='background: #fff;' align='left' valign='top' bgcolor='#292828'>" +
                    "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td align='left' valign='top'>" +
                    "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='right'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td align='center' valign='top'><a href='https://check035.com'><img class='alignnone' style='display: block;' src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='Logo Grow Incubadora de Mentes Creativas' width='130' height='60' /></a></td>" +
                    "</tr>" +
                    "</tbody>" +
                    "</table>" +
                    "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='left'>" +
                    "<tbody>" +
                    "<tr>" +
                    "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000; font-weight: normal; padding-top: 20px;' align='center' valign='top'>Check 035</td>" +
                    "</tr></tbody></table></td></tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                    "</tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                    "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>&nbsp;&nbsp;</td></tr></tbody></table>";


                    mail.Body = cuerpo_correo.ToMessageBody();
                    SmtpClient SmtpServer = new SmtpClient();
                    SmtpServer.CheckCertificateRevocation = true;
                    SmtpServer.Connect("smtpout.secureserver.net", 465, true);
                    SmtpServer.Authenticate("check035@check035.com", "ZaZc3JnfTSH%*4c");
                    SmtpServer.Send(mail);
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails @user ='WEB_CHECKLIST',@mail_send_to = '" + Email + "',@Status = 1");
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails_fail @user ='WEB_CHECKLIST',@mail_send_to = '" + Email + "',@Status = 0, @comment = '" + ex.Message.ToString() + "'");
                    db.SaveChanges();
                }

            }

            return Json(calif, JsonRequestBehavior.AllowGet);
        }
    }
}