using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagLib;
using TagLibFile = TagLib.File;

namespace Storage_Service.Controllers
{
    [ApiController]
    [Route("api/storage/")]
    public class StorageController : ControllerBase
    {
        // GET: api/Storage
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Storage/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Storage
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Storage/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            await using (Stream stream = file.OpenReadStream())
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var fileContent = binaryReader.ReadBytes((int)file.Length);
                    string str = Encoding.Default.GetString(fileContent);
                    //await this.UploadFile(file.ContentDisposition);
                    var filePath = "C:\\users\\Pajama Sammy\\desktop\\testmp3.mp3";

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    TagLibFile.IFileAbstraction f = new TagLibFile.LocalFileAbstraction(filePath);

                    var mp3 = TagLib.File.Create(f);

                    var artist = mp3.Tag.Performers;
                    var album = mp3.Tag.Album;
                    var albumNumber = mp3.Tag.Track;

                    var extension = Path.GetExtension(file.FileName);
                    var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MusifyStorage", artist[0], album);
                    var fileName = albumNumber + " - " + mp3.Tag.Title;
                    var fullPath = Path.Combine(folderPath, fileName+  extension);
        
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                   
                    await using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }


            return new OkResult();
        }
    }
}
