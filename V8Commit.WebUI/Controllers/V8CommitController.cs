using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using _1CV8Adapters;
using Plugin.V8Commit20;
using V8Commit.Services.ConversionServices;
using V8Commit.Services.HashServices;
using V8Commit.WebUI.Models;

namespace V8Commit.WebUI.Controllers
{
    public class V8CommitController : Controller
    {
        private IHashService _hashService;
        private IConversionService<UInt64, DateTime> _convertionService;

        public V8CommitController(IHashService hashService, IConversionService<UInt64, DateTime> covertionService)
        {
            this._convertionService = covertionService;
            this._hashService = hashService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload()
        {
            List<TreeModel> model = null;
            Guid guid = Guid.NewGuid();
            string directory = AppDomain.CurrentDomain.BaseDirectory + "temp\\" + guid + "\\";

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                //Use the following properties to get file's name, size and MIMEType
                //int fileSize = file.ContentLength;
                //string fileName = file.FileName;
                //string mimeType = file.ContentType;
                //Stream fileContent = file.InputStream;

                

                using (BinaryReader reader = new BinaryReader(file.InputStream))
                {
                    using (FileV8Reader v8Reader = new FileV8Reader(reader))
                    {
                        var fileSystem = v8Reader.ReadV8FileSystem();
                        new V8Commit20(_hashService, _convertionService).Parse(v8Reader, fileSystem, directory);
                    }
                }
                
                //To save file, use SaveAs method
                //file.SaveAs(Server.MapPath("~/") + fileName); //File will be saved in application root
            }

            if (Directory.Exists(directory))
            {
                model = new List<TreeModel>();
                string[] dirs = Directory.GetDirectories(directory);
                FillTreeModel(model, dirs);
                //foreach (string dir in dirs)
                //{
                //    model.Add(new TreeModel() { File = Path.GetFileName(dir)});

                //}
            }

            return PartialView("TreeViewPartial", model);
        }

        private void FillTreeModel(List<TreeModel> model, string[] dirs, bool files = false)
        {
            foreach (string dir in dirs)
            {
                TreeModel treeModel = new TreeModel() { File = Path.GetFileName(dir) };
                model.Add(treeModel);

                if (Directory.Exists(dir))
                {
                    string[] nestedFolders = Directory.GetDirectories(dir);
                    if (nestedFolders.Length > 0)
                    {
                        treeModel.Children = new List<TreeModel>();
                        FillTreeModel(treeModel.Children, nestedFolders);
                    }
                }

                if (!files)
                {
                    string[] nestedFiles = Directory.GetFiles(dir);
                    if (nestedFiles.Length > 0)
                    {
                        treeModel.Children = new List<TreeModel>();
                        FillTreeModel(treeModel.Children, nestedFiles, true);
                    }
                }
            }
        }
    }
}