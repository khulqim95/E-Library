using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PortalPMO.ViewModels
{
    public class DataProjectFile_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? TypeDokumenId { get; set; }
        public string TypeDokumen { get; set; }
        public string NamaFile { get; set; }
        public string DownloadPath { get; set; }

        public string FileExt { get; set; }
        public string FileType { get; set; }
        public IFormFile File { get; set; }

        public decimal? Size { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
        public string Keterangan { get; set; }
        public string UploadTime { get; set; }
        public string UploadBy { get; set; }
        public int? UploadById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
